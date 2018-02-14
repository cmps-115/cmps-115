//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using ChessGlobals;
public static class BoardTest
{
	/*private Board board = null;
	public BoardTest (Board board)
	{
		this.board = (Board)board.Clone();//shallow copy
	}*/
	public static void ValidateMark(Board b)
	{
		Board board = (Board)b.Clone();
		Assert.AreNotEqual(board, null);
		int totalTrials = Random.Range (30, 101), ithTrial = 0;
		int row = 0;
		int col = 0;
		Piece p = new King();
		for (ithTrial = 0; ithTrial < totalTrials; ++ithTrial) 
		{
			row = Random.Range (0, 7);
			col = Random.Range (0, 7);
			board.Mark(row,col,p);
			Assert.AreEqual (board.IsOccupied (row, col), true);
		}
		row = 0;
		col = 0;
		p = new Queen();
		for (ithTrial = 0; ithTrial < totalTrials; ++ithTrial) 
		{
			row = Random.Range (0, 7);
			col = Random.Range (0, 7);
			board.Mark(row,col,p);
			board.UnMark(row, col);
			Assert.AreEqual (board.IsOccupied (row, col), false);
		}
	}
	public static void ValidateUnMark(Board b)
	{
		Board board = (Board)b.Clone();
		Assert.AreNotEqual(board, null);
		int totalTrials = Random.Range (30, 101), ithTrial;
		int row = 0;
		int col = 0;
		Piece p = new King();
		for (ithTrial = 0; ithTrial < totalTrials; ++ithTrial) 
		{
			row = Random.Range(0, 7);
			col = Random.Range(0, 7);
			board.UnMark(row,col);
			Assert.AreEqual(board.IsOccupied(row, col), false);
		}
		row = 0;
		col = 0;
		p = new Knight();
		for (ithTrial = 0; ithTrial < totalTrials; ++ithTrial) 
		{
			row = Random.Range (0, 7);
			col = Random.Range (0, 7);
			board.UnMark(row,col);
			board.Mark(row, col,p);
			Assert.AreEqual(board.IsOccupied(row, col), true);
		}
	}
	//if Unmark is good then so is Clear 
	public static void ValidateClear(Board b)
	{
		Board board = (Board)b.Clone();
		Assert.AreNotEqual(board, null);
		int row = 0, col = 0, maxRow = 8, maxCol = 8;
		int totalTrials = Random.Range(10, 101), totalSquares = row * col, ithTrial, ithSquare;
		Piece p = new King();
		board.Clear();
		for (ithTrial = 0; ithTrial < totalTrials; ++ithTrial) 
		{
			for (ithSquare = 0; ithSquare < totalSquares; ++ithSquare) 
			{
				Vector2 vec = new Vector2( Random.Range(0, 7), Random.Range(0, 7));
				board.Mark (vec,p);
			}
			board.Clear();
			for (row = 0; row < maxRow; ++row) 
			{
				for (col = 0; col < maxCol; ++col) 
				{
					Assert.AreEqual (board.IsOccupied (row, col), false);
				}
			}
		}
	}
	public static void ValidateGetPieceAt(Board b)
	{
		Board board = (Board)b.Clone();
		Vector2 pos = new Vector2( Random.Range(0, 7), Random.Range(0, 7));
		Piece p = new King();
		Piece p1 = null;
		board.Mark(pos,p);
		p1 = board.GetPieceAt(pos);
		Assert.AreEqual(p, p1);
	}
	public static void ValidateInitialPiecePositions(Board b)
	{
		Board board = (Board)b.Clone();
		int x, y;
		Pawn pawn = new Pawn();
		Rook rook = new Rook();
		Knight knight = new Knight();
		Bishop bishop = new Bishop();
		King king = new King();
		Queen queen = new Queen();
		var pawn_type = pawn.GetType();
		var rook_type = rook.GetType();
		var knight_type = knight.GetType();
		var bishop_type = bishop.GetType ();
		var king_type = king.GetType ();
		var queen_type = queen.GetType();
		//Check white pieces
		for (y = ChessGlobals.BoardConstants.BOARD_MINIMUM; y < ChessGlobals.BoardConstants.TEAM_ROWS; ++y)
		{
			for (x = ChessGlobals.BoardConstants.BOARD_MINIMUM; x <= ChessGlobals.BoardConstants.BOARD_MAXIMUM; ++x)
			{
				if (y == 1) //pawns
				{ 
					Assert.AreEqual (board.IsOccupied(x, y), true);//a piece should occupy here
					Assert.AreEqual (board.GetPieceAt(x, y).GetTeam(),ChessGlobals.COLOR.WHITE);//correct team
					Assert.AreEqual (board.GetPieceAt(x, y).GetType(), pawn_type);//correct type of piece
				} 
				else 
				{
					if (x == 0 || x == 7) 
					{
						Assert.AreEqual (board.IsOccupied (x, y), true);//a piece should occupy here
						Assert.AreEqual (board.GetPieceAt (x, y).GetTeam (), ChessGlobals.COLOR.WHITE);//correct team
						Assert.AreEqual (board.GetPieceAt (x, y).GetType (), rook_type);//correct type of piece
					} 
					else if (x == 1 || x == 6) 
					{
						Assert.AreEqual (board.IsOccupied (x, y), true);//a piece should occupy here
						Assert.AreEqual (board.GetPieceAt (x, y).GetTeam (), ChessGlobals.COLOR.WHITE);//correct team
						Assert.AreEqual (board.GetPieceAt (x, y).GetType (), knight_type);//correct type of piece
					} 
					else if (x == 2 || x == 5) 
					{
						Assert.AreEqual (board.IsOccupied (x, y), true);//a piece should occupy here
						Assert.AreEqual (board.GetPieceAt (x, y).GetTeam (), ChessGlobals.COLOR.WHITE);//correct team
						Assert.AreEqual (board.GetPieceAt (x, y).GetType (), bishop_type);//correct type of piece
					} 
					else if (x == 3) 
					{
						Assert.AreEqual (board.IsOccupied (x, y), true);//a piece should occupy here
						Assert.AreEqual (board.GetPieceAt (x, y).GetTeam (), ChessGlobals.COLOR.WHITE);//correct team
						Assert.AreEqual (board.GetPieceAt (x, y).GetType (), king_type);//correct type of piece
					}
					else if (x == 4) 
					{
						Assert.AreEqual (board.IsOccupied (x, y), true);//a piece should occupy here
						Assert.AreEqual (board.GetPieceAt (x, y).GetTeam (), ChessGlobals.COLOR.WHITE);//correct team
						Assert.AreEqual (board.GetPieceAt (x, y).GetType (), queen_type);//correct type of piece
					}
				}
			}
		}
 

		//Check black pieces 
		for (y = ChessGlobals.BoardConstants.BOARD_MAXIMUM; y > ChessGlobals.BoardConstants.BOARD_MAXIMUM - ChessGlobals.BoardConstants.TEAM_ROWS; --y) 
		{
			for (x = ChessGlobals.BoardConstants.BOARD_MINIMUM; x <= 7; ++x) 
			{
				if (y == 6) 
				{
					Assert.AreEqual (board.IsOccupied(x, y), true);//a piece should occupy here
					Assert.AreEqual (board.GetPieceAt(x, y).GetTeam(),ChessGlobals.COLOR.BLACK);//correct team
					Assert.AreEqual (board.GetPieceAt(x, y).GetType(), pawn_type);//correct type of piece
				} 
				else 
				{
					if (x == 0 || x == 7) 
					{
						Assert.AreEqual (board.IsOccupied (x, y), true);//a piece should occupy here
						Assert.AreEqual (board.GetPieceAt (x, y).GetTeam (), ChessGlobals.COLOR.BLACK);//correct team
						Assert.AreEqual (board.GetPieceAt (x, y).GetType (), rook_type);//correct type of piece
					} 
					else if (x == 1 || x == 6) 
					{
						Assert.AreEqual (board.IsOccupied (x, y), true);//a piece should occupy here
						Assert.AreEqual (board.GetPieceAt (x, y).GetTeam (), ChessGlobals.COLOR.BLACK);//correct team
						Assert.AreEqual (board.GetPieceAt (x, y).GetType (), knight_type);//correct type of piece
					} 
					else if (x == 2 || x == 5) 
					{
						Assert.AreEqual (board.IsOccupied (x, y), true);//a piece should occupy here
						Assert.AreEqual (board.GetPieceAt (x, y).GetTeam (), ChessGlobals.COLOR.BLACK);//correct team
						Assert.AreEqual (board.GetPieceAt (x, y).GetType (), bishop_type);//correct type of piece
					} 
					else if (x == 3) 
					{
						Assert.AreEqual (board.IsOccupied (x, y), true);//a piece should occupy here
						Assert.AreEqual (board.GetPieceAt (x, y).GetTeam (), ChessGlobals.COLOR.BLACK);//correct team
						Assert.AreEqual (board.GetPieceAt (x, y).GetType (), king_type);//correct type of piece
					}
					else if (x == 4) 
					{
						Assert.AreEqual (board.IsOccupied (x, y), true);//a piece should occupy here
						Assert.AreEqual (board.GetPieceAt (x, y).GetTeam (), ChessGlobals.COLOR.BLACK);//correct team
						Assert.AreEqual (board.GetPieceAt (x, y).GetType (), queen_type);//correct type of piece
					}
				}
			}
		}
	}
}

