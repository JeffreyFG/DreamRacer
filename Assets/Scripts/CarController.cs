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
    private float[,] engineTorque = new float[4,2];

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
        EngineSetup();
    }

    private void EngineSetup(){
    engineTorque[0,0] = EngineSpecs.idle;
    engineTorque[1,0] = EngineSpecs.lowerPowerBand;
    engineTorque[2,0] = EngineSpecs.upperPowerBand;
    engineTorque[3,0] = EngineSpecs.redline;

    engineTorque[0,1] = powerSetup(EngineSpecs.idle);
    engineTorque[1,1] = powerSetup(EngineSpecs.lowerPowerBand);
    engineTorque[2,1] = powerSetup(EngineSpecs.upperPowerBand);
    engineTorque[3,1] = powerSetup(EngineSpecs.redline);

    }
    
    private float powerSetup(float power){
        //power * constant / rpm
        return (power*7000)/currentRPM;
    }
    


    private void FixedUpdate()
    {
        
          
        handleMotorForce();
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        HandleEngine();
        GearShift();
        manager.StartInteraction();   

    }
private void handleMotorForce(){

    if(currentRPM<EngineSpecs.lowerPowerBand){
        motorForce=maxMotorForce*.25f;
    }
    if(currentRPM>EngineSpecs.lowerPowerBand&&currentRPM<EngineSpecs.upperPowerBand){
        motorForce=maxMotorForce;
    }
    if(currentRPM>EngineSpecs.upperPowerBand&&currentRPM<EngineSpecs.redline){
        motorForce=maxMotorForce*.75f;
    }
}
private void HandleEngine(){
     //MAKE SURE the number in GEARRATIO[number] is equal to the number of elements in UNITY EDITOR OTHERWISE GEARBOX WONT WORK!!!!!! for example gearRatio[5], editor must have elements 0-5!!!!
if(currentRPM<EngineSpecs.redline){
        currentRPM=(RB.velocity.magnitude*600/(0.29f*2*Mathf.PI))*gearRatio[5]*gearRatio[currentGear];
       speedMPH=RB.velocity.magnitude*3.6f;
    }

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
        if(verticalInput==1&&currentRPM>EngineSpecs.redline &&currentGear<6){
        //     verticalInput=0;
        //    Debug.Log("Before Delay");
        //    await Task.Delay((int)(EngineSpecs.gearShiftTimeSeconds*1000));
        //    motorForce=1000f;
        //     currentGear++;
        //    Debug.Log("After Delay");
        //     verticalInput=1;
        currentGear++;


        }
          if(currentGear>1&&currentRPM<minRPM){
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