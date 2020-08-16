using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollideController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.CompareTag("Item"))
        {
            Debug.Log("Game over");
            GameObject.Destroy(this.gameObject);
            GameManager.Instance.SetGameWin(false);
        }
        else if (other.CompareTag("Final"))
        {
            Debug.Log("Game win");
            GameObject.Destroy(this.gameObject);
            GameManager.Instance.SetGameWin(true);
        }
        */
    }
}
