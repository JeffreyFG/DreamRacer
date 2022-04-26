using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;

    [System.Serializable] public struct EngineSpecs {
  //more specs to be added later
    public float idle;
    public float lowerPowerBand;
    public float upperPowerBand;
    public float redline;
    public double gearShiftTimeSeconds;
}
public class CarController : MonoBehaviour
{


    public GameManager manager;
  
    public EngineSpecs EngineSpecs;
    public float currentRPM;
    public float speedMPH;
    public Rigidbody RB;
    public int currentGear=1;
    public float[] gearRatio;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbrakeForce;
    private bool isBraking;
    private bool isShifting;
   
    [SerializeField] private float topSpeed;
    [SerializeField] private float maxMotorForce;
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

    void Start(){
        EngineSpecs.lowerPowerBand= (EngineSpecs.redline+EngineSpecs.idle)/3;
        EngineSpecs.upperPowerBand = (EngineSpecs.redline+EngineSpecs.idle)/2;
    }

    static int counter = 0;
    private void FixedUpdate()
    {
        
        if(speedMPH<topSpeed-80){
            handleMotorForce(1f);
        }
        if(speedMPH>topSpeed-80){
            handleMotorForce(.8f);
        }
          if(speedMPH>topSpeed-40){
            handleMotorForce(.4f);
        }
         if(speedMPH>topSpeed-10){
            handleMotorForce(.2f);
        }
         if(speedMPH>=topSpeed){
            handleMotorForce(0f);
        }

        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
       // GearShift();
     
        HandleEngine();

        
        counter++;
        
        if (counter == 10) {
            manager.StartInteraction(); 
            counter = 0;
        } 

    }
private void handleMotorForce(float multiplier){

    if(currentRPM<EngineSpecs.lowerPowerBand){
        motorForce=maxMotorForce*.25f*multiplier;
    }
    if(currentRPM>EngineSpecs.lowerPowerBand&&currentRPM<EngineSpecs.upperPowerBand){
        motorForce=maxMotorForce*multiplier;
    }
    if(currentRPM>EngineSpecs.upperPowerBand&&currentRPM<EngineSpecs.redline){
        motorForce=maxMotorForce*.75f*multiplier;
    }
}
private void HandleEngine(){
     //MAKE SURE the number in GEARRATIO[number] is equal to the number of elements in UNITY EDITOR OTHERWISE GEARBOX WONT WORK!!!!!! for example gearRatio[5], editor must have elements 0-5!!!!
if(currentRPM<EngineSpecs.idle){
    currentRPM=EngineSpecs.idle;
}else{
if(currentRPM<EngineSpecs.redline){
        currentRPM=(RB.velocity.magnitude*600/(0.29f*2*Mathf.PI))*gearRatio[5]*gearRatio[currentGear];
       speedMPH=RB.velocity.magnitude*3.6f;
    }
if(currentRPM>=EngineSpecs.redline){
    currentGear++;
    currentRPM=1600;
}

if(currentRPM<=EngineSpecs.idle&&currentGear>1){
    currentGear--;
}
        //Debug.Log(currentRPM+" RPM");
       

}}
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
    // private void GearShift(){
    //     if(verticalInput==1&&currentRPM>EngineSpecs.redline &&isShifting==true){
    //     //     verticalInput=0;
    //     //    Debug.Log("Before Delay");
    //     //    await Task.Delay((int)(EngineSpecs.gearShiftTimeSeconds*1000));
    //     //    motorForce=1000f;
    //     //     currentGear++;
    //     //    Debug.Log("After Delay");
    //     //     verticalInput=1;
    //     currentGear++;
    //     isShifting=false;

    //     }
    //       if(currentGear>1&&currentRPM<minRPM){
    //         currentGear--;
    //     }
    //     speedMPH = GetComponent<Rigidbody>().velocity.magnitude*3.6f;
    //     //Debug.Log(speedMPH);
    // }
     
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