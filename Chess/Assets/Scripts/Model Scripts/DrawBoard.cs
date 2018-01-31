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

    private static Vector2 squareClicked = new Vector2(-1, -1);
    private static int squareIndex = -1;
    private static bool clicked = false;

    public void HighLightGrid(Vector2 pos)
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

        for (int i = index * 6; i < index * 6 + 6; ++i)
        {
            triCopy.Add(tri[i]);
        }

        mesh.SetTriangles(triCopy, submesh);
    }

    public void HighLightGrid(Vector2[] positions)
    {
        foreach (Vector2 pos in positions)
        {
            HighLightGrid(pos);
        }
    }

    public void ClearHighlights()
    {
        triCopy1.Clear();
        triCopy2.Clear();

        mesh.SetTriangles(triCopy1, 0);
        mesh.SetTriangles(triCopy1, 1);
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
        tri1 = new int[numberOfSquares * 6];
        tri2 = new int[numberOfSquares * 6];
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
        tint1.color = new Color(meshRenderer.materials[2].color.r, meshRenderer.materials[2].color.g + tintColor.g, meshRenderer.materials[2].color.b);
        tint2.color = new Color(meshRenderer.materials[3].color.r, meshRenderer.materials[3].color.g + tintColor.g, meshRenderer.materials[3].color.b);

        meshRenderer.materials[0] = tint1;
        meshRenderer.materials[1] = tint2;
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
                    if (triangleInd > 125)
                        squareIndex = (triangleInd - 128) / 2;
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
