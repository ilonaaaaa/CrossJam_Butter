using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject start;
    [SerializeField] GameObject titre;
    [SerializeField] GameObject fondMenu;
    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;
    [SerializeField] Text mvtC;

    public GameObject winS;
    public GameObject looseS;
    int countTrigger = 0;
    public int mvtCount = 10;
    bool startClick = false;
    private AudioSource _winS;
    private AudioSource _looseS;

    public enum State
    {
        Begining,
        InGame,
        GameOver
    }
    public State gameState = State.Begining;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        gameState = State.Begining;
        player.gameObject.SetActive(false);
        mvtC.gameObject.SetActive(false);
        _winS = winS.GetComponent<AudioSource>();
        _looseS = looseS.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(startClick == false && gameState == State.Begining)
        {
        start.GetComponent<Button>().onClick.AddListener(StartGame);
        }
        
    }

    public void StartGame()
    {
        startClick = true;
        gameState = State.InGame;
        fondMenu.gameObject.SetActive(false);
        start.gameObject.SetActive(false);
        titre.gameObject.SetActive(false);
        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        mvtC.gameObject.SetActive(true);            
    }

    public void WinCheck()
    {
        countTrigger++;
        if (countTrigger == 27)
        {
            GameOverWin();
        }
    }

    public void MvtCheck()
    {
        mvtCount--;
        mvtC.text = mvtCount.ToString();
        if (mvtCount == 0)
        {
            GameOverLose();
        }
    }

    void GameOverWin()
    {
        gameState = State.GameOver;
        _winS.Play();
        win.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        mvtC.gameObject.SetActive(false);
        Invoke("Restart", 3f);        
    }

    void GameOverLose()
    {
        gameState = State.GameOver;
        _looseS.Play();
        lose.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        mvtC.gameObject.SetActive(false);
        Invoke("Restart", 3f);        
    }

    void Restart()
    {
        SceneManager.LoadScene("ButterJam");
    }
}
