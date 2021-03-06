using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace ChessGlobals
{
    public enum PIECE_TYPES { PAWN, ROOK, KNIGHT, BISHOP, QUEEN, KING };
    public enum MODES { PVP = 0, PVC = 1, CVC = 2 }
    public class KingInCheck
    {
        private static bool WhiteKingInCheck;
        private static bool BlackKingInCheck;

        public KingInCheck()
        {
            WhiteKingInCheck = false;
            BlackKingInCheck = false;
        }

        public static void SetWhiteCheck(bool TF)
        {
            WhiteKingInCheck = TF;
        }

        public static void SetBlackCheck(bool TF)
        {
            BlackKingInCheck = TF;
        }

        public static bool IsWhiteInCheck()
        {
            return WhiteKingInCheck;
        }

        public static bool IsBlackInCheck()
        {
            return BlackKingInCheck;
        }

    }

    [Serializable]
    public class Tuple2<T1, T2>
    {
        public T1 t1;
        public T2 t2;
        public Tuple2(T1 t1, T2 t2)
        {
            this.t1 = t1;
            this.t2 = t2;
        }
    }

    [Serializable]
    public class Tuple3<T1, T2, T3>
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

    [Serializable]
    public class Teams
    {
        public static int BLACK_TEAM = 0;
        public static int WHITE_TEAM = 1;
        private int team;

        public Teams()
        {
            team = -1;
        }

        public Teams(int team)
        {
            setTeam(team);
        }

        public void setTeam(int team)
        {
            Assert.AreEqual(team == BLACK_TEAM || team == WHITE_TEAM, true);
            this.team = team;
        }

        public int getTeam()
        {
            return this.team;
        }
    }

    public class GameState
    {
        public static int BLACK_TURN = 0;
        public static int WHITE_TURN = 1;
        public static int WHITE_WIN = 2;
        public static int BLACK_WIN = 3;
        public static int DRAW = 4;
        private int currentState;

        public GameState()
        {
            currentState = -1;
        }

        public GameState(int state)
        {
            setState(state);
        }

        public void setState(int state)
        {
            Assert.AreEqual(
                state == BLACK_TURN ||
                state == WHITE_TURN ||
                state == WHITE_WIN ||
                state == BLACK_WIN ||
                state == DRAW, true
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

        public Move()
        { }

        public Move(Piece piece, Vector2 des)
        {
            this.src = piece.GetPiecePosition();
            this.des = des;
            this.piece = piece;
        }
    }
}