using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentBreakingForce;
    private float currentSteeringAngle;
    [SerializeField] private bool isBreaking;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteeringAngle;

    private bool logShown;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    private void GetInput()
    {
        // horizontalInput = Input.GetAxis(HORIZONTAL);
        // verticalInput = Input.GetAxis(VERTICAL);
        // isBreaking = Input.GetKey(KeyCode.Space);
    }

    public void SetInputsFromIA(float[] inputsFromAI)
    {
        switch (Mathf.Round(inputsFromAI[0]))
        { //Acelerador
            case 0: verticalInput = 0f; break;
            case 1: verticalInput = 1f; break;
            case 2: verticalInput = -1f; break;
        }

        switch (Mathf.Round(inputsFromAI[1]))
        { //Direção
            case 0: verticalInput = 0f; break;
            case 1: verticalInput = 1f; break;
            case 2: verticalInput = -1f; break;
        }

        switch (Mathf.Round(inputsFromAI[2]))
        { //Freio
            case 0: isBreaking = false; break;
            case 1: isBreaking = true; break;
        }
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentBreakingForce = isBreaking ? breakForce : 0f;
        if (isBreaking)
        {
            ApplyBreakingForce();
        }
        else
        {
            ReleaseBreakingForce();
        }
    }

    public void ApplyBreakingForce()
    {
        frontLeftWheelCollider.brakeTorque = currentBreakingForce;
        frontRightWheelCollider.brakeTorque = currentBreakingForce;
        backLeftWheelCollider.brakeTorque = currentBreakingForce;
        backRightWheelCollider.brakeTorque = currentBreakingForce;
    }

    public void ReleaseBreakingForce()
    {
        frontLeftWheelCollider.brakeTorque = 0;
        frontRightWheelCollider.brakeTorque = 0;
        backLeftWheelCollider.brakeTorque = 0;
        backRightWheelCollider.brakeTorque = 0;
    }

    private void HandleSteering()
    {
        currentSteeringAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteeringAngle;
        frontRightWheelCollider.steerAngle = currentSteeringAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}


