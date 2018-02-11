using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//just have to test

public class Rules : MonoBehaviour
{


		
	static public List<Vector2> LegalPawnMove(Piece p,Board b)
	{
		int team;
		int xc, yc;
		List<Vector2> poss = new List<Vector2>() ;

		// 1 black
		// 2 white
		if (p.team == 0)
			team = 1;
		else
			team = 2;


		xc = p.PiecePosition.GetX ();
		yc = p.PiecePosition.GetY ();
		
		// assuming for the board that 0 = free , 1 = black, = 2 = white

		// assuming that white starts at the rows 0,1
		if (team == 2) 
		{

			// to make sure it doesnt go past 
			if (yc < 7) 
			{
				//if the square ahead if empty 
				if (!b.isOccupied(new Vector2(xc,yc+1)) ) 
				{
					poss.Add (new Vector2 (xc, yc + 1));
				}			

			}

			// to make sure its doesnt go past 16
			if (yc < 6 ) 
			{
				// if square that is at y+2 is empty and at starting position
				if (!b.isOccupied(new Vector2(xc,yc+2)) && yc == 1) 
				{
					poss.Add (new Vector2 (xc, yc + 2));
				}			

			}

			// diagonal to left
			if (xc > 0) 
			{
				if (b.squares [xc - 1,yc + 1].GetPiece().team != p.team) {
					poss.Add (new Vector2 (xc - 1, yc + 1));
				}
			}

			// diagonal to right
			if (xc < 7) 
			{
				if (b.squares [xc + 1,yc + 1].GetPiece().team != p.team) 
				{
					poss.Add (new Vector2 (xc + 1, yc + 1));
				}
			}
				
		} else 
		{
			// to make sure it doesnt go past 0
			if (yc > 0) 
			{
				//if the square ahead if empty 
				if (!b.isOccupied(new Vector2(xc,yc-1)))
				{
					poss.Add (new Vector2 (xc, yc - 1));
				}			

			}

			// to make sure its doesnt go past 16
			if (yc > 1)
			{
				//if square that is at y-2 is empty
				if (!b.isOccupied(new Vector2(xc,yc-2)) && yc == 6)
				{
					poss.Add (new Vector2 (xc, yc - 2));
				}			

			}

			// diagonal to left
			if (xc < 7) 
			{
				if (b.squares [xc+1,yc - 1].GetPiece().team != p.team)
				{
					poss.Add (new Vector2 (xc + 1, yc - 1));
				}
			}

			// diagonal to right
			if (xc > 0) 
			{
				if (b.squares [xc - 1,yc - 1].GetPiece().team != p.team) 
				{
					poss.Add (new Vector2 (xc - 1, yc - 1));
				}
			}

		}

		return poss;
	
	}

