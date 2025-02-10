using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("An array of transforms representing camera psoitions")]
    [SerializeField] Transform[] povs;
    [Tooltip("The speed at which the camera follows the plane")]
    [SerializeField] float speed;

    private int index = 1;
    private Vector3 target;

    private void Update()
    {
        // Numbers 1-4 represent differnet povs
        if (Input.GetKeyDown(KeyCode.Alpha1)) index = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) index = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) index = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) index = 3;

        target = povs[index].position;
    }
    //set target to relevant pov
    private void FixedUpdate()
    {
        // Move camera to desired position/orientation. Must be in FixedUpdate to avoid camera jitters
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime*speed);
        transform.forward = povs[index].forward;
    }
}
