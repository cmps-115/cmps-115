/*
 * Name: Arom Zinhart DeGraca
 * */
using UnityEngine;
using ChessGlobals;
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
		bool occupied = false;
		bool blackThreat = false;
		bool whiteThreat = false;
		square = null;
	}
	public Square(Vector2 pos, Piece piece)
	{
		square = new Tuple2<Vector2,Piece> (pos, piece);
	}
	public Square(int x, int y, Piece piece)
	{
		square = new Square (new Vector2 (x, y), piece).getData();
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
		this.square.t2 = piece;
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
	public void getWhiteThreat()
	{
		return this.whiteThreat;
	}
	public void getBlackThreat()
	{
		return this.blackThreat;
	}

}

