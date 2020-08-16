using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public enum Stage
    {
        Logo,
        Prepare,
        Game,
        Settlement
    }

    private Dictionary<Stage, IBaseStage> stages;
    private IBaseStage curStage;

    private void Awake()
    {
        stages = new Dictionary<Stage, IBaseStage>
        {
            { Stage.Logo, new LogoStage() },
            { Stage.Prepare, new PrepareStage() },
            { Stage.Game, new GameStage() },
            { Stage.Settlement, new SettleMentStage() }
        };
    }

    // Update is called once per frame
    void Update()
    {
        curStage?.OnRun();
    }

    public void ChangeStage(Stage stage)
    {
        curStage?.OnExit();
        curStage = stages[stage];
        curStage?.OnEnter();
    }
}
