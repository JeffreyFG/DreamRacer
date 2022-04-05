using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarController : MonoBehaviour
{


public float speedMPH;

public float topSpeed;
public Rigidbody Car_rb;

public string currentGear;
public int gearNum=1;
public List<int> Speeds;


    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbrakeForce;
    private bool isBraking;

    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;




    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private void FixedUpdate()
    {
        speedMPH=Car_rb.velocity.magnitude*3.6f;
          
        if(speedMPH==30){
            Debug.Log("FUCKER");
        }
        Debug.Log(speedMPH);
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

        GearShift();
    }


    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBraking = Input.GetKey(KeyCode.Space);
        
    }

    private void HandleMotor()
    {
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;
          frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbrakeForce = isBraking ? brakeForce : 0f;
        ApplyBraking();       
    }
    private void GearShift(){
        //shift up
      
        if(!isBraking){
            if(Speeds[gearNum]<speedMPH&&gearNum!=Speeds.Count){
                gearNum++;
                Debug.Log("Gear Shift!");
                currentGear=(gearNum+1).ToString();
            }
        }
    //downshift
    if(Speeds[gearNum]>speedMPH&&gearNum !=0){
        gearNum--;
        Debug.Log("down shift!!");
        currentGear = gearNum.ToString();
    }

if(Speeds.Last()<=speedMPH){
    Car_rb.velocity = Vector3.ClampMagnitude(Car_rb.velocity,topSpeed);
}
    }
    private void ApplyBraking()
    {
        frontRightWheelCollider.brakeTorque = currentbrakeForce;
        frontLeftWheelCollider.brakeTorque = currentbrakeForce;
        rearLeftWheelCollider.brakeTorque = currentbrakeForce;
        rearRightWheelCollider.brakeTorque = currentbrakeForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
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