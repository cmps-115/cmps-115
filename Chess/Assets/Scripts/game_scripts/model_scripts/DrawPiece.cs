// Programmer: Ari Berkson
//
// This module places the chess peices on the board.
// This module also detects when the user clicks on a chess peice model.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessGlobals;

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
    private static bool clicked = false;

    private const float MODEL_OFFSET = 0.5f;
    /*private const int BOARD_MINIMUM = 0;
    private const int BOARD_MAXIMUM = 7;
    private const int TEAM_ROWS = 2;*/

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
        //InitPieces();
    }

	public Tuple2<  List< Tuple2<PIECE_TYPES,Vector2> >, List< Tuple2<PIECE_TYPES,Vector2> >  > InitPieces()
    {
		List< Tuple2<PIECE_TYPES,Vector2> > initialWhiteTeamPositions = PlaceWhiteTeam();
		List< Tuple2<PIECE_TYPES,Vector2> > initialBlackTeamPositions = PlaceBlackTeam();
		return new Tuple2<  List< Tuple2<PIECE_TYPES,Vector2> >, List< Tuple2<PIECE_TYPES,Vector2> >  >(initialBlackTeamPositions, initialWhiteTeamPositions);
    }

    #region Place Teams
    private GameObject Spawn(int x, int y, GameObject model)
    {
        return Instantiate(model, new Vector3(x + MODEL_OFFSET, 0, y + MODEL_OFFSET), model.transform.rotation);
    }

	private List< Tuple2<PIECE_TYPES,Vector2> > PlaceWhiteTeam()
    {
		MonoBehaviour.print ("Initial White Piece Position\n");
		List< Tuple2<PIECE_TYPES,Vector2> > positions = new List<Tuple2<PIECE_TYPES,Vector2>>();
		for (int y = ChessGlobals.BoardConstants.BOARD_MINIMUM; y < ChessGlobals.BoardConstants.TEAM_ROWS; ++y)
        {
            GameObject model = null;
			for (int x = ChessGlobals.BoardConstants.BOARD_MINIMUM; x <= ChessGlobals.BoardConstants.BOARD_MAXIMUM; ++x)
            {
                if (y == 1)
                {
                    model = Spawn(x, y, pawn);
					Tuple2<PIECE_TYPES,Vector2> typeAndPos = new Tuple2<PIECE_TYPES,Vector2> (PIECE_TYPES.PAWN, new Vector2 (x, y));
					positions.Add (typeAndPos);
					MonoBehaviour.print("Pawn Coords: " + new Vector2 (x, y) + "\n");
                }
                else
                {
                    if (x == 0 || x == 7)
                    {
                        model = Spawn(x, y, castle);
						Tuple2<PIECE_TYPES,Vector2> typeAndPos = new Tuple2<PIECE_TYPES,Vector2> (PIECE_TYPES.ROOK, new Vector2 (x, y));
						positions.Add (typeAndPos);
						MonoBehaviour.print ("Rook Coords: " + new Vector2 (x, y) + "\n");
                    }
                    else if (x == 1 || x == 6)
                    {
                        model = Spawn(x, y, knight);
						Tuple2<PIECE_TYPES,Vector2> typeAndPos = new Tuple2<PIECE_TYPES,Vector2> (PIECE_TYPES.KNIGHT, new Vector2 (x, y));
						positions.Add (typeAndPos);
						MonoBehaviour.print ("Knight Coords: " + new Vector2 (x, y) + "\n");
                    }
                    else if (x == 2 || x == 5)
                    {
                        model = Spawn(x, y, bishop);
						Tuple2<PIECE_TYPES,Vector2> typeAndPos = new Tuple2<PIECE_TYPES,Vector2> (PIECE_TYPES.BISHOP, new Vector2 (x, y));
						positions.Add (typeAndPos);
						MonoBehaviour.print ("Bishop Coords: " + new Vector2 (x, y) + "\n");
                    }
                    else if (x == 3)
                    {
                        model = Spawn(x, y, king);
						Tuple2<PIECE_TYPES,Vector2> typeAndPos = new Tuple2<PIECE_TYPES,Vector2> (PIECE_TYPES.KING, new Vector2 (x, y));
						positions.Add (typeAndPos);
						MonoBehaviour.print ("King Coords: " + new Vector2 (x, y) + "\n");
                    }
                    else if (x == 4)
                    {
                        model = Spawn(x, y, queen);
						Tuple2<PIECE_TYPES,Vector2> typeAndPos = new Tuple2<PIECE_TYPES,Vector2> (PIECE_TYPES.PAWN, new Vector2 (x, y));
						positions.Add (typeAndPos);
						MonoBehaviour.print ("Queen Coords: " + new Vector2 (x, y) + "\n");
                    }
                }
                model.GetComponent<MeshRenderer>().material = firstTeam;
            }
        }
		return positions;
    }

	private List< Tuple2<PIECE_TYPES,Vector2> > PlaceBlackTeam()
    {
		MonoBehaviour.print ("Initial Black Piece Positions\n");
		List<Tuple2<PIECE_TYPES,Vector2>> positions = new List<Tuple2<PIECE_TYPES,Vector2>>();
		for (int y = ChessGlobals.BoardConstants.BOARD_MAXIMUM; y > ChessGlobals.BoardConstants.BOARD_MAXIMUM - ChessGlobals.BoardConstants.TEAM_ROWS; --y)
        {
            GameObject model = null;
			for (int x = ChessGlobals.BoardConstants.BOARD_MINIMUM; x <= ChessGlobals.BoardConstants.BOARD_MAXIMUM; ++x)
            {
                if (y == 6)
                {
                    model = Spawn(x, y, pawn);
					Tuple2<PIECE_TYPES,Vector2> typeAndPos = new Tuple2<PIECE_TYPES,Vector2> (PIECE_TYPES.PAWN, new Vector2 (x, y));
					positions.Add (typeAndPos);
					MonoBehaviour.print ("Pawn Coords: " + new Vector2 (x, y) + "\n");
                }
                else
                {
                    if (x == 0 || x == 7)
                    {
                        model = Spawn(x, y, castle);
						Tuple2<PIECE_TYPES,Vector2> typeAndPos = new Tuple2<PIECE_TYPES,Vector2> (PIECE_TYPES.ROOK, new Vector2 (x, y));
						positions.Add (typeAndPos);
						MonoBehaviour.print ("Castle Coords: " + new Vector2 (x, y) + "\n");
                    }
                    else if (x == 1 || x == 6)
                    {
                        model = Spawn(x, y, knight);
						Tuple2<PIECE_TYPES,Vector2> typeAndPos = new Tuple2<PIECE_TYPES,Vector2> (PIECE_TYPES.KNIGHT, new Vector2 (x, y));
						positions.Add (typeAndPos);
						MonoBehaviour.print ("Knight Coords: " + new Vector2 (x, y) + "\n");
                    }
                    else if (x == 2 || x == 5)
                    {
                        model = Spawn(x, y, bishop);
						Tuple2<PIECE_TYPES,Vector2> typeAndPos = new Tuple2<PIECE_TYPES,Vector2> (PIECE_TYPES.BISHOP, new Vector2 (x, y));
						positions.Add (typeAndPos);
						MonoBehaviour.print ("Bishop Coords: " + new Vector2 (x, y) + "\n");
                    }
                    else if (x == 3)
                    {
                        model = Spawn(x, y, king);
						Tuple2<PIECE_TYPES,Vector2> typeAndPos = new Tuple2<PIECE_TYPES,Vector2> (PIECE_TYPES.KING, new Vector2 (x, y));
						positions.Add (typeAndPos);
						MonoBehaviour.print ("King Coords: " + new Vector2 (x, y) + "\n");
                    }
                    else if (x == 4)
                    {
                        model = Spawn(x, y, queen);
						Tuple2<PIECE_TYPES,Vector2> typeAndPos = new Tuple2<PIECE_TYPES,Vector2> (PIECE_TYPES.QUEEN, new Vector2 (x, y));
						positions.Add (typeAndPos);
						MonoBehaviour.print ("Queen Coords: " + new Vector2 (x, y) + "\n");
                    }
                }
                model.GetComponent<MeshRenderer>().material = secondTeam;
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
                    piecePosition = new Vector2(hit.transform.position.x - MODEL_OFFSET, hit.transform.position.z - MODEL_OFFSET);
                    clicked = true;
                    return;
                }
            }
        }
        clicked = false;
    }
}
