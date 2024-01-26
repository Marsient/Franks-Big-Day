using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMove : MonoBehaviour
{
    public Camera cam;
    public CharacterController controller;
    public float speed = 10f;
    public float gravity = -10f;
    public float dashSpeed = 1000f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public int dashCD = 0;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    void Update() {
        Movement();
    }
    void FixedUpdate() {
        dashCD -= 1;
    }
    void Movement() {
       //Checks if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Resets the player's velocity if they are on the ground
        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        // Jumps
        if(isGrounded && Input.GetKey(KeyCode.Space)) {
            velocity.y = 13.5f;
        }
        // Lets the player fastfall
        if(!isGrounded && Input.GetKey(KeyCode.LeftControl)) {
            velocity.y += gravity * Time.deltaTime * 7f;
        }
        //Takes the player's movement inputs
        float x = Input.GetAxis("Horizontal") * -1;
        // Moves the player based on their movememnt inputs
        Vector3 move = transform.forward * x;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime * 2f;
        controller.Move(velocity * Time.deltaTime);
        //The dash feature, although it operates more like a teleport
        if (dashCD <= 0 && Input.GetKeyDown(KeyCode.LeftShift)) {
            if(x != 0f) {
                controller.Move(move * dashSpeed * Time.deltaTime);
                dashCD = 30;
            } else {
            controller.Move(transform.forward * dashSpeed * Time.deltaTime * -1);
            dashCD = 30;
            }
        }
    }
}
