using UnityEngine;
using System;

public class ArrowScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D arrowRb;
    [SerializeField] public float force;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        arrowRb = GetComponent<Rigidbody2D>();
    }


    void OnEnable() {
        Vector3 direction = player.transform.position - transform.position;
        arrowRb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
    }
 
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) {
            gameObject.SetActive(false);
        } else if (other.gameObject.CompareTag("Player")){
            gameObject.SetActive(false);
        } else if (other.gameObject.CompareTag("Obstacle")){
            gameObject.SetActive(false);
        } else if (other.gameObject.CompareTag("Bullet")){
            gameObject.SetActive(false);
        }
    }
}
