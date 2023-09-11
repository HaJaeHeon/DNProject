using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_PlayerGrab : MonoBehaviour
{
    Transform targetTr;
    Rigidbody targetRbody;
    FixedJoint fj;

    Rigidbody rbody;
    Transform tr;
    Animator ani;

    H_PlayerCtrl pCtrl;

    public RaycastHit hit;


    public bool isObject;
    public bool isGrab;
    public bool isPull;


    public int objectLayer;

    Ray obRay;

    float pre;
    float cur;
    public float gabDis = 0;

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        ani = GetComponent<Animator>();
        pCtrl = GetComponent<H_PlayerCtrl>();



        objectLayer = LayerMask.GetMask("OBJECT");
    }



    void Update()
    {
        if (isGrab)
        {
            GrabSomething();
            

            ani.SetBool("isGrab", true);
        }
        else if (!isGrab)
        {
            ani.SetBool("isGrab", false);
            pCtrl.MoveRot(1f);

        }
    }
    private void FixedUpdate()
    {
        isGrab = Input.GetMouseButton(0);
    }

    void GrabSomething()
    {
        Vector3 rayPoint = new Vector3(tr.position.x, tr.position.y + 1f, tr.position.z);

        obRay = new Ray(rayPoint,tr.forward);


        Debug.DrawRay(rayPoint, tr.forward * 1f, Color.green);

        isObject = Physics.Raycast(obRay, out hit, 1f, objectLayer);

        PushAndPull();


    }

    void PushAndPull()
    {
        if (isObject && isGrab)
        {
            if (Input.GetMouseButtonDown(0)) //움직일 사물에게는 fixedjoint넣기
            {
                ani.SetBool("isObject", true);
                fj = hit.transform.gameObject.GetComponent<FixedJoint>();
                targetTr = hit.transform.gameObject.GetComponent<Transform>();
                targetRbody = hit.transform.gameObject.GetComponent<Rigidbody>();


                fj.connectedBody = rbody;
                pCtrl.MoveRot(0.1f);
                targetRbody.isKinematic = false;


                StartCoroutine(PushOrPull());
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (fj.connectedBody != null)
                    fj.connectedBody = null;
                else
                    return;
                pCtrl.MoveRot(1f);
                targetRbody.isKinematic = true;
            }
        }
        else if (!isObject)
            ani.SetBool("isObject", false);
    }

    IEnumerator PushOrPull()
    {
        while (fj.connectedBody != null && isObject)
        {
            var a = targetTr.localPosition;
            var b = tr.localPosition;
            var preDis = Vector3.Magnitude(a - b);
            
            yield return new WaitForSeconds(0.1f);

            var c = tr.localPosition;
            var currDis = Vector3.Magnitude(a - c);
            float gabDis = Mathf.Clamp((currDis - preDis) * 100, -1, 1);

            if (gabDis > 1)
                isPull = false;
            else if (gabDis <= -1)
                isPull = true;

            ani.SetFloat("Dis", gabDis);

            pCtrl.moveSpeed = pCtrl.isMove ? 2f : 0f;
        }
    }
}
