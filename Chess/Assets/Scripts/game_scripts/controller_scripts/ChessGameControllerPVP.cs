using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using ChessGlobals;

public class ChessGameControllerPVP : ChessGameController
{
    //nothing in here now since it is the same as ChessGameController
    protected override void Start()
    {
        base.Start();
        //set camera
        firstCameraPos = Camera.main.transform.position;
        secondCameraPos = new Vector3(firstCameraPos.x, firstCameraPos.y, firstCameraPos.z + 18);
        firstCameraRot = Camera.main.transform.eulerAngles.y;
        secondCameraRot = Camera.main.transform.eulerAngles.y + 180;
    }

    protected override void Update()
    {
        base.Update();
        bool pieceDoneMoving = Time.time - moveTime > waitTime && isPieceMove == true;
        if (pieceDoneMoving)
        {
            startMoveTime = Time.time;
            SwitchTurnDisplay();
            isPieceMove = false;
        }
        if (startMoveTime != negativeTime)
            SwitchCamera();
    }

    protected void SwitchCamera()
    {
        var deltaTime = Time.time - startMoveTime;
        RotateCamera(deltaTime);
        MoveCamera(deltaTime);
        if (deltaTime >= 1)
        {
            var temp = firstCameraPos;
            firstCameraPos = secondCameraPos;
            secondCameraPos = temp;

            var temp2 = firstCameraRot;
            firstCameraRot = secondCameraRot;
            secondCameraRot = temp2;

            startMoveTime = negativeTime;
        }
    }

    protected void RotateCamera(float dTime)
    {
        var currentRot = Camera.main.transform.eulerAngles;
        Camera.main.transform.eulerAngles = new Vector3(currentRot.x, Mathf.LerpAngle(firstCameraRot, secondCameraRot, dTime), currentRot.z);
    }

    protected void MoveCamera(float dTime)
    {
        Camera.main.transform.position = Vector3.Lerp(firstCameraPos, secondCameraPos, dTime);
    }

    protected override void PromoteMenuSelect()
    {
        base.PromoteMenuSelect();
        startMoveTime = Time.time;
        SwitchCamera();
    }
}
