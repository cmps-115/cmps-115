/*
 * Name: Akobir Khamidov
 * 
 * */
using UnityEngine;
public class Board{

    private Pair[,] squares;

    public Board()
    {
        squares = new Pair[8, 8];

        //initialze to null
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                squares[i, j] = null;
    }

    public void Mark(Vector2 position, Piece piece)
    {
		if (IsOccupied (position)) 
		{
			squares [(int)position.x, (int)position.y].SetPosition (position);
			squares [(int)position.x, (int)position.y].setPiece (piece);
		}
		else
			squares[(int)position.x, (int)position.y] = new Pair(position, piece);
    }

    public void UnMark(Vector2 position)
    {
		
		squares[(int)position.x, (int)position.y] = null;
    }

    public bool IsOccupied (Vector2 position)
    {
		return squares[(int)position.x, (int)position.y] != null;
    }
	public bool IsOccupied (int x, int y)
	{
		return squares[x, y] != null;
	}
	public Piece GetPieceAt(Vector2 pos)
	{
		if (IsOccupied (pos))
			return squares [(int)pos.x, (int)pos.y].GetPiece ();
		else
			return null;
	}
	public Piece GetPieceAt(int x, int y)
	{
		Vector2 pos = new Vector2 (x, y);
		return GetPieceAt (pos);
	}


   /* public bool IsOccupied(Vector2Int coords)
    {
        return squares[coords] != null; 
    }
   */
}
