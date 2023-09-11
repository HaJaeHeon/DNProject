using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_DoorOpen : MonoBehaviour
{
    private Animator ani;

    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ani.SetBool("isOpen", true);
        }
    }

    
}
