using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Assertions;
using ChessGlobals;
public class ChessGameControllerLAN : NetworkBehaviour
{


	[SerializeField] private DrawBoard drawBoard;
	[SerializeField] private DrawPiece drawPiece;
	private MoveModel moveModel;
	public Text turnDisplay;

	private Board board;
	public Player whitePlayer;
	public Player blackPlayer;
	public Player activePlayer;
	private ChessGlobals.GameState gameState;
	private List<Piece> pieces;
	private List<Piece> capturedPieces;
	// Initialize positions to spots the user cannot choose. I chose Vector3.down because it contains a negative value and be unity has a shorthand for it.
	private Vector3 movePieceFrom = Vector3.down;
	private Vector3 movePieceTo = Vector3.down;
	private List<Vector2> legalMovesForAPiece;
	private bool isPieceMove = false;
	private Piece currentlySelectedPiece;


	private float startMoveTime = -1;
	private float moveTime = 0;
	private const float waitTime = 1.2f;
	private const int negativeTime = -1;

	private Vector3 firstCameraPos;
	private Vector3 secondCameraPos;
	private float firstCameraRot;
	private float secondCameraRot;

	// Use this for initialization
	private void Start () 
	{
		legalMovesForAPiece = new List<Vector2>();
		gameState = new GameState(ChessGlobals.GameState.WHITE_TURN);
		//Initialize Model
		board = new Board();
		pieces = new List<Piece> ();
		//Initialize Movement Model
		moveModel = gameObject.AddComponent<MoveModel> ();
		capturedPieces = new List<Piece> ();

		//Initialize View
		drawBoard.InitBoard();
		var startingPositions = drawPiece.NetworkInitPieces ();
		for (int i = 0; i < startingPositions.t1.Count; ++i) 
		{
			Piece blackPiece = CreatePieceAt(startingPositions.t1[i].t1, startingPositions.t1[i].t2, ChessGlobals.Teams.BLACK_TEAM);
			Piece whitePiece = CreatePieceAt(startingPositions.t2[i].t1, startingPositions.t2[i].t2, ChessGlobals.Teams.WHITE_TEAM);

			board.Mark(blackPiece.GetPiecePosition(), blackPiece);
			board.Mark(whitePiece.GetPiecePosition(), whitePiece);

			pieces.Add (whitePiece);
			pieces.Add (blackPiece);
		}
 
	}

	// Update is called once per frame
	private void Update () 
	{	
		CmdUpdate ();
	}

	[Command]
	private void CmdUpdate()
	{

		bool pieceDoneMoving = Time.time - moveTime > waitTime && isPieceMove == true;
		if (pieceDoneMoving)
		{
			startMoveTime = Time.time;
			isPieceMove = false;
			SwitchTurnDisplay();
		}
		//handles moving a piece
		if ((DrawBoard.IsClicked || DrawPiece.IsClicked) && movePieceFrom != Vector3.down) 
		{
			//here must disallow moving pieces to anywhere other than a legal square
			if (legalMovesForAPiece != null)
			{
				movePieceTo = DrawBoard.IsClicked ? DrawBoard.SquarePosition : DrawPiece.PiecePosition;
				DrawPiece.ClearHighlight();
				if (board.IsOccupied (movePieceTo)) 
				{
					//if the color of the piece matches the current turn
					if (board.GetPieceAt (movePieceTo).GetTeam () == gameState.getState())
					{
						movePieceTo = Vector3.down;
						movePieceFrom = Vector3.down;
						drawBoard.ClearHighlights();
						return;
					}	
				}
				else drawBoard.ClearHighlights();
				if (!legalMovesForAPiece.Contains(movePieceTo)) movePieceTo = Vector3.down;
			}
		}
		//handles clicking a piece
		else if (DrawPiece.IsClicked)
		{
			SelectPiece ();
		} 
		// If the user has clicked on a space to move and a piece to move, move the piece and reset the vectors to numbers the user cannot choose.
		// In the real game we would also have to check if it is a valid move.
		if (movePieceFrom != Vector3.down && movePieceTo != Vector3.down)
		{
			MovePieceModel(movePieceFrom, movePieceTo);
		}
	}

