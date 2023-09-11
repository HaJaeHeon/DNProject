using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_PlayerLighter : MonoBehaviour
{
    private Animator ani;
    [SerializeField]
    private GameObject Lighter;
    [SerializeField]
    private Light Plight;
    public Transform player;


    RaycastHit hit;
    Ray forRay;

    public bool isLight = false;
    bool isCandle;
    bool isMac;

    public int candleLayer;
    int layerMac;
    public int burned=0;


    private void Awake()
    {
        ani = GetComponent<Animator>();
        Plight = gameObject.transform.GetChild(4).transform.GetChild(2).transform.GetChild(0)
            .transform.GetChild(2).transform.GetChild(2)
            .transform.GetChild(0).transform.GetChild(0)
            .transform.GetChild(0).transform.GetChild(5)
            .transform.GetChild(4).GetComponent<Light>();

        Lighter = gameObject.transform.GetChild(4).transform.GetChild(2).transform.GetChild(0)
            .transform.GetChild(2).transform.GetChild(2)
            .transform.GetChild(0).transform.GetChild(0)
            .transform.GetChild(0).transform.GetChild(5).gameObject;

        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        candleLayer = LayerMask.GetMask("CANDLE");
        layerMac = LayerMask.GetMask("MAC");

        forRay = new Ray(player.transform.position, player.transform.forward);


        isMac = Physics.Raycast(forRay, out hit, 2f, layerMac);
        isCandle = Physics.Raycast(forRay, out hit, 2f, candleLayer);
    }

    private void Update()
    {
        LightOnOff();
        //SaveCandle();
    }
    void LightOnOff()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isLight = !isLight;
            StartCoroutine(Lighting());
            LightOn();
            LightOff();
        }

    }

    void LightOn()
    {
        if (isLight)
        {
            Lighter.gameObject.SetActive(true);
            ani.SetBool("isLight", true);
        }
    }
    void LightOff()
    {
        if (!isLight)
        {
            Lighter.gameObject.SetActive(false);
            ani.SetBool("isLight", false);
        }
    }


    IEnumerator Lighting()
    {
        while (isLight)
        {
            Plight.intensity = Random.Range(2f, 5f);
            yield return new WaitForSeconds(0.1f);
            if (!isLight)
                break;
        }
        yield return null;
    }

    //void SaveCandle()
    //{
    //    if (hit.transform.gameObject.layer == candleLayer)
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            Debug.DrawRay(player.transform.position, player.transform.forward, Color.blue, 2f);
    //            GameManager.instance.SavePosition();
    //        }
    //    }
    //}




}
