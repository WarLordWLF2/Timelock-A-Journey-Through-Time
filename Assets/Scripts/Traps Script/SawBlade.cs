using UnityEngine;
using System;

public class SawBlade : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool moveLeft;
    private float leftEdge;
    private float rightEdge;

    void Awake()
    {
        
        leftEdge = transform.position.x - moveDistance;
        rightEdge = transform.position.x + moveDistance;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft) {
            if (transform.position.x > leftEdge) {
                transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y,  transform.position.z);
                transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
            } else {
                moveLeft = false;
            }
        } else {
            if (transform.position.x < rightEdge) {
                transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y,  transform.position.z);
                transform.localScale = new Vector2(-Math.Abs(transform.localScale.x), transform.localScale.y);
            } else {
                moveLeft = true;
            }
        }
    }
}
