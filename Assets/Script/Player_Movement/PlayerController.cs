using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    private float thrusterForce = 1000f;

  


    [Header("Joint Options")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;
    
    //Component caching
    private PlayerMotor motor;
    private ConfigurableJoint joint;
    private Animator animator;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        animator = GetComponent<Animator>();

        SetJointSettings(jointSpring);
    }

    private void Update()
    {
        //Calculate movement velocity as a 3D vector
        float _xMovement = Input.GetAxis("Horizontal");
        float _zMovement = Input.GetAxis("Vertical");

        Vector3 _movementHorizontal = transform.right * _xMovement; //(1, 0, 0) (X, Y, Z) 
        Vector3 _movementVertical = transform.forward * _zMovement; //(0, 0, 1) (X, Y, Z) 1 = Forward / -1 = Backwards

        //Final Movement Vector
        Vector3 _velocity = (_movementHorizontal + _movementVertical) * speed;

        //Animation Move
        animator.SetFloat("ForwardVelocity", _zMovement);


        //Apply Movement
        motor.Move(_velocity);

        //Calculate Rotation as a 3D Vector: This allows to turn around on the camera
        float _yRotation = Input.GetAxisRaw("Mouse X");


        Vector3 _rotation = new Vector3(0f, _yRotation, 0f) * lookSensitivity;

        //Apply Rotation
        motor.Rotate(_rotation);

        //Calculate camera as a 3D Vector: (Tilting Up & Down)
        float _xRotation = Input.GetAxisRaw("Mouse Y");


        float _cameraRotationX = _xRotation * lookSensitivity;

        //Apply Camera Rotation
        motor.RotateCamera(_cameraRotationX);


        //Calculate the thrusterfoce based on player input
        Vector3 _thrusterForce = Vector3.zero;

        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        }else
        {
            SetJointSettings(jointSpring);
        }

        //Apply the thruster force
        motor.ApplyThruster(_thrusterForce);
    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive {
            mode = jointMode ,
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }

}
