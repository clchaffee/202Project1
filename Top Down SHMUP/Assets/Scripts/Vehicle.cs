using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
public class Vehicle : MonoBehaviour
{

    [SerializeField]
    float speed = 5f;

    public Vector3 vehiclePosition = Vector3.zero;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Velocity is direction * speed * deltaTime
        velocity = direction * speed * Time.deltaTime;

        // Add velocity to position 
        vehiclePosition += velocity;

        cam = Camera.main;
        
        
        //deals with containing the player in the viewport, will be converted to collision detection soon
        
        //creates a vector of the viewport, then creates world points using said vector
        Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);
        cam.ViewportToWorldPoint(viewportPos);

        if(viewportPos.x > 1)
        {
            vehiclePosition.x -= .05f;
        }
        if (viewportPos.x < 0)
        {
            vehiclePosition.x += .05f;
        }
        if (viewportPos.y > 1)
        {
            vehiclePosition.y -= .05f;
        }
        if (viewportPos.y < 0)
        {
            vehiclePosition.y +=.05f;
        }

        transform.position = vehiclePosition;

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();

        if(direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
        }
    }
}
