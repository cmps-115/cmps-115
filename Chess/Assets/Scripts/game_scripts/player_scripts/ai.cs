using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using ChessGlobals;
public class AI: Player
{
	private Move lastMove;
	private Move currentMove;
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
		currentMove = move;
	}
	public Move getMove()
	{
		return currentMove;
	}
	public Move getLastMove()
	{
		return lastMove;
	}
	public Move getBestMove()
	{
		
		//get all possible valid moves
		List<Move> allMoves = generateAllLegalMoves();
		int bestResult = Int32.MinValue;
		Move bestMove = null;
		foreach(Move move in allMoves)
		{
			/*executeMove(move)
			int evaluationResult = -1 * negaMax(this.maxDepth,"");
			//System.out.println("result: "+evaluationResult);
			undoMove(move);
			if( evaluationResult > bestResult){
				bestResult = evaluationResult;
				bestMove = move;
			}*/
			int evaluationResult = -1 * negMax (this.depth);
		}
		//foreach move call negmax
		return bestMove;
	}
	public int negMax(int depth)
	{
		return 0;
	}
	private List<Move> generateAllLegalMoves()
	{
		
		List<Piece> allPieces = cgs.getPieces ();
		List<Move> allValidMoves = new List<Move>();
		List<Vector2> validMovesForASinglePiece = null;
		for (int i = 0; i < allPieces.Count; ++i) 
		{
			Move move = null;
			//generate LegalMoves for every piece in the game
			validMovesForASinglePiece = generateLegalMovesForAPiece(allPieces[i]);
			for(int j = 0; j < validMovesForASinglePiece.Count; ++j)
			{
				move = new Move (allPieces [i], validMovesForASinglePiece [i]);
				allValidMoves.Add (move);
			}
		}
		return allValidMoves;
	}
	private List<Vector2> generateLegalMovesForAPiece (Piece piece)
	{
		Assert.AreNotEqual (piece, null);
		return piece.LegalMoves(cgs.getBoard());
	}
	public void DoMove(Move move)
	{
	}

	private int evaluateGameState()
	{
		int whitePlayerScore = 0;
		int blackPlayerScore = 0;
		foreach (Piece piece in this.cgs.getPieces()) 
		{
			if (piece.GetTeam () == ChessGlobals.COLOR.BLACK) 
			{
				blackPlayerScore += getScoreForPieceType (piece);
				blackPlayerScore += getScoreForPiecePosition (piece.GetPiecePosition());
			} 
			else if (piece.GetTeam () == ChessGlobals.COLOR.WHITE) 
			{
				whitePlayerScore += getScoreForPieceType (piece);
				whitePlayerScore += getScoreForPiecePosition (piece.GetPiecePosition());
			} 
			else
			{
				//some illegal state exception
			}
			
		}
		int gameState = this.cgs.getGameState ();
		if (gameState == ChessGlobals.GAME_STATE.BLACK)
			return blackPlayerScore - whitePlayerScore;
		else if (gameState == ChessGlobals.GAME_STATE.WHITE)
			return whitePlayerScore - blackPlayerScore;
		else if (gameState == ChessGlobals.GAME_STATE.WHITE_WIN ||
		         gameState == ChessGlobals.GAME_STATE.BLACK_WIN ||
		         gameState == ChessGlobals.GAME_STATE.DRAW)
			return Int32.MinValue + 1;
		else
			//some illegate state exception
			return 0;
	}
	private int getScoreForPieceType(Piece piece)
	{
		if (GetType (piece) == GetType (King)) 
			return 9999;
		else if (GetType (piece) == GetType (Queen)) 
			return 90;
		else if (GetType (piece) == GetType (Knight))
			return 30;
		else if (GetType (piece) == GetType (Bishop))
			return 30;
		else if (GetType (piece) == GetType (Rook)) 
			return 50;
		else if (GetType (piece) == GetType (Pawn)) 
			return 10;
		else  
		{
			//some invalid state exception
		}
		return 0;
	}
	private int getScoreForPiecePosition(Vector2 pos)
	{
		return getScoreForPieceType((int)pos.x, (int)pos.y);
	}
	private int getScoreForPiecePosition(int x, int y)
	{
		int[,] positionWeight = 
		{ 	 {1,1,1,1,1,1,1,1}
			,{2,2,2,2,2,2,2,2}
			,{2,2,3,3,3,3,2,2}
			,{2,2,3,4,4,3,2,2}
			,{2,2,3,4,4,3,2,2}
			,{2,2,3,3,3,3,2,2}
			,{2,2,2,2,2,2,2,2}
			,{1,1,1,1,1,1,1,1}
		};
		return positionWeight[x][y];
	}


}
