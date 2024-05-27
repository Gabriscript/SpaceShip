using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounders : MonoBehaviour
{
    private float orthoHeight;
    private float orthoWidth;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCameraBounds();
    }

    private void OnDrawGizmosSelected()
    {
        UpdateCameraBounds();

        Vector3 topLeft = Vector3.up * orthoHeight + Vector3.left * orthoWidth;
        Vector3 topRight = Vector3.up * orthoHeight + Vector3.right * orthoWidth;
        Vector3 bottomLeft = Vector3.down * orthoHeight + Vector3.left * orthoWidth;
        Vector3 bottomRight = Vector3.down * orthoHeight + Vector3.right * orthoWidth;

        Debug.DrawLine(topLeft, topRight, Color.red);
        Debug.DrawLine(topRight, bottomRight, Color.red);
        Debug.DrawLine(bottomRight, bottomLeft, Color.red);
        Debug.DrawLine(bottomLeft, topLeft, Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraBounds();

        Vector3 pos = transform.position;
        if (pos.x > orthoWidth)
        {
            pos.x -= orthoWidth * 2;
        } else if (pos.x < -orthoWidth)
        {
            pos.x += orthoWidth * 2;
        }

        if (pos.y > orthoHeight)
        {
            pos.y -= orthoHeight * 2;
        } else if (pos.y < -orthoHeight)
        {
            pos.y += orthoHeight * 2;
        }

        transform.position = pos;
    }

    private void UpdateCameraBounds()
    {
        orthoHeight = Camera.main.orthographicSize;
        orthoWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }
}
