using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class H_SaveFIre : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    Transform Paticle;
    [SerializeField]
    H_PlayerLighter Lighter;
    [SerializeField]
    BoxCollider Bcol;
    //AudioSource audio;
    //AudioClip firebust;

    bool isTri = false;
    bool isSave = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Paticle = transform.GetChild(0).GetComponent<Transform>();
        Lighter = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<H_PlayerLighter>();
        Bcol = GetComponent<BoxCollider>();
        //audio = GetComponent<AudioSource>();
        //firebust = Resources.Load<AudioClip>("FireBust");
    }

    private void FixedUpdate()
    {
        TriSave();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isTri = true;
            if (Lighter.isLight && !isSave)
            {
                Save();
                Paticle.gameObject.SetActive(true);
                //if (!audio.isPlaying)
                    //audio.PlayOneShot(firebust, 1.0f);
                Bcol.enabled = false;
                isSave = true;
            }

        }
    }
    void TriSave()
    {
        if (isTri)
        {
            if (!Lighter.isLight && Input.GetKeyDown(KeyCode.F) && !isSave)
            {
                Save();
                Paticle.gameObject.SetActive(true);
                //if (!audio.isPlaying)
                   // audio.PlayOneShot(firebust, 1.0f);
                Bcol.enabled = false;
                isSave = true;
                //Debug.Log("1");
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

    public void Save()
    {
        PlayerPrefs.SetFloat("x", player.transform.position.x);
        PlayerPrefs.SetFloat("y", player.transform.position.y);
        PlayerPrefs.SetFloat("z", player.transform.position.z);
        PlayerPrefs.SetFloat("Rot_y", player.transform.eulerAngles.y);
    }
}