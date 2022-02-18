using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb; // Rigid body ����
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    public int pointValue;
    public ParticleSystem explosionParticle; // Ŭ���� �� ȿ���ֱ�

    private GameManager gameManager; // GameManager.cs script ��������

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>(); //����Ƽ���� add component�� �Ѵٰ� �ؼ� ���� �ƴ϶� Rigid body ������ ���־�� ��.

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // GameManager.cs script ��� ��������

        targetRb.AddForce(RandomForce(), ForceMode.Impulse); // rigid body�� ���� �߰�.
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse); // rigid body�� ������ �߰�.
        // �������� ������ ���� ����: ForceMode.Force
        // �������� ������ ���� ����: ForceMode.Acceleration
        // �������� ��(���� ���¿��� ����)���� ���� ����: ForceMode.Impulse
        // �������� ��(���� ���¿��� ����)���� ���� ����: ForceMode.VelocityChange
        transform.position = RandomSpawnPos();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() // ���콺 Ŭ������ game object ���ֱ�.
    {
        if (gameManager.isGameActive) // ������ ���ư� �� ����
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); // ��ü Ŭ���� ������ ȿ��
            gameManager.UpdateScore(pointValue); //GameManager.cs script���� ������ UpdateScore() ���.
        }
    }

    private void OnTriggerEnter(Collider other) // target���� censor�� ����� �� ���ֱ�.
    {
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad") && gameManager.isGameActive) //Bad Object�� �ƴ� Good Object�� censor�� ���� ��
        {
            gameManager.UpdateLives(-1); // Live�� ���� �� Game Over ���
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
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos); // y���� -6���� ����, x���� random�ϰ�.
    }
}
