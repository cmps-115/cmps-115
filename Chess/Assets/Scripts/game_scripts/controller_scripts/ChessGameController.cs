using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using ChessGlobals;

//Shorten Declarations
using ListOfPieceTypesAndPositions =  System.Collections.Generic.List< ChessGlobals.Tuple2<ChessGlobals.PIECE_TYPES,UnityEngine.Vector2> >;

public class ChessGameController : MonoBehaviour {

	[SerializeField] private DrawBoard drawBoard;
	[SerializeField] private DrawPiece drawPiece;
	private MoveModel moveModel;
	public Text turnDisplay;

	private Board board;
	public Player playerOne;
	public Player playerTwo;
	public Player activePlayer;
	private List<Piece> pieces;
	private List<Piece> capturedPieces;
	// Initialize positions to spots the user cannot choose. I chose Vector3.down because it contains a negative value and be unity has a shorthand for it.
	private Vector3 movePieceFrom = Vector3.down;
	private Vector3 movePieceTo = Vector3.down;


	private float startMoveTime = -1;

	private Vector3 firstCameraPos;
	private Vector3 secondCameraPos;
	private float firstCameraRot;
	private float secondCameraRot;
	// Use this for initialization
	void Start () {
		//drawBoard = gameObject.AddComponent<DrawBoard> ();
		//drawPiece = gameObject.AddComponent<DrawPiece> ();
		//Initialize Model
		board = new Board();
		//Player blackPlayer = new Player();
		//Player whitePlayer = new Player();
		pieces = new List<Piece> ();
		//Initialize Movement Model
		moveModel = gameObject.AddComponent<MoveModel> ();

		capturedPieces = new List<Piece> ();

		//Initialize View
		drawBoard.InitBoard();
		//Tuple2<  List< Tuple2<PIECE_TYPES,Vector2> >, List< Tuple2<PIECE_TYPES,Vector2> >  > startingPositions = drawPiece.InitPieces ();//t1 is Black team positions, t2 White
		Tuple2<  ListOfPieceTypesAndPositions, ListOfPieceTypesAndPositions > startingPositions = drawPiece.InitPieces ();
		Assert.AreEqual(startingPositions.t1.Count,startingPositions.t2.Count);// this should never be violated
		for (int i = 0; i < startingPositions.t1.Count; ++i) 
		{
			Piece blackPiece = createPieceAt(startingPositions.t1[i].t1, startingPositions.t1[i].t2, ChessGlobals.COLOR.BLACK);
			Piece whitePiece = createPieceAt(startingPositions.t2[i].t1, startingPositions.t2[i].t2, ChessGlobals.COLOR.WHITE);

			//These two assertions should never be violated 
			Assert.AreNotEqual(blackPiece, null);
			Assert.AreNotEqual(whitePiece, null);

			//after all they represent the same things
			Assert.AreEqual (startingPositions.t1[i].t2, blackPiece.GetPiecePosition() );
			Assert.AreEqual (startingPositions.t2[i].t2, whitePiece.GetPiecePosition() );

			board.Mark(blackPiece.GetPiecePosition(), blackPiece);
			board.Mark(whitePiece.GetPiecePosition(), whitePiece);
			//board.ToString();

			pieces.Add (whitePiece);
			pieces.Add (blackPiece);
		}
		BoardTest.ValidateInitialPiecePositions(board);
		BoardTest.ValidateMark(board);
		BoardTest.ValidateUnMark(board);
		BoardTest.ValidateClear(board);
		BoardTest.ValidateGetPieceAt(board);

		//set camera
		firstCameraPos = Camera.main.transform.position;
		secondCameraPos = new Vector3 (firstCameraPos.x, firstCameraPos.y, firstCameraPos.z + 18);
		firstCameraRot = Camera.main.transform.eulerAngles.y;
		secondCameraRot = Camera.main.transform.eulerAngles.y + 180;
		//set turn text
		turnDisplay.text = "White Turn";

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) 
		{
			startMoveTime = Time.time;
			print ("first cam pos: " + firstCameraPos);
			print ("second cam pos:" + secondCameraPos);
			switchTurnDisplay();
		}
		if (startMoveTime != -1)
			SwitchCamera ();

