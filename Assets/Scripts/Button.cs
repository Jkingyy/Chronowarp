using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    
    SpriteRenderer _spriteRenderer;
    
    [SerializeField] Sprite _pressedButton;
    [SerializeField] Sprite _releasedButton;
    
    [SerializeField] List<GameObject> _objectsToActivate;


    private bool _hasPressed;
    private bool _hasReleased;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    
    void Interact(bool hasInteracted)
    {
        foreach (GameObject interactableObject in _objectsToActivate)
        {
            IInteractable interactable = interactableObject.GetComponent<IInteractable>();
            
            if(interactable == null) continue;

            if (hasInteracted)
            {
                interactable.Activate();
            }
            else
            {
                interactable.Deactivate();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerFeet"))
        {
            Interact(true);
            _spriteRenderer.sprite = _pressedButton;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("PlayerFeet"))
        {
            Interact(false);
            _spriteRenderer.sprite = _releasedButton;
        }
    }
}
