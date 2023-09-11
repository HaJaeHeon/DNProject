using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Candle : MonoBehaviour
{
    GameObject fire;
    Light lighting;

    private void Start()
    {
        fire = GetComponentInChildren<ParticleSystem>().gameObject;
        fire.SetActive(false);

        lighting = GetComponentInChildren<Light>();
        lighting.enabled = false;
    }

    public void Save()
    {
        StartCoroutine(SaveDelay());
    }

    IEnumerator SaveDelay()
    {
        yield return new WaitForSeconds(2f);
        fire.SetActive(true);
        lighting.enabled = true;
    }
}
