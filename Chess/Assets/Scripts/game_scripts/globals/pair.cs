/*
 * Name: Akobir Khamidov
 * 
 * */
using UnityEngine;
public class Pair
{
    private Vector2 pos;
    private Piece pie;

	public Pair(Vector2 pos, Piece p)
	{
        this.pos = pos;
        this.pie = pie;
	}

    public void SetPosition(Vector2 pos)
    {
        this.pos = pos;
    }

    public void setPiece(Piece p)
    {
        this.pie = p;
    }

    public Vector2 GetPosition()
    {
        return pos;
    }

    public Piece GetPiece()
    {
        return pie;
    }
}
