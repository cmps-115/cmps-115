//Piece.cs
//Writen by: Austin Harmon
//A piece class that defines the atributes of a generic chess piece
//Each type of piece is a sub class of the generic piece
//Leagal move check functions are enclosed in each subclass


using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using ChessGlobals;



public class Piece
{
    private bool taken;//represents if the piece still exists on the board
    private Vector2 PiecePosition;
    private bool team; //false for black ture for white
	private ChessGlobals.Teams currentTeam;
   
	public Piece()
	{
		taken = false;
	}
	public Piece(bool p_team)
	{
		taken = false;
		team = p_team;
	}
    public Piece(bool p_team, int xCord, int yCord)//Constructor for piece with a specific position
    {
		taken = false;
		team = p_team;
        PiecePosition = new Vector2( xCord, yCord);
    }
	public virtual List<Vector2> LegalMoves(Board chessBoard)
	{
		//if a call ever returns null then its the base class LegalMoves
		return null;
	}
    //*****Accessors*****
    //returns cureent position on board of the given piece
    public Vector2 GetPiecePosition()
    {
        return PiecePosition;  
    }
	//With GetXCoord and GetYCoord we will have the casting take place in one spot
	public int GetPiecePositionX()
	{
		return (int)PiecePosition.x;
	}
	public int GetPiecePositionY()
	{
		return (int)PiecePosition.y;
	}

    public bool GetTeam()
    {
        return team;
    }

    //*****manipulators*****
    public void TakePiece()
    {
       PiecePosition = new Vector2(-1, -1);
       taken = true;
    }

    public void SetPosition(int xCord, int yCord)
    {
        PiecePosition = new Vector2(xCord, yCord);
    }
	public void SetPosition(Vector2 pos)
	{
		PiecePosition = pos;
	}
	public void SetTeam(bool team)
	{
		this.team = team;
	}
	public bool IsTaken()
	{
		return taken != false;
	}

}


public class King : Piece
{
	public King(){}
	public King(bool p_team)
		: base(p_team){}
    public King(bool p_team, int xCord, int yCord) 
        : base(p_team,xCord,yCord){}

