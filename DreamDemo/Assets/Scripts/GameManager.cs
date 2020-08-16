using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> DontDestroyList = new List<GameObject>();

    private UIController m_UIController;
    private StageController m_StageController;
    private PlayerManager m_PlayerManager;

    private Vector3 m_StartPoint;

    private bool m_GameRunning;
    private bool m_GameWin;

    public UIController UIController
    {
        get
        {
            if (m_UIController == null)
            {
                m_UIController = gameObject.GetComponent<UIController>();
            }
            return m_UIController;
        }
    }

    public StageController StageController
    {
        get
        {
            if (m_StageController == null)
            {
                m_StageController = gameObject.GetComponent<StageController>();
            }
            return m_StageController;
        }
    }

    public PlayerManager PlayerManager
    {
        get
        {
            if (m_PlayerManager == null)
            {
                m_PlayerManager = gameObject.GetComponent<PlayerManager>();
            }
            return m_PlayerManager;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        m_GameRunning = false;
        m_GameWin = false;

        StageController.ChangeStage(StageController.Stage.Logo);
    }

    public void LoadScene(int index, LoadSceneMode mode)
    {
        SceneManager.LoadScene(index, mode);
    }

    public void UnLoadScene(int index)
    {
        SceneManager.UnloadSceneAsync(index, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
    }


    public void SetGameRunning(bool run)
    {
        m_GameRunning = run;

        if (run)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    public void SetGameWin(bool win)
    {
        m_GameWin = win;
        SetGameRunning(false);
    }

    public bool IsGameRunning()
    {
        return m_GameRunning;
    }

    public bool IsGameWin()
    {
        return m_GameWin;
    }

    public void SetStartPoint(Vector3 startPoint)
    {
        m_StartPoint = startPoint;
    }

    public Vector3 GetStartPoint()
    {
        return m_StartPoint;
    }
}
