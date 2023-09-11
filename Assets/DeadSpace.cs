using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSpace : MonoBehaviour
{
    public GameObject uiMan;

    private void Start()
    {
        uiMan = GameObject.Find("UIManager");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            uiMan.GetComponent<H_UIManager>().Btn_Load();
    }
}
