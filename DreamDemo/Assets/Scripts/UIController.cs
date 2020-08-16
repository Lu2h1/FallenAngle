using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("标题")]
    public GameObject Logo;

    [Header("结算界面")]
    public GameObject FailPanel;
    public GameObject SuccessPanel;

    [Header("按钮")]
    public GameObject StartButton;

    [Header("准备幕布")]
    public GameObject PreparePanel;
    private Image PreparePanelImage;

    public Image GetPreparePanel()
    {
        if (PreparePanelImage == null)
        {
            PreparePanelImage = PreparePanel.GetComponent<Image>();
        }
        return PreparePanelImage;
    }

    public void ShowtPreparePanel()
    {
        PreparePanel.SetActive(true);
    }

    public void HidetPreparePanel()
    {
        PreparePanel.SetActive(false);
    }

    public void ShowFailPanel()
    {
        FailPanel.SetActive(true);
    }

    public void HideFailPanel()
    {
        FailPanel.SetActive(false);
    }

    public void ShowWinPanel()
    {
        SuccessPanel.SetActive(true);
    }

    public void HideWinPanel()
    {
        SuccessPanel.SetActive(false);
    }

    public void ShowStartButton()
    {
        Debug.Log("ShowStartButton");
        StartButton.SetActive(true);
    }

    public void HideStartButton()
    {
        Debug.Log("HideStartButton");
        StartButton.SetActive(false);
    }

    public void ShowLogo()
    {
        Logo.SetActive(true);
    }

    public void HideLogo()
    {
        Logo.SetActive(false);
    }

    // event
    public void OnStartButtonClick()
    {
        GameManager.Instance.LoadScene(1, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        GameManager.Instance.StageController.ChangeStage(StageController.Stage.Prepare);
    }

    public void OnBackButtonClick()
    {
        HideWinPanel();
        HideFailPanel();
        ShowStartButton();

        GameManager.Instance.UnLoadScene(1);
    }
}
