//Programmer: Ari Berkson
//
//This module moves 1 piece from one location to another.
//Multiple instances of this module will be needed if more than piece needs to be moved at the same time.
//This module will also delete a chess model if a piece is moved on top of it

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveModel : MonoBehaviour {

    private Transform objToMove;
    private GameObject otherObj;
    private Vector3 originalPos;
    private Vector3 midPoint;
    private Vector3 finalPos;

    private bool moving = false;
    private float timeStarted = 0;

    private float movementTime = 1;

    /// <summary>
    /// Moves a piece from (x1, y1) to (x2, y2).
    /// The height determines the height of the arc (default = 2). Set height to zero to slide piece.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="height"></param>
    public void MovePiece(Vector2 from, Vector2 to, int height = 2)
    {
        if (height < 0) throw new System.Exception("Error in MoveModel: MovePiece() height parameter must be greater than zero.");
        if (from.x < 0 || to.x < 0 || from.y > 7 || to.y > 7) throw new System.Exception("Error in MoveModel: MovePiece() coordinates out of range.");

        if (!moving)
        {
            CheckFrom(from, to, height);
            CheckTo(to);
        }
    }

    /// <summary>
    /// Sets how long it takes to move the piece in seconds.
    /// </summary>
    public float MovementTime
    {
        set
        {
            if (value > 0) movementTime = value;
            else throw new System.Exception("Error in MoveModel: movement time cannot be cannot be less than or equal to zero");
        }
        get { return movementTime; }
    }

    public bool isMoving
    {
        get { return moving; }
    }

    // Update is called once per frame
    private void Update ()
    {
        if (objToMove != null)
        {
            if (moving)
            {
                var dTime = Time.time - timeStarted;
                var l1 = Vector3.Lerp(originalPos, midPoint, dTime / movementTime);
                var l2 = Vector3.Lerp(midPoint, finalPos, dTime / movementTime);
                var l3 = Vector3.Lerp(l1, l2, dTime / movementTime);
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

    private void CheckFrom(Vector2 from, Vector2 to, int height = 2)
    {
        Collider[] cols = Physics.OverlapSphere(new Vector3(from.x + 0.5f, 0.5f, from.y + 0.5f), 0.5f);
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

    private void CheckTo(Vector2 to)
    {
        Collider[] cols = Physics.OverlapSphere(new Vector3(to.x + 0.5f, 1, to.y + 0.5f), 0.5f);
        if (cols.Length > 0)
        {
            foreach (Collider c in cols)
            {
                if (c.transform.tag == "ChessPiece") otherObj = c.gameObject;
            }
        }
    }
}
