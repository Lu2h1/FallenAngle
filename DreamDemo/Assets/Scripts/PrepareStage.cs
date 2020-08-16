using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepareStage : IBaseStage
{
    private float startZ;
    private float endZ;
    private readonly float deltaTime = 2;

    private Transform mainCamera;
    private Image preparePanel;

    private float velocityZ;
    private float velocityAlpha;

    private float curZ;
    private float curAlpha;

    public void OnEnter()
    {
        Vector3 startPoint = GameManager.Instance.GetStartPoint();

        startZ = startPoint.z - 10;
        endZ = startPoint.z - 0.5f;

        GameManager.Instance.PlayerManager.CreatePlayer(startPoint);

        mainCamera = Camera.main.transform;
        preparePanel = GameManager.Instance.UIController.GetPreparePanel();

        GameManager.Instance.UIController.ShowtPreparePanel();

        SetPositionZ(mainCamera, endZ);
        SetImageAlpha(preparePanel, 1);

        curZ = startZ;
        curAlpha = 1;

        velocityZ = 0;
        velocityAlpha = 0;
    }

    public void OnExit()
    {
        GameManager.Instance.UIController.HidetPreparePanel();
    }

    public void OnRun()
    {
        Debug.Log("CurZ: " + curZ);
        Debug.Log("CurAlpha: " + curAlpha);
        if (Math.Abs(curZ - endZ) > 0.1f)
        {
            curZ = Mathf.SmoothDamp(curZ, endZ, ref velocityZ, deltaTime);
            SetPositionZ(mainCamera, curZ);

            curAlpha = Mathf.SmoothDamp(curAlpha, 0, ref velocityAlpha, deltaTime);
            if (curAlpha < 0)
            {
                curAlpha = 0;
            }
            SetImageAlpha(preparePanel, curAlpha);
        }
        else
        {
            GameManager.Instance.StageController.ChangeStage(StageController.Stage.Game);
        }
    }

    private void SetPositionZ(Transform target, float z)
    {
        Vector3 pos = target.position;
        pos.z = z;
        target.position = pos;
    }

    private void SetImageAlpha(Image target, float alpha)
    {
        Color color = target.color;
        color.a = alpha;
        target.color = color;
    }
}
