using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_BossDoor : MonoBehaviour
{
    private Animator ani;
    public int burned;
    H_ComFire fire;

    void Start()
    {
        //ani = gameObject.GetComponent<Animator>();
        fire = GameObject.Find("ProfileWindow").transform.GetComponent<H_ComFire>();
    }
    private void Update()
    {
        DoorOpen();
    }
    void DoorOpen()
    {
        if(burned == 9)
        {
            Destroy(this.gameObject);
        }
    }

}
