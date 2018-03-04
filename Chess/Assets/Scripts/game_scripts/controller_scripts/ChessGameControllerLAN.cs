using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Assertions;
using ChessGlobals;

public class ChessGameControllerLAN : NetworkBehaviour
{

    [SerializeField]
    private DrawBoard drawBoard;
    private Board board;
    private List<Piece> pieces;

    private NetworkMoveModel moveModel;

    public Text turnDisplay;

    public Player whitePlayer;
    public Player blackPlayer;
    public Player activePlayer;
    private static GameState gameState;
    private List<Piece> capturedPieces;
    // Initialize positions to spots the user cannot choose. I chose Vector3.down because it contains a negative value and be unity has a shorthand for it.
    [SyncVar] private Vector3 movePieceFrom = Vector3.down;
    [SyncVar] private Vector3 movePieceTo = Vector3.down;
    private Vector3 localFrom = Vector3.down;
    private Vector3 localTo = Vector3.down;
    private List<Vector2> legalMovesForAPiece;
    private bool isPieceMove = false;
    private Piece currentlySelectedPiece;


   // private float startMoveTime = -1;
    private float moveTime = 0;
    private const float waitTime = 1.2f;
    private const int negativeTime = -1;

    private Vector3 secondCameraPos;
    private Vector3 secondCameraRot;

    public override void OnStartLocalPlayer()
    {
        secondCameraPos = Camera.main.transform.position;
        secondCameraRot = Camera.main.transform.eulerAngles;
        if (!isServer)
        {
            secondCameraPos.z = 13;
            secondCameraRot.y = 180;
        }
        Camera.main.transform.position = secondCameraPos;
        Camera.main.transform.eulerAngles = secondCameraRot;
    }

    private void Start()
    {

        if (gameState == null)
            gameState = new GameState(GameState.WHITE_TURN);

        drawBoard = GameObject.Find("Draw Board").GetComponent<DrawBoard>();
        turnDisplay = GameObject.Find("Text").GetComponent<Text>();
        board = NetworkBoard.GetBoard;
        pieces = NetworkBoard.GetPieces;

        legalMovesForAPiece = new List<Vector2>();

        //Initialize Movement Model
        moveModel = GetComponent<NetworkMoveModel>();
        capturedPieces = new List<Piece>();
    }


    // Update is called once per frame
    private void Update()
    {
        if (!isLocalPlayer)
            return;

        bool pieceDoneMoving = Time.time - moveTime > waitTime && isPieceMove == true;
        if (pieceDoneMoving)
        {
            //startMoveTime = Time.time;
            isPieceMove = false;
            if (isServer)
                RpcMsg();
            else
                CmdMsg();
        }

        if (isServer)
            RpcUpdate();
        else
            CmdUpdate();
    }

    [ClientRpc]
    void RpcMsg()
    {
        SwitchTurnDisplay();
    }

    [Command]
    void CmdMsg()
    {
        RpcMsg();
    }

    [Command]
    void CmdUpdate()
    {
        RpcUpdate();
    }

    [ClientRpc]
    private void RpcUpdate()
    {
        //handles moving a piece
        if ((DrawBoard.IsClicked || DrawPiece.IsClicked) && (movePieceFrom != Vector3.down || localFrom != Vector3.down))
        {
            currentlySelectedPiece = board.GetPieceAt(localFrom);
            //here must disallow moving pieces to anywhere other than a legal square
            if (legalMovesForAPiece != null)
            {
                movePieceTo = DrawBoard.IsClicked ? DrawBoard.SquarePosition : DrawPiece.PiecePosition;
                localTo = movePieceTo;
                DrawPiece.ClearHighlight();
                if (board.IsOccupied(localTo))
                {
                    //if the color of the piece matches the current turn
                    if (board.GetPieceAt(movePieceTo).GetTeam() == gameState.getState())
                    {
                        movePieceTo = movePieceFrom = localTo = localFrom = Vector3.down;
                        drawBoard.ClearHighlights();
                        return;
                    }
                }
                else drawBoard.ClearHighlights();
                if (!legalMovesForAPiece.Contains(localTo))
                {
                    movePieceTo =  localTo = Vector3.down;
                }
            }
        }
        //handles clicking a piece
        else if (DrawPiece.IsClicked)
        {
            if (isLocalPlayer)
            {
                if (isServer)
                { 
                    RpcSelectPiece();
                }
                else
                {
                    CmdSelectPiece();
                }
            }
        }
        // If the user has clicked on a space to move and a piece to move, move the piece and reset the vectors to numbers the user cannot choose.
        // In the real game we would also have to check if it is a valid move.
        if (movePieceFrom != Vector3.down && movePieceTo != Vector3.down && isServer)
        {
            RpcMovePieceModel(movePieceFrom, movePieceTo);
        }
        else if (localFrom != Vector3.down && localTo != Vector3.down && !isServer)
        {
            CmdMovePieceModel(localFrom, localTo);
        }
    }

