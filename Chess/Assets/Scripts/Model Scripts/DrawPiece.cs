﻿// Programmer: Ari Berkson
//
// This module places the chess peices on the board.
// This module also detects when the user clicks on a chess peice model.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessGlobals;

//Shorten & Clarify Declarations 
using PieceTypeAndPosition = ChessGlobals.Tuple2<ChessGlobals.PIECE_TYPES, UnityEngine.Vector2>;
using ListOfPieceTypesAndPositions = System.Collections.Generic.List<ChessGlobals.Tuple2<ChessGlobals.PIECE_TYPES, UnityEngine.Vector2>>;
using System;

public class DrawPiece : MonoBehaviour {

    public GameObject king;
    public GameObject queen;
    public GameObject bishop;
    public GameObject knight;
    public GameObject castle;
    public GameObject pawn;

    public Material firstTeam;
    public Material secondTeam;

    private static Vector2 piecePosition = new Vector2(-1, -1);
	private static Renderer pieceRenderer;
	private static Material mat;
    private static bool clicked = false;
	private static bool highlighted = false;

    private const float MODEL_OFFSET = 0.5f;
	private const float MODEL_OFFSET_Y = 0.05f;


	private const int RENDERQUEUE = 3000;
	private const float OUTLINE_THICKNESS = 1.20f;
	private const int HIGHLIGHT_FREQUENCY = 5;


	public static void HighlightPiece()
	{
        if (pieceRenderer != null)
        {
            highlighted = true;
            mat.renderQueue = RENDERQUEUE + 1;
        }
	}

	public static void ClearHighlight()
	{
        if (pieceRenderer != null)
        {
            highlighted = false;
            mat.SetFloat("_OutlineWidth", 0);
            mat.renderQueue = RENDERQUEUE;
        }
	}

    /// <summary>
    /// Returns the positions of the last chess piece clicked.
    /// </summary>
    public static Vector2 PiecePosition
    {
        get { return piecePosition;}
    }

    public static bool IsClicked
    { 
        get
        {
            DetectClick();
            return clicked;
        }
    }

	public Tuple2<ListOfPieceTypesAndPositions, ListOfPieceTypesAndPositions> InitPieces()
    {
		var initialWhiteTeamPositions = PlaceWhiteTeam();
		var initialBlackTeamPositions = PlaceBlackTeam();
		return new Tuple2<ListOfPieceTypesAndPositions, ListOfPieceTypesAndPositions>(initialBlackTeamPositions, initialWhiteTeamPositions);
    }

    public void ChangeModelType(Vector2 pos, string type, int team)
    {
        GameObject new_Model = null;
        if (type == "Rook")
        {
            new_Model = Instantiate(castle);
        }
        else if (type == "Bishop")
        {
            new_Model = Instantiate(bishop);
        }
        else if (type == "Queen")
        {
            new_Model = Instantiate(queen);
        }
        else if (type == "Knight")
        {
            new_Model = Instantiate(knight);
        }

        Material mat = firstTeam;
        if (team == Teams.BLACK_TEAM)
        {
            mat = secondTeam;
        }

        Vector3 newPos = new Vector3(pos.x + MODEL_OFFSET, MODEL_OFFSET_Y, pos.y + MODEL_OFFSET);
        Destroy(MoveModel.CheckAt(pos));
        new_Model.transform.position = newPos;
        new_Model.GetComponent<Renderer>().material = mat;
    }

    #region Place Teams
    private GameObject Spawn(int x, int y, GameObject model)
    {
        return Instantiate(model, new Vector3(x + MODEL_OFFSET, MODEL_OFFSET_Y, y + MODEL_OFFSET), model.transform.rotation);
    }

