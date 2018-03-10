using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using ChessGlobals;

public class NetworkBoard : NetworkBehaviour
{

    [SerializeField] private NetworkDrawPiece drawPiece;
    private static Board board = new Board();

    private static List<Piece> pieces = new List<Piece>();
    // Use this for initialization
    void Start ()
    {
        drawPiece = GameObject.Find("Draw Pieces").GetComponent<NetworkDrawPiece>();

        var startingPositions = drawPiece.NetworkInitPieces();
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


    [Command]
    private void CmdInit()
    {
        print("no?");
        var startingPositions = drawPiece.NetworkInitPieces();
        for (int i = 0; i < startingPositions.t1.Count; ++i)
        {
            Piece blackPiece = CreatePieceAt(startingPositions.t1[i].t1, startingPositions.t1[i].t2, ChessGlobals.Teams.BLACK_TEAM);
            Piece whitePiece = CreatePieceAt(startingPositions.t2[i].t1, startingPositions.t2[i].t2, ChessGlobals.Teams.WHITE_TEAM);

            board.Mark(blackPiece.GetPiecePosition(), blackPiece);
            board.Mark(whitePiece.GetPiecePosition(), whitePiece);

            pieces.Add(whitePiece);
            pieces.Add(blackPiece);
        }
    }

    public static Board GetBoard
    {
        get { return board; }
    }

    public static List<Piece> GetPieces
    {
        get { return pieces; }
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
}