    public override List<Vector2> LegalMoves(Board chessBoard)
    {
        List<Vector2> positions = new List<Vector2>();



		int xCord = GetPiecePositionX();
		int yCord = GetPiecePositionY();


        //Checks availability of the 3 posible squares to move to +1 on the X-axis of the board
        if (xCord + 1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
        {
            if (yCord + 1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (xCord + 1, yCord + 1) == false) 
				{
					positions.Add (new Vector2 (xCord + 1, yCord + 1));
				} 
				else 
				{
					if (chessBoard.GetPieceAt (xCord + 1, yCord + 1).GetTeam () != GetTeam ())
						positions.Add (new Vector2 (xCord + 1, yCord + 1));
				}
            }

            if (yCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
				if (chessBoard.IsOccupied (xCord + 1, yCord - 1) == false) 
				{
					positions.Add (new Vector2 (xCord + 1, yCord - 1));
				} 
				else 
				{
					if (chessBoard.GetPieceAt (xCord + 1, yCord - 1).GetTeam () != GetTeam ())
						positions.Add (new Vector2 (xCord + 1, yCord - 1));
				}
				
            }
			if (chessBoard.IsOccupied (xCord + 1, yCord) == false) 
			{
				positions.Add (new Vector2 (xCord + 1, yCord));
			} 
			else 
			{
				if (chessBoard.GetPieceAt (xCord + 1, yCord).GetTeam () != GetTeam ())
					positions.Add (new Vector2 (xCord + 1, yCord));
			}


        }

        //Checks the same things as the previous block, but -1 on the X-axis 
        if (xCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
        {
            if (yCord + 1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (xCord - 1, yCord + 1) == false) 
				{
					positions.Add (new Vector2 (xCord - 1, yCord + 1));
				} 
				else 
				{
					if (chessBoard.GetPieceAt (xCord - 1, yCord + 1).GetTeam () != GetTeam ())
						positions.Add (new Vector2 (xCord - 1, yCord + 1));
				}
            }

            if (yCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {

				if (chessBoard.IsOccupied (xCord - 1, yCord - 1) == false) 
				{
					positions.Add (new Vector2 (xCord - 1, yCord - 1));
				} 
				else 
				{
					if (chessBoard.GetPieceAt (xCord - 1, yCord - 1).GetTeam () != GetTeam ())
						positions.Add (new Vector2 (xCord - 1, yCord - 1));
				}
            }

			if (chessBoard.IsOccupied (xCord - 1, yCord) == false) 
			{
				positions.Add (new Vector2 (xCord - 1, yCord));
			} 
			else 
			{
				if (chessBoard.GetPieceAt (xCord - 1, yCord).GetTeam () != GetTeam ())
					positions.Add (new Vector2 (xCord - 1, yCord));
			}
			
        }

        //Checks only the square +1 on the Y-axis from the kings posision
        if (yCord + 1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
        {
			if (chessBoard.IsOccupied (xCord, yCord + 1) == false) 
			{
				positions.Add (new Vector2 (xCord, yCord + 1));
			} 
			else 
			{
				if (chessBoard.GetPieceAt (xCord, yCord + 1).GetTeam () != GetTeam ())
					positions.Add (new Vector2 (xCord, yCord + 1));
			}
        }

        //Same as previous block, but -1 on the Y-axis from the kings position
       if (yCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
        {
			if (chessBoard.IsOccupied (xCord, yCord - 1) == false) 
			{
				positions.Add (new Vector2 (xCord, yCord - 1));
			} 
			else 
			{
				if (chessBoard.GetPieceAt (xCord, yCord - 1).GetTeam () != GetTeam ())
					positions.Add (new Vector2 (xCord, yCord - 1));
			}
        }

        return positions;
    }
}

public class Knight : Piece
{
	public Knight(){}
	public Knight(bool p_team)
		: base(p_team){}
    public Knight(bool p_team, int xCord, int yCord)
        : base(p_team, xCord, yCord){}

	public override List<Vector2> LegalMoves(Board chessBoard)
    {
        List<Vector2> positions = new List<Vector2>();



		int xCord = GetPiecePositionX();
		int yCord = GetPiecePositionY();

        //Checks the 2 moves in the positive x direction
        //Does not record the position if the square is ocupied by another piece of the same team
        if (xCord + 2 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
        {
            if (yCord + 1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (xCord + 2, yCord + 1) == false) 
				{
					positions.Add (new Vector2 (xCord + 2, yCord + 1));
				} 
				else 
				{
					if (chessBoard.GetPieceAt (xCord + 2, yCord + 1).GetTeam () != GetTeam ())
						positions.Add (new Vector2 (xCord + 2, yCord + 1));
				}
            }

            if (yCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
				if (chessBoard.IsOccupied (xCord + 2, yCord - 1) == false) 
				{
					positions.Add (new Vector2 (xCord + 2, yCord - 1));
				} 
				else 
				{
					if (chessBoard.GetPieceAt (xCord + 2, yCord - 1).GetTeam () != GetTeam ())
						positions.Add (new Vector2 (xCord + 2, yCord - 1));
				}
            }
        }

        //Same function as the previous loop in different direction
        if (xCord - 2 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
        {
            if (yCord + 1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (xCord - 2, yCord + 1) == false) 
				{
					positions.Add (new Vector2 (xCord - 2, yCord + 1));
				} 
				else 
				{
					if (chessBoard.GetPieceAt (xCord - 2, yCord + 1).GetTeam () != GetTeam ())
						positions.Add (new Vector2 (xCord - 2, yCord + 1));
				}
            }

            if (yCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
				if (chessBoard.IsOccupied (xCord - 2, yCord - 1) == false) 
				{
					positions.Add (new Vector2 (xCord - 2, yCord - 1));
				} 
				else 
				{
					if (chessBoard.GetPieceAt (xCord - 2, yCord - 1).GetTeam () != GetTeam ())
						positions.Add (new Vector2 (xCord - 2, yCord - 1));
				}
            }

        }

        //Same function as the previous loop in different direction
        if (yCord + 2 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
        {
            if (xCord + 1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (xCord + 1, yCord + 2) == false) 
				{
					positions.Add (new Vector2 (xCord + 1, yCord + 2));
				} 
				else 
				{
					if (chessBoard.GetPieceAt (xCord + 1, yCord + 2).GetTeam () != GetTeam ())
						positions.Add (new Vector2 (xCord + 1, yCord + 2));
				}
            }

            if (xCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
				if (chessBoard.IsOccupied (xCord - 1, yCord + 2) == false) 
				{
					positions.Add (new Vector2 (xCord - 1, yCord + 2));
				} 
				else 
				{
					if (chessBoard.GetPieceAt (xCord - 1, yCord + 2).GetTeam () != GetTeam ())
						positions.Add (new Vector2 (xCord - 1, yCord + 2));
				}
            }
        }

        //Same function as the previous loop in different direction
        if (yCord - 2 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
        {
            if (xCord + 1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (xCord + 1, yCord - 2) == false) 
				{
					positions.Add (new Vector2 (xCord + 1, yCord - 2));
				} 
				else 
				{
					if (chessBoard.GetPieceAt (xCord + 1, yCord - 2).GetTeam () != GetTeam ())
						positions.Add (new Vector2 (xCord + 1, yCord - 2));
				}
            }

			if (chessBoard.IsOccupied (xCord - 1, yCord - 2) == false) 
			{
				positions.Add (new Vector2 (xCord - 1, yCord - 2));
			} 
			else 
			{
				if (chessBoard.GetPieceAt (xCord - 1, yCord + 2).GetTeam () != GetTeam ())
					positions.Add (new Vector2 (xCord - 1, yCord + 2));
			}

        }
        return positions;
    }
}


public class Queen : Piece
{
	public Queen(){}
	public Queen(bool p_team)
		: base(p_team){}
    public Queen(bool p_team, int xCord, int yCord)
        : base (p_team, xCord, yCord)
    {}
	public override List<Vector2> LegalMoves(Board chessBoard)
    {
        Rook tempRook = new Rook(GetTeam(), GetPiecePositionX(), GetPiecePositionX());
        Bishop tempBishop = new Bishop(GetTeam(), GetPiecePositionX(), GetPiecePositionY());
        List<Vector2> positions = new List<Vector2>();

        List<Vector2> rookPositions = tempRook.LegalMoves(chessBoard);
        List<Vector2> bishopPositions = tempBishop.LegalMoves(chessBoard);

        positions.AddRange(rookPositions);
        positions.AddRange(bishopPositions);

        return positions;
    }
}

public class Pawn : Piece
{
	public Pawn(){}
	public Pawn(bool p_team)
		: base(p_team){}
    public Pawn(bool p_team, int xCord, int yCord)
        : base (p_team, xCord, yCord)
    {}

	public override List<Vector2> LegalMoves(Board chessBoard)
    {
        bool team = GetTeam();
        List<Vector2> positions = new List<Vector2>();

 
		int xCord = GetPiecePositionX();
		int yCord = GetPiecePositionY();

        //assuming for the board that ChessGlobals.BoardConstants.BOARD_MINIMUM = free , 1 = black, = 2 = white

        //assuming that white starts at the rows ChessGlobals.BoardConstants.BOARD_MINIMUM,1
        if (team == ChessGlobals.COLOR.WHITE)//Where are you getting this from Austin?
        {

            //to make sure it doesnt go past 
            if (yCord < ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (xCord, yCord + 1) == false) 
				{
					positions.Add (new Vector2 (xCord, yCord + 1));
				} 
				else 
				{
					if (chessBoard.GetPieceAt (xCord, yCord + 1).GetTeam () != GetTeam ())
						positions.Add (new Vector2 (xCord, yCord + 1));
				}
            }

            //Checks if the piece is on the second row
            if (yCord == 1)
            {
				if (chessBoard.IsOccupied (xCord, yCord + 2) == false) 
				{
					positions.Add (new Vector2 (xCord, yCord + 2));
				} 
				else 
				{
					if (chessBoard.GetPieceAt (xCord, yCord + 2).GetTeam () != GetTeam ())
						positions.Add (new Vector2 (xCord, yCord + 2));
				}
            }

            //diagonal to left
            if (xCord > ChessGlobals.BoardConstants.BOARD_MINIMUM && yCord < ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (xCord - 1, yCord + 1)) 
				{
					if (chessBoard.GetPieceAt (xCord - 1, yCord + 1).GetTeam () == ChessGlobals.COLOR.BLACK)
						positions.Add (new Vector2 (xCord - 1, yCord + 1));
				} 
            }

            //diagonal to right
            if (xCord < ChessGlobals.BoardConstants.BOARD_MAXIMUM && yCord < ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (xCord + 1, yCord + 1)) 
				{
					if (chessBoard.GetPieceAt (xCord + 1, yCord + 1).GetTeam () == ChessGlobals.COLOR.BLACK)
						positions.Add (new Vector2 (xCord + 1, yCord + 1));
				} 
            }

        }
        else
        {
            //to make sure it doesnt go past ChessGlobals.BoardConstants.BOARD_MINIMUM
            if (yCord > ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
				if (chessBoard.IsOccupied (xCord, yCord - 1) == false) 
				{
					positions.Add (new Vector2 (xCord, yCord - 1));
				} 
				else 
				{
					if (chessBoard.GetPieceAt (xCord, yCord - 1).GetTeam () == ChessGlobals.COLOR.WHITE)
						positions.Add (new Vector2 (xCord, yCord - 1));
				}
            }

            //Checks if the pawn in on its starting row
            if (yCord == 6)
            {
				if (yCord == 6 && chessBoard.IsOccupied(xCord, yCord - 2) == false) 
				{
					positions.Add (new Vector2 (xCord, yCord - 2));
				} 
				else 
				{
					if (yCord == 6 && chessBoard.GetPieceAt(xCord, yCord - 2).GetTeam() == ChessGlobals.COLOR.WHITE)
						positions.Add (new Vector2 (xCord, yCord - 2));
				}
            }

            //diagonal to left
            if (xCord < ChessGlobals.BoardConstants.BOARD_MAXIMUM && yCord > ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
				if (chessBoard.IsOccupied (xCord + 1, yCord - 1))  
				{
					if(chessBoard.GetPieceAt(xCord + 1, yCord - 1).GetTeam() == ChessGlobals.COLOR.WHITE)
						positions.Add (new Vector2 (xCord + 1, yCord - 1));
				} 
            }


            //diagonal to right
            if (xCord > ChessGlobals.BoardConstants.BOARD_MINIMUM && yCord > ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
				if (chessBoard.IsOccupied (xCord - 1, yCord - 1)) // not sure about this one Austin
				{
					if(chessBoard.GetPieceAt(xCord - 1, yCord - 1).GetTeam() == ChessGlobals.COLOR.WHITE)
						positions.Add (new Vector2 (xCord - 1, yCord - 1));
				} 

            }
        }
        return positions;
    }
}


