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

    [SerializeField] AudioClip _buttonPress;
    [SerializeField] AudioClip _buttonRelease;
    
    private bool _hasPressed;
    private bool _hasReleased;
    public  bool hasDestructible;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (hasDestructible)
        {
            Interact(true);
            PressButton();
        }
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
                PressButton();
                
                isPressed = true;
            }
        }
    }

    public void isDestroyed()
    {
        hasDestructible = false;
        if (isEmpty)
        {
            Interact(true);
            ReleaseButton();
            
        }
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("PlayerFeet") || other.CompareTag("Destructible")) 
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
                ReleaseButton();
            }
        }
    }
    
    void ReleaseButton()
    {
        _spriteRenderer.sprite = _releasedButton;
        isPressed = false;
        SoundFXManager.Instance.PlaySoundFXClip(_buttonRelease,transform,0.5f);
    }
    
    void PressButton()
    {
        _spriteRenderer.sprite = _pressedButton;
        isPressed = true;
        SoundFXManager.Instance.PlaySoundFXClip(_buttonPress,transform,1f);
    }
}
