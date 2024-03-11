using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBullet : MonoBehaviour
{
    #region Variables
    [SerializeField] private float shotSpeed; // The speed of the bullet
    public float bulletDamage;
    private Rigidbody2D _rb; // Reference to the Rigidbody2D component
    private SpriteRenderer _spriteRenderer; // Reference to the SpriteRenderer component
    private CircleCollider2D _circleCollider2D; // Reference to the CircleCollider2D component
    
    [SerializeField] ParticleSystem explosion;
    
    #endregion
    
    #region Unity Callbacks
    // Start is called before the first frame update
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        _circleCollider2D = GetComponent<CircleCollider2D>(); // Get the CircleCollider2D component
        // Get the Rigidbody2D component
        _rb = GetComponent<Rigidbody2D>();
        // Set the initial velocity of the bullet
        Destroy(gameObject, 20f);
    }
    #endregion

    #region Bullet Movement
    // Set the velocity of the bullet to move straight
    public void SetStraightVelocity(Vector2 Direction)
    {
        _rb.velocity = Direction * shotSpeed;
    }
    #endregion

    #region Collision Handling
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Damage the enemy
         IDamageable iDamageable = other.gameObject.GetComponent<IDamageable>();
         if (iDamageable != null)
         {
             iDamageable.Damage(1);
         }
        
        // Destroy the bullet
        explosion.Play();
        HideBullet();
        Destroy(gameObject,0.6f);
    }

    void HideBullet()
    {
        Destroy(_spriteRenderer);
        Destroy(_circleCollider2D);
    }
    #endregion
}