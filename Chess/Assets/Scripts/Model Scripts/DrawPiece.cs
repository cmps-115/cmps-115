// Programmer: Ari Berkson
//
// This module places the chess peices on the board.
// This module also detects when the user clicks on a chess peice model.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPiece : MonoBehaviour {

    public GameObject king;
    public GameObject queen;
    public GameObject bishop;
    public GameObject knight;
    public GameObject castle;
    public GameObject pawn;

    public Material firstTeam;
    public Material secondTeam;

    private static Vector2 piecePosition = new Vector2(-1, -1);
    private static Renderer pieceRenderer;
    private static bool clicked = false;

    private const float MODEL_OFFSET = 0.5f;
    private const float MODEL_OFFSET_Y = 0.05f;
    private const int BOARD_MINIMUM = 0;
    private const int BOARD_MAXIMUM = 7;
    private const int TEAM_ROWS = 2;

    private const int RENDERQUEUE = 3000;

    public static void HighlightPiece(float OutlineThickness = 1.25f)
    {
        if (pieceRenderer == null)
            throw new System.Exception("Error in DrawPiece: Cannot Highlight a piece because a piece has not been clicked");

        var mat = pieceRenderer.material;
        mat.shader = Shader.Find("StandardOutline");
        mat.SetFloat("_OutlineWidth", OutlineThickness);
        mat.renderQueue = RENDERQUEUE + 1;
    }

    public static void ClearHighlight()
    {
        if (pieceRenderer == null)
            throw new System.Exception("Error in DrawPiece: Cannot ClearHighlight because a piece has not been clicked");

        var mat = pieceRenderer.material;
        mat.shader = Shader.Find("StandardOutline");
        mat.SetFloat("_OutlineWidth", 0);
        mat.renderQueue = RENDERQUEUE;
    }

    /// <summary>
    /// Returns the positions of the last chess piece clicked.
    /// </summary>
    public static Vector2 PiecePosition
    {
        get { return piecePosition;}
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
    private void Start ()
    {
        InitPieces();
    }

    private void InitPieces()
    {
        PlaceFirstTeam();
        PlaceSecondTeam();
    }

    #region Place Teams
    private GameObject Spawn(int x, int y, GameObject model)
    {
        return Instantiate(model, new Vector3(x + MODEL_OFFSET, MODEL_OFFSET_Y, y + MODEL_OFFSET), model.transform.rotation);
    }

    private void PlaceFirstTeam()
    {
        for (int y = BOARD_MINIMUM; y < TEAM_ROWS; ++y)
        {
            GameObject model = null;
            for (int x = BOARD_MINIMUM; x <= BOARD_MAXIMUM; ++x)
            {
                if (y == 1)
                {
                    model = Spawn(x, y, pawn);
                }
                else
                {
                    if (x == 0 || x == 7)
                    {
                        model = Spawn(x, y, castle);
                    }
                    else if (x == 1 || x == 6)
                    {
                        model = Spawn(x, y, knight);
                    }
                    else if (x == 2 || x == 5)
                    {
                        model = Spawn(x, y, bishop);
                    }
                    else if (x == 3)
                    {
                        model = Spawn(x, y, king);
                    }
                    else if (x == 4)
                    {
                        model = Spawn(x, y, queen);
                    }
                }
                model.GetComponent<MeshRenderer>().material = firstTeam;
            }
        }
    }

    private void PlaceSecondTeam()
    {
        for (int y = BOARD_MAXIMUM; y > BOARD_MAXIMUM - TEAM_ROWS; --y)
        {
            GameObject model = null;
            for (int x = BOARD_MINIMUM; x <= 7; ++x)
            {
                if (y == 6)
                {
                    model = Spawn(x, y, pawn);
                }
                else
                {
                    if (x == 0 || x == 7)
                    {
                        model = Spawn(x, y, castle);
                    }
                    else if (x == 1 || x == 6)
                    {
                        model = Spawn(x, y, knight);
                    }
                    else if (x == 2 || x == 5)
                    {
                        model = Spawn(x, y, bishop);
                    }
                    else if (x == 3)
                    {
                        model = Spawn(x, y, king);
                    }
                    else if (x == 4)
                    {
                        model = Spawn(x, y, queen);
                    }
                }
                model.GetComponent<MeshRenderer>().material = secondTeam;
            }
        }
    }
    #endregion


    /// <summary>
    /// Detects when a piece has been clicked on.
    /// Should be called in an update function.
    /// </summary>
    private static void DetectClick()
    {
        //Checks for left mouse button down.
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;

            //create a ray from the camera through the mouse cursor.
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "ChessPiece")
                {
                    pieceRenderer = hit.transform.GetComponent<Renderer>();
                    piecePosition = new Vector2Int((int)hit.transform.position.x, (int)hit.transform.position.z);
                    clicked = true;
                    return;
                }
            }
        }
        clicked = false;
    }
}
