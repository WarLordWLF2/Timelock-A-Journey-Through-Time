using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float aggroRange = 20;
    private bool enemyDead;

    [Header("Bullets")]
    public GameObject bullet;
    
    public UnityEngine.Rendering.Universal.Light2D gunLight; 
    public Transform bulletPos;
    private float timer;
    [SerializeField] private float finalTimer;

    // [Header("Audio")]
    // public AudioSource gunshotSound; 

    // Update is called once per frame

   
    void Update()
    {
        if (enemyDead || player == null) return;

        float playerDistance = Vector2.Distance(transform.position, player.position);

        if (playerDistance <= aggroRange)
        {
            if (playerDistance < aggroRange)
            {
                timer += Time.deltaTime;
                if (timer > finalTimer)
                {
                    timer = 0;
                    Shoot();
                }
            }
            RotateTowardsPlayer();
        }
    }

    // Shoots
    private void Shoot()
    {
        GameObject bullet = ArrowPool.instance.GetArrowObj();

        if (bullet != null)
        {
            bullet.transform.position = bulletPos.position;
            bullet.SetActive(true);
        }
        //  if (gunshotSound != null)
        // {
        //     gunshotSound.Play();
        // }
    }

    // Tracks the Player by Rotation
    void RotateTowardsPlayer()
    {
        // Calculate direction
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg; // Get angle

        // Apply rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }



}