	private ListOfPieceTypesAndPositions PlaceWhiteTeam()
    {
		ListOfPieceTypesAndPositions positions = new ListOfPieceTypesAndPositions();
		for (int y = ChessGlobals.BoardConstants.BOARD_MINIMUM; y < ChessGlobals.BoardConstants.TEAM_ROWS; ++y)
        {
            GameObject model = null;
			for (int x = ChessGlobals.BoardConstants.BOARD_MINIMUM; x <= ChessGlobals.BoardConstants.BOARD_MAXIMUM; ++x)
            {
                if (y == 1)
                {
                    model = Spawn(x, y, pawn);
					PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition (PIECE_TYPES.PAWN, new Vector2 (x, y));
					positions.Add (typeAndPos);
                }
                else
                {
                    if (x == 0 || x == 7)
                    {
                        model = Spawn(x, y, castle);
						PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition (PIECE_TYPES.ROOK, new Vector2 (x, y));
						positions.Add (typeAndPos);
                    }
                    else if (x == 1 || x == 6)
                    {
                        model = Spawn(x, y, knight);
						PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition (PIECE_TYPES.KNIGHT, new Vector2 (x, y));
						positions.Add (typeAndPos);
                    }
                    else if (x == 2 || x == 5)
                    {
                        model = Spawn(x, y, bishop);
						PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition (PIECE_TYPES.BISHOP, new Vector2 (x, y));
						positions.Add (typeAndPos);
                    }
                    else if (x == 4)
                    {
                        model = Spawn(x, y, king);
						PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition (PIECE_TYPES.KING, new Vector2 (x, y));
						positions.Add (typeAndPos);
                    }
                    else if (x == 3)
                    {
                        model = Spawn(x, y, queen);
						PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition (PIECE_TYPES.QUEEN, new Vector2 (x, y));
						positions.Add (typeAndPos);
                    }
                }
                model.GetComponent<MeshRenderer>().material = firstTeam;
            }
        }
		return positions;
    }

	private ListOfPieceTypesAndPositions PlaceBlackTeam()
    {
		List<Tuple2<PIECE_TYPES,Vector2>> positions = new List<Tuple2<PIECE_TYPES,Vector2>>();
		for (int y = ChessGlobals.BoardConstants.BOARD_MAXIMUM; y > ChessGlobals.BoardConstants.BOARD_MAXIMUM - ChessGlobals.BoardConstants.TEAM_ROWS; --y)
        {
            GameObject model = null;
			for (int x = ChessGlobals.BoardConstants.BOARD_MINIMUM; x <= ChessGlobals.BoardConstants.BOARD_MAXIMUM; ++x)
            {
                if (y == 6)
                {
                    model = Spawn(x, y, pawn);
					PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition (PIECE_TYPES.PAWN, new Vector2 (x, y));
					positions.Add(typeAndPos);
                }
                else
                {
                    if (x == 0 || x == 7)
                    {
                        model = Spawn(x, y, castle);
						PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition (PIECE_TYPES.ROOK, new Vector2 (x, y));
						positions.Add (typeAndPos);
                    }
                    else if (x == 1 || x == 6)
                    {
                        model = Spawn(x, y, knight);
						PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition (PIECE_TYPES.KNIGHT, new Vector2 (x, y));
						positions.Add (typeAndPos);
                    }
                    else if (x == 2 || x == 5)
                    {
                        model = Spawn(x, y, bishop);
						PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition (PIECE_TYPES.BISHOP, new Vector2 (x, y));
						positions.Add (typeAndPos);
                    }
                    else if (x == 4)
                    {
                        model = Spawn(x, y, king);
						PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition (PIECE_TYPES.KING, new Vector2 (x, y));
						positions.Add (typeAndPos);
                    }
                    else if (x == 3)
                    {
                        model = Spawn(x, y, queen);
						PieceTypeAndPosition typeAndPos = new PieceTypeAndPosition (PIECE_TYPES.QUEEN, new Vector2 (x, y));
						positions.Add (typeAndPos);
                    }
                }
                model.GetComponent<MeshRenderer>().material = secondTeam;
            }
        }
		return positions;
    }

    #endregion


    /// <summary>
    /// Detects when a piece has been clicked on.
    /// Should be called in an update function.
    /// </summary>
    private static void DetectClick()
    {
        //Checks for left mouse button down.
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;

            //create a ray from the camera through the mouse cursor.
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "ChessPiece")
                {
                    if (highlighted)
                    {
                        ClearHighlight();
                    }

					pieceRenderer = hit.transform.GetComponent<Renderer>();
                    piecePosition = new Vector2(hit.transform.position.x - MODEL_OFFSET, hit.transform.position.z - MODEL_OFFSET);
                    clicked = true;
					mat = pieceRenderer.material;
					mat.shader = Shader.Find("StandardOutline");
					return;
                }
            }
        }
        clicked = false;
    }

	private void Update()
	{
		if (highlighted)
		{
			HighlightBreath();
		}
	}

	private void HighlightBreath()
	{
		var dThickness = (OUTLINE_THICKNESS - 1) / 2;
		var sin = dThickness * Mathf.Sin(HIGHLIGHT_FREQUENCY * Time.time);
		var thickness = (OUTLINE_THICKNESS - dThickness / 2) + sin;
		mat.SetFloat("_OutlineWidth", thickness);
    }
}
