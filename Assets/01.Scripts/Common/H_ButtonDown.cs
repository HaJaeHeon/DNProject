using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_ButtonDown : MonoBehaviour
{
    [SerializeField]
    BoxCollider bCol;
    public Animator doorAni;
    [SerializeField]
    Animator buttonAni;

    private void Start()
    {
        bCol = GetComponent<BoxCollider>();
        buttonAni = GetComponent<Animator>();
        //doorAni = GameObject.Find("P_Door_01_").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            buttonAni.SetBool("isPress", true);
            DoorOpen();
        }
    }
    void DoorOpen()
    {
        doorAni.SetBool("isOpen", true);
    }
}
