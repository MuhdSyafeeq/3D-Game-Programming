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

    [SerializeField] Transform Player;
    [SerializeField] private Vector3 cameraOffset_;

    [Range(0.01f, 1.0f)]
    [SerializeField] private float SmoothFactor      = 0.125f; //0.5f;
    [SerializeField] private float RotationSpeed     = 5.0f;

    [SerializeField] CinemachineFreeLook myScripts;

    private Quaternion canTurnAngle, turnUpAngle;

    void Start()
    {
        cameraOffset_ = transform.position - Player.position;
    }
    
    void LateUpdate()
    {
        Vector3 desiredPosition = Player.position + cameraOffset_;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothFactor);
        transform.position = Player.position + cameraOffset_;

        transform.LookAt(Player.transform);


        if(Input.GetKeyDown(key: KeyCode.LeftAlt))
        {
            myScripts.enabled = false;
        }
        if (Input.GetKeyUp(key: KeyCode.LeftAlt))
        {
            myScripts.enabled = true;
        }
    }
}
