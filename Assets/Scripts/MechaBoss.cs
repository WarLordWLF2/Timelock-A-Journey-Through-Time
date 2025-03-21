using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MechaBoss : MonoBehaviour
{
    // Boss Behaviour
    [SerializeField] private GameObject player;
    private Animator anim;
    private Rigidbody2D enemyRb;
    [SerializeField] private float moveSpeed;
    private float freezeSpeed = 0;
    private float currSpeed;
    [SerializeField] private float aggroRange;
    [SerializeField] private float attackRange;

    [Header("Attacks")]
    public GameObject bullet;
    public Transform cannon_1;
    public Transform cannon_2;
    private float timer;

    [Header("I-Frame")]
    [SerializeField] private float iFrameDur;
    [SerializeField] private int flashesNum;
    private SpriteRenderer playerSprite;

    [Header("Health")]
    public float boss_health = 15f;
    public float currHealth;
    private bool bossDead;

    // Boss Movement
    public GameObject corner_1;
    public GameObject corner_2;
    private Transform currentPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boss_health = 15;
        currHealth = boss_health;
        anim = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentPosition = corner_2.transform;
        currSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceBetweenPlayer = UnityEngine.Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distanceBetweenPlayer);

        // Movement
        UnityEngine.Vector2 newPosition = currentPosition.position - transform.position;
        if (currentPosition == corner_2.transform)
        {
            enemyRb.linearVelocity = new UnityEngine.Vector2(currSpeed, 0);
        }
        else
        {
            enemyRb.linearVelocity = new UnityEngine.Vector2(-currSpeed, 0);
        }

        if (UnityEngine.Vector2.Distance(transform.position, currentPosition.position) < 0.5f && currentPosition == corner_2.transform)
        {
            currSpeed = freezeSpeed;
            // currentPosition = corner_1.transform;
        }
        if (UnityEngine.Vector2.Distance(transform.position, currentPosition.position) < 0.5f && currentPosition == corner_1.transform)
        {
            currSpeed = freezeSpeed;
            // currentPosition = corner_2.transform;
        }


        // Boss Faces at the player
        if (distanceBetweenPlayer <= aggroRange)
        {
            FaceToPlayer();
        }

        // Attacks
        if (distanceBetweenPlayer <= attackRange && !bossDead)
        {
            timer += Time.deltaTime;
            if (timer > 3f)
            {
                timer = 0;
                shoot1();
            }
        }

        if (boss_health < currHealth && !bossDead)
        {
            // Hurts by the Player
            currHealth = boss_health;
            anim.SetTrigger("isDamaged");
            StartCoroutine(IFrame_Damage());

            if (UnityEngine.Vector2.Distance(transform.position, currentPosition.position) < 0.5f && currentPosition == corner_2.transform)
            {
                currSpeed = moveSpeed;
                currentPosition = corner_1.transform;
            }
            if (UnityEngine.Vector2.Distance(transform.position, currentPosition.position) < 0.5f && currentPosition == corner_1.transform)
            {
                currSpeed = moveSpeed;
                currentPosition = corner_2.transform;
            }
        }

        // Checks if the boss is dead
        if (boss_health <= 0)
        {
            Die();
        }
    }

    // When Boss Dies
    void Die()
    {
        bossDead = true;
        anim.SetBool("isDefeated", true);
        enemyRb.linearVelocity = UnityEngine.Vector2.zero;
    }

    // Boss has Invulnerability
    public IEnumerator IFrame_Damage()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < flashesNum; i++)
        {
            playerSprite.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDur / (flashesNum * 2));
            playerSprite.color = Color.white;
            yield return new WaitForSeconds(iFrameDur / (flashesNum * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    // Boss Attacks
    void shoot1()
    {
        anim.SetTrigger("isAttacking");
        Instantiate(bullet, cannon_1.position, UnityEngine.Quaternion.identity);
        Instantiate(bullet, cannon_2.position, UnityEngine.Quaternion.identity);
    }

    // Focuses on Player
    void FaceToPlayer()
    {

        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new UnityEngine.Vector2(-Math.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            transform.localScale = new UnityEngine.Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(corner_1.transform.position, 0.5f);
        Gizmos.DrawWireSphere(corner_2.transform.position, 0.5f);
    }

}
