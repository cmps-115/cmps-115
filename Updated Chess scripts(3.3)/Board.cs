/*
 * Name: Akobir Khamidov & Arom Zinhart DeGraca
 * */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;
using System.Collections.Generic;
using ChessGlobals;



[Serializable]
public class Board
{
	private Square[,] squares;
    private List<Piece> capturedPieces;
    private List<Piece> activePieces;

	public Board()
    {
		int rows = BoardConstants.BOARD_MAXIMUM + 1;
		int cols = BoardConstants.BOARD_MAXIMUM + 1;
		squares = new Square[rows, cols];
        capturedPieces = new List<Piece>();
        activePieces = new List<Piece>();
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

    public Square GetSquare(int xCord, int yCord)
    {
        return squares[xCord, yCord];
    }

    public Square GetSquare(Vector2 position)
    {
		return GetSquare ((int)position.x, (int)position.y);
    }
            
    public Board Clone()
    {
        return DeepCopy.Copy(this) as Board;
    }

    public List<Piece> GetActivePieces()
    {
        return activePieces;
    }

    public List<Piece> GetInactivePieces()
    {
        return capturedPieces;
    }

    public void AddActivePiece(Piece newPiece)
    {
        activePieces.Add(newPiece);
    }

    public void CaptureActivePiece(Piece takenPiece)
    {
        activePieces.Remove(takenPiece);
        capturedPieces.Add(takenPiece);
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

    public void UpdateBoardThreat(Piece testPiece, Vector2 testMove)
    {
        List<Piece> activePieces = new List<Piece>();
        activePieces.AddRange(GetActivePieces());
        Debug.Log("updateBoard Called");
        Vector2 originalPosition = Vector2.down;
        Piece tempRemovedPiece = new Piece();


        if (testPiece != null)
        {
            Debug.Log("Piece position is updated");
            originalPosition = testPiece.GetPiecePosition();

           
            if (IsOccupied(testMove))
            {
                Debug.Log("Square is ocupied");
                Debug.Log("capacity before remove section" + activePieces.Capacity);
                tempRemovedPiece = GetPieceAt(testMove);
                activePieces.Remove(tempRemovedPiece);
                Debug.Log("capacity after remove section" + activePieces.Capacity);
            }

            testPiece.SetPosition(testMove);
            Mark(testMove, testPiece);
            UnMark(originalPosition);


                      
            Debug.Log(testPiece.GetPiecePosition());
            Debug.Log(this);
        }

        for (int row = BoardConstants.BOARD_MINIMUM; row <= BoardConstants.BOARD_MAXIMUM; row++)
        {
            for (int column = BoardConstants.BOARD_MINIMUM; column <= BoardConstants.BOARD_MAXIMUM; column++)
            {
                GetSquare(row, column).setBlackThreat(false);
                GetSquare(row, column).setWhiteThreat(false);
            }
        }
        Debug.Log("Reseting check values to false");
        KingInCheck.SetBlackCheck(false);
        KingInCheck.SetWhiteCheck(false);

        foreach (Piece inPlay in activePieces)
        {
           
            foreach (Vector2 moves in inPlay.LegalMoves(this))
            {
                
                if (inPlay.GetTeam() == Teams.BLACK_TEAM)
                {
                    squares[(int)moves.x, (int)moves.y].setBlackThreat(true);
                    //GetSquare(moves).setBlackThreat(true);
                    if(GetSquare(moves).isSquareOccupied())
                    {
                        if (GetSquare(moves).GetPiece().GetType() == typeof(King) && (GetSquare(moves).getBlackThreat() == true))
                        {
                            Debug.Log("Position of king being checked: " + inPlay.GetPiecePosition());
                            Debug.Log("Move that threatens king: " + moves);
                            Debug.Log("Set White check to true in update");
                            KingInCheck.SetWhiteCheck(true);
                        }    
                    }
                }
                else
                {
                    squares[(int)moves.x, (int)moves.y].setWhiteThreat(true);
                    //GetSquare(moves).setWhiteThreat(true);
                    if(GetSquare(moves).isSquareOccupied())
                    {
                        if (GetSquare(moves).GetPiece().GetType() == typeof(King) && (GetSquare(moves).getWhiteThreat() == true))
                        {
                            Debug.Log("Position of king being checked: " + inPlay.GetPiecePosition());
                            Debug.Log("Move that threatens king: " + moves);
                            Debug.Log("Set black Check to true in update");
                            KingInCheck.SetBlackCheck(true);
                        }
                    }
                }
            }
        }
        if(testPiece != null)
        {
            testPiece.SetPosition(originalPosition);
            UnMark(testMove);
            Mark(originalPosition, testPiece);
            if (IsOccupied((int)testMove.x, (int)testMove.y))
            {
                activePieces.Add(tempRemovedPiece);
            }
        }



    }
}

