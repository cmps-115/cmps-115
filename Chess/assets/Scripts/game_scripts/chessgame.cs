using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ChessGame : MonoBehavior {
  private Rules rules;
  private Board board;
  private Player playerOne;
  private Player playerTwo;
  private Player activePlayer;
  private List<Piece> pieces;
  private List<Piece> capturedPieces;
  public ChessGame();
  public setPlayer(Player playerOne, Player playerTwo);
  public void startGame();
  public void swapActivePlayer();
  public void createAndAddPiece(PieceType pieceType);
  public Move movePiece(Move move);
  public void waitForMove();
  public void undoMove(Move move);
  public bool endConditionReached();
  public Position getNonCapturedPieceAtPosition(Position pos);
  public bool isNonCapturedPieceAtPosition(Position pos);
  public GameState getGameState();
}