    [Command]
    private void CmdSelectPiece()
    {
        movePieceFrom = DrawBoard.IsClicked ? DrawBoard.SquarePosition : DrawPiece.PiecePosition;
        localFrom = movePieceFrom;
        RpcSelectPiece();
    }

    [ClientRpc]
    private void RpcSelectPiece()
    {
        movePieceFrom = DrawBoard.IsClicked ? DrawBoard.SquarePosition : DrawPiece.PiecePosition;
        localFrom = movePieceFrom;
        if (board.GetPieceAt(movePieceFrom) == null)
        {
            movePieceTo = movePieceFrom = Vector3.down;
            return;
        }

        bool selectedTeamPiece = board.GetPieceAt(movePieceFrom).GetTeam() == gameState.getState();
        bool playersTurn = System.Convert.ToBoolean(board.GetPieceAt(movePieceFrom).GetTeam()) == isServer;

        if (selectedTeamPiece && playersTurn)
        {
            DrawPiece.HighlightPiece();
            drawBoard.HighLightGrid(movePieceFrom);
            currentlySelectedPiece = board.GetPieceAt(movePieceFrom);
            legalMovesForAPiece = currentlySelectedPiece.LegalMoves(board);
            drawBoard.HighLightGrid(legalMovesForAPiece);
        }
        else
        {
            movePieceFrom = Vector3.down;
        }
    }

    [Command]
    private void CmdMovePieceModel(Vector3 from, Vector3 to)
    {
        moveModel.MovePiece(from, to);
        RpcMovePieceModel(from, to);
    }

    [ClientRpc]
    private void RpcMovePieceModel(Vector3 from, Vector3 to)
    {
        if (currentlySelectedPiece == null)
            return;

        moveModel.MovePiece(from, to);
        TookPiece();

        if (isServer)
            RpcMark(from, to);
        else
            CmdMark(from, to);

        currentlySelectedPiece.SetPosition(to);
        moveTime = Time.time;
        isPieceMove = true;

        DrawPiece.ClearHighlight();
        drawBoard.ClearHighlights();
    }

    [ClientRpc]
    private void RpcMark(Vector3 from, Vector3 to)
    {
        currentlySelectedPiece = board.GetPieceAt(from);
        board.Mark(to, currentlySelectedPiece);
        board.UnMark(from);
        movePieceTo = movePieceFrom = localFrom = localTo = Vector3.down;
    }

    [Command]
    private void CmdMark(Vector3 from, Vector3 to)
    {
        RpcMark(from, to);
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
            gameState.setState(ChessGlobals.GameState.BLACK_TURN);
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
        var piece = CreatePiece(pieceType);
        piece.SetPosition((int)pos.x, (int)pos.y);
        return piece;
    }

    public Piece CreatePieceAt(PIECE_TYPES pieceType, Vector2 pos, int team)
    {
        Piece piece = CreatePiece(pieceType);
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
        if (IsNonCapturedPieceAt(pos))
        {
            return pos;
        }
        return new Vector2(-1, -1);//invalid position for now
    }

    public bool IsNonCapturedPieceAt(Vector2 pos)
    {
        if (board.IsOccupied(pos))
        {
            var p = board.GetPieceAt(pos);
            if (p == null)
                return false;
            else
                if (!p.IsTaken())
                return true;
        }
        return false;
    }
    /* public GameState GetGameState()
     {
         return gameState;
     }

     private int GetStateOfGame()
     {
         return gameState.getState();
     }*/

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        Debug.Log("asdfa");
    }
}
