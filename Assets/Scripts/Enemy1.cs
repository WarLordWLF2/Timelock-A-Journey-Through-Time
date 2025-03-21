using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Basically this script is just a copy of the enemy script but its for enemies thats mirrored

public class Mirrored_Enemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Animator anim;
    private Rigidbody2D enemyRb;
    [SerializeField] private float moveSpeed;
    private float aggroRange = 6;

    [Header("Enemy Health")]
    [SerializeField] public float enemyHealth = 20f;
    public float currHealth;
    private bool enemyDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector2.Distance(transform.position, player.position);

        if (enemyHealth < currHealth)
        {
            currHealth = enemyHealth;
            anim.SetTrigger("isDamaged");
        }

        if (enemyHealth <= 0)
        {
            Die();
        }


        if (playerDistance <= aggroRange && !enemyDead)
        {
            anim.SetBool("isWalking", true);
            Move();
        } else {
            anim.SetBool("isWalking", false);
        }

        
    }

    

    void Die() {
        enemyDead = true;
        anim.SetBool("isDead", true);
        enemyRb.linearVelocity = Vector2.zero;
    }

    void Move()
    {

        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(-Math.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
        }

        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed);

    }
}
