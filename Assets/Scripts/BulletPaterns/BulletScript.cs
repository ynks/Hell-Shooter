using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletLife = 10.0f;  // Defines how long before the bullet is destroyed
    public float speed = 1.0f;
    
    //Damage and multiplier is stored inside each bullet.
    public float storedDamage = 1.0f;

    private Vector2 _spawnPoint;
    private float _timer;

    private Vector3 _transformRight;
    private Transform _tr;

    public void SetLifeAndSpeed(float life, float s)
    {
        speed = s;
        bulletLife = life;
    }
   
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
       	_tr = transform;  // Cache the transform reference
        _spawnPoint = _tr.position;  // Use Vector2 directly for more efficient storage
        _transformRight = _tr.right.normalized;  // Cache direction to avoid recomputing per frame
        
    }

    void Update()
    {
		_timer += Time.deltaTime;

        if (_timer > bulletLife)
        {
            Destroy(gameObject);  // Slightly faster than `this.gameObject`
            return;  // Early return to avoid unnecessary movement calculations
        }
        
        // Only move if the bullet is alive
        _tr.position = _spawnPoint + _direction * (_timer * speed);  // Avoid calling the Movement() method
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.unityLogger.Log(other.gameObject.name + " collided with " + this.gameObject.name);
        if (other.gameObject.CompareTag("Enemy") && CompareTag("PlayerBullet"))
        {
            other.gameObject.SendMessage("DoDamage", storedDamage);
            Destroy(this.gameObject);
        }
        
        //Warning! Asteroid implementation is on AsteroidController

        //@TODO: Add fancy effects
        
    }

}
