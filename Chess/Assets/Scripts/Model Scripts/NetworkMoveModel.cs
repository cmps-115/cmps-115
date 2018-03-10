using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkMoveModel : NetworkBehaviour {

    private Transform objToMove;
    private GameObject otherObj;
    private Vector3 originalPos;
    private Vector3 midPoint;
    private Vector3 finalPos;

    private bool moving = false;
    private bool overlapped = false;
    private float timeStarted = 0;

    private float movementTime = 1;

    private const float MODEL_OFFSET = 0.5f;
    private const int BOARD_MINIMUM = 0;
    private const int BOARD_MAXIMUM = 7;

    /// <summary>
    /// Moves a piece from (x1, y1) to (x2, y2).
    /// The height determines the height of the arc (default = 2). Set height to zero to slide piece.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="height"></param>
    public void MovePiece(Vector2 from, Vector2 to, int height = 2)//to is where they click
    {
        if (height < 0) throw new System.Exception("Error in MoveModel: MovePiece() height parameter must be greater than zero.");
        if (from.x < BOARD_MINIMUM || to.x < BOARD_MINIMUM || from.y > BOARD_MAXIMUM || to.y > BOARD_MAXIMUM) throw new System.Exception("Error in MoveModel: MovePiece() coordinates out of range.");

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

    public bool Overlapped
    {
        get { return overlapped; }
    }

    // Update is called once per frame
    private void Update()
    {
        LerpModel();
    }

    private void LerpModel()
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

    private Collider[] GetOverLappingColliders(Vector3 position)
    {
        return Physics.OverlapSphere(new Vector3(position.x + MODEL_OFFSET, MODEL_OFFSET, position.y + MODEL_OFFSET), MODEL_OFFSET);
    }

    private void CheckFrom(Vector2 from, Vector2 to, int height = 2)
    {
        Collider[] cols = GetOverLappingColliders(from);
        if (cols.Length > 0)
        {
            foreach (Collider c in cols)
            {
                if (c.transform.tag == "ChessPiece")
                {
                    objToMove = c.transform;
                    originalPos = objToMove.position;
                    midPoint = (new Vector3(to.x + MODEL_OFFSET, height * 2, to.y + MODEL_OFFSET) + originalPos) / 2;
                    finalPos = new Vector3(to.x + MODEL_OFFSET, originalPos.y, to.y + MODEL_OFFSET);
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
        Collider[] cols = GetOverLappingColliders(to);
        if (cols.Length > 0)
        {
            foreach (Collider c in cols)
            {
                if (c.transform.tag == "ChessPiece")
                {
                    otherObj = c.gameObject;
                    overlapped = true;
                }
            }
        }
        else overlapped = false;
    }
}
