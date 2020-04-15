using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public float playerSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float sprintSpeed = 2f;

    private Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    private bool m_triggerOnceInteract = false;

    public DialogueManager m_dialogueManager;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetButton("Sprint") && isGrounded)
        {
            controller.Move(move * playerSpeed * Time.deltaTime * sprintSpeed);
        }
        else
        {
            controller.Move(move * playerSpeed * Time.deltaTime);
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(Input.GetButtonUp("Interact"))
        {
            m_triggerOnceInteract = false;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetButton("Interact") && !m_triggerOnceInteract && other.gameObject.CompareTag("InteractObject"))
        {
            m_triggerOnceInteract = true;
            m_dialogueManager.StartDialogue(other.gameObject.GetComponent<Dialogue>());
        }
    }
}
