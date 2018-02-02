//Rules.cs---to be concatinated with the other rules file to create one to store all the rules
//Contains the legal move checks for the king, queen, and Knight
//currenty only looks for spaces a piece can move to without concideration of check
//*****UNFINISHED*****
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules
{


    //Function for finding all the leagal moves for a Knight piece
    //Checks all 8 possible squares, and only records the squares that arent occupied by a piece of the same team
    static public List<Vector2> LegalKnightMove(Piece P, Board B)
    {
        List<Vector2> poss = new List<Vector2>();

        int xCord = P.PiecePosition.GetX();
        int yCord = P.PiecePosition.GetY();
      
        //Checks the 2 moves in the positive x direction
        //Does not record the position if the square is ocupied by another piece of the same team
        if(xCord + 2 <= 7)
        {
            if(yCord + 1 <= 7)
            {
                if (B.squares[xCord + 2, yCord + 1] == null)
                {
                    poss.Add(new Vector2(xCord + 2, yCord + 1));
                }
                else
                {
                    if (B.squares[xCord + 2, yCord + 1].GetPiece().team != P.team) poss.Add(new Vector2(xCord + 2, yCord + 1));
                }
            }

            if(yCord - 1 >= 0)
            {
                if (B.squares[xCord + 2, yCord - 1] == null)
                {
                    poss.Add(new Vector2(xCord + 2, yCord - 1));
                }
                else
                {
                    if (B.squares[xCord + 2, yCord - 1].GetPiece().team != P.team) poss.Add(new Vector2(xCord + 2, yCord - 1));
                }
            }
        }

        //Same function as the previous loop in different direction
        if (xCord - 2 >= 0)
        {
            if(yCord + 1 <= 7)
            {
                if (B.squares[xCord - 2, yCord + 1] == null)
                {
                    poss.Add(new Vector2(xCord - 2, yCord + 1));
                }
                else
                {
                    if (B.squares[xCord - 2, yCord + 1].GetPiece().team != P.team) poss.Add(new Vector2(xCord - 2, yCord + 1));
                }
            }

            if(yCord - 1 >= 0)
            {
                if (B.squares[xCord - 2, yCord - 1] == null)
                {
                    poss.Add(new Vector2(xCord - 2, yCord - 1));
                }
                else
                {
                    if (B.squares[xCord - 2, yCord - 1].GetPiece().team != P.team) poss.Add(new Vector2(xCord - 2, yCord - 1));
                }
            }
        }

        //Same function as the previous loop in different direction
        if (yCord + 2 <= 7)
        {
            if(xCord + 1 <= 7)
            {
                if (B.squares[xCord + 1, yCord + 2] == null)
                {
                    poss.Add(new Vector2(xCord + 1, yCord + 2));
                }
                else
                {
                    if (B.squares[xCord + 1, yCord + 2].GetPiece().team != P.team) poss.Add(new Vector2(xCord + 1, yCord + 2));
                }
            }
 
            if(xCord - 1 >= 0)
            {
                if (B.squares[xCord - 1, yCord + 2] == null)
                {
                    poss.Add(new Vector2(xCord - 1, yCord + 2));
                }
                else
                {
                    if (B.squares[xCord - 1, yCord - 2].GetPiece().team != P.team) poss.Add(new Vector2(xCord - 1, yCord + 2));
                }
            }
        }

        //Same function as the previous loop in different direction
        if (yCord - 2 >= 0)
        {
            if (xCord + 1 <= 7)
            {
                if (B.squares[xCord + 1, yCord - 2] == null)
                {
                    poss.Add(new Vector2(xCord + 1, yCord - 2));
                }
                else
                {
                    if (B.squares[xCord + 1, yCord - 2].GetPiece().team != P.team) poss.Add(new Vector2(xCord + 1, yCord - 2));
                }
            }

            if (xCord - 1 >= 0)
            {
                if (B.squares[xCord - 1, yCord - 2] == null)
                {
                    poss.Add(new Vector2(xCord - 1, yCord - 2));
                }
                else
                {
                    if (B.squares[xCord - 1, yCord - 2].GetPiece().team != P.team) poss.Add(new Vector2(xCord - 1, yCord - 2));
                }
            }

        }
        return poss;
    }


    //Checks for all valid king moves
    //Same structure as knight and explicitly searches each of the 8 possible spaces
    static public List<Vector2> LegalKingMove(Piece P, Board B)
    {
        List<Vector2> poss = new List<Vector2>();

        int xCord = P.PiecePosition.GetX();
        int yCord = P.PiecePosition.GetY();

        
        //Checks availability of the 3 posible squares to move to +1 on the X-axis of the board
        if (xCord + 1 <= 7)
        {
            if (yCord + 1 <= 7)
            {
                if (B.squares[xCord + 1, yCord + 1] == null)
                {
                    poss.Add(new Vector2(xCord + 1, yCord + 1));
                }
                else
                {
                    if (B.squares[xCord + 1, yCord + 1].GetPiece().team != P.team) poss.Add(new Vector2(xCord + 1, yCord + 1));
                }
            }

            if (yCord - 1 >= 0)
            {
                if (B.squares[xCord + 1, yCord - 1] == null)
                {
                    poss.Add(new Vector2(xCord + 1, yCord - 1));
                }
                else
                {
                    if (B.squares[xCord + 1, yCord - 1].GetPiece().team != P.team) poss.Add(new Vector2(xCord + 1, yCord - 1));
                }
            }

            if (B.squares[xCord + 1, yCord] == null)
            {
                poss.Add(new Vector2(xCord + 1, yCord));
            }
            else
            {
                if (B.squares[xCord + 1, yCord].GetPiece().team != P.team) poss.Add(new Vector2(xCord + 1, yCord));
            }


        }

        //Checks the same things as the previous block, but -1 on the X-axis 
        if (xCord - 1 >= 0)
        {
            if (yCord + 1 <= 7)
            {
                if (B.squares[xCord - 1, yCord + 1] == null)
                {
                    poss.Add(new Vector2(xCord - 1, yCord + 1));
                }
                else
                {
                    if (B.squares[xCord - 1, yCord + 1].GetPiece().team != P.team) poss.Add(new Vector2(xCord - 1, yCord + 1));
                }
            }

            if (yCord - 1 >= 0)
            {
                if (B.squares[xCord - 1, yCord - 1] == null)
                {
                    poss.Add(new Vector2(xCord - 1, yCord - 1));
                }
                else
                {
                    if (B.squares[xCord - 1, yCord - 1].GetPiece().team != P.team) poss.Add(new Vector2(xCord - 1, yCord - 1));
                }
            }

            if (B.squares[xCord - 1, yCord] == null)
            {
                poss.Add(new Vector2(xCord - 1, yCord));
            }
            else
            {
                if (B.squares[xCord - 1, yCord].GetPiece().team != P.team) poss.Add(new Vector2(xCord - 1, yCord));
            }
        }

        //Checks only the square +1 on the Y-axis from the kings posision
        if (yCord + 1 <= 7)
        {
            if (B.squares[xCord, yCord + 1] == null)
            {
                poss.Add(new Vector2(xCord, yCord + 1));
            }
            else
            {
                if (B.squares[xCord, yCord + 1].GetPiece().team != P.team) poss.Add(new Vector2(xCord, yCord + 1));
            }
        }

        //Same as previous block, but -1 on the Y-axis from the kings position
        if (yCord - 1 >= 0)
        {
            if (B.squares[xCord, yCord - 1] == null)
            {
                poss.Add(new Vector2(xCord, yCord - 1));
            }
            else
            {
                if (B.squares[xCord, yCord - 1].GetPiece().team != P.team) poss.Add(new Vector2(xCord, yCord - 1));
            }

        }
        return poss;
    }


    //Function for finding all possible queen moves.
    //Reuses the Rook and Bishop functions as the queen is just a combination of the two pieces in terms of movement
    //The two move lists are concatenated together to create one full list of positions.
    //There can be no overlap of moves from rook and bishop because they are both sourced at the sam position and have opisite move syles

    
    public List<Vector2> LegalQueenMove(Piece P, Board B)
    {
    List<Vector2> poss = new List<Vector2>();

    poss.AddRange(LegalRookMove(P, B));
    poss.AddRange(LegalBishopMove(P, B));

    return poss;
    }
    

}


