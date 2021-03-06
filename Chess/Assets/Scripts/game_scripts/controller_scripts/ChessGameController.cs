using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using ChessGlobals;

//Shorten Declarations
using ListOfPieceTypesAndPositions = System.Collections.Generic.List<ChessGlobals.Tuple2<ChessGlobals.PIECE_TYPES, UnityEngine.Vector2>>;
using TMPro;

public class ChessGameController : MonoBehaviour 
{

	[SerializeField] protected DrawBoard drawBoard;
	[SerializeField] protected DrawPiece drawPiece;
	protected MoveModel moveModel;
    public GameObject promoteMenu;
    public GameObject endGameMenu;
	public TextMeshProUGUI turnDisplay;

	protected Board board;
	public Player whitePlayer;
	public Player blackPlayer;
	public Player activePlayer;
	protected ChessGlobals.GameState gameState;
    protected List<Piece> pieces;
    protected List<Piece> capturedPieces;
    // Initialize positions to spots the user cannot choose. I chose Vector3.down because it contains a negative value and be unity has a shorthand for it.
    protected Vector3 movePieceFrom = Vector3.down;
    protected Vector3 movePieceTo = Vector3.down;
    protected List<Vector2> legalMovesForAPiece;
    protected bool isPieceMove = false;
    protected Piece currentlySelectedPiece;


    protected float startMoveTime = -1;
    protected float moveTime = 0;
    protected const float waitTime = 1.2f;
    protected const int negativeTime = -1;

    protected Vector3 firstCameraPos;
    protected Vector3 secondCameraPos;
    protected float firstCameraRot;
    protected float secondCameraRot;

    protected bool promoteMenuActive = false;
    protected string promoteType = "";

    // Use this for initialization
    protected virtual void Start () 
	{
        legalMovesForAPiece = new List<Vector2>();
        gameState = new GameState(ChessGlobals.GameState.WHITE_TURN);

		//Initialize Model
		board = new Board();

		//Initialize Movement Model
		moveModel = gameObject.AddComponent<MoveModel> ();
		capturedPieces = new List<Piece> ();

		//Initialize View
		//drawBoard.InitBoard();
		var startingPositions = drawPiece.InitPieces ();
		for (int i = 0; i < startingPositions.t1.Count; ++i) 
		{
			Piece blackPiece = CreatePieceAt(startingPositions.t1[i].t1, startingPositions.t1[i].t2, ChessGlobals.Teams.BLACK_TEAM);
			Piece whitePiece = CreatePieceAt(startingPositions.t2[i].t1, startingPositions.t2[i].t2, ChessGlobals.Teams.WHITE_TEAM);

			board.Mark(blackPiece.GetPiecePosition(), blackPiece);
			board.Mark(whitePiece.GetPiecePosition(), whitePiece);

            board.AddActivePiece(whitePiece);
            board.AddActivePiece(blackPiece);
        }
    }

