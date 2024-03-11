using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerFeet")) return;
        
        IDamageable iDamageable = other.gameObject.GetComponentInParent<IDamageable>();
        if (iDamageable != null)
        {
            iDamageable.Damage(1);
        }
        
        PlayerMovement playerMovement = other.gameObject.GetComponentInParent<PlayerMovement>();
        if(playerMovement == null) return;
        Collider2D collider = other.gameObject.GetComponent<Collider2D>();
        collider.enabled = false;
        playerMovement.OnLoop.Invoke();

    }
}
