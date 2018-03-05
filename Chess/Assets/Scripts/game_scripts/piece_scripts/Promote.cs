using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ChessGlobals;


public class Promote : MonoBehaviour
{
    //Queen
    //Rook
    //Bishop
    //Knight
    public static bool IsPromotable(Piece piece)
    {
        if (piece.GetType() == typeof(Pawn))
        {
            int promotePosition = 0;
            //black
            if (piece.GetTeam() == ChessGlobals.Teams.BLACK_TEAM)
            {
                if (piece.GetPiecePositionY() == promotePosition)
                    return true;
            }
            else
            {
                //for white
                promotePosition = 7;
                if (piece.GetPiecePositionY() == promotePosition)
                    return true;
            }
        }
        return false;

    }

    public static Piece Promotes(string choice, Pawn piece)
    {
        Piece newPiece = null;
        if (choice == "Rook")
        {
            newPiece = new Rook(piece.GetTeam(), piece.GetPiecePositionX(), piece.GetPiecePositionY());
        }
        else if (choice == "Bishop")
        {
            newPiece = new Bishop(piece.GetTeam(), piece.GetPiecePositionX(), piece.GetPiecePositionY());
        }
        else if (choice == "Queen")
        {
            newPiece = new Queen(piece.GetTeam(), piece.GetPiecePositionX(), piece.GetPiecePositionY());
        }
        else if (choice == "Knight")
        {
            newPiece = new Knight(piece.GetTeam(), piece.GetPiecePositionX(), piece.GetPiecePositionY());
        }

        return newPiece;

    }

}

