using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_ComFire : MonoBehaviour
{
    [SerializeField]
    private Transform fire;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private H_PlayerLighter pLighter;
    [SerializeField]
    private BoxCollider Bcol;
    [SerializeField]
    H_BossDoor bDoor;

    bool isBurn;
    bool isTri;

    private void Start()
    {
        fire = gameObject.transform.GetChild(1).GetComponentInChildren<Transform>();
        player = GameObject.FindWithTag("Player").gameObject.GetComponent<Transform>();
        pLighter = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<H_PlayerLighter>();
        Bcol = gameObject.GetComponentInChildren<BoxCollider>();
        bDoor = GameObject.Find("ProfileDoor (1)").GetComponentInChildren<H_BossDoor>();

    }
    private void FixedUpdate()
    {
        TriBurn();
    }
    void TriBurn()
    {
        if (isTri)
        {
            if (!pLighter.isLight && Input.GetKeyDown(KeyCode.F) && !isBurn)
            {
                BurnCount();
                fire.gameObject.SetActive(true);
                //if (!audio.isPlaying)
                // audio.PlayOneShot(firebust, 1.0f);
                Bcol.enabled = false;
                isBurn = true;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isTri = true;
            if (pLighter.isLight && !isBurn)
            {
                BurnCount();
                fire.gameObject.SetActive(true);
                //if (!audio.isPlaying)
                //audio.PlayOneShot(firebust, 1.0f);
                Bcol.enabled = false;
                isBurn = true;
            }

        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isTri = false;
        }
    }

    void BurnCount()
    {
        bDoor.burned += 1;

    }


}
