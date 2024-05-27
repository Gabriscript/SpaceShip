using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class CarControls : MonoBehaviour
{
    public float thrustSpeed;
    public float turnSpeed;
    public float hoverPower;
    public float hoverHeight;
    float newSpeed = 5 ;

    private float thrustInput;
    private float turnInput;
    private Rigidbody shipRigidBody;
    public GameObject fireBall; // I take a prefab object assigned from the inspector
    public Transform cannonPosition;// I create an empty object and use his transform.position as fire start point 
    bool speedPressed = false;
    // Use this for initialization
    void Start()
    {
        shipRigidBody = GetComponent<Rigidbody>();
        
        

    }

    // Update is called once per frame
    void Update()
    {
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        // When you press the spacebar you are able to shoot

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(fireBall, cannonPosition.position, cannonPosition.rotation);
        }
        //When R i press you can give some acceleration
        if (Input.GetKeyUp(KeyCode.R) && speedPressed == false)
        {
        
           
            speedPressed = true;
            thrustSpeed *= newSpeed; 
          // after 3 second slow to the normal speed
           Invoke("SpeedNormal", 3);
        }
    

    }

    void FixedUpdate()
    {
        // Turning the ship
        shipRigidBody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);

        // Moving the ship
        shipRigidBody.AddRelativeForce(0f, 0f, thrustInput * thrustSpeed);

        // Hovering
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
           UnityEngine.Vector3 appliedHoverForce = UnityEngine.Vector3.up * proportionalHeight * hoverPower;
            shipRigidBody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }
      
      
    }
    // back to the normal speed
    void SpeedNormal()
    {
        speedPressed = false;
        thrustSpeed /= newSpeed;   
    }
}
