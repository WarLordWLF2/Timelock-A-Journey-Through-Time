using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1.5f;
    private float destroyTimer = 1.5f;
    [SerializeField] private Rigidbody2D platformRB;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player")) {
            StartCoroutine(Fall());
        } else {
            Destroy(gameObject);
        }
    }

    private IEnumerator Fall() {
        yield return new WaitForSeconds(fallDelay);
        platformRB.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(destroyTimer);
        Destroy(gameObject, destroyTimer);
    }
}
