using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    Rigidbody rb;

    [SerializeField] float thrustForce = 1000f;
    [SerializeField] float rotationForce = 100f;

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0)
        {
            ApplyRotation(rotationForce);
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationForce);
        }
       
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
