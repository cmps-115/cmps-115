using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using ChessGlobals;
using System.Collections;

public class AI: MonoBehaviour
{
	private Move lastMove;
	private Move currentMove;
	private int depth;
	private ChessGameController cgs;
	private Board boardCopy;
    private Move bestMove;
    private bool thinking;
 
	/*public AI(ChessGameController cgs, int depth)//Rules now in each piece
	{
		Assert.AreNotEqual (cgs, null);
		this.cgs = cgs;
		this.depth = depth;
		lastMove = null;
	}*/

    public void Init(ChessGameController cgs, int depth)
    {
        this.cgs = cgs;
        this.depth = depth;
        lastMove = null;
        bestMove = null;
        thinking = false;
    }


    public void SetMove (Move move)
	{
		currentMove = move;
	}

	public Move GetMove()
	{
		return currentMove;
	}

	public Move GetLastMove()
	{
		return lastMove;
	}

    public bool IsThinking()
    {
        return thinking;
    }

	private void ExecuteMove(Move move)
	{
		Assert.AreNotEqual (boardCopy, null);
		boardCopy.Mark (move.des, move.piece);
        boardCopy.UnMark(move.src);
	}
	private void UndoMove(Move move)
	{
		Assert.AreNotEqual (boardCopy, null);
		boardCopy.UnMark (move);
	}
	public Move GetBestMove()
	{
		//get current board
		return bestMove;
	}

    public void CalculateMove()
    {
        boardCopy = cgs.GetBoardClone();
        List<Move> allMoves = GenerateAllLegalMoves();
        //get all possible valid moves
        thinking = true;
        StartCoroutine(Thinking(allMoves));
    }

    private IEnumerator Thinking(List<Move> allMoves)
    {
        int bestResult = Int32.MinValue;
        foreach (Move move in allMoves)
        {
            ExecuteMove(move);
            int evaluationResult = -1 * NegMax(this.depth, Int32.MinValue, Int32.MaxValue);
            UndoMove(move);
            if (evaluationResult > bestResult)
            {
                bestResult = evaluationResult;
                bestMove = move;
            }
            yield return null;
        }
        thinking = false;
    }

	public int NegMax(int depth, int alpha, int beta)
	{
		if (depth <= 0 || this.cgs.GetGameState ().getState() == ChessGlobals.GameState.BLACK_WIN || this.cgs.GetGameState().getState() == ChessGlobals.GameState.WHITE_WIN)
			return EvaluateGameState ();
		List<Move> moves = GenerateAllLegalMoves();
		int currentMax = Int32.MinValue;
		foreach(Move currentMove in moves)
		{
			ExecuteMove(currentMove);
			int score = -1 * NegMax(depth - 1,-beta,-alpha);
			UndoMove(currentMove);
			if(score > currentMax)
				currentMax = score;
			alpha = (score > alpha) ? score : alpha;
			if (alpha >= beta)
				break;
		}
		return currentMax;
	}


	private List<Move> GenerateAllLegalMoves()
	{
		List<Piece> allPieces = cgs.GetPieces ();
		List<Move> allValidMoves = new List<Move>();
		List<Vector2> validMovesForASinglePiece = null;
		for (int i = 0; i < allPieces.Count; ++i) 
		{
            if (allPieces[i].GetTeam() == GameState.BLACK_TURN)
            {
                Move move = null;
                //generate LegalMoves for every piece in the game
                validMovesForASinglePiece = GenerateLegalMovesForAPiece(allPieces[i]);
                for (int j = 0; j < validMovesForASinglePiece.Count; ++j)
                {
                    move = new Move(allPieces[i], validMovesForASinglePiece[j]);
                    allValidMoves.Add(move);
                }
            }
		}
		return allValidMoves;
	}

	private List<Vector2> GenerateLegalMovesForAPiece (Piece piece)
	{
		Assert.AreNotEqual (piece, null);
		return piece.LegalMoves(cgs.GetBoard());
	}

	private int EvaluateGameState()
	{
		int whitePlayerScore = 0;
		int blackPlayerScore = 0;
		foreach (Piece piece in this.cgs.GetPieces()) 
		{
			if (piece.GetTeam () ==  ChessGlobals.Teams.BLACK_TEAM) 
			{
				blackPlayerScore += GetScoreForPieceType (piece);
				blackPlayerScore += GetScoreForPiecePosition (piece.GetPiecePosition());
			} 
			else if (piece.GetTeam () ==  ChessGlobals.Teams.WHITE_TEAM) 
			{
				whitePlayerScore += GetScoreForPieceType (piece);
				whitePlayerScore += GetScoreForPiecePosition (piece.GetPiecePosition());
			} 
			else
			{
				Debug.Log ("Illegal team color evaluateGameState()");
			}

		}
		ChessGlobals.GameState gameState = this.cgs.GetGameState ();
		if (gameState.getState() == ChessGlobals.GameState.BLACK_TURN)
			return blackPlayerScore - whitePlayerScore;
		else if (gameState.getState() == ChessGlobals.GameState.WHITE_TURN)
			return whitePlayerScore - blackPlayerScore;
		else if (gameState.getState() == ChessGlobals.GameState.WHITE_WIN || gameState.getState() == ChessGlobals.GameState.BLACK_WIN || gameState.getState() == ChessGlobals.GameState.DRAW)
			return Int32.MinValue + 1;
		Debug.Log ("Illegal game state in evaluateGameState()");
		return 0;
	}

	private int GetScoreForPieceType(Piece piece)
	{
		if (piece.GetType() == typeof(King)) 
			return 9999;
		else if (piece.GetType() == typeof(Queen)) 
			return 90;
		else if (piece.GetType() == typeof(Knight))
			return 30;
		else if (piece.GetType() == typeof(Bishop))
			return 30;
		else if (piece.GetType() == typeof(Rook)) 
			return 50;
		else if (piece.GetType() == typeof(Pawn)) 
			return 10;
		else  
		{
			Debug.Log ("Illegal piece type");
		}
		return 0;
	}

	private int GetScoreForPiecePosition(Vector2 pos)
	{
		return GetScoreForPiecePosition((int)pos.x, (int)pos.y);
	}

	private int GetScoreForPiecePosition(int x, int y)
	{
		int[,] positionWeight = new int[,]
		{ 	{1,1,1,1,1,1,1,1},
			{2,2,2,2,2,2,2,2},
			{2,2,3,3,3,3,2,2},
			{2,2,3,4,4,3,2,2},
			{2,2,3,4,4,3,2,2},
			{2,2,3,3,3,3,2,2},
			{2,2,2,2,2,2,2,2},
			{1,1,1,1,1,1,1,1}
		};
		return positionWeight[x,y];
	}
}
