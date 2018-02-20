using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace ChessGlobals
{
    public enum PIECE_TYPES { PAWN, ROOK, KNIGHT, BISHOP, QUEEN, KING};
	public enum GAME_STATE {BLACK_TURN = 0, WHITE_TURN = 1, WHITE_WIN = 2, BLACK_WIN = 3, DRAW = 4};
	public enum TEAM {BLACK_TEAM = 0, WHITE_TEAM = 1};
	public enum MODES{PVP = 0, PVC = 1, CVC = 2}
	public class KingInCheck
	{
		public bool WhiteKingInCheck = false;
		public bool BlackKingInCheck = false;
	}
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
	public class Teams
	{
		public static int BlackTeam = 0;
		public static int WhiteTeam = 1;
		private int team;
		Teams()
		{
			team = -1;
		}
		Teams(int team)
		{
			setTeam (team);
		}
		public void setTeam (int team)
		{
			Assert.AreEqual (team == 0 || team == 1, true);
			this.team = team;
		}
		public int getTeam()
		{
			return this.team;
		}
	}
	public class GameState
	{
		//public const int BLACK_TURN = 0, WHITE_TURN = 1, WHITE_WIN = 2, BLACK_WIN = 3, DRAW = 4
		public static int BlackTurn = 0;
		public static int WhiteTurn = 1;
		public static int WhiteWin = 2;
		public static int BlackeWin = 3;
		public static int Draw = 4;
		private int currentState;
		public GameState()
		{
			currentState = -1;
		}
		public GameState(int state)
		{
			setState (state);
		}
		public void setState(int state)
		{
			Assert.AreEqual (
				state == BlackTurn ||
				state == WhiteTurn ||
				state == WhiteWin ||
				state == BlackeWin ||
				state == Draw, true
			);
			currentState = state;
		}
		public int getState()
		{
			return this.currentState;
		}

	}
	public static class BoardConstants
	{
		public const int BOARD_MINIMUM = 0;
		public const int BOARD_MAXIMUM = 7;
		public const int TEAM_ROWS = 2;
		public const bool OCCUPIED_SQUARE = true;
		public const bool UNOCCUPIED_SQUARE = false;
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