/*
 * Name: Arom Zinhart DeGraca
 * */
using UnityEngine;
using ChessGlobals;
using System;

[Serializable]
public class Square
{
	private Tuple2<Vector2,Piece> square;
	private bool occupied;
	private bool blackThreat;
	private bool whiteThreat;

	private Tuple2<Vector2,Piece> getData()
	{
		return square;
	}

	public Square()
	{
		occupied = BoardConstants.UNOCCUPIED_SQUARE;
		blackThreat = false;
		whiteThreat = false;
		square = null;
	}

	//most other constructor requests are forwarded here
	public Square(int x, int y, Piece piece)
	{ 
		square = new Tuple2<Vector2,Piece>(new Vector2 (x, y), piece);
		setOccupied(true);
		setBlackThreat (false);
		setWhiteThreat(false);
	}

	public Square(int x, int y, Piece piece, bool occupied)
	{ 
		square = new Tuple2<Vector2,Piece>(new Vector2 (x, y), piece);
		setOccupied(occupied);
		setBlackThreat (false);
		setWhiteThreat(false);
	}

	public Square(Vector2 pos, Piece piece)
		:this((int)pos.x,(int)pos.y, piece)
	{
	}

	public Square(Vector2 pos, Piece piece, bool occupied)
		:this((int)pos.x,(int)pos.y, piece, occupied)
	{	 
	}

    public void SetPosition(Vector2 pos)
    {
		SetPosition((int)pos.x, (int)pos.y);
    }

	public void SetPosition(int x, int y)
	{
		this.square.t1.x = x;
		this.square.t1.y = y;
	}

    public void setPiece(Piece piece)
    {
        if (piece != null)
		    this.square.t2 = piece;
    }
	public void setOccupied(bool occupied)
	{
		this.occupied = occupied;
	}

	public bool isSquareOccupied()
	{
		return occupied;
	}

    public Vector2 GetPosition()
    {
		return this.square.t1;
    }

    public Piece GetPiece()
    {
		return this.square.t2;
    }

	public void setWhiteThreat(bool whiteThreat)
	{
		this.whiteThreat = whiteThreat;
	}

	public void setBlackThreat(bool blackThreat)
	{
		this.blackThreat = blackThreat;
	}

	public bool getWhiteThreat()
	{
		return this.whiteThreat;
	}

	public bool getBlackThreat()
	{
		return this.blackThreat;
	}
}

