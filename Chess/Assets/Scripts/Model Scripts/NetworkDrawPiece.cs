using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessGlobals;
using UnityEngine.Networking;

//Shorten & Clarify Declarations 
using PieceTypeAndPosition = ChessGlobals.Tuple2<ChessGlobals.PIECE_TYPES, UnityEngine.Vector2>;
using ListOfPieceTypesAndPositions = System.Collections.Generic.List<ChessGlobals.Tuple2<ChessGlobals.PIECE_TYPES, UnityEngine.Vector2>>;

public class NetworkDrawPiece : NetworkBehaviour {

    public GameObject white_king;
    public GameObject white_queen;
    public GameObject white_bishop;
    public GameObject white_knight;
    public GameObject white_castle;
    public GameObject white_pawn;

    public GameObject black_king;
    public GameObject black_queen;
    public GameObject black_bishop;
    public GameObject black_knight;
    public GameObject black_castle;
    public GameObject black_pawn;

    public Material firstTeam;
    public Material secondTeam;

    private static Vector2 piecePosition = new Vector2(-1, -1);
    private static Renderer pieceRenderer;
    private static Material mat;
    private static bool clicked = false;
    private static bool highlighted = false;

    private const float MODEL_OFFSET = 0.5f;
    private const float MODEL_OFFSET_Y = 0.05f;


    private const int RENDERQUEUE = 3000;
    private const float OUTLINE_THICKNESS = 1.20f;
    private const int HIGHLIGHT_FREQUENCY = 5;


    public static void HighlightPiece()
    {
        print(pieceRenderer);
        if (pieceRenderer != null)
        {
            highlighted = true;
            mat.renderQueue = RENDERQUEUE + 1;
        }
    }

    public static void ClearHighlight()
    {
        if (pieceRenderer != null)
        {
            highlighted = false;
            mat.SetFloat("_OutlineWidth", 0);
            mat.renderQueue = RENDERQUEUE;
        }
    }

    /// <summary>
    /// Returns the positions of the last chess piece clicked.
    /// </summary>
    public static Vector2 PiecePosition
    {
        get { return piecePosition; }
    }

    public static bool IsClicked
    {
        get
        {
            DetectClick();
            return clicked;
        }
    }

    public static bool IsInitialized
    {
        get { return IsClicked; }
    }

    public Tuple2<ListOfPieceTypesAndPositions, ListOfPieceTypesAndPositions> NetworkInitPieces()
    {
        var initialWhiteTeamPositions = NetworkPlaceWhiteTeam();
        var initialBlackTeamPositions = NetworkPlaceBlackTeam();
        return new Tuple2<ListOfPieceTypesAndPositions, ListOfPieceTypesAndPositions>(initialBlackTeamPositions, initialWhiteTeamPositions);
    }

    #region Place Teams
    private GameObject Spawn(int x, int y, GameObject model, Material mat)
    {
        GameObject obj = Instantiate(model, new Vector3(x + MODEL_OFFSET, MODEL_OFFSET_Y, y + MODEL_OFFSET), model.transform.rotation);
        obj.GetComponent<Renderer>().material = mat;
        CmdNetworkSpawn(obj);
        return obj;
    }

    [Command]
    private void CmdNetworkSpawn(GameObject piece)
    {
        NetworkServer.Spawn(piece);
    }

