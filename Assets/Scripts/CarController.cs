using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Timers;
using UnityEngine.UI;
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
    public Text rpmText;
    public Text mphText;
    public Text gearText;

    public AudioSource engineSound;

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

    // Gameplay speed up
    public bool isBoosted = false;
    private float boostTimer = 0;
    public float boostTime = 0;

    // Gameplay slow down
    public bool isSlowed = false;
    private float slowTimer = 0;
    public float slowTime = 0;

    // Gameplay stop
    public bool isStopped = false;
    private float stopTimer = 0;
    public float stopTime = 0;
    public float Try = 0;
   
    [SerializeField] private  float topSpeed;
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
        InvokeRepeating("sendPos", 0, 0.06f);
    }
    private void FixedUpdate()
    {
        // Gameplay speed up
        if(isBoosted == true){
            RB.velocity =  RB.velocity + RB.velocity*0.001f;
            boostTimer += Time.deltaTime;
            if(boostTimer >= boostTime){
                // topSpeed = 200;
                // motorForce = 1;
                // maxMotorForce = 2000;
                isBoosted = false;
            }
        }
        // Gameplay slow down
        if(isSlowed == true){
            RB.velocity =  RB.velocity - RB.velocity*0.01f;
            // RB.angularVelocity *= 0.4f;
            slowTimer += Time.deltaTime;
            if(slowTimer >= slowTime){
                // topSpeed = 200;
                // motorForce = 1;
                // maxMotorForce = 2000;
                // brakeForce = 0;
                // currentbrakeForce = 0;
                isSlowed = false;
            }
        }
        // Gameplay stop
        if(isStopped == true){
            RB.velocity = Vector3.zero;
            // RB.angularVelocity = Vector3.zero;
            stopTimer += Time.deltaTime;
            if(stopTimer >= stopTime){
                // topSpeed = 200;
                // motorForce = 1;
                // maxMotorForce = 2000;
                // currentbrakeForce = 0;
                // brakeForce = 0;
                isStopped = false;
            }
        }
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
    
    }

    private void Update(){
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, Try, 0);

        if (Input.GetKeyUp(KeyCode.RightAlt))
        {
            transform.Rotate(0,0,180);
        } 
    }

    private void sendPos(){
        manager.StartInteraction();   
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

     
        engineSound.pitch = currentRPM / 1000;

        rpmText.text = currentRPM.ToString();
        mphText.text = speedMPH.ToString();
        gearText.text = currentGear.ToString();




        if (currentRPM<EngineSpecs.idle &&Input.GetKeyDown("t")){
    currentRPM=EngineSpecs.idle;
}else{
if(currentRPM<EngineSpecs.redline){
        currentRPM=(RB.velocity.magnitude*600/(0.29f*2*Mathf.PI))*gearRatio[5]*gearRatio[currentGear]+1500;
       speedMPH=RB.velocity.magnitude*3.6f;
    }
if(currentRPM>=EngineSpecs.redline){
    currentGear++;
    currentRPM=1800;
}

if(currentRPM<=EngineSpecs.idle+200&&currentGear>1){
    currentGear--;
}
        //Debug.Log(currentRPM+" RPM");
       

}}
    
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