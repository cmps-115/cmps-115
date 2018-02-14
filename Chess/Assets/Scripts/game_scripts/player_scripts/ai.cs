using System.Collections.Generic;
using UnityEngine;
using ChessGlobals;
	public class AI: Player{
	private int depth;
	private ChessGameController chessGame;
	public AI(ChessGameController chessGame, int depth)//Rules now in each piece
	{
	}
	public override Move getMove()
	{
		Move x = new Move();
		return x;
	}
	public override int getId()
	{
		return 0;
	}
	public override char getColor()
	{
		return '0';
	}
	public override bool moveSuccessfullyExcecuted()
	{
		return false;
	}
	public Move getBestMove()
	{
		return new Move ();
	}
	public void DoMove(Move move)
	{
	}
	public List<Move> generateMoves()
	{
		return new List<Move> ();
	}
	public int evaluateGameState()
	{
		return 0;
	}
	public int getScoreForSquare(Vector2 square)
	{
		return 0;
	}
	public int getScoreForPieceType(PIECE_TYPES type)
	{
		return 0;
	}
	public int negMax(int depth)
	{
		return 0;
	}

}
