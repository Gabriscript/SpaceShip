using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public int damage = 10;

    public float velocity = 10;
    public float expirationTime = 1.5f;
    Vector3 direction;
    float expirationTimer;
    bool ejected = false;

    public void Eject(Vector3 direction)
    {
        this.direction = direction;
        ejected = true;
        expirationTimer = expirationTime;
    } 
        void OnTriggerEnter(Collider other) {
        
          if(other.gameObject.GetComponent<Obstacles>() != null)
          {
                other.gameObject.GetComponent<Obstacles>().Hit(damage);
                Destroy(gameObject);
          }
        
        
        }
    
    void Update()
    {
        if (ejected)
        {
            transform.position += direction * velocity * Time.deltaTime;
            expirationTimer -= Time.deltaTime;
            if(expirationTimer <= 0)
                Destroy(gameObject);
        }  
    }
}
