//Piece.cs
//Writen by: Austin Harmon
//A piece class that defines the atributes of a generic chess piece
//Each type of piece is a sub class of the generic piece
//Leagal move check functions are enclosed in each subclass


using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using ChessGlobals;

[Serializable]
public class Piece
{
    private bool taken;//represents if the piece still exists on the board
    private Vector2 piecePosition;
    private ChessGlobals.Teams pieceTeam;

    public Piece()
    {
        taken = false;
        pieceTeam = null;
    }
    public Piece(int p_team)
    {
        taken = false;
        pieceTeam = new Teams(p_team);
    }
    public Piece(int p_team, int xCord, int yCord)//Constructor for piece with a specific position
    {
        taken = false;
        pieceTeam = new Teams(p_team);
        piecePosition = new Vector2(xCord, yCord);
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
        return piecePosition;
    }
    //With GetXCoord and GetYCoord we will have the casting take place in one spot
    public int GetPiecePositionX()
    {
        return (int)piecePosition.x;
    }
    public int GetPiecePositionY()
    {
        return (int)piecePosition.y;
    }
    public int GetTeam()
    {
        return pieceTeam.getTeam();
    }

    //*****manipulators*****
    public void TakePiece()
    {
        piecePosition = new Vector2(-1, -1);
        taken = true;
    }

    public void SetPosition(int xCord, int yCord)
    {
        piecePosition = new Vector2(xCord, yCord);
    }
    public void SetPosition(Vector2 pos)
    {
        piecePosition = pos;
    }

    public void SetTeam(int team)
    {
        if (this.pieceTeam == null)
            this.pieceTeam = new Teams(team);
        else
            this.pieceTeam.setTeam(team);
    }

    public bool IsTaken()
    {
        return taken != false;
    }

    public System.Object Clone()
    {
        return new Piece
        {
            taken = taken,
            piecePosition = piecePosition,
            pieceTeam = pieceTeam
        };
    }

    //*****Helper*****
    public bool CheckMove(Board cloneChessBoard, Vector2 testMove)
    {
        cloneChessBoard.UpdateBoardThreat(this, testMove);

        if (GetTeam() == Teams.WHITE_TEAM)
        {
            return KingInCheck.IsWhiteInCheck();
        }
        else
        {
            return KingInCheck.IsBlackInCheck();
        }
    }

    public List<Vector2> CheckLegalMoves(Board boardClone, List<Vector2> currentLegalMoves)
    {
        List<Vector2> newLegalMoves = new List<Vector2>();
        Board boardCopy = DeepCopy.Copy(boardClone);
        bool wasBlackinCheck = KingInCheck.IsBlackInCheck();
        bool wasWhiteinCheck = KingInCheck.IsWhiteInCheck();


        int selectedPiece = boardClone.GetActivePieces().IndexOf(this);
        Piece selectedCopy = boardCopy.GetActivePieces()[selectedPiece];


        Debug.Log("Current legal moves are: ");
        foreach (Vector2 move in currentLegalMoves)
        {
            //When checkMove is false, the current teams king is not in check and the move is valid
            if (selectedCopy.CheckMove(boardCopy, move) == false)
            {
                newLegalMoves.Add(move);
            }
        }

        KingInCheck.SetBlackCheck(wasBlackinCheck);
        KingInCheck.SetWhiteCheck(wasWhiteinCheck);


        return newLegalMoves;
    }

}

