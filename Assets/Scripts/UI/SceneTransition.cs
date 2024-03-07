using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    
    Animator _animator;
    
    [SerializeField] PlayerStats playerStats;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void FadeToBlack()
    {
        _animator.SetTrigger("FadeOut");
    }
    
    public void FadeToClear()
    {
        _animator.SetTrigger("FadeIn");
    }
    
    void LoadNextScene()
    {
        SceneManager.LoadScene(playerStats.currentLevel++);
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
