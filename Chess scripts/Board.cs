using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessGlobals;


public class Board
{

    public Pair[,] squares;

    public Board()
    {
        squares = new Pair[8,8];

        //initialze to null
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                squares[i , j] = null;
    }

    public void Mark(Vector2Int square, Piece piece)
    {
        squares[square.x,square.y] = new Pair(square, piece);
    }

    public void UnMark(Vector2Int Vector2Int)
    {
        squares[Vector2Int.x, Vector2Int.y] = null;
    }

    public bool IsOccupied(Vector2Int square)
    {
        if (squares[square.x, square.y] != null)
            return true;
        else
            return false;
    }
}