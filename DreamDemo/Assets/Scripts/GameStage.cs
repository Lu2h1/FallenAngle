using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStage : IBaseStage
{
    public void OnEnter()
    {
        GameManager.Instance.SetGameRunning(true);
    }

    public void OnExit()
    {
        GameManager.Instance.SetGameRunning(false);
    }

    public void OnRun()
    {
    }
}
