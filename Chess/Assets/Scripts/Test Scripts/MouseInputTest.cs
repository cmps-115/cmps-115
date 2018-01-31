using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputTest : MonoBehaviour {

    public DrawBoard drawBoard;
	
	// Update is called once per frame
	void Update ()
    {
		if (DrawBoard.IsClicked)
        {
            drawBoard.HighLightGrid(DrawBoard.SquarePosition);
        }
        else if (DrawPiece.IsClicked)
        {
            drawBoard.HighLightGrid(DrawPiece.PiecePosition);
        }
	}
}
