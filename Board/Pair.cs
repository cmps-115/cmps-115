/*
 * Name: Akobir Khamidov
 * 
 * */

public class Pair
{
    private Position pos;
    private Piece pie;

	public Pair(Position pos, Piece p)
	{
        this.pos = pos;
        this.pie = p;
	}

    public void SetPosition(Position pos)
    {
        this.pos = pos;
    }

    public void setPiece(Piece p)
    {
        this.pie = p;
    }

    public Position GetPosition()
    {
        return pos;
    }

    public Piece GetPiece()
    {
        return pie;
    }
}
