using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class Controller : MonoBehaviour
{
    [Header("旋转")]
    public Transform Target;
    public float DeltaSpeed = 1; // °/s
    public float RotateDeltaTime = 0.5f;
    public float RotateMaxSpeed = 2f;

    private float m_TargetRotate;
    private float m_CurDeltaTime;
    private float m_CurRotateZ;
    private float m_CurRotateSpeed;

    [Header("平移")]
    public Transform MainCharacterCamera;
    public Transform MainCharacter;
    public float[] MainCharacterCameraMoveLimit = { 0.26f, 0.26f };
    public float MoveDeltaSpeed = 0.01f;
    public float MoveMaxSpeed = 1;
    public float MoveDeltaTime = 0.5f;

    private Vector3 m_LastMousePosition;
    private Vector3 m_TargetPosition;
    private Vector3 m_CurPosition;
    private Vector2 m_CurMoveSpeed;
    private float m_CameraLimitX;
    private float m_CameraLimitY;

    // Start is called before the first frame update
    void Start()
    {
        if (Target == null)
        {
            Debug.LogError("Have not band target");
        }

        if (MainCharacter == null)
        {
            Debug.LogError("Have not band mainPlayer");
        }

        if (MainCharacterCamera == null)
        {
            MainCharacterCamera = Camera.main.transform;
        }

        m_CurDeltaTime = 0;
        m_CurRotateSpeed = 0;

        m_TargetRotate = Target.localEulerAngles.z;
        m_CurRotateZ = Target.localEulerAngles.z;

        m_LastMousePosition = Input.mousePosition;
        m_TargetPosition = Vector3.zero;
        m_CurPosition = Vector3.zero;
        m_CurMoveSpeed = Vector2.zero;
        m_CameraLimitX = MainCharacterCameraMoveLimit[0];
        m_CameraLimitY = MainCharacterCameraMoveLimit[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            #region 旋转
            if (m_CurDeltaTime <= 0)
            {
                m_CurDeltaTime = 1;
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    m_TargetRotate += DeltaSpeed;
                }
                else if (Input.GetKey(KeyCode.Mouse1))
                {
                    m_TargetRotate -= DeltaSpeed;
                }
            }
            m_CurDeltaTime -= Time.deltaTime;

            // 旋转
            m_CurRotateZ = Mathf.SmoothDampAngle(m_CurRotateZ, m_TargetRotate, ref m_CurRotateSpeed, RotateDeltaTime, RotateMaxSpeed);
            if (m_CurRotateZ != m_TargetRotate)
            {
                Target.localEulerAngles = new Vector3(0, 0, m_CurRotateZ);
            }
            #endregion
        }


        #region 平移
        if (MainCharacter != null && MainCharacterCamera != null)
        {
            if (m_CurPosition != m_TargetPosition)
            {
                m_CurPosition = Vector2.SmoothDamp(m_CurPosition, m_TargetPosition, ref m_CurMoveSpeed, MoveDeltaTime, MoveMaxSpeed);
                Debug.Log("Cur position: " + m_CurPosition);
                Debug.Log("target position: " + m_TargetPosition);

                Vector3 newPosition = m_CurPosition;
                newPosition.z = MainCharacter.position.z;
                MainCharacter.SetPositionAndRotation(newPosition, MainCharacter.rotation);

                if (newPosition.x < -m_CameraLimitX)
                {
                    newPosition.x = -m_CameraLimitX;
                }
                else if (newPosition.x > m_CameraLimitX)
                {
                    newPosition.x = m_CameraLimitX;
                }

                if (newPosition.y < -m_CameraLimitY)
                {
                    newPosition.y = -m_CameraLimitY;
                }
                else if (newPosition.y > m_CameraLimitY)
                {
                    newPosition.y = m_CameraLimitY;
                }

                newPosition.z = MainCharacterCamera.position.z;
                MainCharacterCamera.SetPositionAndRotation(newPosition, MainCharacterCamera.rotation);
            }

            if (Vector3.Distance(m_LastMousePosition, Input.mousePosition) > 0.1f)
            {
                Vector3 moveDirection = Input.mousePosition - m_LastMousePosition;
                moveDirection = moveDirection.normalized;
                m_TargetPosition -= moveDirection * MoveDeltaSpeed;
                Debug.Log("move direction: " + moveDirection);

                m_LastMousePosition = Input.mousePosition;
            }
        }
        #endregion
    }
}
