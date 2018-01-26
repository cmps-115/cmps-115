using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//not done yet
// have to the bishop
public class Rules : MonoBehaviour
{


		
	public List<Vector2> LegalPawnMove(Piece p,Board b){
		int team;
		int xc, yc;
		List<Vector2> poss = new List<Vector2>() ;

		//1 black
		//2 white
		if (p.team == 0)
			team = 1;
		else
			team = 2;


		xc = p.PiecePosition.GetX ();
		yc = p.PiecePosition.GetY ();
		
		//assuming for the board that 0 = free , 1 = black, = 2 = white

		//assuming that white starts at the rows 0,1
		if (team == 2) {

			//to make sure it doesnt go past 
			if (yc < 7) {
				//if the square ahead if empty 
				if (b.squares [xc] [yc + 1] == 0) {
					poss.Add (new Vector2 (xc, yc + 1));
				}			
				//if square is occupied by opposite team
				else if (b.squares [xc] [yc + 1] != 0 && b.squares [xc] [yc + 1] == 1)
					poss.Add (new Vector2 (xc, yc + 1));
			}

			//to make sure its doesnt go past 16
			if (yc < 6 ) {
				//if square that is at y+2 is empty and at starting position
				if (b.squares [xc] [yc + 2] == 0 && yc == 1) {
					poss.Add (new Vector2 (xc, yc + 2));
				}			
				//if square is occupied by opposite team
				else if (b.squares [xc] [yc + 2] != 0 && b.squares [xc] [yc + 2] == 1 && yc == 1)
					poss.Add (new Vector2 (xc, yc + 2));
			}

			//diagonal to left
			if (xc > 0) {
				if (b.squares [xc - 1] [yc + 1] == 1) {
					poss.Add (new Vector2 (xc - 1, yc + 1));
				}
			}

			//diagonal to right
			if (xc < 7) {
				if (b.squares [xc + 1] [yc + 1] == 1) {
					poss.Add (new Vector2 (xc + 1, yc + 1));
				}
			}
				
		} else {
			//to make sure it doesnt go past 0
			if (yc > 0) {
				//if the square ahead if empty 
				if (b.squares [xc] [yc - 1] == 0) {
					poss.Add (new Vector2 (xc, yc - 1));
				}			
				//if square is occupied by opposite team
				else if (b.squares [xc] [yc - 1] != 0 && b.squares [xc] [yc - 1] == 2)
					poss.Add (new Vector2 (xc, yc - 1));
			}

			//to make sure its doesnt go past 16
			if (yc > 1) {
				//if square that is at y-2 is empty
				if (b.squares [xc] [yc - 2] == 0 && yc == 6) {
					poss.Add (new Vector2 (xc, yc - 2));
				}			
				//if square is occupied by opposite team
				else if (b.squares [xc] [yc - 2] != 0 && b.squares [xc] [yc - 2] == 2 yc == 6)
					poss.Add (new Vector2 (xc, yc - 2));
			}

			//diagonal to left
			if (xc < 7) {
				if (b.squares [xc+1] [yc - 1] == 2) {
					poss.Add (new Vector2 (xc + 1, yc - 1));
				}
			}

			//diagonal to right
			if (xc > 0) {
				if (b.squares [xc - 1] [yc - 1] == 2) {
					poss.Add (new Vector2 (xc - 1, yc - 1));
				}
			}

		}

		return poss;
	
	}

	public List<Vector2> LegalRookMove(Piece p,Board b){
		int team;
		int xc, yc;
		List<Vector2> poss = new List<Vector2>() ;

		//1 black
		//2 white
		if (p.team == 0)
			team = 1;
		else
			team = 2;

		xc = p.PiecePosition.GetX ();
		yc = p.PiecePosition.GetY ();

		//if its a white piece
		if (team == 2) {

			//check for moves in front
			int i1 = yc + 1;
			//if its empty, it can move there
			while (b [xc] [i1] == 0 && i1 < 8) {
				poss.Add (new Vector2 (xc, i1));
				i1++;
			}
			//if the next is occupied by a black piece
			if (i1 < 7) {
				if (b [xc] [i1] == 1) {
					poss.Add (new Vector2 (xc, i1));
				}
			}
			//check for moves in back
			i1 = yc - 1;
			//if its empty, it can move there
			while (b [xc] [i1] == 0 && i1 > 0) {
				poss.Add (new Vector2 (xc, i1));
				i1--;
			}
			//if the prev is occupied by a black piece
			if (i1 > -1) {
				if (b [xc] [i1] == 1) {
					poss.Add (new Vector2 (xc, i1));
				}
			}


			//check for moves to the right
			i1 = xc + 1;
			//if its empty, it can move there
			while (b [i1] [yc] == 0 && i1 < 8) {
				poss.Add (new Vector2 (i1, yc));
				i1++;
			}
			//if the next is occupied by a black piece
			if (i1 < 7) {
				if (b [i1] [yc] == 1) {
					poss.Add (new Vector2 (i1, yc));
				}
			}

			//check for moves to the left
			i1 = xc - 1;
			//if its empty, it can move there
			while (b [i1] [yc] == 0 && i1 > 0) {
				poss.Add (new Vector2 (i1, yc));
				i1--;
			}
			//if the prev is occupied by a black piece
			if (i1 > -1) {
				if (b [i1] [yc] == 1) {
					poss.Add (new Vector2 (i1, yc));
				}
			}

		} else {

			//check for moves in back
			int i1 = yc + 1;
			//if its empty, it can move there
			while (b [xc] [i1] == 0 && i1 < 8) {
				poss.Add (new Vector2 (xc, i1));
				i1++;
			}
			//if the next is occupied by a white piece
			if (i1 < 7) {
				if (b [xc] [i1] == 2) {
					poss.Add (new Vector2 (xc, i1));
				}
			}
			//check for moves in front
			i1 = yc - 1;
			//if its empty, it can move there
			while (b [xc] [i1] == 0 && i1 > 0) {
				poss.Add (new Vector2 (xc, i1));
				i1--;
			}
			//if the prev is occupied by a white piece
			if (i1 > -1) {
				if (b [xc] [i1] == 2) {
					poss.Add (new Vector2 (xc, i1));
				}
			}


			//check for moves to the left
			i1 = xc + 1;
			//if its empty, it can move there
			while (b [i1] [yc] == 0 && i1 < 8) {
				poss.Add (new Vector2 (i1, yc));
				i1++;
			}
			//if the next is occupied by a black piece
			if (i1 < 7) {
				if (b [i1] [yc] == 2) {
					poss.Add (new Vector2 (i1, yc));
				}
			}

			//check for moves to the right
			i1 = xc - 1;
			//if its empty, it can move there
			while (b [i1] [yc] == 0 && i1 > 0) {
				poss.Add (new Vector2 (i1, yc));
				i1--;
			}
			//if the prev is occupied by a black piece
			if (i1 > -1) {
				if (b [i1] [yc] == 2) {
					poss.Add (new Vector2 (i1, yc));
				}
			}
		}


	}
}

