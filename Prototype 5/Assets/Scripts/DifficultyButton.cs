using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager; //GameManager.cs 불러오기

    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // GameManager.cs의 요소 가져오기

        button.onClick.AddListener(SetDifficulty); //클릭하면 SetDifficulty 실행
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDifficulty()
    {
        Debug.Log(button.gameObject.name + " was clicked");
        gameManager.StartGame(difficulty); // GameManager.cs의 StartGame() 실행
    }
}
