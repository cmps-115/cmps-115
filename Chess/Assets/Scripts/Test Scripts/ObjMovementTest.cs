using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMovementTest : MonoBehaviour {

    MoveModel mm1;
    MoveModel mm2;

	// Use this for initialization
	void Start () {
        mm1 = gameObject.AddComponent<MoveModel>();
        mm2 = gameObject.AddComponent<MoveModel>();
    }
	
	// Update is called once per frame
    //demo of moving pieces around the board.
	void Update () {
		
        if (Time.time >= 2 && Time.time <= 3)
        {
            mm1.MovePiece(new Vector2(4, 1), new Vector2(4, 3), 0);
        }
        else if (Time.time >= 4 && Time.time <= 5)
        {
            mm1.MovePiece(new Vector2(4, 6), new Vector2(4, 4), 0);
        }
        else if (Time.time >= 6 && Time.time <= 7)
        {
            mm1.MovePiece(new Vector2(5, 1), new Vector2(5, 3), 0);
        }
        else if (Time.time >= 8 && Time.time <= 9)
        {
            mm1.MovePiece(new Vector2(4, 4), new Vector2(5, 3), 0);
        }
        else if (Time.time >= 10 && Time.time <= 11)
        {
            mm1.MovePiece(new Vector2(6, 0), new Vector2(5, 2));
        }
        else if (Time.time >= 10 && Time.time <= 11)
        {
            mm1.MovePiece(new Vector2(6, 0), new Vector2(5, 2));
        }
        else if (Time.time >= 12 && Time.time <= 13)
        {
            mm1.MovePiece(new Vector2(3, 6), new Vector2(3, 4), 0);
        }
        else if (Time.time >= 14 && Time.time <= 15)
        {
            mm1.MovePiece(new Vector2(4, 3), new Vector2(3, 4), 0);
        }
        else if (Time.time >= 16 && Time.time <= 17)
        {
            mm1.MovePiece(new Vector2(5, 0), new Vector2(3, 3), 0);
        }
        else if (Time.time >= 18 && Time.time <= 19)
        {
            mm1.MovePiece(new Vector2(4, 0), new Vector2(4, 1), 0);
        }
        else if (Time.time >= 20 && Time.time <= 21)
        {
            mm1.MovePiece(new Vector2(3, 0), new Vector2(7, 0));
            mm2.MovePiece(new Vector2(7, 0), new Vector2(3, 0), 0);
        }

    }
}