public class Rook : Piece
{
	public Rook(){}
	public Rook(bool p_team)
		: base(p_team){}
    public  Rook(bool p_team, int xCord, int yCord)
        : base (p_team,xCord,yCord)
    {}

	public override List<Vector2> LegalMoves(Board chessBoard)
    {
        List<Vector2> positions = new List<Vector2>();

        //bool team = GetTeam();
        int xCord, yCord;
           
		xCord = GetPiecePositionX();
		yCord = GetPiecePositionY();

        // if its a white piece
        if (GetTeam() == ChessGlobals.COLOR.WHITE)
        {

            // check for moves in front
            int i1 = yCord + 1;
            // if its empty, it can move there
            while (i1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM && !chessBoard.IsOccupied(new Vector2(xCord, i1)))
            {
                positions.Add(new Vector2(xCord, i1));
                i1++;
            }
            // if the next is occupied by a black piece
            if (i1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (xCord, i1))  
				{
					if(chessBoard.GetPieceAt(xCord, i1).GetTeam() != GetTeam())
						positions.Add (new Vector2 (xCord, i1));
				} 
            }
            // check for moves in back
            i1 = yCord - 1;
            // if its empty, it can move there
            while (i1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM && !chessBoard.IsOccupied(new Vector2(xCord, i1)))
            {
                positions.Add(new Vector2(xCord, i1));
                i1--;
            }
            // if the prev is occupied by a black piece
            if (i1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
				if (chessBoard.IsOccupied (xCord, i1)) // not sure about this one Austin
				{
					if(chessBoard.GetPieceAt(xCord, i1).GetTeam() != GetTeam())
						positions.Add (new Vector2 (xCord, i1));
				} 
            }


            // check for moves to the right
            i1 = xCord + 1;
            // if its empty, it can move there
            while (i1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM && !chessBoard.IsOccupied(new Vector2(i1, yCord)))
            {
                positions.Add(new Vector2(i1, yCord));
                i1++;
            }
            // if the next is occupied by a black piece
            if (i1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (i1, yCord)) // not sure about this one Austin
				{
					if(chessBoard.GetPieceAt(i1, yCord).GetTeam() != GetTeam())
						positions.Add (new Vector2 (i1, yCord));
				} 
            }

