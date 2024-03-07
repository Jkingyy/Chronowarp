using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerResources : MonoBehaviour, IDamageable
{
    
    [SerializeField] PlayerStats playerStats;
    GameObject[] _ghostList;
    
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(playerStats.currentHealth <= 0)
        {
            DestroyAllGhosts();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Damage(int DamageAmount)
    {
        playerStats.currentHealth -= DamageAmount;
        
        if (playerStats.currentHealth <= 0)
        {
            Die();
        }
    }
    
    
    void Die()
    {
        DestroyAllGhosts();
        _animator.SetTrigger("Die");
    }


    void DestroyAllGhosts()
    {
        _ghostList = GameObject.FindGameObjectsWithTag("PlayerGhost");
        
        foreach (GameObject _GameObject in _ghostList)
        {
            Destroy(_GameObject);
        }
    }
}
