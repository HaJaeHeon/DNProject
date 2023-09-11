using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class H_MonsterCtrl : MonoBehaviour
{

    public List<Transform> wayPoints;
    public int nexIdx = 0;
    private NavMeshAgent nav;
    public Transform target;
    Animator ani;
    H_PlayerLighter pLighter;
    H_BossDoor bDoor;

    float mSpeed = 2f;
    float damping = 1.0f;
    float dis;

    bool isTrace;

    public float speed
    {
        get { return nav.velocity.magnitude; }
    }


    void Start()
    {
        bDoor = GameObject.Find("ProfileDoor (1)").GetComponentInChildren<H_BossDoor>();
        target = GameObject.FindWithTag("Player").transform;
        ani = gameObject.GetComponent<Animator>();
        pLighter = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<H_PlayerLighter>(); nav = GetComponent<NavMeshAgent>();
        nav.autoBraking = false;
        nav.updateRotation = false;
        nav.speed = mSpeed;
        var group = GameObject.Find("WayPointGroup");
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);
        }
        MoveWayPoint();
    }

    private void Update()
    {
        MoveWayPoint();
        AroundTarget();
        Die();
        if (bDoor.burned != 9)
            rot();

    }
    void MoveWayPoint()
    {
        if (!isTrace)
        {
            nav.isStopped = false;
            nav.destination = wayPoints[nexIdx].position;

            patrol();
        }
        if (isTrace)
            return;
    }

    void rot()
    {
        Quaternion rot = Quaternion.LookRotation(nav.desiredVelocity);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * damping);
    }

    void AroundTarget()
    {
        dis = Vector3.Distance(transform.position, target.position);
        if (dis < 5f)
        {
            isTrace = true;
            mSpeed = 3f;
            TraceTarget();
        }
        else if (dis >= 5f)
        {
            isTrace = false;
            mSpeed = 2f;
            ani.SetBool("isTrace", false);
                MoveWayPoint();
            
        }

    }


    void patrol()
    {
        if (nav.remainingDistance < 2f)
        {
            if (nexIdx + 1 >= wayPoints.Count)
                nexIdx = 0;
            else
                nexIdx += 1;
        }
        nav.destination = wayPoints[nexIdx].position;
        
    }
    void endTrace()
    {
        isTrace = false;
        ani.SetBool("isTrace", false);

        float remainDis;
        float temp;
        int resultIdx = 0;

        for (int i = 0; i < wayPoints.Count - 1; i++)
        {
            remainDis = Vector3.Distance(transform.position, wayPoints[i].position);
            temp = Vector3.Distance(transform.position, wayPoints[i + 1].position);
            remainDis = (remainDis < temp) ? remainDis : temp;
            resultIdx = (remainDis < temp) ? i : i + 1;
        }
        nexIdx = resultIdx;
        MoveWayPoint();
    }

    void TraceTarget()
    {
        dis = Vector3.Distance(transform.position, target.position);
        ani.SetBool("isTrace", true);
        nav.destination = target.position;
        if (dis < 5f)
        {
            if (nav.remainingDistance < 1f)
            {
                Vector3 cen = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                Debug.DrawRay(cen, transform.forward, Color.green, 2f);
                nav.isStopped = true;
                ani.SetBool("isAttack", true);
            }
            else if(nav.remainingDistance >= 1f && nav.remainingDistance <5f)
            {
                nav.isStopped = false;
                ani.SetBool("isAttack", false);
            }
            else if(nav.remainingDistance >= 5f)
            {
                endTrace();
                mSpeed = 2f;
            }
        }
        if(dis >= 5f)
        {
            endTrace();
            mSpeed = 2f;
        }
    }

    void Die()
    {
        if (bDoor.burned == 9)
        {
            ani.SetBool("isDie", true);
            nav.isStopped = true;
        }
        else if (bDoor.burned != 9)
            return;
    }
}
