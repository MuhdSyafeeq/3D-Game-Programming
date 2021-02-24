using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    #region Singleton Method
    public static PlayerCamera instance;
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

    [SerializeField] float RotateSpeed = 1;
    [SerializeField] Transform Target, Player;
    [SerializeField] float mouseX, mouseY;
    //[SerializeField] CinemachineBrain theBrain;
    //[SerializeField] CinemachineFreeLook theController;

    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        //if(theController == null || theBrain == null)
        //{
        //    theBrain = GetComponent<CinemachineBrain>();
        //    theController = GetComponentInChildren<CinemachineFreeLook>();
        //}
    }
    
    void LateUpdate()
    {
        //CameraControl();
        //theController.m_Follow = MoveCharacter.instance.transform;
        //theBrain.transform.position = MoveCharacter.instance.transform.position;
    }

    void CameraControl()
    {
        //mouseX += Input.GetAxis("Mouse X") * RotateSpeed;
        //mouseY -= Input.GetAxis("Mouse Y") * RotateSpeed;
        //mouseY = Mathf.Clamp(mouseY, -35, 60);

        //transform.LookAt(Target);

        //if (Input.GetKey(KeyCode.LeftAlt))
        //{
        //    Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        //}
        //else
        //{
        //    Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        //    Player.rotation = Quaternion.Euler(0, mouseX, 0);
        //}

    }
}
