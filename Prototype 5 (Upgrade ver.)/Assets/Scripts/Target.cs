using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb; // Rigid body 선언
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    public int pointValue;
    public ParticleSystem explosionParticle; // 클릭할 때 효과주기

    private GameManager gameManager; // GameManager.cs script 가져오기

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>(); //유니티에서 add component만 한다고 해서 끝이 아니라 Rigid body 선언을 해주어야 함.

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // GameManager.cs script 요소 가져오기

        targetRb.AddForce(RandomForce(), ForceMode.Impulse); // rigid body에 힘을 추가.
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse); // rigid body에 돌림힘 추가.
        // 연속적인 힘에서 무게 적용: ForceMode.Force
        // 연속적인 힘에서 무게 무시: ForceMode.Acceleration
        // 순간적인 힘(정지 상태에서 시작)에서 무게 적용: ForceMode.Impulse
        // 순간적인 힘(정지 상태에서 시작)에서 무게 무시: ForceMode.VelocityChange
        transform.position = RandomSpawnPos();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() // 마우스 클릭으로 game object 없애기.
    {
        if (gameManager.isGameActive) // 게임이 돌아갈 때 까지
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); // 물체 클릭할 때마다 효과
            gameManager.UpdateScore(pointValue); //GameManager.cs script에서 가져온 UpdateScore() 요소.
        }
    }

    private void OnTriggerEnter(Collider other) // target들이 censor에 닿았을 때 없애기.
    {
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad") && gameManager.isGameActive) //Bad Object가 아닌 Good Object가 censor에 닿을 때
        {
            gameManager.UpdateLives(-1); // Live가 없을 때 Game Over 출력
        }
    }

    public void DestroyTarget() // for swipe
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }


    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos); // y값은 -6으로 고정, x값은 random하게.
    }
}
