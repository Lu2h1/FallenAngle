using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMoveController : MonoBehaviour
{
    public Transform Target = null;

    [Header("移动")]
    public float RoomMoveSpeed = 0.02f;
    public float Accelerate = 0.001f;

    private float m_CurRoomMoveSpeed;
    private float m_TimeCount;

    // Start is called before the first frame update
    void Start()
    {
        if (Target == null)
        {
            Target = this.transform;
        }

        m_CurRoomMoveSpeed = Accelerate;
        m_TimeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameRunning() == false)
        {
            return;
        }

        if (Target != null)
        {
            m_TimeCount += Time.deltaTime;
            if (m_CurRoomMoveSpeed < RoomMoveSpeed && m_TimeCount > 1)
            {
                m_TimeCount = 0;
                m_CurRoomMoveSpeed += Accelerate;

                if (m_CurRoomMoveSpeed > RoomMoveSpeed)
                {
                    m_CurRoomMoveSpeed = RoomMoveSpeed;
                }
            }

            #region 移动
            Target.Translate(Vector3.forward * m_CurRoomMoveSpeed);
            #endregion
        }
    }

    public void SetRoomMoveSpeed(float speed)
    {
        RoomMoveSpeed = speed;
    }
}
