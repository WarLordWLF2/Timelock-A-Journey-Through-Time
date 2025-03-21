using Unity.VisualScripting;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    private RespawnScript respawn;

    
    void Awake()
    {
        respawn = GameObject.FindGameObjectWithTag("Fall").GetComponent<RespawnScript>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) {
            respawn.respawnPoint = this.gameObject;
        }
    }
}
