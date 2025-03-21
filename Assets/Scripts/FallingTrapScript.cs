using UnityEditor;
using UnityEngine;

public class FallingTrapScript : MonoBehaviour
{
    private float destroyTimer;
    private Rigidbody2D trap2D;
    private BoxCollider2D trapCollider;
    [SerializeField] private float detectPlayer; 
    [SerializeField] private float fallSpeed;
    private bool isFalling = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trap2D = GetComponent<Rigidbody2D>();
        trapCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if (isFalling == false) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, detectPlayer);
            Debug.DrawRay(transform.position, Vector2.down * detectPlayer, Color.red);

            if (hit.transform.tag == "Player") {
                trap2D.gravityScale = fallSpeed;
                isFalling = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player")) {
            Destroy(gameObject);
        } else {
            trap2D.gravityScale = 0;
            trapCollider.enabled = false;
        }
    }
}
