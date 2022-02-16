using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerUpStrength = 10;
    public float speed = 5.0f;
    public bool hasPowerup = false;
    public GameObject powerupIndicator;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0); //set the position of powerupIndicator
    }

    
    private void OnTriggerEnter(Collider other) //When player eat the jam, player gains power and jam is destroyed.
    {
        if(other.CompareTag("Powerup")) //when collid with the jam that has "Powerup" tag.
        {
            hasPowerup = true; //gain power.
            powerupIndicator.gameObject.SetActive(true); //turn on the powerup Indicator.
            Destroy(other.gameObject); //when collid with jam, destroy the jam.
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine() //after 7 seconds from getting powerup, the player loses the power and powerup Indicator.
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup) //when player has powerup and collid with enemy.
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            Debug.Log("Collided with: " + collision.gameObject.name + " with powerup set to " + hasPowerup); //alert
        }
    }
}
