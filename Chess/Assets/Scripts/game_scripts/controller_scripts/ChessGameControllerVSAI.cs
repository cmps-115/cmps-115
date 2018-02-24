using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using ChessGlobals;

public class ChessGameControllerVSAI : ChessGameController
{

    private AI enemyAI;
    private bool aiMoved = false;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        enemyAI = new AI(this, 2);
    }

    // Update is called once per frame
    protected override void Update()
    {
        bool pieceDoneMoving = Time.time - moveTime > waitTime && isPieceMove == true;
        if (pieceDoneMoving)
        {
            startMoveTime = Time.time;
            isPieceMove = false;
            SwitchTurn();
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
                    else drawBoard.ClearHighlights();
                    if (!legalMovesForAPiece.Contains(movePieceTo)) movePieceTo = Vector3.down;
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
                aiMoved = false;
            }
        }
        else if (gameState.getState() == GameState.BLACK_TURN && !aiMoved)
        {
            GetAIMove();
        }
        else if (gameState.getState() == GameState.BLACK_TURN && aiMoved)
        {
            if (movePieceFrom != Vector3.down && movePieceTo != Vector3.down)
            {
                MovePieceModel(movePieceFrom, movePieceTo);
            }
        }
    }

    private void GetAIMove()
    {
        aiMoved = true;
        movePieceFrom = enemyAI.GetBestMove().src;
        movePieceTo = enemyAI.GetBestMove().des;
        currentlySelectedPiece = board.GetPieceAt(movePieceFrom);
    }
}