[Serializable]
public class King : Piece
{
    public King() { }
    public King(int p_team)
        : base(p_team) { }
    public King(int p_team, int xCord, int yCord)
        : base(p_team, xCord, yCord) { }

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
                if (chessBoard.IsOccupied(xCord + 1, yCord + 1) == false)
                {
                    positions.Add(new Vector2(xCord + 1, yCord + 1));
                }
                else
                {
                    if (chessBoard.GetPieceAt(xCord + 1, yCord + 1).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord + 1, yCord + 1));
                }
            }

            if (yCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
                if (chessBoard.IsOccupied(xCord + 1, yCord - 1) == false)
                {
                    positions.Add(new Vector2(xCord + 1, yCord - 1));
                }
                else
                {
                    if (chessBoard.GetPieceAt(xCord + 1, yCord - 1).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord + 1, yCord - 1));
                }

            }
            if (chessBoard.IsOccupied(xCord + 1, yCord) == false)
            {
                positions.Add(new Vector2(xCord + 1, yCord));
            }
            else
            {
                if (chessBoard.GetPieceAt(xCord + 1, yCord).GetTeam() != GetTeam())
                    positions.Add(new Vector2(xCord + 1, yCord));
            }


        }

        //Checks the same things as the previous block, but -1 on the X-axis 
        if (xCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
        {
            if (yCord + 1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
                if (chessBoard.IsOccupied(xCord - 1, yCord + 1) == false)
                {
                    positions.Add(new Vector2(xCord - 1, yCord + 1));
                }
                else
                {
                    if (chessBoard.GetPieceAt(xCord - 1, yCord + 1).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord - 1, yCord + 1));
                }
            }

            if (yCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {

                if (chessBoard.IsOccupied(xCord - 1, yCord - 1) == false)
                {
                    positions.Add(new Vector2(xCord - 1, yCord - 1));
                }
                else
                {
                    if (chessBoard.GetPieceAt(xCord - 1, yCord - 1).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord - 1, yCord - 1));
                }
            }

            if (chessBoard.IsOccupied(xCord - 1, yCord) == false)
            {
                positions.Add(new Vector2(xCord - 1, yCord));
            }
            else
            {
                if (chessBoard.GetPieceAt(xCord - 1, yCord).GetTeam() != GetTeam())
                    positions.Add(new Vector2(xCord - 1, yCord));
            }

        }

        //Checks only the square +1 on the Y-axis from the kings posision
        if (yCord + 1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
        {
            if (chessBoard.IsOccupied(xCord, yCord + 1) == false)
            {
                positions.Add(new Vector2(xCord, yCord + 1));
            }
            else
            {
                if (chessBoard.GetPieceAt(xCord, yCord + 1).GetTeam() != GetTeam())
                    positions.Add(new Vector2(xCord, yCord + 1));
            }
        }

        //Same as previous block, but -1 on the Y-axis from the kings position
        if (yCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
        {
            if (chessBoard.IsOccupied(xCord, yCord - 1) == false)
            {
                positions.Add(new Vector2(xCord, yCord - 1));
            }
            else
            {
                if (chessBoard.GetPieceAt(xCord, yCord - 1).GetTeam() != GetTeam())
                    positions.Add(new Vector2(xCord, yCord - 1));
            }
        }


        return positions;
        //return LegalMovesCheck(chessBoard, positions);
    }
    /*
    public List<Vector2> LegalMovesCheck(Board chessBoard, List<Vector2> positionsBeforeCheck)
    {

        List<Vector2> positionsAfterCheck = new List<Vector2>();

        //For Black King
        if (GetTeam() == Teams.BLACK_TEAM)
        {
            foreach (Vector2 move in positionsBeforeCheck)
            {
                if (chessBoard.GetSquare(move).getWhiteThreat() == false)
                {
                    positionsAfterCheck.Add(move);
                }
            }
        }
        //For White King
        else
        {
            foreach (Vector2 move in positionsBeforeCheck)
            {
                if (chessBoard.GetSquare(move).getBlackThreat() == false)
                {
                    positionsAfterCheck.Add(move);
                }
            }
        }
        return positionsAfterCheck;
    }
    */
}