            // check for moves to the left
            i1 = xCord - 1;
            // if its empty, it can move there
            while (i1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM && !chessBoard.IsOccupied(new Vector2(i1, yCord)))
            {
                positions.Add(new Vector2(i1, yCord));
                i1--;
            }
            // if the prev is occupied by a black piece
            if (i1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
				if (chessBoard.IsOccupied (i1, yCord)) // not sure about this one Austin
				{
					if(chessBoard.GetPieceAt(i1, yCord).GetTeam() != GetTeam())
						positions.Add (new Vector2 (i1, yCord));
				} 
            }

        }
        else
        {

            // check for moves in back
            int i1 = yCord + 1;
            // if its empty, it can move there
            while (i1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM && !chessBoard.IsOccupied(new Vector2(xCord, i1)))
            {
                positions.Add(new Vector2(xCord, i1));
                i1++;
            }
            // if the next is occupied by a white piece
            if (i1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (xCord, yCord - 1)) // not sure about this one Austin
				{
					if(chessBoard.GetPieceAt(xCord, yCord - 1).GetTeam() != GetTeam())
						positions.Add (new Vector2 (xCord, yCord - 1));
				} 
            }
            // check for moves in front
            i1 = yCord - 1;
            // if its empty, it can move there
            while (i1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM && !chessBoard.IsOccupied(new Vector2(xCord, i1)))
            {
                positions.Add(new Vector2(xCord, i1));
                i1--;
            }
            // if the prev is occupied by a white piece
            if (i1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
				if (chessBoard.IsOccupied (xCord, i1)) // not sure about this one Austin
				{
					if(chessBoard.GetPieceAt(xCord, i1).GetTeam() != GetTeam())
						positions.Add (new Vector2 (xCord, i1));
				} 
            }


            // check for moves to the left
            i1 = xCord + 1;
            // if its empty, it can move there
            while (i1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM && !chessBoard.IsOccupied(new Vector2(i1, yCord)))
            {
                positions.Add(new Vector2(i1, yCord));
                i1++;
            }
            // if the next is occupied by a black piece
            if (i1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (i1, yCord)) // not sure about this one Austin
				{
					if(chessBoard.GetPieceAt(i1, yCord).GetTeam() != GetTeam())
						positions.Add (new Vector2 (i1, yCord));
				} 
            }

            // check for moves to the right
            i1 = xCord - 1;
            // if its empty, it can move there
            while (i1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM && !chessBoard.IsOccupied(new Vector2(i1, yCord)))
            {
                positions.Add(new Vector2(i1, yCord));
                i1--;
            }
            // if the prev is occupied by a black piece
            if (i1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
				if (chessBoard.IsOccupied (i1, yCord))  
				{
					if(chessBoard.GetPieceAt(i1, yCord).GetTeam() != GetTeam())
						positions.Add (new Vector2 (i1, yCord));
				} 
            }
        }
        return positions;
    }
}

