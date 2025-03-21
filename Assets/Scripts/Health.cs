using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField] private float health;
    public float currHealth { get; private set; }

    // For Damage
    private Animator anim;
    private BoxCollider2D playerBox;

    [Header("I-frames")]
    [SerializeField] private float iFrameDur = 2f;
    [SerializeField] private int flashesNum = 5;
    private SpriteRenderer playerSprite;

    private AudioSource[] audioPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        currHealth = health;
    }

    void Start()
    {
        audioPlayer = GetComponents<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            anim.SetTrigger("gotHit");
            DamageTaken(1);
        } else if (other.gameObject.CompareTag("Trap")) {
            anim.SetTrigger("gotHit");
            DamageTaken(0.5f);
        }else if (other.gameObject.CompareTag("Boss")) {
            anim.SetTrigger("gotHit");
            DamageTaken(1.5f);
        } else if (other.gameObject.CompareTag("Bullet")) {
            anim.SetTrigger("gotHit");
            DamageTaken(0.5f);
        } else if (other.gameObject.CompareTag("Respawn")) {
            anim.SetTrigger("gotHit");
            DamageTaken(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fall")) {
            anim.SetTrigger("gotHit");
            DamageTaken(1f);
        } else if (other.gameObject.CompareTag("Trap")) {
            anim.SetTrigger("gotHit");
            DamageTaken(1f);
        }
    }

    // Damage taken by Player
    public void DamageTaken(float getHurt)
    {
        currHealth = Mathf.Clamp(currHealth - getHurt, 0, health);

        if (currHealth > 0)
        {
            // Player gets Hurt
            anim.SetTrigger("gotHit");
            audioPlayer[1].Play();
            StartCoroutine(IFrame_Damage());
        }
        else
        {
            // Player Dies
            anim.SetTrigger("gotHit");
            GetComponent<Player>().enabled = false;
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private IEnumerator IFrame_Damage() {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(6, 9, true);
        Physics2D.IgnoreLayerCollision(6, 10, true);
        Physics2D.IgnoreLayerCollision(6, 11, true);

        for (int i = 0; i < flashesNum; i++) {
            playerSprite.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDur / (flashesNum * 2));
            playerSprite.color = Color.white;
            yield return new WaitForSeconds(iFrameDur / (flashesNum * 2));
        } 
        
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 9, false);
        Physics2D.IgnoreLayerCollision(6, 10, false);
        Physics2D.IgnoreLayerCollision(6, 11, false);
    }
}