	// Update is called once per frame
	protected virtual void Update () 
	{

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
                if (!legalMovesForAPiece.Contains(movePieceTo))
                {
                    movePieceTo = Vector3.down;
                    currentlySelectedPiece = null;
                }
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

	protected void SelectPiece()
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

            CheckingForCheck();

            drawBoard.HighLightGrid(legalMovesForAPiece);
        }
        else
        {
            movePieceFrom = Vector3.down;
        }
	}

    protected void CheckingForCheck()
    {
        if (currentlySelectedPiece.GetTeam() == Teams.BLACK_TEAM)
        {
            if (KingInCheck.IsBlackInCheck() == true || board.GetSquare(currentlySelectedPiece.GetPiecePosition()).getWhiteThreat() == true || currentlySelectedPiece.GetType() == typeof(King))
            {
                print("Black King in check, or Square is thretened");
                legalMovesForAPiece = currentlySelectedPiece.CheckLegalMoves(board, legalMovesForAPiece);
            }
        }
        else
        {
            if (KingInCheck.IsWhiteInCheck() == true || board.GetSquare(currentlySelectedPiece.GetPiecePosition()).getBlackThreat() == true || currentlySelectedPiece.GetType() == typeof(King))
            {
                print("White King is in check, or square is threatened");
                legalMovesForAPiece = currentlySelectedPiece.CheckLegalMoves(board, legalMovesForAPiece);
            }
        }
    }

    protected void CheckMate()
    {
        if (KingInCheck.IsWhiteInCheck())
        {
            foreach (Piece whitePiece in GetPieces(board))
            {
                if (whitePiece.GetTeam() == Teams.WHITE_TEAM && whitePiece.CheckLegalMoves(board, whitePiece.LegalMoves(board)).Capacity > 0)
                    return;
            }
        }

        if (KingInCheck.IsBlackInCheck())
        {
            foreach (Piece blackPiece in GetPieces(board))
            {
                if (blackPiece.GetTeam() == Teams.BLACK_TEAM && blackPiece.CheckLegalMoves(board, blackPiece.LegalMoves(board)).Capacity > 0)
                    return;
            }
        }

        endGameMenu.SetActive(true);
        print("No legal moves were found, the game is over");

        return;
    }

    protected void MovePieceModel(Vector3 from, Vector3 to)
    {
        if (currentlySelectedPiece == null)
            return;

        moveModel.MovePiece(from, to);
        TookPiece();
        board.Mark(to, currentlySelectedPiece);
        board.UnMark(from);

        TogglePromoteMenu();

        currentlySelectedPiece.SetPosition(to);
        movePieceTo = movePieceFrom = Vector3.down;
        moveTime = Time.time;
        isPieceMove = true;

        DrawPiece.ClearHighlight();
        drawBoard.ClearHighlights();


        board.UpdateBoardThreat(null, Vector2.down);
        if (KingInCheck.IsBlackInCheck() == true || KingInCheck.IsWhiteInCheck() == true)
            CheckMate();
    }

    protected void TookPiece()
    {
        if (moveModel.Overlapped)
        {
            var pieceBeingTaken = board.GetPieceAt(movePieceTo);
            board.CaptureActivePiece(pieceBeingTaken);
        }
    }

    protected void TogglePromoteMenu()
    {
        if (Promote.IsPromotable(currentlySelectedPiece))
        {
            promoteMenu.SetActive(true);
        }
        else
        {
            promoteMenu.SetActive(false);
        }
    }

    protected virtual void PromoteMenuSelect()
    {
        board.Mark(currentlySelectedPiece.GetPiecePosition(), Promote.Promotes(promoteType, (Pawn)currentlySelectedPiece));
        drawPiece.ChangeModelType(currentlySelectedPiece.GetPiecePosition(), promoteType, currentlySelectedPiece.GetTeam());
        DrawPiece.ClearHighlight();
        promoteMenu.SetActive(false);
        //SwitchTurnDisplay();
    }

    public void PromoteMenuListener(string button_name)
    {
        promoteType = button_name;
        PromoteMenuSelect();
    }

    protected void SwitchTurn()
	{
		if (gameState.getState() == ChessGlobals.GameState.WHITE_TURN) 
		{
			gameState.setState( ChessGlobals.GameState.BLACK_TURN);
            drawBoard.ClearHighlights();
		}
		else if (gameState.getState() == ChessGlobals.GameState.BLACK_TURN)
		{
            drawBoard.ClearHighlights();
            gameState.setState(ChessGlobals.GameState.WHITE_TURN);
		}
	}

    protected void SwitchTurnDisplay()
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

    protected Piece CreatePiece(PIECE_TYPES pieceType)
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

    protected Piece CreatePieceAt(PIECE_TYPES pieceType, Vector2 pos, bool team)
	{
		var piece = CreatePiece(pieceType);
		piece.SetPosition((int)pos.x, (int)pos.y);
		return piece;
	}

	public Piece CreatePieceAt(PIECE_TYPES pieceType, Vector2 pos, int team)
	{
		Piece piece = CreatePiece (pieceType);
		piece.SetPosition((int)pos.x, (int)pos.y);
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

	public List<Piece> GetPieces(Board chessBoard)
	{
		return chessBoard.GetActivePieces();
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

	protected int GetStateOfGame()
	{
		return gameState.getState ();
	}
}
