using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using ChessGlobals;

public class ChessGameControllerVSAI : ChessGameController
{

    private AI enemyAI;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        enemyAI = new AI(this, 1);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Time.time - moveTime > waitTime && isPieceMove == true)
        {
            print(gameState.getState());
            startMoveTime = Time.time;
            isPieceMove = false;
            SwitchTurn();
            print(gameState.getState());
        }

        if (gameState.getState() == GameState.WHITE_TURN)
        {
            //handles moving a piece
            if ((DrawBoard.IsClicked || DrawPiece.IsClicked) && movePieceFrom != Vector3.down)
            {
                //here must disallow moving pieces to anywhere other than a legal square
                if (legalMovesForAPiece != null)
                {
                    movePieceTo = DrawBoard.IsClicked ? DrawBoard.SquarePosition : DrawPiece.PiecePosition;
                    DrawPiece.ClearHighlight();
                    if (board.IsOccupied(movePieceTo))
                    {
                        //if the color of the piece matches the current turn
                        if (board.GetPieceAt(movePieceTo).GetTeam() == gameState.getState())
                        {
                            movePieceTo = Vector3.down;
                            movePieceFrom = Vector3.down;
                            drawBoard.ClearHighlights();
                            return;
                        }
                    }
                    Vector2 chosenMove = movePieceTo;
                    var found = false;
                    foreach (Vector2 pos in legalMovesForAPiece)
                    {
                        if (pos == chosenMove)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found == false)
                    {
                        movePieceTo = Vector3.down;
                    }
                }
            }
            //handles clicking a piece
            else if (DrawPiece.IsClicked)
            {
                SelectPiece();
            }
            // If the user has clicked on a space to move and a piece to move, move the piece and reset the vectors to numbers the user cannot choose.
            // In the real game we would also have to check if it is a valid move.
            if (movePieceFrom != Vector3.down && movePieceTo != Vector3.down)
            {
                MovePieceModel(movePieceFrom, movePieceTo);
            }
        }
        else if (gameState.getState() == GameState.BLACK_TURN)
        {
            print("black turn");
            print(enemyAI.GetBestMove().src);
            print(enemyAI.GetBestMove().des);
            var from = enemyAI.GetBestMove().src;
            var to = enemyAI.GetBestMove().des;
            currentlySelectedPiece = board.GetPieceAt(from);
            SwitchTurn();
            MovePieceModel(from, to);
        }
    }
}
