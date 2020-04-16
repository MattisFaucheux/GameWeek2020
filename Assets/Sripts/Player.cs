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


    public float pushPower = 2.0f;

    public float m_timeRespawnWater = 1.0f;
    private Vector3 m_spawnPoint;


    private bool m_triggerOnceInteract = false;

    public DialogueManager m_dialogueManager;

    public Transform m_playerModel;
    public float m_rotateSpeed = 20f;
    private Vector3 m_lastDir = new Vector3(1, 90, 0);

    public static float m_numberBalloons;

    public TMPro.TextMeshProUGUI m_BalloonTxt;
    public GameObject m_balloonObj;

    private void Start()
    {
        m_spawnPoint = transform.position;

    }

    void Update()
    {
        m_BalloonTxt.text = m_numberBalloons.ToString();

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

        RotatePlayer();
    }

    private void RotatePlayer()
    {
        Vector3 dir;

        if (Input.GetAxis("Vertical") <= 0.3 && Input.GetAxis("Vertical") >= -0.3 &&
            Input.GetAxis("Horizontal") <= 0.3 && Input.GetAxis("Horizontal") >= -0.3)
        {
            dir = m_lastDir;
        }
        else
        {
            dir = new Vector3(Input.GetAxis("Vertical"), 90, -Input.GetAxis("Horizontal"));
            m_lastDir = dir;
        }
        Quaternion Rotation = Quaternion.LookRotation(dir);


        m_playerModel.rotation = Quaternion.Lerp(m_playerModel.rotation, Rotation, m_rotateSpeed * Time.deltaTime);

    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetButton("Interact") && !m_triggerOnceInteract && other.gameObject.CompareTag("InteractObject"))
        {
            m_triggerOnceInteract = true;

            Baguette baguette = other.gameObject.GetComponent<Baguette>();
            if (baguette)
            {
                baguette.m_audioSource.Stop();
                baguette.m_audioSource.PlayOneShot(baguette.m_sound);
            }

            if (other.gameObject.GetComponent<Dialogue>())
            {
                m_dialogueManager.StartDialogue(other.gameObject.GetComponent<Dialogue>());
            }
        }
        else if(other.gameObject.CompareTag("Water"))
        {
            StartCoroutine(RespawnWater());
        }
        else if(other.gameObject.CompareTag("Balloon"))
        {
            m_numberBalloons += 1;
            Destroy(other.gameObject);
        }

    }


    IEnumerator RespawnWater()
    {
        yield return new WaitForSeconds(m_timeRespawnWater);
        controller.enabled = false;
        transform.position = m_spawnPoint;
        controller.enabled = true;
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic) 
        { 
            return; 
        }

        if (hit.moveDirection.y < -0.3) 
        { 
            return; 
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.velocity = pushDir * pushPower;

    }
}
