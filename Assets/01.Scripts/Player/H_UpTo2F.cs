using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_UpTo2F : MonoBehaviour
{

    Rigidbody rbody;
    Transform tr;
    Animator ani;

    H_PlayerGrab pGrab;
    H_PlayerRay pRay;
    H_PlayerCtrl pCtrl;

    RaycastHit hit;

    Ray ladRay;

    float v;
    public int ladderLayer;

    public bool isLadder;


    private void Awake()
    {
        pGrab = gameObject.GetComponent<H_PlayerGrab>();
        pRay = gameObject.GetComponent<H_PlayerRay>();
        pCtrl = gameObject.GetComponent<H_PlayerCtrl>();

        rbody = gameObject.GetComponent<Rigidbody>();
        tr = gameObject.GetComponent<Transform>();
        ani = gameObject.GetComponent<Animator>();

        ladderLayer = LayerMask.GetMask("LADDER");
    }

    private void Update()
    {
        if (pGrab.isGrab)
        {
            Ladder();
            ArriveRoof();
        }
    }
    void Ladder()
    {
        Vector3 ladPoint = new Vector3(tr.position.x, tr.position.y + 0.5f, tr.position.z);
        ladRay = new Ray(ladPoint, tr.forward);
        isLadder = Physics.Raycast(ladRay, out hit, 1f, ladderLayer);

        if (isLadder && !pCtrl.isCrawl)
        {
            if (Input.GetMouseButton(0))
            {
                ani.SetBool("isLadder", true);
                pCtrl.enabled = false;

                rbody.useGravity = false;
                rbody.isKinematic = true;
                LadderMove();
            }
        }
        else if (!isLadder)
            return;
    }
    void LadderMove()
    {
        v = Input.GetAxis("Vertical");
        float moveSpeed = 3f;
        ani.SetFloat("Laddering", Mathf.Clamp(v * 10, -1, 1));

        Vector3 moveVec = new Vector3(0f, v, 0f);
        moveVec = moveVec.normalized * moveSpeed * Time.deltaTime;
        rbody.MovePosition(tr.position + moveVec);

        OutOfLadder();

    }

    void OutOfLadder()
    {
        if(pRay.isFloor && v <0f && isLadder && !pGrab.isObject)
        {
            pCtrl.enabled = true;
            v = 0f;

            rbody.useGravity = true;
            rbody.isKinematic = false;
            pCtrl.moveSpeed = 2f;

            ani.SetBool("isLadder", false);
        }
    }


    

    void ArriveRoof()
    {
        if(pRay.isRoof)
        {
            v = 0f;
            tr.Translate(0, 0.3f, 0.3f);

            pCtrl.enabled = true;
            rbody.useGravity = true;
            rbody.isKinematic = false;
            ani.SetBool("isLadder", false);
        }
    }    


    
}
