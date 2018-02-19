/*
 * Name: Akobir Khamidov & Arom Zinhart DeGraca
 * =
 * */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;
using ChessGlobals;
public class Board : ICloneable
{
	private const int rows = 8;
    	private const int cols = 8;
    	private Square[,] squares;
	public Board()
    {
		squares = new Square[rows, cols];
        	//initialze to null
		for (int i = 0; i < rows; ++i)
			for (int j = 0; j < cols; ++j)
				UnMark (i, j);
	}
	public void Mark(Move move)
	{
		Mark (move.des, move.piece);
	}
	public void Mark(Vector2 position, Piece piece)
    {
		if (IsOccupied (position)) 
		{
			squares [(int)position.x, (int)position.y].SetPosition (position);
			squares [(int)position.x, (int)position.y].setPiece (piece);
		} 
		else 
		{
			piece.SetPosition (position);
			squares [(int)position.x, (int)position.y] = new Square (position, piece);
		}
	}
	public void Mark(int x, int y, Piece piece)
	{
		Mark (new Vector2 (x, y), piece);
	}
	public void UnMark(Vector2 position)
	{
		UnMark ( (int)position.x, (int)position.y );
	}
	public void UnMark(int x, int y)
	{
		squares[x, y] = null;
	}
	public void Clear()
	{
		for (int i = 0; i < rows; ++i)
			for (int j = 0; j < cols; ++j)
				UnMark (i, j);
	}
	public bool IsOccupied (Vector2 position)
    	{
		return IsOccupied ((int)position.x, (int)position.y);
    	}
	public bool IsOccupied (int x, int y)
	{
		return squares[x, y] != null;
	}
	public Piece GetPieceAt(Vector2 pos)
	{
		MonoBehaviour.print (squares [(int)pos.x, (int)pos.y]);
		if (IsOccupied (pos)) {	
			MonoBehaviour.print ("IN GET PIECE AT\n");
			Assert.AreNotEqual (squares [(int)pos.x, (int)pos.y].GetPiece (), null);
			return squares [(int)pos.x, (int)pos.y].GetPiece ();
		} 
		else 
		{
			MonoBehaviour.print ("IN GET PIECE AT NULL\n");
			return null;
		}
	}
	public Piece GetPieceAt(int x, int y)
	{
		return GetPieceAt (new Vector2 (x, y));
	}
	public object Clone()
	{
		return this.MemberwiseClone();
	}
	public string ToString()
	{
		string debug = null;
		for (int i = 0; i < rows; ++i) 
		{
			for (int j = 0; j < cols; ++j) 
			{
				Piece piece = GetPieceAt(i, j);
				if (IsOccupied(i, j)) 
				{
					if (piece.GetTeam () == false)
						debug = "Team: Black Piece type: " + piece.GetType () + " Position: " + piece.GetPiecePosition();
					else
						debug = "Team: White Piece type: " + piece.GetType () + " Position: " + piece.GetPiecePosition();
					MonoBehaviour.print (debug +" ");// can use get type to determine which kind subclass is piece
					
				}
				else
					MonoBehaviour.print ("Empty Square at: " + new Vector2(i,j)+"\n");
			}
			MonoBehaviour.print ("\n");
		}
		return "";
	}
}
