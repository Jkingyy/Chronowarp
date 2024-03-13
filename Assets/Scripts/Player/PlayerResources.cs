using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerResources : MonoBehaviour, IDamageable
{
    
    [SerializeField] PlayerStats playerStats;
    GameObject[] _ghostList;
    
    private Animator _animator;
    private SceneTransition _sceneTransition;
    private HealthHeartBar _healthHeartBar;
    private PlayerMovement _playerMovement;
    private UIManager _uiManager;
    
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip damageSound;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _sceneTransition = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<SceneTransition>();
        _healthHeartBar = GameObject.FindGameObjectWithTag("HealthHeartBar").GetComponent<HealthHeartBar>();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _playerMovement = GetComponent<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerStats.LoadData();
        
        if (playerStats.newLevel)
        {
            playerStats.currentHealth = playerStats.maxHealth;
            playerStats.newLevel = false;
            _sceneTransition.FadeToClear();
            _healthHeartBar.DrawHearts();
        }
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
        _healthHeartBar.DrawHearts();
        
        if (playerStats.currentHealth <= 0)
        {
            Die();
        }
        else
        {
            _animator.SetTrigger("Damaged");
            SoundFXManager.Instance.PlaySoundFXClip(damageSound, transform, 1f);
        }
        
        
    }
    
    
    void Die()
    {
        DestroyAllGhosts();
        _playerMovement.DisablePlayerMovement();
        _animator.SetTrigger("Die");
        SoundFXManager.Instance.PlaySoundFXClip(deathSound, transform, 1f);
    }
    
    void ShowGameOver()
    {
        _uiManager.GameOver();
    }


    public void DestroyAllGhosts()
    {
        _ghostList = GameObject.FindGameObjectsWithTag("PlayerGhost");
        
        foreach (GameObject _GameObject in _ghostList)
        {
            Destroy(_GameObject);
        }
    }
}
