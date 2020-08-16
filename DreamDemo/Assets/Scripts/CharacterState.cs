using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public Animator Animator;

    public enum State
    {
        Idle = 0,
        Slight_Left = 1,
        Slight_Right = 2,
        Fierce_Left = 3,
        Fierce_Right = 4
    };

    private State m_CurState;
    private State m_NewState;

    // Start is called before the first frame update
    void Start()
    {
        m_CurState = State.Idle;
        m_NewState = m_CurState;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurState != m_NewState)
        {
            m_CurState = m_NewState;
            switch (m_NewState)
            {
                case State.Idle:
                    IdleHandle();
                    break;
                case State.Slight_Left:
                    SlightLeftHandle();
                    break;
                case State.Slight_Right:
                    SlightRightHandle();
                    break;
                case State.Fierce_Left:
                    FierceLeftHandle();
                    break;
                case State.Fierce_Right:
                    FierceRightHandle();
                    break;
                default:
                    break;
            }
        }
    }

    public void SetCharacterState(State state)
    {
        m_NewState = state;
    }

    private void IdleHandle()
    {
        Debug.LogWarning("IdleHandle");
        Animator.SetBool("Idle", true);
        Animator.SetBool("Slight", false);
        Animator.SetBool("Fierce", false);
    }

    private void SlightLeftHandle()
    {
        Debug.LogWarning("SlightLeftHandle");
        Animator.SetBool("Idle", false);
        Animator.SetBool("Slight", true);
        Animator.SetBool("Fierce", false);
        Animator.SetBool("Right", false);
    }

    private void SlightRightHandle()
    {
        Debug.LogWarning("SlightRightHandle");
        Animator.SetBool("Idle", false);
        Animator.SetBool("Slight", true);
        Animator.SetBool("Fierce", false);
        Animator.SetBool("Right", true);
    }

    private void FierceLeftHandle()
    {
        Debug.LogWarning("FierceLeftHandle");
        Animator.SetBool("Idle", false);
        Animator.SetBool("Slight", false);
        Animator.SetBool("Fierce", true);
        Animator.SetBool("Right", false);
    }

    private void FierceRightHandle()
    {
        Debug.LogWarning("FierceRightHandle");
        Animator.SetBool("Idle", false);
        Animator.SetBool("Slight", false);
        Animator.SetBool("Fierce", true);
        Animator.SetBool("Right", true);
    }
}
