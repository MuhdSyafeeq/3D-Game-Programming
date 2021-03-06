using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    #region Singleton Method
    public static MoveCharacter instance;
    void Awake()
    {
        if (instance == null)
        {
            //Debug.Log("SPAWNED");
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            setPause(false);
            setTimeScale(1);
            if (MainMenu.loadLevel)
            {
                instance.LoadData();

                LoadData();
                Debug.Log("Loading..");
                MainMenu.loadLevel = false;
            }
        }
        else if(instance != null)
        {
            
            gameObject.tag = "Untagged";
            //this.gameObject.SetActive(false);
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

    public bool isAttack = false;
    bool isAttacking = false;
    int attackNum;

    [Header("Menu Canvas Settings")]
    [SerializeField] public static bool isPaused = false;
    [SerializeField] GameObject Menu;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] Clock clock;
    [SerializeField] GameObject gameOver;

    public void setStaminaData(int value)
    {
        currentStamina.setStamina(value);
    }

    public void SaveData()
    {
        SaveSystem.SavePlayer();
    }

    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadPlayer();


        //Inventory.instance.inventories = data.playerInventories;

        //currentStamina.setStamina(data.Stamina);
        //PlayerCamera.instance.GetComponentInChildren<Canvas>().GetComponentInChildren<Health>().setHealth(data.Health);


        Vector3 newPosition;
        newPosition.x = data.position[0];
        newPosition.y = data.position[1];
        newPosition.z = data.position[2];

        this.transform.position = newPosition;

        Debug.Log(newPosition[0] + "," + newPosition[1] + "," + newPosition[2]);
        Debug.Log(transform.position);
    }

    public void setTimeScale(int @Timer)
    {
        Time.timeScale = Timer;
    }

    public void setPause(bool Result)
    {
        isPaused = Result;
    }

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
        currentStamina = PlayerCamera.instance.GetComponentInChildren<Canvas>().GetComponentInChildren<Stamina>();

        if(!isPaused && Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Menu.SetActive(true);
            isPaused = true;
        }
        else if(isPaused && Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 1;
            Menu.SetActive(false);
            isPaused = false;
        }

        if (!isPaused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (clock.time > 21600)
            {
                textMesh.text = (((84600f + 21600) / 3600) - (clock.time / 3600)).ToString("F2") + " Hour(s) Left";
            }
            else if (clock.time > 0)
            {
                textMesh.text = (((84600f + 21600) / 3600) - ((clock.time + 84600) / 3600)).ToString("F2") + " Hour(s) Left";
            }
                

            if (clock.time >= 21600 && clock.days > 0)
            {
                GameOver.isDied = true;
                gameOver.SetActive(true);
                isPaused = true;
                Time.timeScale = 0f;
            }

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

            if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
            {
                KeepLoop = true;
                anime.Play("Dash", -1, 0f);
                anime.SetBool("KeepDash", true);
            }

            if (Input.GetKey(KeyCode.LeftShift) == false && isGrounded)
            {
                KeepLoop = false;
                anime.SetBool("KeepDash", false);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    attackNum++;

                    if (attackNum % 2 == 0)
                        anime.Play("AttackA");
                    else
                        anime.Play("AttackB");
                    isAttack = true;
                    Invoke("SetAttackAvailable", 1);
                }
            }
            else
            {
                isAttack = false;
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void SetAttackAvailable() {
        isAttacking = false;
    }
}
