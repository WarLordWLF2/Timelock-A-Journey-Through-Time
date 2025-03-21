using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;

    float offsetX = 3.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + offsetX,transform.position.y,transform.position.z);
    }
}
