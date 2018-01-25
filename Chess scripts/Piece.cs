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
    enum PIECE_TYPES { pawn, rook, knight, bishop, queen, king };
    int type;
    bool taken;//represents if the piece still exists on the board
    Position PiecePosition;
    bool team; //0 for black 1 for white

    public Piece(int p_type, Position pos, bool t)
    {
        type = p_type;
        taken = false;
        PiecePosition = pos;
        team = t;
    }

    //returns cureent position on board of the given piece
    public Position GetPiecePosition()
    {
        return PiecePosition;
    }

    //Sets the position of the piece
    //Only sets the position of the prospected position change is a valid move
    public void SetPosition(tuple pos)
    {
        position = pos;
    }

    //returns the type of piece
    public PIECE_TYPES getType()
    {
        return type;
    }

    public bool IsValidMove(PIECE_TYPES type, tuple move)
    {

    }

    private Piece TookPiece()
    {

    }
}



