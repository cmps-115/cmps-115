using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessGlobals
{
    public enum PIECE_TYPES { PAWN, ROOK, KNIGHT, BISHOP, QUEEN, KING};
	public enum GAME_STATE {BLACK = 0,WHITE = 1, WHITE_WIN = 2, BLACK_WIN = 3, DRAW = 4};
	public enum MODES{PVP = 0, PVC = 1, CVC = 2}
	public class COLOR
	{
		public static bool BLACK = false;
		public static bool WHITE = true;
	}
	public class Tuple2<T1,T2>
	{
		public T1 t1;
		public T2 t2;
		public Tuple2(T1 t1, T2 t2)
		{
			this.t1 = t1;
			this.t2 = t2;
		}
	}
	public class Tuple3<T1,T2,T3>
	{
		public T1 t1;
		public T2 t2;
		public T3 t3;
		public Tuple3(T1 t1, T2 t2, T3 t3)
		{
			this.t1 = t1;
			this.t2 = t2;
			this.t3 = t3;
		}
	}
	public static class BoardConstants
	{
		public const int BOARD_MINIMUM = 0;
		public const int BOARD_MAXIMUM = 7;
		public const int TEAM_ROWS = 2;
	}
	public class Move
	{
		public Vector2 src;
		public Vector2 des;
		public Piece piece;
		public Move ()
		{}
		public Move (Piece piece, Vector2 des )
		{
			this.src = piece.GetPiecePosition();
			this.des = des;
			this.piece = piece;
		}
	}
}