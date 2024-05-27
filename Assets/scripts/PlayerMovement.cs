using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject projectile;  // Ensure this is assigned in the Inspector
    public GameObject ImposionFX;
    public float maxRotationSpeed = 180f;
    public float rotationSpeedAcceleration = 720f;

    public float maxVelocity = 10f;
    public float velocityAcceleration = 5f;
    public float velocityDeceleration = 4f;

    private float currentRotationSpeed = 0f;
    private float currentVelocity = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacles>() != null)
            Implode();
    }

    void Implode()
    {
        AudioManager.instance.PlayshipExplsosion();
        Instantiate(ImposionFX,transform.position,Quaternion.identity);
        GameManager.instance.ChangeState(GameState.GameOver);
    }
    public void Reset()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        currentRotationSpeed = 0f;
        currentVelocity = 0f;
    }

    void Update()
    {
        UpdateRotation();
        UpdateAcceleration();
        UpdateProjectile();
    }

    void UpdateRotation()
    {
        if (Input.GetAxis("Horizontal") < 0) // rotate counterclockwise
        {
            currentRotationSpeed -= rotationSpeedAcceleration * Time.deltaTime;
            currentRotationSpeed = Mathf.Max(-maxRotationSpeed, currentRotationSpeed);
        } else if (Input.GetAxis("Horizontal") > 0) // rotate clockwise
        {
            currentRotationSpeed += rotationSpeedAcceleration * Time.deltaTime;
            currentRotationSpeed = Mathf.Min(maxRotationSpeed, currentRotationSpeed);
        } else
        {
            currentRotationSpeed = 0;
        }

        if (currentRotationSpeed != 0)
        {
            transform.Rotate(Vector3.back * Time.deltaTime * currentRotationSpeed);
        }
    }

    void UpdateAcceleration()
    {
        if (Input.GetAxis("Vertical") > 0) // Accelerate
        {
            currentVelocity += velocityAcceleration * Time.deltaTime;
            currentVelocity = Mathf.Min(currentVelocity, maxVelocity);
        } else
        {
            if (currentVelocity > 0)
            {
                currentVelocity -= velocityDeceleration * Time.deltaTime;
                currentVelocity = Mathf.Max(0, currentVelocity);
            }
        }

        if (currentVelocity != 0)
        {
            transform.Translate(Vector3.up * currentVelocity * Time.deltaTime);
        }
    }

    void UpdateProjectile()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (projectile != null)
            {
                GameObject instance = Instantiate(projectile, transform.position, transform.rotation);
               // Debug.Log("Projectile instantiated at position: " + transform.position + " with rotation: " + transform.rotation);

                Projectiles projectileScript = instance.GetComponent<Projectiles>();
                if (projectileScript != null)
                {
                    projectileScript.Eject(transform.up);
                } else
                {
                    Debug.LogError("Instantiated projectile does not have the 'Projectiles' script attached.");
                }
            } else
            {
                Debug.LogError("Projectile prefab is not assigned in the Inspector.");
            }
            AudioManager.instance.PlayShooting();
        }
    }
}
