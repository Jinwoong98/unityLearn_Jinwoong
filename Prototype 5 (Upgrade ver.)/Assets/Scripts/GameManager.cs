using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Text를 만들어주기 위한 Library
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets; // List
    public TextMeshProUGUI scoreText; //Score Text를 넣어주기위한 박스 생성
    public TextMeshProUGUI gameOverText; //Game Over Text를 넣어주기위한 박스 생성
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public bool isGameActive; // 게임 멈추게 하기
    public GameObject titleScreen; // 시작 페이지 박스 생성

    public GameObject pauseScreen; // 게임 중단 박스 생성

    private int score;
    private int lives;
    private float spawnRate = 1.0f;

    private bool paused;

    // public GameObject[] targets2; (Array)
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangePaused();
        }
    }

    IEnumerator SpawnTarget() // Target들 무한 생성
    {
        while (isGameActive) // true = 무한생성, if false = 생성되지 않음 until Game is over
        {
            yield return new WaitForSeconds(spawnRate); // 1초에 한번 씩
            int index = Random.Range(0, targets.Count); // index 0 부터 target 개수만큼 무작위로
            Instantiate(targets[index]); // Game Object 복사본 만들기   
        }
    }

    public void UpdateScore(int scoreToAdd) // Target Script에서도 쓸수있게 public으로 바꿔줌.
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame() // Game Restart 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 게임 다시 시작
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget()); // IEnumerator 함수 끌어다 가져옴
        score = 0;

        // Easy: 1 / 1 = 1초 마다, Medium: 1 / 2 = 0.5초 마다, Hard: 1/3 = 0.3초 마다
        spawnRate /= difficulty; // spawnRate = spawnRate / difficulty; 

        UpdateScore(0);
        UpdateLives(3);
        titleScreen.gameObject.SetActive(false); // 게임이 시작했을 때 시작 페이지 끄기
    }
    
    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
