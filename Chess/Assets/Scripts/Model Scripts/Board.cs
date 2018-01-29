using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public List<GameObject> Pieces = new List<GameObject>();
    private GameObject[,] BoardTiles = new GameObject[8,8];

    // Use this for initialization
    void Start ()
    {
        PlaceFirstTeam();
        PlaceSecondTeam();
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #region Place Teams
    private void PlaceFirstTeam()
    {
        for (int y = 0; y < 2; ++y)
        {
            for (int x = 0; x < 8; ++x)
            {
                if (y == 1)
                {
                    BoardTiles[y, x] = Pieces[5];
                }
                else
                {
                    if (x == 0 || x == 7)
                    {
                        BoardTiles[(int)0,(int) 0] = Pieces[(int)4];
                    }
                    else if (x == 1 || x == 6)
                    {
                        BoardTiles[y, x] = Pieces[3];   
                    }
                    else if (x == 2 || x == 5)
                    {
                        BoardTiles[y, x] = Pieces[2];
                    }
                    else if (x == 3)
                    {
                        BoardTiles[y, x] = Pieces[0];
                    }
                    else if (x == 4)
                    {
                        BoardTiles[y, x] = Pieces[1];
                    }
                }
            }
        }
    }

    private void PlaceSecondTeam()
    {
        for (int y = 7; y > 5; --y)
        {
            for (int x = 0; x < 8; ++x)
            {
                if (y == 6)
                {
                    BoardTiles[y, x] = Pieces[11];
                }
                else
                {
                    if (x == 0 || x == 7)
                    {
                        BoardTiles[y, x] = Pieces[10];
                    }
                    else if (x == 1 || x == 6)
                    {
                        BoardTiles[y, x] = Pieces[9];
                    }
                    else if (x == 2 || x == 5)
                    {
                        BoardTiles[y, x] = Pieces[8];
                    }
                    else if (x == 3)
                    {
                        BoardTiles[y, x] = Pieces[6];
                    }
                    else if (x == 4)
                    {
                        BoardTiles[y, x] = Pieces[7];
                    }
                }
            }
        }
    }
    #endregion

    public void Mark(int x, int y, GameObject piece)
    {
        BoardTiles[x, y] = piece;
    }

    public void UnMark(int x, int y)
    {
        BoardTiles[x, y] = null;
    }

    public bool IsOccupied (int x, int y)
    {
        return BoardTiles[x, y] != null;
    }

    public bool IsOccupied(Vector2 coords)
    {
        return BoardTiles[(int) coords.x, (int) coords.y] != null;
    }
}
