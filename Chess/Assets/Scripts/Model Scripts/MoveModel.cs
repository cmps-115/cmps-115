//Programmer: Ari Berkson
//
//This module moves 1 piece from one location to another.
//Multiple instances of this module will be needed if more than piece needs to be moved at the same time.
//This module will also delete a chess model if a piece is moved on top of it

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveModel : MonoBehaviour {

    Transform objToMove;
    GameObject otherObj;
    Vector3 originalPos;
    Vector3 midPoint;
    Vector3 finalPos;

    bool moving = false;
    float timeStarted = 0;

    float movementTime = 1;
	
	// Update is called once per frame
	void Update () {

        if (objToMove != null)
        {
            if (moving)
            {
                float dTime = Time.time - timeStarted;
                Vector3 l1 = Vector3.Lerp(originalPos, midPoint, dTime / movementTime);
                Vector3 l2 = Vector3.Lerp(midPoint, finalPos, dTime / movementTime);
                Vector3 l3 = Vector3.Lerp(l1, l2, dTime / movementTime);
                objToMove.position = l3;
            }
            if (objToMove.position == finalPos)
            {
                if (otherObj != null)
                {
                    if (otherObj.transform.position == objToMove.position) Destroy(otherObj);
                }
                moving = false;
            }
        }
    }

    /// <summary>
    /// Moves a piece from (x1, y1) to (x2, y2).
    /// The height determines the height of the arc (default = 2). Set height to zero to slide piece.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="height"></param>
    public void MovePiece(Vector2 from, Vector2 to, int height = 2)
    {
        if (height < 0)  throw new System.Exception("Error in MoveModel: MovePiece() height parameter must be greater than zero.");
        if (from.x < 0 || to.x < 0 || from.y > 7 || to.y > 7) throw new System.Exception("Error in MoveModel: MovePiece() coordinates out of range.");

        if (!moving)
        {
            CheckFrom(from, to, height);
            CheckTo(to);
        }
    }

    void CheckFrom(Vector2 from, Vector2 to, int height = 2)
    {
        Collider[] cols = Physics.OverlapSphere(new Vector3(from.x + 0.5f, 1, from.y), 0.5f);
        if (cols.Length > 0)
        {
            foreach (Collider c in cols)
            {
                if (c.transform.tag == "ChessPiece")
                {
                    objToMove = c.transform;
                    originalPos = objToMove.position;
                    midPoint = (new Vector3(to.x + 0.5f, height * 2, to.y + 0.5f) + originalPos) / 2;
                    finalPos = new Vector3(to.x + 0.5f, originalPos.y, to.y + 0.5f);
                    moving = true;
                    timeStarted = Time.time;
                    return;
                }
            }
        }
        else throw new System.Exception("Error in MoveModel: MovePiece() could not find chess model at location.");
    }

    void CheckTo(Vector2 to)
    {
        Collider[] cols = Physics.OverlapSphere(new Vector3(to.x + 0.5f, 1, to.y + 0.5f), 0.5f);
        if (cols.Length > 0)
        {
            foreach (Collider c in cols)
            {
                if (c.transform.tag == "ChessPiece")  otherObj = c.gameObject;
            }
        }
    }

    public float MovementTime
    {
        set
        {
            if (value > 0) movementTime = value;
            else throw new System.Exception("Error in MoveModel: movement time cannot be cannot be less than or equal to zero");
        }
        get { return movementTime; }
    }
}
