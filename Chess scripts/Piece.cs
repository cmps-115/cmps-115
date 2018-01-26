//Piece.cs
//A piece class that defines the atributes of a generic chess piece
//Each type of piece is classified by an enumeration(the corisponding int value is referenced below)
//Piece uses the position class to define a pieces current position
//*****UNFINISHED*****

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Piece
{
    //Each piece name is represented by an number
    //Pawn = 0, rook = 1, knight = 2, bishop = 3, queen = 4, king = 5
    public enum PIECE_TYPES { Pawn, Rook, Knight, Bishop, Queen, King };
    public PIECE_TYPES type;
    public bool taken;//represents if the piece still exists on the board
    public Position PiecePosition;
    public bool team; //0 for black 1 for white

    
    public Piece(PIECE_TYPES p_type, bool t)//Constructor for a piece, without a specified position
    {
        type = p_type;
        taken = false;
        PiecePosition = new Position();
        team = t;
    }

    public Piece(PIECE_TYPES p_trype, bool t, string sqName, int Xcord, int Ycord)//Constructor for piece with a specific position
    {
        type = p_trype;
        taken = false;
        PiecePosition = new Position(sqName, Xcord, Ycord);
        team = t;
    }
    //returns cureent position on board of the given piece
    public Position GetPiecePosition()
    {
        return PiecePosition;  
    }

    //returns the type of piece
    public PIECE_TYPES GetPieceType()
    {
        return type;
    }
    
    public void PieceTaken()
    {
        PiecePosition.SetPosition("Out", -1, -1);
        taken = true;
    }

    public void Promote()//Promote turns a pawn that has reached rank 8 or 1 and it can be changed into another piece
    {
        if (type == PIECE_TYPES.Pawn && ((PiecePosition.GetY() == 1) || (PiecePosition.GetY() == 8)))
        {
            //call a function that asks for a choice of promotion
            //using queen as substitute
            type = PIECE_TYPES.Queen;
            //call a function that changes the model from a pawn, to the chosen promotion

        }
        else return;
    }
}