public class Bishop : Piece
{
	public Bishop(){}
	public Bishop(bool p_team)
		: base(p_team){}
    public Bishop(bool p_team, int xCord, int yCord)
        :base(p_team,xCord,yCord)
    {}

	public override List<Vector2> LegalMoves(Board chessBoard)
    {
        
        int xc, yc;
        List<Vector2> positions = new List<Vector2>();



		xc = GetPiecePositionX();
		yc = GetPiecePositionY();

        // if its a white piece
       // if (GetTeam() == ChessGlobals.COLOR.WHITE)
       // {

            // check for moves in diagonally to the right and up
            int i1 = yc + 1;
            int i2 = xc + 1;
            // if its empty, it can move there
            while (i1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM && i2 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM && !chessBoard.IsOccupied(new Vector2(i2, i1)))
            {
                positions.Add(new Vector2(i2, i1));
                i1++;
                i2++;
            }
            // if the next is occupied by a black piece
            if (i1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM && i2 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (i2, i1))  
				{
                if (chessBoard.GetPieceAt(i2, i1).GetTeam() != GetTeam())
						positions.Add (new Vector2 (i2, i1));
				} 
            }
            // check for moves in down left
            i1 = yc - 1;
            i2 = xc - 1;
            // if its empty, it can move there
            while (i1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM && i2 >= ChessGlobals.BoardConstants.BOARD_MINIMUM && !chessBoard.IsOccupied(new Vector2(i2, i1)))
            {
                positions.Add(new Vector2(i2, i1));
                i1--;
                i2--;
            }
            // if the prev is occupied by a black piece
            if (i1 > ChessGlobals.BoardConstants.BOARD_MINIMUM && i2 > ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
				if (chessBoard.IsOccupied (i2, i1)) // not sure about this one Austin
				{
					if(chessBoard.GetPieceAt(i2, i1).GetTeam() != GetTeam())
						positions.Add (new Vector2 (i2, i1));
				} 
            }


