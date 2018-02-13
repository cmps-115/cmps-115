using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessGlobals
{
    public enum PIECE_TYPES { Pawn, Rook, Knight, Bishop, Queen, King };
	public enum GAME_STATE {WHITE_WIN, BLACK_WIN, DRAW};
	public class COLOR
	{
		public static bool BLACK = false;
		public static bool WHITE = true;
	}
	//public enum COLOR {BLACK = false, WHITE = true};
	//public const bool BLACK = false;
	//public const bool WHITE = true;
}