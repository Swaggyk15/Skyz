using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float _responsiveness = 500f;
    [SerializeField] private float _throttleAmt = 25f;
    [SerializeField] private float _maxThrust = 5f; 
    private float _throttle;

    private float _roll;
    private float _pitch;
    private float _yaw;

    //to visually help when it comes to the blade spinning
    [SerializeField] private float _rotorSpeedModifier = 10f;
    [SerializeField] private Transform _rotorsTransform;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInputs();

        _rotorsTransform.Rotate(Vector3.up * (_maxThrust * _throttle) * _rotorSpeedModifier);
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(transform.up * _throttle, ForceMode.Impulse);
        
        _rigidbody.AddTorque(transform.right * _pitch * _responsiveness);
        _rigidbody.AddTorque(-transform.forward * _roll * _responsiveness);
        _rigidbody.AddTorque(transform.up * _yaw * _responsiveness);
    }

    private void HandleInputs()
    {
        _roll = Input.GetAxis("Roll");
        _pitch = Input.GetAxis("Pitch");
        _yaw = Input.GetAxis("Yaw");

        if (Input.GetKey(KeyCode.Space))
        {
            _throttle += Time.deltaTime * _throttleAmt;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            _throttle -= Time.deltaTime * _throttleAmt;
        }

        _throttle = Mathf.Clamp(_throttle,0f,100f);
    }
}