	private void SelectPiece()
	{
		movePieceFrom = DrawBoard.IsClicked ? DrawBoard.SquarePosition : DrawPiece.PiecePosition;
		if (board.GetPieceAt(movePieceFrom) == null)
		{
			movePieceTo = movePieceFrom = Vector3.down;
			return;
		}

		if (board.GetPieceAt(movePieceFrom).GetTeam() == gameState.getState())
		{
			DrawPiece.HighlightPiece();
			drawBoard.HighLightGrid(movePieceFrom);
			currentlySelectedPiece = board.GetPieceAt(new Vector2(movePieceFrom.x, movePieceFrom.y));
			legalMovesForAPiece = currentlySelectedPiece.LegalMoves(this.board);
			drawBoard.HighLightGrid(legalMovesForAPiece);
		}
		else
		{
			movePieceFrom = Vector3.down;
		}
	}

	private void MovePieceModel(Vector3 from, Vector3 to)
	{
		if (currentlySelectedPiece == null)
			return;

		moveModel.MovePiece(from, to);
		TookPiece();
		board.Mark(to, currentlySelectedPiece);
		board.UnMark(from);

		currentlySelectedPiece.SetPosition(to);
		movePieceTo = movePieceFrom = Vector3.down;
		moveTime = Time.time;
		isPieceMove = true;

		DrawPiece.ClearHighlight();
		drawBoard.ClearHighlights();
	}

	private void TookPiece()
	{
		if (moveModel.Overlapped)
		{
			var pieceBeingTaken = board.GetPieceAt(movePieceTo);
			pieces.Remove(pieceBeingTaken);
			capturedPieces.Add(pieceBeingTaken);
		}
	}

	private void SwitchTurn()
	{
		if (gameState.getState() == ChessGlobals.GameState.WHITE_TURN) 
		{
			gameState.setState( ChessGlobals.GameState.BLACK_TURN);
		}
		else if (gameState.getState() == ChessGlobals.GameState.BLACK_TURN)
		{
			gameState.setState(ChessGlobals.GameState.WHITE_TURN);
		}
	}

	private void SwitchTurnDisplay()
	{
		if (gameState.getState() == ChessGlobals.GameState.WHITE_TURN) 
		{
			turnDisplay.text = "Black Turn";
			SwitchTurn();
		} 
		else if (gameState.getState() == ChessGlobals.GameState.BLACK_TURN) 
		{
			turnDisplay.text = "White Turn";
			SwitchTurn();
		}
	}

	private Piece CreatePiece(PIECE_TYPES pieceType)
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

	private Piece CreatePieceAt(PIECE_TYPES pieceType, Vector2 pos, bool team)
	{
		var piece = CreatePiece (pieceType);
		piece.SetPosition ( (int)pos.x, (int)pos.y);
		return piece;
	}

	public Piece CreatePieceAt(PIECE_TYPES pieceType, Vector2 pos, int team)
	{
		Piece piece = CreatePiece (pieceType);
		piece.SetPosition ( (int)pos.x, (int)pos.y);
		piece.SetTeam(team);
		return piece;
	}

	public void SwapActivePlayer()
	{
		if (activePlayer == whitePlayer)
			activePlayer = blackPlayer;
		else if (activePlayer == blackPlayer)
			activePlayer = whitePlayer;
	}

	public void SetPlayer(Player whitePlayer, Player blackPlayer)
	{
		this.whitePlayer = whitePlayer;
		this.blackPlayer = blackPlayer;
	}

	public List<Piece> GetPieces()
	{
		return pieces;
	}

	public Board GetBoard()
	{
		//because some bs with c# and const, cloneing will definently prevent unintentional modifications to the game board 
		return board;
	}

	public Board GetBoardClone()
	{
		return DeepCopy.Copy(board) as Board;
	}

	public Move MovePiece(Move move)
	{
		return null;
	}

	public void UndoMove(Move move)
	{
	}
	public bool EndConditionReached()
	{
		return false;
	}

	//not sure if this is needed
	public Vector2 GetNonCapturedPieceAt(Vector2 pos)
	{
		if (IsNonCapturedPieceAt (pos)) 
		{
			return pos;
		}
		return new Vector2 (-1, -1);//invalid position for now
	}

	public bool IsNonCapturedPieceAt(Vector2 pos)
	{
		if (board.IsOccupied (pos)) 
		{
			var p = board.GetPieceAt (pos);
			if (p == null)
				return false;
			else
				if (!p.IsTaken ())
					return true;
		}
		return false;
	}
	public GameState GetGameState()
	{
		return gameState;
	} 

	private int GetStateOfGame()
	{
		return gameState.getState ();
	}
}
