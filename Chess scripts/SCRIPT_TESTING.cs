using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessGlobals;

public class SCRIPT_TESTING : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print("Test started");
       // Piece testPawn = new Piece(PIECE_TYPES.Pawn, true, "a2", 1, 2);

        /*print("Pawn Created");

        print(testPawn.PiecePosition.GetX());
        print(testPawn.PiecePosition.GetY());
        print(testPawn.PiecePosition.GetSquareName());
        print(testPawn.GetPieceType());

        testPawn.PiecePosition.SetPosition("a3", 1, 3);
        print(testPawn.PiecePosition.GetX());
        print(testPawn.PiecePosition.GetY());
        print(testPawn.PiecePosition.GetSquareName());
        print(testPawn.PiecePosition.GetCordinate());

        testPawn.PiecePosition.SetPosition("a8", 1, 8);
        testPawn.Promote();
        print(testPawn.PiecePosition.GetX());
        print(testPawn.PiecePosition.GetY());
        print(testPawn.PiecePosition.GetSquareName());
        print(testPawn.GetPieceType());

        testPawn.PieceTaken();
        print(testPawn.PiecePosition.GetX());
        print(testPawn.PiecePosition.GetY());
        print(testPawn.PiecePosition.GetSquareName());
        */
        //Test Section for Rules: King, Knight
        Board testBoard = new Board();

        Piece testKing = new Piece(PIECE_TYPES.King, true, "", 3, 3);
        Piece testKnight = new Piece(PIECE_TYPES.Knight, true, "", 5, 5);
        Piece testKing2 = new Piece(PIECE_TYPES.King, true, "", 0, 0);
        Piece testKnight2 = new Piece(PIECE_TYPES.Knight, true, "", 7, 7);
        Piece testPawn2 = new Piece(PIECE_TYPES.Pawn, false, "", 3, 6);
        Piece testPawn3 = new Piece(PIECE_TYPES.Pawn, false, "", 2, 4);
        Piece testPawn4 = new Piece(PIECE_TYPES.Pawn, true, "", 4, 2);
        Piece testPawn5 = new Piece(PIECE_TYPES.Pawn, true, "", 6, 3);

        testBoard.Mark(testKing.PiecePosition, testKing);
        testBoard.Mark(testKing2.PiecePosition, testKing2);
        testBoard.Mark(testKnight.PiecePosition, testKnight);
        testBoard.Mark(testKnight2.PiecePosition, testKnight2);
        testBoard.Mark(testPawn2.PiecePosition, testPawn2);
        testBoard.Mark(testPawn3.PiecePosition, testPawn3);
        testBoard.Mark(testPawn4.PiecePosition, testPawn4);
        testBoard.Mark(testPawn5.PiecePosition, testPawn5);

        List<Vector2> king1Positions = Rules.LegalKingMove(testKing, testBoard);
        List<Vector2> king2Positions = Rules.LegalKingMove(testKing2, testBoard);
        List<Vector2> knightPositions = Rules.LegalKinghtMove(testKnight, testBoard);
        List<Vector2> knight2Positions = Rules.LegalKinghtMove(testKnight2, testBoard);

        print("knight 1 Moves");
        foreach(var Vector2 in knightPositions)
        {
            print(Vector2.x);
            print(Vector2.y);
        }
        print("knight 2 moves");
        foreach (var Vector2 in knight2Positions)
        {
            print(Vector2.x);
            print(Vector2.y);
        }
        print("King 1 moves");
        foreach (var Vector2 in king1Positions)
        {
            print(Vector2.x);
            print(Vector2.y);
        }
        print("King 2 moves");
        foreach (var Vector2 in king2Positions)
        {
            print(Vector2.x);
            print(Vector2.y);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
