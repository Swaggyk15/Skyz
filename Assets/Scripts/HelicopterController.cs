using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    // create rigidbody for applying physics-based forces
    private Rigidbody _rigidbody;

    // rotation responsiveness control for smoother movement
    [SerializeField] private float _responsiveness = 500f;

    // rate at which throttle increases/decreases 
    [SerializeField] private float _throttleAmt = 25f;

    // max thrust that can be applied when throttle is at 100%
    [SerializeField] private float _maxThrust = 5f; 
    private float _throttle;

    // left/right tilt control
    private float _roll;

    // forward/backward tilt control
    private float _pitch;

    // rotation laong y-axis
    private float _yaw;

    // used to visually simulate rotor spinning
    [SerializeField] private float _rotorSpeedModifier = 10f;

    // reference to the motors transform
    [SerializeField] private Transform _rotorsTransform;
    private void Awake()
    {
        // assign rigidbody
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // used for handling user input
        HandleInputs();

        // based on throttle level, the blades will begin to rotate
        _rotorsTransform.Rotate(Vector3.up * (_maxThrust * _throttle) * _rotorSpeedModifier);
    }

    private void FixedUpdate()
    {
        // applying vertical force based on throttle level -> aka lifting helicopter from ground using throttle
        _rigidbody.AddForce(transform.up * _throttle, ForceMode.Impulse);
        
        // torque based rotation for forward/backward tilt
        _rigidbody.AddTorque(transform.right * _pitch * _responsiveness);

        // torque based rotation for left/right tilt
        _rigidbody.AddTorque(-transform.forward * _roll * _responsiveness);

        // torque based rotation for yaw
        _rigidbody.AddTorque(transform.up * _yaw * _responsiveness);
    }

    private void HandleInputs()
    {
        // mapped based on Unity input manager
        _roll = Input.GetAxis("Roll");
        _pitch = Input.GetAxis("Pitch");
        _yaw = Input.GetAxis("Yaw");

        // increase or decrease throttle depending on whether Space or Left Shift key is pressed by the user
        if (Input.GetKey(KeyCode.Space))
        {
            _throttle += Time.deltaTime * _throttleAmt;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            _throttle -= Time.deltaTime * _throttleAmt;
        }

        // ensures that throttle is clamped to remain in the range of 0% to 100%
        _throttle = Mathf.Clamp(_throttle,0f,100f);
    }
}
