/*
 * Name: Akobir Khamidov
 * 
 * */
using UnityEngine;
using ChessGlobals;
public class Square
{
	private Tuple2<Vector2,Piece> square;
    //private Vector2 pos;
    //private Piece piece;

	public Square(Vector2 pos, Piece piece)
	{
		square = new Tuple2<Vector2,Piece> (pos, piece);
		//this.square.t1 = pos;
		//this.square.t2 = piece;
        //this.pos = pos;
        //this.piece = piece;
	}

    public void SetPosition(Vector2 pos)
    {
        //this.pos = pos;
		this.square.t1 = pos;
    }

    public void setPiece(Piece piece)
    {
        //this.piece = piece;
		this.square.t2 = piece;
    }

    public Vector2 GetPosition()
    {
		return this.square.t1;
        //return pos;
    }

    public Piece GetPiece()
    {
		return this.square.t2;
        //return piece;
    }
}

