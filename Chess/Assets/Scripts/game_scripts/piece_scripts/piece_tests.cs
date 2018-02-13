using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessGlobals;

public class SCRIPT_TESTING : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print("Test started");
        Piece testPawn = new Piece(PIECE_TYPES.Pawn, true, "a2", 1, 2);
        print("Pawn Created");

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

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