            // check for moves to the right and down
            i1 = xc + 1;
            i2 = yc - 1;
            // if its empty, it can move there
            while (i1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM && i2 >= ChessGlobals.BoardConstants.BOARD_MINIMUM && !chessBoard.IsOccupied(new Vector2(i1, i2)))
            {
                positions.Add(new Vector2(i1, i2));
                i1++;
                i2--;
            }
            // if the next is occupied by a black piece
            if (i1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM && i2 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
				if (chessBoard.IsOccupied (i1, i2)) // not sure about this one Austin
				{
					if(chessBoard.GetPieceAt(i1, i2).GetTeam() != GetTeam())
						positions.Add (new Vector2 (i1, i2));
				} 
            }

            // check for moves to the up left
            i1 = xc - 1;
            i2 = yc + 1;

            // if its empty, it can move there
            while (i1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM && i2 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM && !chessBoard.IsOccupied(new Vector2(i1, i2)))
            {
                positions.Add(new Vector2(i1, i2));
                i1--;
                i2++;
            }
            // if the prev is occupied by a black piece
            if (i1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM && i2 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (i1, i2)) // not sure about this one Austin
				{
					if(chessBoard.GetPieceAt(i1, i2).GetTeam() != GetTeam())
						positions.Add (new Vector2 (i1, i2));
				} 
            }

        //}
        /*
        else
        {

            // check for moves in diagonally to the left and down 
            int i1 = yc + 1;
            int i2 = xc + 1;
            // if its empty, it can move there
            while (i1 < 8 && i2 < 8 && !chessBoard.IsOccupied(new Vector2(i2, i1)))
            {
                positions.Add(new Vector2(i2, i1));
                i1++;
                i2++;
            }
            // if the next is occupied by a white piece
            if (i1 < ChessGlobals.BoardConstants.BOARD_MAXIMUM && i2 < ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
				if (chessBoard.IsOccupied (i2, i1)) // not sure about this one Austin
				{
					if(chessBoard.GetPieceAt(i2, i1).GetTeam() != GetTeam())
						positions.Add (new Vector2 (i2, i1));
				} 
            }
            // check for moves in up right
            i1 = yc - 1;
            i2 = xc - 1;
            // if its empty, it can move there
            while (i1 > ChessGlobals.BoardConstants.BOARD_MINIMUM && i2 > ChessGlobals.BoardConstants.BOARD_MINIMUM && !chessBoard.IsOccupied(new Vector2(i2, i1)))
            {
                positions.Add(new Vector2(i2, i1));
                i1--;
                i2--;
            }
            // if the prev is occupied by a white piece
            if (i1 > -1 && i2 > -1)
            {
				if (chessBoard.IsOccupied (i2, i1)) // not sure about this one Austin
				{
					if(chessBoard.GetPieceAt(i2, i1).GetTeam() != GetTeam())
						positions.Add (new Vector2 (i2, i1));
				} 
            }


            // check for moves to the left and up
            i1 = xc + 1;
            i2 = yc - 1;
            // if its empty, it can move there
            while (i1 < 8 && i2 > ChessGlobals.BoardConstants.BOARD_MINIMUM && !chessBoard.IsOccupied(new Vector2(i1, i2)))
            {
                positions.Add(new Vector2(i1, i2));
                i1++;
                i2--;

            }
            // if the next is occupied by a white piece
            if (i1 < ChessGlobals.BoardConstants.BOARD_MAXIMUM && i2 > -1)
            {
				if (chessBoard.IsOccupied (i1, i2)) // not sure about this one Austin
				{
					if(chessBoard.GetPieceAt(i1, i2).GetTeam() != GetTeam())
						positions.Add (new Vector2 (i1, i2));
				} 
            }

            // check for moves to the right up
            i1 = xc - 1;
            i2 = yc - 1;
            // if its empty, it can move there
            while (i1 > ChessGlobals.BoardConstants.BOARD_MINIMUM && i2 > ChessGlobals.BoardConstants.BOARD_MINIMUM && !chessBoard.IsOccupied(new Vector2(i1, i2)))
            {
                positions.Add(new Vector2(i1, i2));
                i1--;
                i2--;
            }
            // if the prev is occupied by a white piece
            if (i1 > -1 && i2 > -1)
            {
				if (chessBoard.IsOccupied (i1, i2)) // not sure about this one Austin
				{
					if(chessBoard.GetPieceAt(i1, i2).GetTeam() != GetTeam())
						positions.Add (new Vector2 (i1, i2));
				} 
            }
        }
        */
        return positions;
    }
}





