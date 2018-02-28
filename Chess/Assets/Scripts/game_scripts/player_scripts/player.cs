using System.Collections.Generic;
using UnityEngine;
using ChessGlobals;  
public interface Player{
	void SetMove (Move move);
	Move GetMove();
	Move GetLastMove();
	//List<Move> generateAllLegalMoves (Piece piece);
}
