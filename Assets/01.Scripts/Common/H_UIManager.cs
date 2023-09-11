using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class H_UIManager : MonoBehaviour
{
    public Transform Canvas;
    public Transform menu;
    public Transform player;

    private void Start()
    {
        Canvas = GameObject.Find("SettingWindow").gameObject.transform;
        player = GameObject.Find("Player").gameObject.transform;
        menu = Canvas.transform.GetChild(0).gameObject.transform;
        menu.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (menu.gameObject.activeSelf)
                menu.gameObject.SetActive(false);
            else
                menu.gameObject.SetActive(true);

        if (menu.gameObject.activeSelf)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void Btn_Resume()
    {
        menu.gameObject.SetActive(false);
    }

    public void Btn_Load()
    {
        menu.gameObject.SetActive(false);
        H_GameManager.instance.Load();
    }
    public void Btn_MainMenu()
    {
        menu.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
