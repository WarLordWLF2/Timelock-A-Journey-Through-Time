using UnityEngine;

public class DispenserScript : MonoBehaviour
{
    [Header("Tracking Player")]
    private float playerDist = 15.5f;
    private GameObject player;

    [Header("Bullets")]
    public GameObject arrow;
    public Transform arrowPos;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < playerDist)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }

    }

    private void shoot()
    {
        GameObject arrow = ArrowPool.instance.GetArrowObj();

        if (arrow != null) {
            arrow.transform.position = arrowPos.position;
            arrow.SetActive(true);
        }
    }
}