/*
//checks for all valid moves of a King
 static public List<Vector2> LegalKingMove(Piece P, Board B)
{
    int xCord = P.PiecePosition.GetX();// current x cordinate
    int yCord = P.PiecePosition.GetY();// current y cordinate
    int newX = xCord;
    int newY = yCord;//x y cordinates for potential moves
    List<Vector2> poss = new List<Vector2>();//List of positions that are valid moves



    //Segment checks for valid moves of a black king
    if (P.team == false)
    {
        //Loop moves the cordinates being checked in the y direction starting 1 line behind the king and ending 1 line infront of the king
        //9 total squares are checked including the king space
        for (int i = 0; i < 2; i++)
        {
            newY = newY - 1;
            if (newY >= 0 && newY <= 7)
            {
                for (int j = 0; j < 2; j++)//Loop moves the cordinates being checked from one square to the left of the king, to 1 square to the right of the king
                {
                    newX = newX - 1;
                    if (newX >= 0 && newX <= 7)
                    {
                        if (B.squares[newX, newY] == null)//checks the team of the current square and enters only if the team that owns the quare is not the same team as the piece
                        {
                            poss.Add(new Vector2(newX, newY));
                        }
                        else
                        {
                            if (B.squares[newX, newY].GetPiece().team != false)
                            {
                                poss.Add(new Vector2(newX, newY));
                            }
                        }
                    }
                    newX = newX + 2;
                }
            }
            newY = newY + 2;
        }

        //Same loop as for the black king, only the team value that is checked is changed
        if (P.team == true)
        {
            //Loop moves the cordinates being checked in the y direction starting 1 line behind the king and ending 1 line infront of the king
            //9 total squares are checked including the king space
            for (int i = 0; i < 2; i++)
            {
                newY = newY - 1;
                if ((newY >= 0) && (newY <= 7))
                {
                    for (int j = 0; j < 2; j++)//Loop moves the cordinates being checked from one square to the left of the king, to 1 square to the right of the king
                    {
                        newX = newX - 1;
                        if ((newX >= 0) && (newX <= 7))
                        {
                            if (B.squares[newX, newY] == null)//checks the team of the current square and enters only if the team that owns the quare is not the same team as the piece
                            {
                                poss.Add(new Vector2(newX, newY));
                            }
                            else
                            {
                                if (B.squares[newX, newY].GetPiece().team != true)
                                {
                                    poss.Add(new Vector2(newX, newY));
                                }
                            }
                        }
                        newX = newX + 2;
                    }
                }
                newY = newY + 2;
            }
        }
    }
    return poss;
}
*/



