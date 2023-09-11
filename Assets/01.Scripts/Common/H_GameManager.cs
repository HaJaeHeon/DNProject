using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class H_GameManager : MonoBehaviour
{
    GameObject player;
    public static H_GameManager instance = null;
    public bool isGameover = false;
    public Button pbutton;
    //public Button lcbutton;
    public Button exbutton;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        player = GameObject.Find("Player");
        PlayerPrefs.SetFloat("x", -34f);
        PlayerPrefs.SetFloat("y", 1.7f);
        PlayerPrefs.SetFloat("z", -1.4f);
        PlayerPrefs.SetFloat("Rot_y", 180f);
        
        
        
        

    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();

    }
    private void Update()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            pbutton = GameObject.Find("Play_Button").GetComponent<Button>();
            //lcbutton = GameObject.Find("CheckPoint_Button").GetComponent<Button>();
            exbutton = GameObject.Find("Exit_Button").GetComponent<Button>();
        }
    }
    public void Load()
    {
        player = GameObject.Find("Player");
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
        player.transform.eulerAngles = new Vector3(0f, PlayerPrefs.GetFloat("Rot_y"), 0f);
    }

    public void EndScene(float delay)
    {
        StartCoroutine(End(delay));
    }

    IEnumerator End(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("End");
        isGameover = false;
    }
    IEnumerator DeleayTime(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    public void PlayGame()
    {
        pbutton = GameObject.Find("Play_Button").gameObject.GetComponent<Button>();
        pbutton.onClick.AddListener(PlayGame);
        player = GameObject.Find("Player");
        StartCoroutine(DeleayTime(2f));
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("-34f"), PlayerPrefs.GetFloat("1.7f"), PlayerPrefs.GetFloat("-1.4f"));
        player.transform.eulerAngles = new Vector3(0f, PlayerPrefs.GetFloat("180f"), 0f);
        SceneManager.LoadScene("PlayScene");
    }

    public void LastCheckPoint()
    {
        //lcbutton = GameObject.Find("CheckPoint_Button").gameObject.GetComponent<Button>();
        //lcbutton.onClick.AddListener(Load);
        StartCoroutine(DeleayTime(2f));
        player = GameObject.Find("Player");

        SceneManager.LoadScene("PlayScene");
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
        player.transform.eulerAngles = new Vector3(0f, PlayerPrefs.GetFloat("Rot_y"), 0f);

    }
    public void SelectMap()
    {

    }
    public void Exit()
    {
        exbutton = GameObject.Find("Exit_Button").gameObject.GetComponent<Button>();
        exbutton.onClick.AddListener(Exit);
        Application.Quit();
    }
}
