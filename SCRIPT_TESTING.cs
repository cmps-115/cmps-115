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
        King BKing = new King(COLOR.BLACK, 0, 0);
        Queen BQueen = new Queen(COLOR.BLACK, 6, 6);
        Rook BRook = new Rook(COLOR.BLACK, 5, 3);
        Bishop BBishop = new Bishop(COLOR.BLACK, 2, 2);
        Knight BKnight = new Knight(COLOR.BLACK, 6, 1);
        Pawn BPawn1 = new Pawn(COLOR.BLACK, 2, 6);
        Pawn BPawn2 = new Pawn(COLOR.BLACK, 7, 1);

        Pawn WTestPawn1 = new Pawn(COLOR.WHITE, 0, 1);
        Pawn WTestPawn2 = new Pawn(COLOR.WHITE, 3, 1);
        Pawn WTestPawn3 = new Pawn(COLOR.WHITE, 4, 2);
        Pawn WTestPawn4 = new Pawn(COLOR.WHITE, 3, 5);

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

        print("King Moves");
        foreach(Vector2Int move in BKingMoves)
        {
            print(move.ToString());
        }
        print("Queen Moves");
        foreach (Vector2Int move in BQeenMoves)
        {
            print(move.ToString());
        }
        print("Knight Moves");
        foreach (Vector2Int move in BKnightMoves)
        {
            print(move.ToString());
        }
        print("Bishop Moves");
        foreach (Vector2Int move in BBishopsMoves)
        {
            print(move.ToString());
        }
        print("Rook Moves");
        foreach (Vector2Int move in BRookMoves)
        {
            print(move.ToString());
        }
        print("Pawn 1 Moves");
        foreach (Vector2Int move in BPawn1Moves)
        {
            print(move.ToString());
        }
        print("Pawn 2 Moves");
        foreach (Vector2Int move in BPawn2Moves)
        {
            print(move.ToString());
        }

        King WKing = new King(COLOR.WHITE, 0, 0);
        Queen WQueen = new Queen(COLOR.WHITE, 6, 6);
        Rook WRook = new Rook(COLOR.WHITE, 5, 3);
        Bishop WBishop = new Bishop(COLOR.WHITE, 2, 2);
        Knight WKnight = new Knight(COLOR.WHITE, 6, 1);
        Pawn WPawn1 = new Pawn(COLOR.WHITE, 5, 1);
        Pawn WPawn2 = new Pawn(COLOR.WHITE, 2, 4);

        Pawn BTestPawn1 = new Pawn(COLOR.BLACK, 0, 1);
        Pawn BTestPawn2 = new Pawn(COLOR.BLACK, 3, 1);
        Pawn BTestPawn3 = new Pawn(COLOR.BLACK, 4, 2);
        Pawn BTestPawn4 = new Pawn(COLOR.BLACK, 3, 5);

        WTestBoard.Mark(WKing.GetPiecePosition(), WKing);
        WTestBoard.Mark(WQueen.GetPiecePosition(), WQueen);
        WTestBoard.Mark(WRook.GetPiecePosition(), WRook);
        WTestBoard.Mark(WBishop.GetPiecePosition(), WBishop);
        WTestBoard.Mark(WKnight.GetPiecePosition(), WKnight);
        WTestBoard.Mark(WPawn1.GetPiecePosition(), WPawn1);
        WTestBoard.Mark(WPawn2.GetPiecePosition(), WPawn2);

        WTestBoard.Mark(BTestPawn1.GetPiecePosition(), BTestPawn1);
        WTestBoard.Mark(BTestPawn2.GetPiecePosition(), BTestPawn2);
        WTestBoard.Mark(BTestPawn3.GetPiecePosition(), BTestPawn3);
        WTestBoard.Mark(BTestPawn4.GetPiecePosition(), BTestPawn4);

        List<Vector2Int> WKingMoves = WKing.LegalMoves(WTestBoard);
        List<Vector2Int> WQeenMoves = WQueen.LegalMoves(WTestBoard);
        List<Vector2Int> WKnightMoves = WKnight.LegalMoves(WTestBoard);
        List<Vector2Int> WBishopsMoves = WBishop.LegalMoves(WTestBoard);
        List<Vector2Int> WRookMoves = WRook.LegalMoves(WTestBoard);
        List<Vector2Int> WPawn1Moves = WPawn1.LegalMoves(WTestBoard);
        List<Vector2Int> WPawn2Moves = WPawn2.LegalMoves(WTestBoard);

        print("White piece tests:");


        print("King Moves");
        foreach (Vector2Int move in WKingMoves)
        {
            print(move.ToString());
        }
        print("Queen Moves");
        foreach (Vector2Int move in WQeenMoves)
        {
            print(move.ToString());
        }
        print("Knight Moves");
        foreach (Vector2Int move in WKnightMoves)
        {
            print(move.ToString());
        }
        print("Bishop Moves");
        foreach (Vector2Int move in WBishopsMoves)
        {
            print(move.ToString());
        }
        print("Rook Moves");
        foreach (Vector2Int move in WRookMoves)
        {
            print(move.ToString());
        }
        print("Pawn 1 Moves");
        foreach (Vector2Int move in WPawn1Moves)
        {
            print(move.ToString());
        }
        print("Pawn 2 Moves");
        foreach (Vector2Int move in WPawn2Moves)
        {
            print(move.ToString());
        }
    }

 
    // Update is called once per frame
    void Update () {
		
	}
}
