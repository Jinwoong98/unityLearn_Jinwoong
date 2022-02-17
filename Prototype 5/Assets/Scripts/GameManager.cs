using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Text�� ������ֱ� ���� Library
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets; // List
    public TextMeshProUGUI scoreText; //Score Text�� �־��ֱ����� �ڽ� ����
    public TextMeshProUGUI gameOverText; //Game Over Text�� �־��ֱ����� �ڽ� ����
    public Button restartButton;
    public bool isGameActive; // ���� ���߰� �ϱ�
    public GameObject titleScreen; // ���� ������ �ڽ� ����

    private int score;
    private float spawnRate = 1.0f;


    // public GameObject[] targets2; (Array)
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget() // Target�� ���� ����
    {
        while (isGameActive) // true = ���ѻ���, if false = �������� ���� until Game is over
        {
            yield return new WaitForSeconds(spawnRate); // 1�ʿ� �ѹ� ��
            int index = Random.Range(0, targets.Count); // index 0 ���� target ������ŭ ��������
            Instantiate(targets[index]); // Game Object ���纻 �����   
        }
    }

    public void UpdateScore(int scoreToAdd) // Target Script������ �����ְ� public���� �ٲ���.
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame() // Game Restart 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� �ٽ� ����
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget()); // IEnumerator �Լ� ����� ������
        score = 0;

        // Easy: 1 / 1 = 1�� ����, Medium: 1 / 2 = 0.5�� ����, Hard: 1/3 = 0.3�� ����
        spawnRate /= difficulty; // spawnRate = spawnRate / difficulty; 

        UpdateScore(0);
        titleScreen.gameObject.SetActive(false); // ������ �������� �� ���� ������ ����
    }
}
