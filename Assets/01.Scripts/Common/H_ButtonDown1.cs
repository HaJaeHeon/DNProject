using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_ButtonDown1 : MonoBehaviour
{
    [SerializeField]
    BoxCollider bCol;
    [SerializeField]
    Animator doorAni;
    [SerializeField]
    Animator buttonAni;

    private void Start()
    {
        bCol = GetComponent<BoxCollider>();
        buttonAni = GetComponent<Animator>();
        doorAni = GameObject.Find("ProfileDoor").GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            buttonAni.SetBool("isPress", true);
            Invoke("DoorOpen", 1f);
        }
    }
    void DoorOpen()
    {
        doorAni.SetBool("isOpen", true);
    }
}
