using UnityEngine;
using System;

public class SawBlade_Vertical : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool moveTop;
    private float topEdge;
    private float bottomEdge;

    void Awake()
    {
        
        topEdge = transform.position.y + moveDistance;
        bottomEdge = transform.position.y - moveDistance;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update()
    {
        if (moveTop) {
            if (transform.position.y < topEdge) {
                transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime, transform.position.z);
            } else {
                moveTop = false;
            }
        } else {
            if (transform.position.y > bottomEdge) {
                transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime,  transform.position.z);
            } else {
                moveTop = true;
            }
        }
    }
}
