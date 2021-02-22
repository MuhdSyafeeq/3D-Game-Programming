using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    #region Singleton Method
    public static MoveCharacter instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;
    [SerializeField] float gravity = -9.87f;
    [SerializeField] float speed = 6f;
    [SerializeField] float jumpHeight = 2f;

    [SerializeField] float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    [SerializeField] private Vector3 velocity;
    [SerializeField] bool isGrounded;

    [SerializeField] Stamina currentStamina;
    [SerializeField] Animator anime;
    [SerializeField] bool KeepLoop = false;
    private float inputH = 0.0f, inputV = 0.0f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var body = hit.collider.GetComponent<Rigidbody>();

        // no rigidbody
        if (body == null || body.isKinematic) { return; }
        // We dont want to push objects below us
        if (hit.moveDirection.y <= -0.3) { return; }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(inputH, 0f, inputV).normalized;

        if (KeepLoop && currentStamina.checkStamina())
        {
            currentStamina.reduceStamina((float)(Time.deltaTime * 0.5));
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * (speed * 2) * Time.deltaTime);
        }
        else { KeepLoop = false; anime.SetBool("KeepDash", false); currentStamina.addStamina(Time.deltaTime); }



        anime.SetFloat("inputH", Mathf.Abs(inputH));
        anime.SetFloat("inputV", Mathf.Abs(inputV));
        

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anime.Play("Jump", -1, 0f);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            KeepLoop = true;
            anime.Play("Dash", -1, 0f);
            anime.SetBool("KeepDash", true);
        }

        if(Input.GetKey(KeyCode.LeftShift) == false && isGrounded)
        {
            KeepLoop = false;
            anime.SetBool("KeepDash", false);
        }

        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
