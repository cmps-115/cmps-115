﻿//Programmer: Ari Berkson
//
//This module places the chess peices on the board.
//This module also detects when the user clicks on a chess peice model.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private static bool clicked = false;

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

    // Use this for initialization
    private void Start ()
    {
        InitPieces();
    }

    private void InitPieces()
    {
        PlaceFirstTeam();
        PlaceSecondTeam();
    }

    #region Place Teams
    private void PlaceFirstTeam()
    {
        for (int y = 0; y < 2; ++y)
        {
            GameObject model = null;
            for (int x = 0; x < 8; ++x)
            {
                if (y == 1)
                {
                    model = Instantiate(pawn, new Vector3(x + 0.5f, 0, y + 0.5f), pawn.transform.rotation);
                }
                else
                {
                    if (x == 0 || x == 7)
                    {
                        model = Instantiate(castle, new Vector3(x + 0.5f, 0, y + 0.5f), castle.transform.rotation);
                    }
                    else if (x == 1 || x == 6)
                    {
                        model = Instantiate(knight, new Vector3(x + 0.5f, 0, y + 0.5f), knight.transform.rotation);
                    }
                    else if (x == 2 || x == 5)
                    {
                        model = Instantiate(bishop, new Vector3(x + 0.5f, 0, y + 0.5f), bishop.transform.rotation);
                    }
                    else if (x == 3)
                    {
                        model = Instantiate(king, new Vector3(x + 0.5f, 0, y + 0.5f), king.transform.rotation);
                    }
                    else if (x == 4)
                    {
                        model = Instantiate(queen, new Vector3(x + 0.5f, 0, y + 0.5f), queen.transform.rotation);
                    }
                }
                model.GetComponent<MeshRenderer>().material = firstTeam;
            }
        }
    }

    private void PlaceSecondTeam()
    {
        for (int y = 7; y > 5; --y)
        {
            GameObject model = null;
            for (int x = 0; x < 8; ++x)
            {
                if (y == 6)
                {
                    model = Instantiate(pawn, new Vector3(x + 0.5f, 0, y + 0.5f), pawn.transform.rotation);
                }
                else
                {
                    if (x == 0 || x == 7)
                    {
                        model = Instantiate(castle, new Vector3(x + 0.5f, 0, y + 0.5f), castle.transform.rotation);
                    }
                    else if (x == 1 || x == 6)
                    {
                        model = Instantiate(knight, new Vector3(x + 0.5f, 0, y + 0.5f), Quaternion.Euler(-90, 225, 0));
                    }
                    else if (x == 2 || x == 5)
                    {
                        model = Instantiate(bishop, new Vector3(x + 0.5f, 0, y + 0.5f), bishop.transform.rotation);
                    }
                    else if (x == 3)
                    {
                        model = Instantiate(king, new Vector3(x + 0.5f, 0, y + 0.5f), king.transform.rotation);
                    }
                    else if (x == 4)
                    {
                        model = Instantiate(queen, new Vector3(x + 0.5f, 0, y + 0.5f), queen.transform.rotation);
                    }
                }
                model.GetComponent<MeshRenderer>().material = secondTeam;
            }
        }
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
                    piecePosition = new Vector2(hit.transform.position.x - 0.5f, hit.transform.position.z - 0.5f);
                    clicked = true;
                    return;
                }
            }
        }
        clicked = false;
    }
}
