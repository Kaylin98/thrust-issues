using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;

    [Header("Movement Physics")]
    [SerializeField] float thrustForce = 1000f;
    [SerializeField] float rotationForce = 100f;

    [Header("Audio Assets")]
    [SerializeField] AudioSource mainAudioSource;
    [SerializeField] AudioSource sideThrusterAudioSource;
    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] AudioClip rotationSound;

    [Header("VFX Particles")]
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody rb;
    

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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
        
        // Only checking the MAIN audio source
        if (!mainAudioSource.isPlaying)
        {
            // Using Play() instead of PlayOneShot for continuous engine loops
            mainAudioSource.clip = mainEngineSound;
            mainAudioSource.Play();
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        mainAudioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0)
        {
            RotateRight();
        }
        else if (rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(rotationForce);
        if (!rightThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Play();
        }
        
        // Using the SECOND audio source
        if (!sideThrusterAudioSource.isPlaying)
        {
            sideThrusterAudioSource.clip = rotationSound;
            sideThrusterAudioSource.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationForce);
        if (!leftThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Play();
        }

        // Using the SECOND audio source
        if (!sideThrusterAudioSource.isPlaying)
        {
            sideThrusterAudioSource.clip = rotationSound;
            sideThrusterAudioSource.Play();
        }
    }

    private void StopRotating()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
        
        sideThrusterAudioSource.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; 
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;    
    }
}