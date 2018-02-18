using ChessGlobals;  
public interface Player{
	void setMove (Move move);
	Move getMove();
	Move getLastMove();
	bool isMoveSuccessfullyExcecuted (Move move);
}
