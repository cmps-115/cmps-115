using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessGlobals;

public class SCRIPT_TESTING : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Board BTestBoard = new Board();
        Board WTestBoard = new Board();

        //Black test Pieces
        King BKing = new King(Globals.BLACK, 0, 0);
        Queen BQueen = new Queen(Globals.BLACK, 6, 6);
        Rook BRook = new Rook(Globals.BLACK, 5, 3);
        Bishop BBishop = new Bishop(Globals.BLACK, 2, 2);
        Knight BKnight = new Knight(Globals.BLACK, 6, 1);
        Pawn BPawn1 = new Pawn(Globals.BLACK, 2, 4);
        Pawn BPawn2 = new Pawn(Globals.BLACK, 7, 1);

        Pawn WTestPawn1 = new Pawn(Globals.WHITE, 0, 1);
        Pawn WTestPawn2 = new Pawn(Globals.WHITE, 3, 1);
        Pawn WTestPawn3 = new Pawn(Globals.WHITE, 4, 2);
        Pawn WTestPawn4 = new Pawn(Globals.WHITE, 3, 5);

        BTestBoard.Mark(BKing.GetPiecePosition(), BKing);
        BTestBoard.Mark(BQueen.GetPiecePosition(), BQueen);
        BTestBoard.Mark(BRook.GetPiecePosition(), BRook);
        BTestBoard.Mark(BBishop.GetPiecePosition(), BBishop);
        BTestBoard.Mark(BKnight.GetPiecePosition(), BKnight);
        BTestBoard.Mark(BPawn1.GetPiecePosition(), BPawn1);
        BTestBoard.Mark(BPawn2.GetPiecePosition(), BPawn2);

        BTestBoard.Mark(WTestPawn1.GetPiecePosition(), WTestPawn1);
        BTestBoard.Mark(WTestPawn2.GetPiecePosition(), WTestPawn2);
        BTestBoard.Mark(WTestPawn3.GetPiecePosition(), WTestPawn3);
        BTestBoard.Mark(WTestPawn4.GetPiecePosition(), WTestPawn4);

        List<Vector2Int> BKingMoves = BKing.LegalMoves(BTestBoard);
        List<Vector2Int> BQeenMoves = BQueen.LegalMoves(BTestBoard);
        List<Vector2Int> BKnightMoves = BKnight.LegalMoves(BTestBoard);
        List<Vector2Int> BBishopsMoves = BBishop.LegalMoves(BTestBoard);
        List<Vector2Int> BRookMoves = BRook.LegalMoves(BTestBoard);
        List<Vector2Int> BPawn1Moves = BPawn1.LegalMoves(BTestBoard);
        List<Vector2Int> BPawn2Moves = BPawn2.LegalMoves(BTestBoard);

        foreach(Vector2Int move in BKingMoves)
        {
            print(move.ToString());
        }

        foreach (Vector2Int move in BQeenMoves)
        {
            print(move.ToString());
        }

        foreach (Vector2Int move in BKnightMoves)
        {
            print(move.ToString());
        }

        foreach (Vector2Int move in BBishopsMoves)
        {
            print(move.ToString());
        }

        foreach (Vector2Int move in BRookMoves)
        {
            print(move.ToString());
        }

        foreach (Vector2Int move in BPawn1Moves)
        {
            print(move.ToString());
        }

        foreach (Vector2Int move in BPawn2Moves)
        {
            print(move.ToString());
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
