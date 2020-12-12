using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform Player;
    private Vector3 cameraOffset_;

    [Range(0.01f, 1.0f)]
    private float SmoothFactor     = 0.5f;
    private float RotationSpeed    = 5.0f;

    private Quaternion canTurnAngle, turnUpAngle;

    void Start()
    {
        cameraOffset_ = transform.position - Player.position;
    }

    void Awake(){ }
    void Update()
    {
    }
    void FixedUpdate() { }
    void LateUpdate()
    {

        canTurnAngle = Quaternion.AngleAxis(
            Input.GetAxis("Mouse X") * RotationSpeed,
            Vector3.up
            );

        turnUpAngle = Quaternion.AngleAxis(
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
