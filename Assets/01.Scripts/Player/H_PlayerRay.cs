using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_PlayerRay : MonoBehaviour
{
    Transform tr;
    H_PlayerCtrl pCtrl;
    Animator ani;

    public Transform LFray;
    public Transform LBray;
    public Transform RFray;
    public Transform RBray;
    public Transform HeadRay;

    public RaycastHit hit;
    public Ray ray;

    public Ray jLFray;
    public Ray jLBray;
    public Ray jRFray;
    public Ray jRBray;
    public Ray Hray;

    private int floorLayer;
    public int roopLayer;
    private int objLayer;
    private int foLayer;

    float jdistance = 0.15f;

    public bool isFloor;

    bool isLFHit;
    bool isLBHit;
    bool isRFHit;
    bool isRBHit;

    public bool isRoof;

    void Start()
    {
        tr = gameObject.transform.GetChild(5).gameObject.GetComponent<Transform>();
        pCtrl = gameObject.GetComponent<H_PlayerCtrl>();
        ani = gameObject.GetComponent<Animator>();

        LFray = gameObject.transform.GetChild(5).GetComponent<Transform>();
        LBray = gameObject.transform.GetChild(6).GetComponent<Transform>();
        RFray = gameObject.transform.GetChild(7).GetComponent<Transform>();
        RBray = gameObject.transform.GetChild(8).GetComponent<Transform>();
        HeadRay = gameObject.transform.GetChild(9).GetComponent<Transform>();

        floorLayer = LayerMask.GetMask("FLOOR");
        roopLayer = LayerMask.GetMask("ROOF");
        objLayer = LayerMask.GetMask("OBJECT");
        foLayer = objLayer | floorLayer;
    }

    void FixedUpdate()
    {
        JumpHit();
    }

    void JumpHit()
    {
        OnDrawJRayLine();

        jLFray = new Ray(LFray.position, LFray.up);
        jLBray = new Ray(LBray.position, LBray.up);
        jRFray = new Ray(RFray.position, RFray.up);
        jRBray = new Ray(RBray.position, RBray.up);

        Hray = new Ray(HeadRay.position, HeadRay.up);


        isLFHit = Physics.Raycast(jLFray, out hit, jdistance, foLayer);
        isLBHit = Physics.Raycast(jLBray, out hit, jdistance, foLayer);
        isRFHit = Physics.Raycast(jRFray, out hit, jdistance, foLayer);
        isRBHit = Physics.Raycast(jRBray, out hit, jdistance, foLayer);

        isRoof = Physics.Raycast(Hray, out hit, 1f, roopLayer);

        if(!isLFHit)
            Debug.DrawRay(LFray.position, LFray.up * hit.distance, Color.red);
        if (!isLBHit)
            Debug.DrawRay(LBray.position, LFray.up * hit.distance, Color.red);
        if (!isRFHit)
            Debug.DrawRay(RFray.position, LFray.up * hit.distance, Color.red);
        if (!isRBHit)
            Debug.DrawRay(RBray.position, LFray.up * hit.distance, Color.red);

        if (isLFHit || isLBHit || isRFHit || isRBHit)
        {
            isFloor = true;
        }
        else
        {
            isFloor = false;
        }
    }

    public void OnDrawJRayLine()
    {
        if (hit.collider == null)
        {
            Debug.DrawRay(LFray.position, LFray.up * this.jdistance, Color.green);
            Debug.DrawRay(LBray.position, LBray.up * this.jdistance, Color.green);
            Debug.DrawRay(RFray.position, RFray.up * this.jdistance, Color.green);
            Debug.DrawRay(RBray.position, RBray.up * this.jdistance, Color.green);
            Debug.DrawRay(HeadRay.position, HeadRay.up * 0.5f, Color.green);
        }
    }
}