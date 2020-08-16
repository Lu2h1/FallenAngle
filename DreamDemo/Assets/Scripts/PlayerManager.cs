using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject PlayerPrefab;

    private GameObject m_Player;

    public void InitPlayer(Vector3 initPos)
    {
        m_Player = Instantiate(PlayerPrefab, initPos, Quaternion.identity);
    }

    public GameObject GetPlayer()
    {
        if (m_Player == null)
        {
            InitPlayer(Vector3.zero);
        }
        return m_Player;
    }

    public void CreatePlayer(Vector3 initPos)
    {
        if (m_Player == null)
        {
            InitPlayer(initPos);
        }
        else
        {
            m_Player.transform.position = initPos;
        }
    }

    public void DestroyPlayer()
    {
        if (null != m_Player)
        {
            m_Player.transform.position = Vector3.up * -1000;
        }
    }
}
