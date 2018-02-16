// Programmer: Ari Berkson
//
// Tests various functions from DrawBoard, DrawPiece, and MoveModel.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputTest : MonoBehaviour {

    public DrawBoard drawBoard;

    private MoveModel mm1;

    // Initialize positions to spots the user cannot choose. I chose Vector3.down because it contains a negative value and be unity has a shorthand for it.
    private Vector3 movePieceFrom = Vector3.down;
    private Vector3 movePieceTo = Vector3.down;
	
    private void Start()
    {
        mm1 = gameObject.AddComponent<MoveModel>();
    }

	// Update is called once per frame
	private void Update ()
    {
        // Check to see if a position is clicked after already selecting a piece.
        if ((DrawBoard.IsClicked || DrawPiece.IsClicked) && movePieceFrom != Vector3.down)
        {
            movePieceTo = DrawBoard.IsClicked ? DrawBoard.SquarePosition : DrawPiece.PiecePosition;
            drawBoard.HighLightGrid(movePieceTo);
        }
        else if (DrawPiece.IsClicked && movePieceFrom == Vector3.down)
        {
            movePieceFrom = DrawPiece.PiecePosition;
            DrawPiece.HighlightPiece(1.25f);
            drawBoard.HighLightGrid(movePieceFrom);
        }
        
        // If the user has clicked on a space to move and a piece to move, move the piece and reset the vectors to numbers the user cannot choose.
        // In the real game we would also have to check if it is a valid move.
        if (movePieceFrom != Vector3.down && movePieceTo != Vector3.down)
        {
            mm1.MovePiece(movePieceFrom, movePieceTo);
            movePieceTo = movePieceFrom = Vector3.down;
            DrawPiece.ClearHighlight();
            drawBoard.ClearHighlights();
        }
	}
}
