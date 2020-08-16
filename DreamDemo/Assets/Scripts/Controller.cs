using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Transform MainCharacterCamera;
    public Transform MainCharacter;
    public CharacterState CharacterState;
    [Header("旋转")]
    public float DeltaSpeed = 1; // °/s
    public float RotateDeltaTime = 0.5f;
    public float RotateMaxSpeed = 2f;

    private float m_TargetRotate;
    private float m_CurDeltaTime;
    private float m_CurMainCharacterRotateZ;
    private float m_CurMainCharacterCameraRotateZ;
    private float m_CurRotateSpeed;

    [Header("平移")]
    public float[] MainCharacterMoveLimit = { 0.26f, 0.26f };
    public float[] MainCharacterCameraMoveLimit = { 0.26f, 0.26f };
    public float MoveDeltaSpeed = 0.01f;
    public float MoveMaxSpeed = 1;
    public float MoveDeltaTime = 0.5f;
    public float SlightThread = 0.1f;
    public float FierceThread = 0.3f;

    private Vector3 m_LastMousePosition;
    private Vector3 m_MainCharacterTargetPosition;
    private Vector3 m_MainCharacterCameraTargetPosition;
    private Vector3 m_CurMainCharacterPosition;
    private Vector3 m_CurMainCharacterCameraPosition;
    private Vector2 m_CurMainCharacterMoveSpeed;
    private Vector2 m_CurMainCharacterCameraMoveSpeed;
    private float m_CameraLimitX;
    private float m_CameraLimitY;
    private float m_MainCharacterLimitX;
    private float m_MainCharacterLimitY;

    // Start is called before the first frame update
    void Start()
    {
        if (MainCharacter == null)
        {
            MainCharacter = GameManager.Instance.PlayerManager.GetPlayer().transform;
        }

        if (CharacterState == null)
        {
            CharacterState = MainCharacter.GetComponent<CharacterState>();
        }

        if (MainCharacterCamera == null)
        {
            MainCharacterCamera = Camera.main.transform;
        }

        m_CurDeltaTime = 0;
        m_CurRotateSpeed = 0;

        m_TargetRotate = 0;
        m_CurMainCharacterRotateZ = 0;
        m_CurMainCharacterCameraRotateZ = 0;

        m_LastMousePosition = Input.mousePosition;
        m_MainCharacterTargetPosition = Vector3.zero;
        m_MainCharacterCameraTargetPosition = Vector3.zero;
        m_CurMainCharacterPosition = Vector3.zero;
        m_CurMainCharacterCameraPosition = Vector3.zero;
        m_CurMainCharacterMoveSpeed = Vector2.zero;
        m_CurMainCharacterCameraMoveSpeed = Vector2.zero;
        m_CameraLimitX = MainCharacterCameraMoveLimit[0];
        m_CameraLimitY = MainCharacterCameraMoveLimit[1];
        m_MainCharacterLimitX = MainCharacterMoveLimit[0];
        m_MainCharacterLimitY = MainCharacterMoveLimit[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameRunning() == false)
        {
            return;
        }

        if (MainCharacter != null && MainCharacterCamera != null)
        {
            #region 旋转
            if (m_CurDeltaTime <= 0)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    m_TargetRotate += DeltaSpeed;
                    m_CurDeltaTime = 1;
                }
                else if (Input.GetKey(KeyCode.Mouse1))
                {
                    m_TargetRotate -= DeltaSpeed;
                    m_CurDeltaTime = 1;
                }
                else
                {
                    m_CurDeltaTime = 0;
                }
            }
            m_CurDeltaTime -= Time.deltaTime;

            // 旋转
            m_CurMainCharacterRotateZ = Mathf.SmoothDampAngle(m_CurMainCharacterRotateZ, m_TargetRotate, ref m_CurRotateSpeed, RotateDeltaTime, RotateMaxSpeed);
            m_CurMainCharacterCameraRotateZ = Mathf.SmoothDampAngle(m_CurMainCharacterCameraRotateZ, m_TargetRotate, ref m_CurRotateSpeed, RotateDeltaTime + 1.5f, RotateMaxSpeed);
            if (Math.Abs(m_CurMainCharacterRotateZ - m_TargetRotate) > 0.1)
            {
                MainCharacterCamera.localEulerAngles = new Vector3(0, 0, -m_CurMainCharacterRotateZ);
            }
            if (Math.Abs(m_CurMainCharacterCameraRotateZ - m_TargetRotate) > 0.1)
            {
                MainCharacter.localEulerAngles = new Vector3(0, 0, -m_CurMainCharacterCameraRotateZ);
            }
            #endregion

            #region 平移
            // Main character
            if (Vector3.Distance(m_CurMainCharacterPosition, m_MainCharacterTargetPosition) > 0.1)
            {
                m_CurMainCharacterPosition = Vector2.SmoothDamp(m_CurMainCharacterPosition, m_MainCharacterTargetPosition, ref m_CurMainCharacterMoveSpeed, MoveDeltaTime, MoveMaxSpeed);

                Vector3 newPosition = m_CurMainCharacterPosition;


                newPosition.z = MainCharacter.position.z;
                MainCharacter.SetPositionAndRotation(newPosition, MainCharacter.rotation);

                // move state
                if (Math.Abs(m_CurMainCharacterMoveSpeed.x) > FierceThread)
                {
                    Debug.LogWarning("强烈摇摆" + m_CurMainCharacterMoveSpeed.x);
                    CharacterState.SetCharacterState(m_CurMainCharacterMoveSpeed.x > 0 ? CharacterState.State.Fierce_Right : CharacterState.State.Fierce_Left);
                }
                else if (Math.Abs(m_CurMainCharacterMoveSpeed.x) > SlightThread)
                {
                    Debug.LogWarning("轻微摇摆" + m_CurMainCharacterMoveSpeed.x);
                    CharacterState.SetCharacterState(m_CurMainCharacterMoveSpeed.x > 0 ? CharacterState.State.Slight_Right : CharacterState.State.Slight_Left);
                }
            }
            else
            {
                Debug.LogWarning("闲置" + m_CurMainCharacterMoveSpeed.x);
                CharacterState.SetCharacterState(CharacterState.State.Idle);
            }

            // Main Character Camera
            if (Vector3.Distance(m_CurMainCharacterCameraPosition, m_MainCharacterCameraTargetPosition) > 0.1)
            {
                m_CurMainCharacterCameraPosition = Vector2.SmoothDamp(m_CurMainCharacterCameraPosition, m_MainCharacterCameraTargetPosition, ref m_CurMainCharacterCameraMoveSpeed, MoveDeltaTime, MoveMaxSpeed);

                Vector3 newPosition = m_CurMainCharacterCameraPosition;


                newPosition.z = MainCharacterCamera.position.z;
                MainCharacterCamera.SetPositionAndRotation(newPosition, MainCharacterCamera.rotation);
            }

            if (Vector3.Distance(m_LastMousePosition, Input.mousePosition) > 0.1f)
            {
                Vector3 moveDirection = Input.mousePosition - m_LastMousePosition;
                moveDirection = moveDirection.normalized;

                // Main Character
                m_MainCharacterTargetPosition += moveDirection * MoveDeltaSpeed;
                if (m_MainCharacterTargetPosition.x < -m_MainCharacterLimitX)
                {
                    m_MainCharacterTargetPosition.x = -m_MainCharacterLimitX;
                }
                else if (m_MainCharacterTargetPosition.x > m_MainCharacterLimitX)
                {
                    m_MainCharacterTargetPosition.x = m_MainCharacterLimitX;
                }

                if (m_MainCharacterTargetPosition.y < -m_MainCharacterLimitY)
                {
                    m_MainCharacterTargetPosition.y = -m_MainCharacterLimitY;
                }
                else if (m_MainCharacterTargetPosition.y > m_MainCharacterLimitY)
                {
                    m_MainCharacterTargetPosition.y = m_MainCharacterLimitY;
                }

                // Main Character Camera
                m_MainCharacterCameraTargetPosition += moveDirection * MoveDeltaSpeed;
                if (m_MainCharacterCameraTargetPosition.x < -m_CameraLimitX)
                {
                    m_MainCharacterCameraTargetPosition.x = -m_CameraLimitX;
                }
                else if (m_MainCharacterCameraTargetPosition.x > m_CameraLimitX)
                {
                    m_MainCharacterCameraTargetPosition.x = m_CameraLimitX;
                }

                if (m_MainCharacterCameraTargetPosition.y < -m_CameraLimitY)
                {
                    m_MainCharacterCameraTargetPosition.y = -m_CameraLimitY;
                }
                else if (m_MainCharacterCameraTargetPosition.y > m_CameraLimitY)
                {
                    m_MainCharacterCameraTargetPosition.y = m_CameraLimitY;
                }

                m_LastMousePosition = Input.mousePosition;
            }
            #endregion
        }

    }
}
