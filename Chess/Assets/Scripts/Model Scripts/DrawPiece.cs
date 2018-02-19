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
    private static Material mat;
    private static bool clicked = false;
    private static bool highlighted = false;

    private const float MODEL_OFFSET = 0.5f;
    private const float MODEL_OFFSET_Y = 0.05f;
    private const int BOARD_MINIMUM = 0;
    private const int BOARD_MAXIMUM = 7;
    private const int TEAM_ROWS = 2;

    private const int RENDERQUEUE = 3000;
    private const float OUTLINE_THICKNESS = 1.15f;
    private const int HIGHLIGHT_FREQUENCY = 5;

    public static void HighlightPiece()
    {
        if (pieceRenderer == null)
            throw new System.Exception("Error in DrawPiece: Cannot Highlight a piece because a piece has not been clicked");
  
        highlighted = true;
        mat.renderQueue = RENDERQUEUE + 1;
    }

    public static void ClearHighlight()
    {
        if (pieceRenderer == null)
            throw new System.Exception("Error in DrawPiece: Cannot ClearHighlight because a piece has not been clicked");

        highlighted = false;
        mat.SetFloat("_OutlineWidth", 1);
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
                    mat = pieceRenderer.material;
                    mat.shader = Shader.Find("StandardOutline");
                    return;
                }
            }
        }
        clicked = false;
    }

    private void Update()
    {
        if (highlighted)
        {
            HighlightBreath();
        }
    }

    private void HighlightBreath()
    {
        var dThickness = (OUTLINE_THICKNESS - 1) / 2;
        var sin = dThickness * Mathf.Sin(HIGHLIGHT_FREQUENCY * Time.time);
        var thickness = (OUTLINE_THICKNESS - dThickness / 2) + sin;
        mat.SetFloat("_OutlineWidth", thickness);
    }
}
