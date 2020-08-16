using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettleMentStage : IBaseStage
{
    public void OnEnter()
    {
        if (GameManager.Instance.IsGameWin())
        {
            GameManager.Instance.UIController.ShowWinPanel();
        }
        else
        {
            GameManager.Instance.UIController.ShowFailPanel();
        }
    }

    public void OnExit()
    {
        GameManager.Instance.UIController.HideWinPanel();
        GameManager.Instance.UIController.HideFailPanel();
    }

    public void OnRun()
    {
    }
}