[Serializable]
public class Knight : Piece
{
    public Knight() { }
    public Knight(int p_team)
        : base(p_team) { }
    public Knight(int p_team, int xCord, int yCord)
        : base(p_team, xCord, yCord) { }

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
                if (chessBoard.IsOccupied(xCord + 2, yCord + 1) == false)
                {
                    positions.Add(new Vector2(xCord + 2, yCord + 1));
                }
                else
                {
                    if (chessBoard.GetPieceAt(xCord + 2, yCord + 1).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord + 2, yCord + 1));
                }
            }

            if (yCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
                if (chessBoard.IsOccupied(xCord + 2, yCord - 1) == false)
                {
                    positions.Add(new Vector2(xCord + 2, yCord - 1));
                }
                else
                {
                    if (chessBoard.GetPieceAt(xCord + 2, yCord - 1).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord + 2, yCord - 1));
                }
            }
        }

        //Same function as the previous loop in different direction
        if (xCord - 2 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
        {
            if (yCord + 1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
                if (chessBoard.IsOccupied(xCord - 2, yCord + 1) == false)
                {
                    positions.Add(new Vector2(xCord - 2, yCord + 1));
                }
                else
                {
                    if (chessBoard.GetPieceAt(xCord - 2, yCord + 1).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord - 2, yCord + 1));
                }
            }

            if (yCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
                if (chessBoard.IsOccupied(xCord - 2, yCord - 1) == false)
                {
                    positions.Add(new Vector2(xCord - 2, yCord - 1));
                }
                else
                {
                    if (chessBoard.GetPieceAt(xCord - 2, yCord - 1).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord - 2, yCord - 1));
                }
            }

        }

        //Same function as the previous loop in different direction
        if (yCord + 2 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
        {
            if (xCord + 1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
                if (chessBoard.IsOccupied(xCord + 1, yCord + 2) == false)
                {
                    positions.Add(new Vector2(xCord + 1, yCord + 2));
                }
                else
                {
                    if (chessBoard.GetPieceAt(xCord + 1, yCord + 2).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord + 1, yCord + 2));
                }
            }

            if (xCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
                if (chessBoard.IsOccupied(xCord - 1, yCord + 2) == false)
                {
                    positions.Add(new Vector2(xCord - 1, yCord + 2));
                }
                else
                {
                    if (chessBoard.GetPieceAt(xCord - 1, yCord + 2).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord - 1, yCord + 2));
                }
            }
        }

        //Same function as the previous loop in different direction
        if (yCord - 2 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
        {
            if (xCord + 1 <= ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
                if (chessBoard.IsOccupied(xCord + 1, yCord - 2) == false)
                {
                    positions.Add(new Vector2(xCord + 1, yCord - 2));
                }
                else
                {
                    if (chessBoard.GetPieceAt(xCord + 1, yCord - 2).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord + 1, yCord - 2));
                }
            }

            if (xCord - 1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
                if (chessBoard.IsOccupied(xCord - 1, yCord - 2) == false)
                {
                    positions.Add(new Vector2(xCord - 1, yCord - 2));
                }
                else
                {
                    if (chessBoard.GetPieceAt(xCord - 1, yCord - 2).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord - 1, yCord - 2));
                }
            }
        }

        return positions;
    }
}

[Serializable]
public class Queen : Piece
{
    public Queen() { }
    public Queen(int p_team)
        : base(p_team) { }
    public Queen(int p_team, int xCord, int yCord)
        : base(p_team, xCord, yCord)
    { }
    public override List<Vector2> LegalMoves(Board chessBoard)
    {
        Rook tempRook = new Rook(GetTeam(), GetPiecePositionX(), GetPiecePositionY());
        Bishop tempBishop = new Bishop(GetTeam(), GetPiecePositionX(), GetPiecePositionY());
        List<Vector2> positions = new List<Vector2>();

        List<Vector2> rookPositions = tempRook.LegalMoves(chessBoard);
        List<Vector2> bishopPositions = tempBishop.LegalMoves(chessBoard);

        positions.AddRange(rookPositions);
        positions.AddRange(bishopPositions);

        return positions;
    }
}

[Serializable]
public class Pawn : Piece
{
    public Pawn() { }
    public Pawn(int p_team)
        : base(p_team) { }
    public Pawn(int p_team, int xCord, int yCord)
        : base(p_team, xCord, yCord)
    { }

