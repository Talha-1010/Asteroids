using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    //field
    Rigidbody2D rigidbody2D;
    Vector2 thrustDirection;
    const float rotateDegreesPerSecond = 90;

    const float ThrustForce= 2;
    float radius;
    CircleCollider2D circleCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        thrustDirection = new Vector2(1, 0);
        radius = gameObject.GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    void Update()
    {


        float rotationInput = Input.GetAxis("Rotate");
        if (rotationInput != 0)
        {
            // calculate rotation amount and apply rotation
            float rotationAmount = rotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);
            float angleZ = transform.eulerAngles.z;
            float AngleInRad = angleZ * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(AngleInRad);
            thrustDirection.y = Mathf.Sin(AngleInRad);
        }

    }
    void FixedUpdate()
    {
        if(Input.GetAxis("Thrust")>0)
        {
            rigidbody2D.AddForce(ThrustForce * thrustDirection,ForceMode2D.Force);
        }
        
    }

    void OnBecameInvisible()
    {
        Vector2 position;

        position = transform.position;


        if (position.x -radius> ScreenUtils.ScreenRight)
        {
            position.x *= -1;

        }
        else if (position.x +radius < ScreenUtils.ScreenLeft)
        {
            position.x *= -1;
        }
        if (ScreenUtils.ScreenTop < position.y-radius)
        {
            position.y *= -1;
        }
        else if (ScreenUtils.ScreenBottom > position.y+radius)
        {
            position.y *= -1;
        }

        transform.position = position;
    }
    
}
