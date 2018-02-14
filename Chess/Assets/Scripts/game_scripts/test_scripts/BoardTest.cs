//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using ChessGlobals;
public class BoardTest
{
	private Board board = null;
	public BoardTest (Board board)
	{
		this.board = board;
	}
	public void ValidateMark()
	{
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
	public void ValidateUnmark()
	{
		Assert.AreNotEqual(board, null);
		int totalTrials = Random.Range (30, 101), ithTrial;
		int row = 0;
		int col = 0;
		Piece p = new King();
		for (ithTrial = 0; ithTrial < totalTrials; ++ithTrial) 
		{
			row = Random.Range (0, 7);
			col = Random.Range (0, 7);
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
	public void ValidateClear()
	{
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
	public void ValidateGetPieceAt()
	{
		Vector2 pos = new Vector2( Random.Range(0, 7), Random.Range(0, 7));
		Piece p = new King();
		Piece p1 = null;
		board.Mark(pos,p);
		p1 = board.GetPieceAt(pos);
		Assert.AreEqual (p, p1);
	}
	public void ValidateInitialPiecePositions()
	{
		//Check white pieces


		//Check black pieces 

	}

}