    public override List<Vector2> LegalMoves(Board chessBoard)
    {
        int team = GetTeam();
        List<Vector2> positions = new List<Vector2>();


        int xCord = GetPiecePositionX();
        int yCord = GetPiecePositionY();

        //assuming for the board that ChessGlobals.BoardConstants.BOARD_MINIMUM = free , 1 = black, = 2 = white

        //assuming that white starts at the rows ChessGlobals.BoardConstants.BOARD_MINIMUM,1
        if (team == ChessGlobals.Teams.WHITE_TEAM)//Where are you getting this from Austin?
        {

            //to make sure it doesnt go past 
            if (yCord < ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
                if (chessBoard.IsOccupied(xCord, yCord + 1) == false)
                {
                    positions.Add(new Vector2(xCord, yCord + 1));
                }
            }

            //Checks if the piece is on the second row
            if (yCord == 1)
            {
                if (chessBoard.IsOccupied(xCord, yCord + 1) == false)
                {
                    if (chessBoard.IsOccupied(xCord, yCord + 2) == false)
                    {
                        positions.Add(new Vector2(xCord, yCord + 2));
                    }
                }
            }

            //diagonal to left
            if (xCord > ChessGlobals.BoardConstants.BOARD_MINIMUM && yCord < ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
                if (chessBoard.IsOccupied(xCord - 1, yCord + 1))
                {
                    if (chessBoard.GetPieceAt(xCord - 1, yCord + 1).GetTeam() == ChessGlobals.Teams.BLACK_TEAM)
                        positions.Add(new Vector2(xCord - 1, yCord + 1));
                }
            }

            //diagonal to right
            if (xCord < ChessGlobals.BoardConstants.BOARD_MAXIMUM && yCord < ChessGlobals.BoardConstants.BOARD_MAXIMUM)
            {
                if (chessBoard.IsOccupied(xCord + 1, yCord + 1))
                {
                    if (chessBoard.GetPieceAt(xCord + 1, yCord + 1).GetTeam() == ChessGlobals.Teams.BLACK_TEAM)
                        positions.Add(new Vector2(xCord + 1, yCord + 1));
                }
            }

        }
        else
        {
            //to make sure it doesnt go past ChessGlobals.BoardConstants.BOARD_MINIMUM
            if (yCord > ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
                if (chessBoard.IsOccupied(xCord, yCord - 1) == false)
                {
                    positions.Add(new Vector2(xCord, yCord - 1));
                }
            }

            //Checks if the pawn in on its starting row
            if (yCord == 6)

                if (chessBoard.IsOccupied(xCord, yCord - 1) == false)
                {
                    if (yCord == 6 && chessBoard.IsOccupied(xCord, yCord - 2) == false)
                    {
                        positions.Add(new Vector2(xCord, yCord - 2));
                    }
                }

            //diagonal to left
            if (xCord < ChessGlobals.BoardConstants.BOARD_MAXIMUM && yCord > ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
                if (chessBoard.IsOccupied(xCord + 1, yCord - 1))
                {
                    if (chessBoard.GetPieceAt(xCord + 1, yCord - 1).GetTeam() == ChessGlobals.Teams.WHITE_TEAM)
                        positions.Add(new Vector2(xCord + 1, yCord - 1));
                }
            }


            //diagonal to right
            if (xCord > ChessGlobals.BoardConstants.BOARD_MINIMUM && yCord > ChessGlobals.BoardConstants.BOARD_MINIMUM)
            {
                if (chessBoard.IsOccupied(xCord - 1, yCord - 1)) // not sure about this one Austin
                {
                    if (chessBoard.GetPieceAt(xCord - 1, yCord - 1).GetTeam() == ChessGlobals.Teams.WHITE_TEAM)
                        positions.Add(new Vector2(xCord - 1, yCord - 1));
                }

            }
        }
        return positions;
    }
}

