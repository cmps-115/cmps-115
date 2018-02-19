using System.Collections.Generic;
using UnityEngine;
using ChessGlobals;  
public interface Player{
	void setMove (Move move);
	Move getMove();
	Move getLastMove();
	//List<Move> generateAllLegalMoves (Piece piece);
}
