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
	private Square[,] squares;
	public Board()
    {
		int rows = BoardConstants.BOARD_MAXIMUM + 1;
		int cols = BoardConstants.BOARD_MAXIMUM + 1;
		squares = new Square[rows, cols];
        //initialze to null
		for (int row = BoardConstants.BOARD_MINIMUM; row <= BoardConstants.BOARD_MAXIMUM; ++row)
			for (int col = BoardConstants.BOARD_MINIMUM; col <= BoardConstants.BOARD_MAXIMUM; ++col)
				squares[row,col] = new Square();
	}
	//all other mark overloads are forwarded here
	public void Mark(Vector2 position, Piece piece)
	{
		int row = (int)position.x;
		int col = (int)position.y;
		if (IsOccupied (position)) 
		{
			squares [row, col].SetPosition (position);
			squares [row, col].setPiece (piece);
			squares [row, col].setOccupied(BoardConstants.OCCUPIED_SQUARE);
		} 
		else 
		{
			piece.SetPosition (position);
			squares [row, col] = new Square(position, piece, BoardConstants.OCCUPIED_SQUARE);
		}
	}

	public void Mark(Move move)
	{
		Mark (move.des, move.piece);
	}

	public void Mark(int x, int y, Piece piece)
	{
		Mark (new Vector2 (x, y), piece);
	}

	public void UnMark(Move move)
	{
		UnMark (move.des);
		Mark (move.src, move.piece);
	}

	public void UnMark(Vector2 position)
	{
		UnMark((int)position.x, (int)position.y);
	}

	public void UnMark(int x, int y)
	{
		squares [x, y].setPiece (null);
		squares [x, y].setOccupied (false);
	}

	public void Clear()
	{
		for (int row = BoardConstants.BOARD_MINIMUM; row <= BoardConstants.BOARD_MAXIMUM; ++row)
			for (int col = BoardConstants.BOARD_MINIMUM; col <= BoardConstants.BOARD_MAXIMUM; ++col)
				UnMark (row, col);
	}
	//all other overloads forwarded here
	public bool IsOccupied (int x, int y)
	{
		return squares[x,y].isSquareOccupied();
	}

	public bool IsOccupied (Vector2 position)
    {
		return IsOccupied ((int)position.x, (int)position.y);
	}

	public Piece GetPieceAt(Vector2 pos)
	{
		if (IsOccupied (pos)) 
		{	
			return squares [(int)pos.x, (int)pos.y].GetPiece ();
		} 
		else 
		{
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

	public override string ToString()
	{
		/*string debug = null;
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
		}*/
		return "";
	}
}
