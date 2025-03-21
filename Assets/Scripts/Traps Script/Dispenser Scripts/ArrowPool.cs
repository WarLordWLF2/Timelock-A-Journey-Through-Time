using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Pool;

public class ArrowPool : MonoBehaviour
{
    public static ArrowPool instance;
    private List<GameObject> pooledQuiver = new List<GameObject>(); 
    private int amnt = 10;

    [SerializeField] private GameObject arrowPrefab;

    
    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < amnt; i++)
        {
            GameObject obj = Instantiate(arrowPrefab);
            obj.SetActive(false);
            pooledQuiver.Add(obj);
        }
    }

    public GameObject GetArrowObj() {
        for (int i = 0; i < pooledQuiver.Count; i++)
        {
            if (!pooledQuiver[i].activeInHierarchy) {
                return pooledQuiver[i];
            }
        }

        return null;
    }
}
