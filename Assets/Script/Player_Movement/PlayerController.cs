using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        //Calculate movement velocity as a 3D vector
        float _xMovement = Input.GetAxisRaw("Horizontal");
        float _zMovement = Input.GetAxisRaw("Vertical");

        Vector3 _movementHorizontal = transform.right * _xMovement; //(1, 0, 0) (X, Y, Z) 
        Vector3 _movementVertical = transform.forward * _zMovement; //(0, 0, 1) (X, Y, Z) 1 = Forward / -1 = Backwards

        //Final Movement Vector
        Vector3 _velocity = (_movementHorizontal + _movementVertical).normalized * speed;


        //Apply Movement
        motor.Move(_velocity);

        //Calculate Rotation as a 3D Vector: This allows to turn around on the camera
        float _yRotation = Input.GetAxisRaw("Mouse X");


        Vector3 _rotation = new Vector3(0f, _yRotation, 0f) * lookSensitivity;

        //Apply Rotation
        motor.Rotate(_rotation);

        //Calculate camera as a 3D Vector: (Tilting Up & Down)
        float _xRotation = Input.GetAxisRaw("Mouse Y");


        Vector3 _cameraRotation = new Vector3(_xRotation, 0f, 0f) * lookSensitivity;

        //Apply Camera Rotation
        motor.RotateCamera(_cameraRotation);
    }

}
