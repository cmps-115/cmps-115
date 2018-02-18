using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using ChessGlobals;
public class AI: Player
{
	private Move lastMove;
	private int depth;
	private ChessGameController cgs;
	public AI(ChessGameController cgs, int depth)//Rules now in each piece
	{
		Assert.AreNotEqual (cgs, null);
		this.cgs = cgs;
		this.depth = depth;
		lastMove = null;
	}
	public void setMove (Move move)
	{
	}
	public Move getMove()
	{
		return new Move();
	}
	public Move getLastMove()
	{
		return lastMove;
	}
	public List<Vector2> generateLegalMoves (Piece piece)
	{
		Assert.AreNotEqual (piece, null);
		return piece.LegalMoves(cgs.getBoard());
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
