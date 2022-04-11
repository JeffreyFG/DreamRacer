using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


    [System.Serializable] public struct EngineSpecs {
  //more specs to be added later
    public float redline;
}
public class CarController : MonoBehaviour
{


    public GameManager manager;
  
    public EngineSpecs EngineSpecs;
    public float currentRPM;
    public float speedMPH;
    public Rigidbody RB;
    public int minRPM;
    public int currentGear=1;
    public float[] gearRatio;
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
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private void FixedUpdate()
    {
        speedMPH=RB.velocity.magnitude*3.6f;
          
        if(speedMPH==30){
        }
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        HandleEngine();
        manager.StartInteraction();   
        GearShift();
    }

private void HandleEngine(){
     //MAKE SURE the number in GEARRATIO[number] is equal to the number of elements in UNITY EDITOR OTHERWISE GEARBOX WONT WORK!!!!!! for example gearRatio[5], editor must have elements 0-5!!!!
        currentRPM=(RB.velocity.magnitude*600/(0.29f*2*Mathf.PI))*gearRatio[5]*gearRatio[currentGear];
    
        //Debug.Log(currentRPM+" RPM");
       
}
    private void Update() {
        //sends location update to server 
        
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
        if(currentGear==gearRatio.Length&&currentRPM==EngineSpecs.redline){
            motorForce=0f;
        }
        if(verticalInput==1&&currentRPM>EngineSpecs.redline){
            currentGear++;

        }
          if(verticalInput==-1&&currentGear>0&&currentRPM<minRPM){
            currentGear--;
        }
        speedMPH = GetComponent<Rigidbody>().velocity.magnitude*3.6f;
        //Debug.Log(speedMPH);
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
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
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