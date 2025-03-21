using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CoinManage : MonoBehaviour
{
    public int fragCount;
    public bool unlockDoor = false;
    public TextMeshProUGUI fragsCollected;
    public GameObject door1;
    public bool isDoorDestroyed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fragsCollected.text = fragCount.ToString();
    }
}