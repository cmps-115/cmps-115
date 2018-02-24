/*
 * Name: Akobir Khamidov & Arom Zinhart DeGraca
 * =
 * */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;

using ChessGlobals;

[Serializable]
public class Board
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
        if (piece == null)
        {
            Debug.Log("tried to mark null");
            return;
        }

        int row = (int)position.x;
		int col = (int)position.y;
        if (position.x >= BoardConstants.BOARD_MINIMUM && position.y >= BoardConstants.BOARD_MINIMUM && position.x <= BoardConstants.BOARD_MAXIMUM && position.y <= BoardConstants.BOARD_MAXIMUM)
        {
            if (IsOccupied(position))
            {
                piece.SetPosition(position);
                squares[row, col].SetPosition(position);
                squares[row, col].setPiece(piece);
                //squares[row, col].setOccupied(BoardConstants.OCCUPIED_SQUARE);
            }
            else
            {
                piece.SetPosition(position);
                squares[row, col] = new Square(position, piece, BoardConstants.OCCUPIED_SQUARE);
            }
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
        if (x >= BoardConstants.BOARD_MINIMUM && y >= BoardConstants.BOARD_MINIMUM && x <= BoardConstants.BOARD_MAXIMUM && y <= BoardConstants.BOARD_MAXIMUM)
        {
            squares[x, y].setPiece(null);
            squares[x, y].setOccupied(false);
        }
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
        if (x >= BoardConstants.BOARD_MINIMUM && y >= BoardConstants.BOARD_MINIMUM && x <= BoardConstants.BOARD_MAXIMUM && y <= BoardConstants.BOARD_MAXIMUM)
            return squares[x, y].isSquareOccupied();
        else return false;
	}

	public bool IsOccupied (Vector2 position)
    {
        if (position.x >= BoardConstants.BOARD_MINIMUM && position.y >= BoardConstants.BOARD_MINIMUM && position.x <= BoardConstants.BOARD_MAXIMUM && position.y <= BoardConstants.BOARD_MAXIMUM)
            return IsOccupied((int)position.x, (int)position.y);
        else return false;
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

    public Board Clone()
    {
        return DeepCopy.Copy(this) as Board;
    }

	public override string ToString()
	{
        string b = "";
        for (int i = BoardConstants.BOARD_MINIMUM; i <= BoardConstants.BOARD_MAXIMUM; ++i)
        {
            for (int j = BoardConstants.BOARD_MINIMUM; j <= BoardConstants.BOARD_MAXIMUM; ++j)
            {
                b += GetPieceAt(j, i) != null ? GetPieceAt(j, i).ToString() : "empty";
                b += " at: " + j + " " + i + "\n";
            }
        }
        return b;
	}
}

