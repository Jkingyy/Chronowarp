using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartBar : MonoBehaviour
{
    public GameObject heartPrefab;
    
    public PlayerStats playerStats;
    
    List<HealthHeart> _heartList = new List<HealthHeart>();


    void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        _heartList = new List<HealthHeart>();
    }

    void CreateEmptyHeart()
    {
        GameObject _NewHeart = Instantiate(heartPrefab) ?? throw new ArgumentNullException("Instantiate(heartPrefab)");
        _NewHeart.transform.SetParent(transform);
        
        HealthHeart _Heart = _NewHeart.GetComponent<HealthHeart>();
        _Heart.SetHeartImage(HeartStatus.Empty);
        _heartList.Add(_Heart);

    }

    void DrawHearts()
    {
        ClearHearts();
        
        for (int i = 0; i < playerStats.maxHealth; i++)
        {
            CreateEmptyHeart();
        }
        
        for(int i = 0; i < playerStats.currentHealth; i++)
        {
            _heartList[i].SetHeartImage(HeartStatus.Full);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        DrawHearts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