	static public List<Vector2> LegalRookMove(Piece p,Board b)
	{
		int team;
		int xc, yc;
		List<Vector2> poss = new List<Vector2>() ;

		// 1 black
		// 2 white
		if (p.team == 0)
			team = 1;
		else
			team = 2;

		xc = p.PiecePosition.GetX ();
		yc = p.PiecePosition.GetY ();

		// if its a white piece
		if (team == 2) 
		{

			// check for moves in front
			int i1 = yc + 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(xc,i1)) && i1 < 8) 
			{
				poss.Add (new Vector2 (xc, i1));
				i1++;
			}
			// if the next is occupied by a black piece
			if (i1 < 7) 
			{
				if (b.squares [xc,i1].GetPiece().team != p.team)
				{
					poss.Add (new Vector2 (xc, i1));
				}
			}
			// check for moves in back
			i1 = yc - 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(xc,i1)) && i1 > 0) 
			{
				poss.Add (new Vector2 (xc, i1));
				i1--;
			}
			// if the prev is occupied by a black piece
			if (i1 > -1) 
			{
				if (b.squares [xc,i1].GetPiece().team != p.team) 
				{
					poss.Add (new Vector2 (xc, i1));
				}
			}


			// check for moves to the right
			i1 = xc + 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(i1,yc)) && i1 < 8) 
			{
				poss.Add (new Vector2 (i1, yc));
				i1++;
			}
			// if the next is occupied by a black piece
			if (i1 < 7) 
			{
				if (b.squares [i1,yc].GetPiece().team != p.team) 
				{
					poss.Add (new Vector2 (i1, yc));
				}
			}

			// check for moves to the left
			i1 = xc - 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(i1,yc)) && i1 > 0)
			{
				poss.Add (new Vector2 (i1, yc));
				i1--;
			}
			// if the prev is occupied by a black piece
			if (i1 > -1) 
			{
				if (b.squares [i1,yc].GetPiece().team != p.team)
				{
					poss.Add (new Vector2 (i1, yc));
				}
			}

		} else {

			// check for moves in back
			int i1 = yc + 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(xc,i1))&& i1 < 8) 
			{
				poss.Add (new Vector2 (xc, i1));
				i1++;
			}
			// if the next is occupied by a white piece
			if (i1 < 7) 
			{
				if (b.squares [xc,i1].GetPiece().team != p.team)
				{
					poss.Add (new Vector2 (xc, i1));
				}
			}
			// check for moves in front
			i1 = yc - 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(xc,i1))&& i1 > 0) 
			{
				poss.Add (new Vector2 (xc, i1));
				i1--;
			}
			// if the prev is occupied by a white piece
			if (i1 > -1) 
			{
				if (b.squares [xc,i1].GetPiece().team != p.team) 
				{
					poss.Add (new Vector2 (xc, i1));
				}
			}


			// check for moves to the left
			i1 = xc + 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(i1,yc))&& i1 < 8)
			{
				poss.Add (new Vector2 (i1, yc));
				i1++;
			}
			// if the next is occupied by a black piece
			if (i1 < 7)
			{
				if (b.squares [i1,yc].GetPiece().team != p.team) 
				{
					poss.Add (new Vector2 (i1, yc));
				}
			}

			// check for moves to the right
			i1 = xc - 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(i1,yc)) && i1 > 0)
			{
				poss.Add (new Vector2 (i1, yc));
				i1--;
			}
			// if the prev is occupied by a black piece
			if (i1 > -1) 
			{
				if (b.squares [i1,yc].GetPiece().team != p.team) 
				{
					poss.Add (new Vector2 (i1, yc));
				}
			}
		}

		return poss;

	}

	static public List<Vector2> LegalBishopMove(Piece p,Board b)
	{
		int team;
		int xc, yc;
		List<Vector2> poss = new List<Vector2>() ;

		// 1 black
		// 2 white
		if (p.team == 0)
			team = 1;
		else
			team = 2;

		xc = p.PiecePosition.GetX ();
		yc = p.PiecePosition.GetY ();

		// if its a white piece
		if (team == 2) 
		{

			// check for moves in diagonally to the right and up
			int i1 = yc + 1;
			int i2 = xc + 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(i2,i1))&& i1 < 8 && i2<8) 
			{
				poss.Add (new Vector2 (i2, i1));
				i1++;
				i2++;
			}
			// if the next is occupied by a black piece
			if (i1 < 7 && i2 <7 )
			{
				if (b.squares [i2,i1].GetPiece().team != p.team == 1) 
				{
					poss.Add (new Vector2 (i2, i1));
				}
			}
			// check for moves in down left
			i1 = yc - 1;
			i2 = xc - 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(i2,i1)) && i1 > 0 && i2>0)
			{
				poss.Add (new Vector2 (i2, i1));
				i1--;
				i2--;
			}
			// if the prev is occupied by a black piece
			if (i1 > -1 && i2>-1) 
			{
				if (b.squares [i2,i1].GetPiece().team != p.team) 
				{
					poss.Add (new Vector2 (i2, i1));
				}
			}


			// check for moves to the right and down
			i1 = xc + 1;
			i2 = yc - 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(i1,i2)) && i1 < 8 && i2 > 0)
			{
				poss.Add (new Vector2 (i1, i2));
				i1++;
				i2--;
			}
			// if the next is occupied by a black piece
			if (i1 < 7 && i2>-1) 
			{
				if (b.squares [i1,i2].GetPiece().team != p.team) 
				{
					poss.Add (new Vector2 (i1, i2));
				}
			}

			// check for moves to the left down
			i1 = xc - 1;
			i2 = yc - 1;
			// if its empty, it can move there
			while (b [i1,i2] == 0 && i1 > 0 && i2>0)
			{
				poss.Add (new Vector2 (i1, i2));
				i1--;
				i2--;
			}
			// if the prev is occupied by a black piece
			if (i1 > -1 && i2>-1) 
			{
				if (b.squares [i1,i2].GetPiece().team != p.team) 
				{
					poss.Add (new Vector2 (i1, i2));
				}
			}

		} else 
		{

			// check for moves in diagonally to the left and down 
			int i1 = yc + 1;
			int i2 = xc + 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(i2,i1)) && i1 < 8 && i2<8) 
			{
				poss.Add (new Vector2 (i2, i1));
				i1++;
				i2++;
			}
			// if the next is occupied by a white piece
			if (i1 < 7 && i2 <7 ) 
			{
				if (b.squares [i2,i1].GetPiece().team != p.team) 
				{
					poss.Add (new Vector2 (i2, i1));
				}
			}
			// check for moves in up right
			i1 = yc - 1;
			i2 = xc - 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(i2,i1))&& i1 > 0 && i2>0) 
			{
				poss.Add (new Vector2 (i2, i1));
				i1--;
				i2--;
			}
			// if the prev is occupied by a white piece
			if (i1 > -1 && i2>-1) 
			{
				if (b.squares [i2,i1].GetPiece().team != p.team) 
				{
					poss.Add (new Vector2 (i2, i1));
				}
			}


			// check for moves to the left and up
			i1 = xc + 1;
			i2 = yc - 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(i1,i2)) && i1 < 8 && i2 > 0) 
			{
				poss.Add (new Vector2 (i1, i2));
				i1++;
				i2--;

			}
			// if the next is occupied by a white piece
			if (i1 < 7 && i2>-1) 
			{
				if (b.squares [i1,i2].GetPiece().team != p.team) 
				{
					poss.Add (new Vector2 (i1, i2));
				}
			}

			// check for moves to the right up
			i1 = xc - 1;
			i2 = yc - 1;
			// if its empty, it can move there
			while (!b.isOccupied(new Vector2(i1,i2)) && i1 > 0 && i2>0) 
			{
				poss.Add (new Vector2 (i1, i2));
				i1--;
				i2--;
			}
			// if the prev is occupied by a white piece
			if (i1 > -1 && i2>-1) 
			{
				if (b.squares [i1,i2].GetPiece().team != p.team)
				{
					poss.Add (new Vector2 (i1, i2));
				}
			}
		}
		return poss;
	}

	// it seems like user input is prompted in Update()
	// https://docs.unity3d.com/ScriptReference/Input-inputString.html
	// I think in the controller update, when dealing with the pawns there should
	// be a conditional checking if its at one of the edges
	// if it then the player would be prompted with the coices to upgrade
	// the coice would be a string which would then be passed to and the 
	// would be promoted accordingly
	public bool Promote (Piece p , string choice )
	{
		if (choice == "Bishop" || choice == "bishop") {
			p.type = PIECE_TYPES.Bishop;
			return true;
		} else if (choice == "Rook" || choice == "rook") {
			p.type = PIECE_TYPES.Rook;
			return true;
		} else if (choice == "Knight" || choice == "knight") {
			p.type = PIECE_TYPES.Knight;
			return true;
		} else if (choice == "Queen" || choice == "queen") {
			p.type = PIECE_TYPES.Queen; 
			return true;
		} else
			return false;
	}
}

