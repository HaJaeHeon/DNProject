using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_DownFloor : MonoBehaviour
{
    BoxCollider bColl;
    MeshRenderer meshR;

    private void Awake()
    {
        bColl = this.GetComponent<BoxCollider>();
        meshR = this.GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Invoke("Fall", 2f);
    }

    void Fall()
    {
        StartCoroutine(SpawnBox());
    }

    IEnumerator SpawnBox()
    {
        bColl.enabled = false;
        meshR.enabled = false;
        yield return new WaitForSeconds(5f);
        bColl.enabled = true;
        meshR.enabled = true;
    }
}
