using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartBar : MonoBehaviour
{
    public GameObject heartPrefab;
    
    public PlayerStats playerStats;
    
    List<HealthHeart> heartList = new List<HealthHeart>();


    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        heartList = new List<HealthHeart>();
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);
        
        HealthHeart heart = newHeart.GetComponent<HealthHeart>();
        heart.SetHeartImage(HeartStatus.Empty);
        heartList.Add(heart);

    }

    public void DrawHearts()
    {
        ClearHearts();
        
        for (int i = 0; i < playerStats.maxHealth; i++)
        {
            CreateEmptyHeart();
        }
        
        for(int i = 0; i < playerStats.currentHealth; i++)
        {
            heartList[i].SetHeartImage(HeartStatus.Full);
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