[Serializable]
public class Rook : Piece
{
    public Rook() { }
    public Rook(int p_team)
        : base(p_team) { }
    public Rook(int p_team, int xCord, int yCord)
        : base(p_team, xCord, yCord)
    { }

    public override List<Vector2> LegalMoves(Board chessBoard)
    {
        List<Vector2> positions = new List<Vector2>();

        //bool team = GetTeam();
        int xCord, yCord;

        xCord = GetPiecePositionX();
        yCord = GetPiecePositionY();

        // if its a white piece
        if (GetTeam() == ChessGlobals.Teams.WHITE_TEAM)
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
                if (chessBoard.IsOccupied(xCord, i1))
                {
                    if (chessBoard.GetPieceAt(xCord, i1).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord, i1));
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
                if (chessBoard.IsOccupied(xCord, i1)) // not sure about this one Austin
                {
                    if (chessBoard.GetPieceAt(xCord, i1).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord, i1));
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
                if (chessBoard.IsOccupied(i1, yCord)) // not sure about this one Austin
                {
                    if (chessBoard.GetPieceAt(i1, yCord).GetTeam() != GetTeam())
                        positions.Add(new Vector2(i1, yCord));
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
                if (chessBoard.IsOccupied(i1, yCord)) // not sure about this one Austin
                {
                    if (chessBoard.GetPieceAt(i1, yCord).GetTeam() != GetTeam())
                        positions.Add(new Vector2(i1, yCord));
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
                if (chessBoard.IsOccupied(xCord, yCord - 1)) // not sure about this one Austin
                {
                    if (chessBoard.GetPieceAt(xCord, yCord - 1).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord, yCord - 1));
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
                if (chessBoard.IsOccupied(xCord, i1)) // not sure about this one Austin
                {
                    if (chessBoard.GetPieceAt(xCord, i1).GetTeam() != GetTeam())
                        positions.Add(new Vector2(xCord, i1));
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
                if (chessBoard.IsOccupied(i1, yCord)) // not sure about this one Austin
                {
                    if (chessBoard.GetPieceAt(i1, yCord).GetTeam() != GetTeam())
                        positions.Add(new Vector2(i1, yCord));
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
                if (chessBoard.IsOccupied(i1, yCord))
                {
                    if (chessBoard.GetPieceAt(i1, yCord).GetTeam() != GetTeam())
                        positions.Add(new Vector2(i1, yCord));
                }
            }
        }
        return positions;
    }
}

[Serializable]
public class Bishop : Piece
{
    public Bishop() { }
    public Bishop(int p_team)
        : base(p_team) { }
    public Bishop(int p_team, int xCord, int yCord)
        : base(p_team, xCord, yCord)
    { }

    public override List<Vector2> LegalMoves(Board chessBoard)
    {

        int xc, yc;
        List<Vector2> positions = new List<Vector2>();

        xc = GetPiecePositionX();
        yc = GetPiecePositionY();

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
            if (chessBoard.IsOccupied(i2, i1))
            {
                if (chessBoard.GetPieceAt(i2, i1).GetTeam() != GetTeam())
                    positions.Add(new Vector2(i2, i1));
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
        if (i1 >= ChessGlobals.BoardConstants.BOARD_MINIMUM && i2 >= ChessGlobals.BoardConstants.BOARD_MINIMUM)
        {
            if (chessBoard.IsOccupied(i2, i1)) // not sure about this one Austin
            {
                if (chessBoard.GetPieceAt(i2, i1).GetTeam() != GetTeam())
                    positions.Add(new Vector2(i2, i1));
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
            if (chessBoard.IsOccupied(i1, i2)) // not sure about this one Austin
            {
                if (chessBoard.GetPieceAt(i1, i2).GetTeam() != GetTeam())
                    positions.Add(new Vector2(i1, i2));
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
            if (chessBoard.IsOccupied(i1, i2)) // not sure about this one Austin
            {
                if (chessBoard.GetPieceAt(i1, i2).GetTeam() != GetTeam())
                    positions.Add(new Vector2(i1, i2));
            }
        }
        return positions;
    }
}