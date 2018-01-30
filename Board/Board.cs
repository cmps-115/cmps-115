/*
 * Name: Akobir Khamidov
 * 
 * */

public class Board{

    private Pair[][] squares;

    public Board()
    {
        squares = new Pair[8, 8];
    }

    public void Mark(Postion square, Piece piece)
    {
        squares[square.GetX()][sqaure.GetY()] = new Pair(sqaure, piece);
    }

    public void UnMark(Position position)
    {
        squares[square.GetX()][sqaure.GetY()] = null;
    }

    public bool IsOccupied (Position square)
    {
        return squares[square.GetX()][sqaure.GetY()] != null;
    }

    public bool IsOccupied(Vector2Int coords)
    {
        return squares[coords] != null; 
    }

}
