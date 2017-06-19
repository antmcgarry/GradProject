using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;


    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    //Get a movemnt vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Get a rotation vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //Get a Camera Rotation Vector
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    //Run every physics iteration
    private void FixedUpdate()
    {
        PerformMevement();
        PerformRotation();
    }

    void PerformMevement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.deltaTime);
        }
    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            cam.transform.Rotate(-cameraRotation); //The minus the controls not invert
        }

    }
}
