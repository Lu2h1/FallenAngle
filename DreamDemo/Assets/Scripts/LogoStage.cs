using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoStage : IBaseStage
{
    public void OnEnter()
    {
        GameManager.Instance.SetStartPoint(Vector3.zero);
        GameManager.Instance.UIController.ShowLogo();
        GameManager.Instance.UIController.ShowStartButton();
    }

    public void OnExit()
    {
        GameManager.Instance.UIController.HideLogo();
        GameManager.Instance.UIController.HideStartButton();
    }

    public void OnRun()
    {
    }
}
