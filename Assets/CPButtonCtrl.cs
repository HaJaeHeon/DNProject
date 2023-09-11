using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPButtonCtrl : MonoBehaviour
{
    GameObject gameMg;
    // Start is called before the first frame update
    void Start()
    {
        gameMg = GameObject.Find("GameManager");

        gameObject.GetComponent<Button>().onClick.AddListener(gameMg.GetComponent<H_GameManager>().LastCheckPoint);
    }
}
