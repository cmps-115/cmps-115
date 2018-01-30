/*
 * Name: Akobir Khamidov
 * 
 * */

public class Board{

    private Pair[][] squares;

    public Board()
    {
        squares = new Pair[8, 8];

        //initialze to null
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                sqaures[i][j] = null;
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
