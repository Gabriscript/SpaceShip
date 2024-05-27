using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Asteroids : Obstacles
{
    public int MaxFragments = 4;
    public float minVelocity = 1f;
    public float maxVelocity = 3f;
    public int FragmentDepth
    {
        get => fragmentDepth;
        set { 
            fragmentDepth = value;
            float scale = Mathf.Pow(.8f, fragmentDepth);
            transform.localScale = new Vector3(scale, scale, scale);

        
        }
    }
    int fragmentDepth = 0;
    private float velocity;
    private Vector3 rotationAxis;
    private float angularSpeed;

    void Randomize()
    {
        // Randomize the travel direction and velocity
       // direction = Random.insideUnitCircle.normalized;
        velocity = Random.Range(minVelocity, maxVelocity);

        // Randomize the spin axis and angular speed
        rotationAxis = Random.onUnitSphere;
        angularSpeed = Random.Range(10f, 100f);

        // Randomize starting position
        transform.rotation = Random.rotation;

      /*  float orthoHeight = Camera.main.orthographicSize;
        float orthoWidth = Camera.main.orthographicSize * Camera.main.aspect;

        transform.position = new Vector3(Random.Range(-orthoWidth, orthoWidth),
                                         Random.Range(-orthoHeight, orthoHeight));*/
    }

    public override void Init(Vector2 position, Vector2 direction)
    {
        base.Init(position, direction);
        Randomize();
    }

    protected override void Destruct()
    {
      base.Destruct();
    }

    protected override void Movement()
    {
        transform.position += direction * velocity * Time.deltaTime;
        transform.Rotate(rotationAxis, angularSpeed * Time.deltaTime);
    }

   
}
