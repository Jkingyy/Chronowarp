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

    public bool isPressed;
    public bool isEmpty;

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
    void FixedUpdate()
    {
        isEmpty = true;

    }
    
    void Interact(bool hasInteracted)
    {
       foreach (GameObject interactableObject in _objectsToActivate)
        {
            if(interactableObject == null) continue;
            
            IInteractable interactable = interactableObject.GetComponent<IInteractable>();
            
            if(interactable == null) continue;


            interactable.ChangeState();
            

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerFeet"))
        {
            if (!isPressed)
            {
                Interact(true);
                _spriteRenderer.sprite = _pressedButton;
                isPressed = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("PlayerFeet"))
        {
            isEmpty = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("PlayerFeet"))
        {
            if (isPressed && isEmpty)
            {
                Interact(false);
                _spriteRenderer.sprite = _releasedButton;
                isPressed = false;
            }
        }
    }
}