    public ListOfPieceTypesAndPositions NetworkPlaceWhiteTeam()
    {
        ListOfPieceTypesAndPositions positions = new ListOfPieceTypesAndPositions();
        for (int y = ChessGlobals.BoardConstants.BOARD_MINIMUM; y < ChessGlobals.BoardConstants.TEAM_ROWS; ++y)
        {
            GameObject model = null;
            for (int x = ChessGlobals.BoardConstants.BOARD_MINIMUM; x <= ChessGlobals.BoardConstants.BOARD_MAXIMUM; ++x)
            {
                if (y == 1)
                {
                    if (isServer)
                        model = Spawn(x, y, white_pawn, firstTeam);
                    PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition(PIECE_TYPES.PAWN, new Vector2(x, y));
                    positions.Add(typeAndPos);
                }
                else
                {
                    if (x == 0 || x == 7)
                    {
                        if (isServer)
                            model = Spawn(x, y, white_castle, firstTeam);
                        PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition(PIECE_TYPES.ROOK, new Vector2(x, y));
                        positions.Add(typeAndPos);
                    }
                    else if (x == 1 || x == 6)
                    {
                        if (isServer)
                            model = Spawn(x, y, white_knight, firstTeam);
                        PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition(PIECE_TYPES.KNIGHT, new Vector2(x, y));
                        positions.Add(typeAndPos);
                    }
                    else if (x == 2 || x == 5)
                    {
                        if (isServer)
                            model = Spawn(x, y, white_bishop, firstTeam);
                        PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition(PIECE_TYPES.BISHOP, new Vector2(x, y));
                        positions.Add(typeAndPos);
                    }
                    else if (x == 3)
                    {
                        if (isServer)
                            model = Spawn(x, y, white_king, firstTeam);
                        PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition(PIECE_TYPES.KING, new Vector2(x, y));
                        positions.Add(typeAndPos);
                    }
                    else if (x == 4)
                    {
                        if (isServer)
                            model = Spawn(x, y, white_queen, firstTeam);
                        PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition(PIECE_TYPES.QUEEN, new Vector2(x, y));
                        positions.Add(typeAndPos);
                    }
                }
            }
        }
        return positions;
    }

    public ListOfPieceTypesAndPositions NetworkPlaceBlackTeam()
    {
        List<Tuple2<PIECE_TYPES, Vector2>> positions = new List<Tuple2<PIECE_TYPES, Vector2>>();
        for (int y = ChessGlobals.BoardConstants.BOARD_MAXIMUM; y > ChessGlobals.BoardConstants.BOARD_MAXIMUM - ChessGlobals.BoardConstants.TEAM_ROWS; --y)
        {
            GameObject model = null;
            for (int x = ChessGlobals.BoardConstants.BOARD_MINIMUM; x <= ChessGlobals.BoardConstants.BOARD_MAXIMUM; ++x)
            {
                if (y == 6)
                {
                    if (isServer)
                        model = Spawn(x, y, black_pawn, secondTeam);
                    PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition(PIECE_TYPES.PAWN, new Vector2(x, y));
                    positions.Add(typeAndPos);
                }
                else
                {
                    if (x == 0 || x == 7)
                    {
                        if (isServer)
                            model = Spawn(x, y, black_castle, secondTeam);
                        PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition(PIECE_TYPES.ROOK, new Vector2(x, y));
                        positions.Add(typeAndPos);
                    }
                    else if (x == 1 || x == 6)
                    {
                        if (isServer)
                            model = Spawn(x, y, black_knight, secondTeam);
                        PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition(PIECE_TYPES.KNIGHT, new Vector2(x, y));
                        positions.Add(typeAndPos);
                    }
                    else if (x == 2 || x == 5)
                    {
                        if (isServer)
                            model = Spawn(x, y, black_bishop, secondTeam);
                        PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition(PIECE_TYPES.BISHOP, new Vector2(x, y));
                        positions.Add(typeAndPos);
                    }
                    else if (x == 3)
                    {
                        if (isServer)
                            model = Spawn(x, y, black_king, secondTeam);
                        PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition(PIECE_TYPES.KING, new Vector2(x, y));
                        positions.Add(typeAndPos);
                    }
                    else if (x == 4)
                    {
                        if (isServer)
                            model = Spawn(x, y, black_queen, secondTeam);
                        PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition(PIECE_TYPES.QUEEN, new Vector2(x, y));
                        positions.Add(typeAndPos);
                    }
                }
            }
        }
        return positions;
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
                    if (highlighted)
                    {
                        ClearHighlight();
                    }

                    pieceRenderer = hit.transform.GetComponent<Renderer>();
                    piecePosition = new Vector2(hit.transform.position.x - MODEL_OFFSET, hit.transform.position.z - MODEL_OFFSET);
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
            CmdHighlightBreath();
        }
    }

    private void CmdHighlightBreath()
    {
        var dThickness = (OUTLINE_THICKNESS - 1) / 2;
        var sin = dThickness * Mathf.Sin(HIGHLIGHT_FREQUENCY * Time.time);
        var thickness = (OUTLINE_THICKNESS - dThickness / 2) + sin;
        mat.SetFloat("_OutlineWidth", thickness);
    }
}
