using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] private Vector3 cameraOffset_;

    [Range(0.01f, 1.0f)]
    [SerializeField] private float SmoothFactor     = 0.5f;
    [SerializeField] private float RotationSpeed    = 5.0f;

    void Start()
    {
        cameraOffset_ = transform.position - Player.position;
    }

    void Awake(){ }
    void Update(){ }
    void FixedUpdate() { }

    void LateUpdate()
    {
       
        Quaternion canTurnAngle = Quaternion.AngleAxis(
            Input.GetAxis("Mouse X") * RotationSpeed,
            Vector3.up
            );

        Quaternion turnUpAngle = Quaternion.AngleAxis(
            Input.GetAxis("Mouse Y") * RotationSpeed,
            Vector3.forward
            );

        cameraOffset_ = (turnUpAngle * canTurnAngle) * cameraOffset_;

        Vector3 newPos = Player.position + cameraOffset_;

        transform.position = Vector3.Slerp(
            transform.position, 
            newPos, 
            SmoothFactor
            );

        transform.LookAt(Player.transform);
    }
}