		//for piece movements
		if ((DrawBoard.IsClicked || DrawPiece.IsClicked) && movePieceFrom != Vector3.down)
		{
			movePieceTo = DrawBoard.IsClicked ? DrawBoard.SquarePosition : DrawPiece.PiecePosition;
			drawBoard.HighLightGrid(movePieceTo);
		}
		else if (DrawPiece.IsClicked && movePieceFrom == Vector3.down)
		{
			movePieceFrom = DrawPiece.PiecePosition;
			drawBoard.HighLightGrid(movePieceFrom);
		}

		// If the user has clicked on a space to move and a piece to move, move the piece and reset the vectors to numbers the user cannot choose.
		// In the real game we would also have to check if it is a valid move.
		if (movePieceFrom != Vector3.down && movePieceTo != Vector3.down)
		{
			moveModel.MovePiece(movePieceFrom, movePieceTo);
			movePieceTo = movePieceFrom = Vector3.down;
			drawBoard.ClearHighlights();
		}/**/
	}
	private void switchTurnDisplay()
	{
		if(turnDisplay.text == "White Turn")
			turnDisplay.text = "Black Turn";
		else if(turnDisplay.text == "Black Turn")
			turnDisplay.text = "White Turn";
	}
	private void SwitchCamera()
	{
		var deltaTime = Time.time - startMoveTime;
		RotateCamera (deltaTime);
		MoveCamera (deltaTime);
		if (deltaTime >= 1) 
		{
			var temp = firstCameraPos;
			firstCameraPos = secondCameraPos;
			secondCameraPos = temp;

			var temp2 = firstCameraRot;
			firstCameraRot = secondCameraRot;
			secondCameraRot = temp2;

			startMoveTime = -1;
		}
	}

	private void RotateCamera(float dTime)
	{
		var currentRot = Camera.main.transform.eulerAngles;
		Camera.main.transform.eulerAngles = new Vector3 (currentRot.x, Mathf.LerpAngle(firstCameraRot, secondCameraRot, dTime), currentRot.z);
	}

	private void MoveCamera(float dTime)
	{
		Camera.main.transform.position = Vector3.Lerp(firstCameraPos, secondCameraPos, dTime);
	}
	public void swapActivePlayer()
	{
		if (activePlayer == playerOne)
			activePlayer = playerTwo;
		if (activePlayer == playerTwo)
			activePlayer = playerOne;
	}
	public void setPlayer(Player playerOne, Player playerTwo)
	{
		this.playerOne = playerOne;
		this.playerTwo = playerTwo;
	}
	public void startGame()
	{
	}
	public Piece createPiece(PIECE_TYPES pieceType)
	{
		if (pieceType == PIECE_TYPES.KING) 
		{
			return new King();
		}
		else if (pieceType == PIECE_TYPES.QUEEN) 
		{
			return new Queen();
		}
		else if (pieceType == PIECE_TYPES.ROOK) 
		{
			return new Rook();
		}
		else if (pieceType == PIECE_TYPES.BISHOP) 
		{
			return new Bishop();
		}
		else if (pieceType == PIECE_TYPES.KNIGHT) 
		{
			return new Knight();
		}
		else //if (pieceType == PIECE_TYPES.PAWN) 
		{
			return new Pawn();
		}

	}
	public Piece createPieceAt(PIECE_TYPES pieceType, Vector2 pos, bool team)
	{
		Piece piece = createPiece (pieceType);
		piece.SetPosition ( (int)pos.x, (int)pos.y);
		piece.SetTeam(team);
		return piece;
	}
	public Move movePiece(Move move)
	{
		return null;
	}
	public void waitForMove()
	{
	}
	public void undoMove(Move move)
	{
	}
	public bool endConditionReached()
	{
		return false;
	}
	//not sure if this is needed
	public Vector2 getNonCapturedPieceAt(Vector2 pos)
	{
		if (isNonCapturedPieceAt (pos)) 
		{
			return pos;
		}
		return new Vector2 (-1, -1);//invalid position for now
	}
	public bool isNonCapturedPieceAt(Vector2 pos)
	{
		if (board.IsOccupied (pos)) 
		{
			Piece p = board.GetPieceAt (pos);
			if (p == null)
				return false;
			else
				if (!p.IsTaken ())
					return true;
		}
		return false;
	}
	public GAME_STATE getGameState()
	{
		return GAME_STATE.DRAW;
	}


}
