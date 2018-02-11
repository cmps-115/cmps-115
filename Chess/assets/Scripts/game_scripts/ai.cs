using System.Collections;
class AI{
  private int depth;
  private ChessGame chessGame;
  public AI(ChessGame chessGame, Rules rules, int depth);
  public Move getMove();
  public bool moveSuccessfullyExcecuted();
  public Move getBestMove();
  public void DoMove(Move move);
  public List<Move> generateMoves();
  public int evaluateGameState();
  public int getScoreForSquare(Position square);
  public int getScoreForPieceType(PieceType type);
  public int negMax(int depth);
  public int getId();
  public char getColor();
}
