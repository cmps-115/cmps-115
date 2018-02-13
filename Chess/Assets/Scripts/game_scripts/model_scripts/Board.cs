using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board  {

    private Piece[,] BoardTiles = new Piece[8,8];

    public void Mark(int x, int y, GameObject piece)
    {
        //BoardTiles[x, y] = piece;
    }

    public void UnMark(int x, int y)
    {
        //BoardTiles[x, y] = null;
    }

    public bool IsOccupied (int x, int y)
    {
        return BoardTiles[x, y] != null;
    }

    public bool IsOccupied(Vector2 coords)
    {
        return BoardTiles[(int) coords.x, (int) coords.y] != null;
    }
}
