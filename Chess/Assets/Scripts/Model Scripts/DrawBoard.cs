// Programmer: Ari Berkson
//
// Draws the board by placing vertices and drawing triangles.
// Uses triangle to detect where on the board the user has clicked.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class DrawBoard : MonoBehaviour
{
    public Material tint1;
    public Material tint2;

    [SerializeField] [Range(1, 16)] static int size = 8;
    private int rows;
    private int columns;

    [SerializeField] Color tintColor;

    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private MeshFilter meshFilter;
    private Mesh mesh;

    private int[] tri1;
    private int[] tri2;
    private List<int> triCopy1 = new List<int>();
    private List<int> triCopy2 = new List<int>();

    private static int squareIndex = -1;
    private static bool clicked = false;

    private const int NUMBER_OF_TRIS = 6;
    private const int TRIANGLE_INDEX = 125;
    private const int TRIANGLE_DIFFERENCE = 128;
    private const int LAYER_ZERO = 0;
    private const int LAYER_ONE = 1;

    public void HighLightGrid(Vector3 pos)
    {
        int[] tri;
        List<int> triCopy;
        int index = (int)pos.x + (int)pos.y * size;
        int submesh;

        if ((pos.x + pos.y) % 2 == 0)
        {
            tri = tri2;
            triCopy = triCopy1;
            submesh = 0;
        }
        else
        {
            tri = tri1;
            triCopy = triCopy2;
            submesh = 1;
        }

        for (int i = index * NUMBER_OF_TRIS; i < index * NUMBER_OF_TRIS + NUMBER_OF_TRIS; ++i)
        {
            triCopy.Add(tri[i]);
        }

        mesh.SetTriangles(triCopy, submesh);
    }

    public void HighLightGrid(List<Vector3> positions)
    {
        foreach (Vector3 pos in positions)
        {
            HighLightGrid(pos);
        }
    }

    public void ClearHighlights()
    {
        triCopy1.Clear();
        triCopy2.Clear();

        mesh.SetTriangles(triCopy1, LAYER_ZERO);
        mesh.SetTriangles(triCopy1, LAYER_ONE);
    }

    public static Vector2 SquarePosition
    {
        get
        {
            int x = squareIndex % size;
            int y = squareIndex / size;
            return new Vector2(x, y);
        }
    }

    public static bool IsClicked
    {

        get
        {
            DetectClick();
            return clicked;
        }
    }

    // Use this for initialization
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();

        rows = size;
        columns = size;

        InitBoard();
    }

    private void InitBoard()
    {
        DrawMesh();
        TintMaterials();
        meshFilter.mesh = mesh;
    }

    private void DrawMesh()
    {
        mesh = new Mesh();
        int count = 0;
        int triCount = 0;
        int numberOfVertices = (2 * rows + 1) * (2 * columns + 1);
        int numberOfSquares = rows * columns;

        mesh.subMeshCount = 4;

        Vector3[] vertices = new Vector3[numberOfVertices];
        Vector2[] uv = new Vector2[numberOfVertices];
        Vector3[] normals = new Vector3[numberOfVertices];
        tri1 = new int[numberOfSquares * NUMBER_OF_TRIS];
        tri2 = new int[numberOfSquares * NUMBER_OF_TRIS];
        int[] tri = tri1;
        for (int i = 0; i <= rows; ++i)
        {
            for (int j = 0; j <= columns; ++j)
            {
                vertices[count] = new Vector3(j, 0, i);
                normals[count] = -Vector3.forward;
                uv[count] = new Vector2((j + rows) / rows, (i + columns) / columns);
                if (i > 0 && j > 0)
                {
                    //bottom right tri
                    tri[triCount++] = count;
                    tri[triCount++] = count - rows - 1;
                    tri[triCount++] = count - rows - 2;

                    //upper left tri
                    tri[triCount++] = count;
                    tri[triCount++] = count - rows - 2;
                    tri[triCount++] = count - 1;
                }
                count++;
                tri = (j + i) % 2 == 0 ? tri1 : tri2;
            }
        }
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.SetTriangles(tri1, 2);
        mesh.SetTriangles(tri2, 3);
        mesh.uv = uv;
        mesh.uv2 = uv;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    private void TintMaterials()
    {
        //applies ting to the tint material.
        tint1.color = new Color(0, meshRenderer.materials[2].color.g + tintColor.g, 0);
        tint2.color = new Color(0, meshRenderer.materials[3].color.g + tintColor.g, 0);

        meshRenderer.materials[LAYER_ZERO] = tint1;
        meshRenderer.materials[LAYER_ONE] = tint2;
    }


    /// <summary>
    /// Detects when a space on the board has been clicked on.
    /// This method should be called in an update function.
    /// If a click has been detected use SquareClicked to get the vector2.
    /// </summary>
    private static void DetectClick()
    {
        //Checks for left mouse button down.
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;

            //create a ray from the camera through the mouse cursor.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "ChessBoard")
                {
                    int triangleInd = hit.triangleIndex;
                    if (triangleInd > TRIANGLE_INDEX)
                        squareIndex = (triangleInd - TRIANGLE_DIFFERENCE) / 2;
                    else
                        squareIndex = triangleInd / 2;

                    clicked =  true;
                    return;
                }
            }
        }
        clicked =  false;
    }
}
