using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerResources : MonoBehaviour, IDamageable
{
    
    [SerializeField] PlayerStats _playerStats;
    [SerializeField] Transform _ghostList;
    
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Loop"))
        {
            Damage(1);
        }
    }
    public void Damage(int damageAmount)
    {
        _playerStats.currentHealth -= damageAmount;
        
        if (_playerStats.currentHealth <= 0)
        {
            Die();
        }
    }
    
    
    public void Die()
    {
        DestroyAllGhosts();
        _animator.SetTrigger("Die");
    }


    void DestroyAllGhosts()
    {
        foreach (Transform t in _ghostList)
        {
            Destroy(t.gameObject);
        }
    }
}
