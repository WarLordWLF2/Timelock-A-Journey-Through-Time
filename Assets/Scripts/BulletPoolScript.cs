using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPoolScript : MonoBehaviour
{

    private static BulletPoolScript instance;
    private List<GameObject> bulletsPooled = new List<GameObject>();
    private int amount = 10;

    [SerializeField] private GameObject bulletPrefab;
    
    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
