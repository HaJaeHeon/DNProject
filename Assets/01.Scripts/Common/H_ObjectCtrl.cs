using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_ObjectCtrl : MonoBehaviour
{
    H_PlayerGrab pGrab;
    FixedJoint fj;

    private void Awake()
    {
        pGrab = GameObject.FindWithTag("Player").GetComponent<H_PlayerGrab>();
        fj = gameObject.GetComponent<FixedJoint>();
    }

    private void Update()
    {
        CheckFj();
    }
    void CheckFj()
    {
        if (fj.connectedBody != null)
        {
            if (!pGrab.isObject || !pGrab.isGrab)
            {
                fj.connectedBody = null;
            }
        }
    }
}


