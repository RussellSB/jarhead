using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float speed = 9;                         // Speed of player
    public float acceleration = 75;                 // Acceleration when starting to walk
    public float deceleration = 70;                 // Deceleration when slowing down
    private Vector3 velocity;                       // To store x and y velocity at the current update

    private void FixedUpdate()
    {
        float moveHor = Input.GetAxisRaw("Horizontal");
        float moveVer = Input.GetAxisRaw("Vertical");

        // Horzontal Movement
        if (moveHor != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveHor, acceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
        }

        // Vertical Movement
        if (moveVer != 0)
        {
            velocity.y = Mathf.MoveTowards(velocity.y, speed * moveVer, acceleration * Time.deltaTime);
            velocity.z = Mathf.MoveTowards(velocity.z, speed * moveVer, acceleration * Time.deltaTime);

        }
        else
        {
            velocity.y = Mathf.MoveTowards(velocity.y, 0, deceleration * Time.deltaTime);
            velocity.z = Mathf.MoveTowards(velocity.z, speed * moveVer, acceleration * Time.deltaTime);
        }

        transform.Translate(velocity * Time.deltaTime);
    }
}