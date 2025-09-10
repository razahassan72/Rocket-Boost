using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;

    [SerializeField] float thrustStrenght = 100f;
    [SerializeField] float rotationStrength = 100f;

    [SerializeField] ParticleSystem MainBooster;
    [SerializeField] ParticleSystem LeftBooster;
    [SerializeField] ParticleSystem RightBooster;

    [SerializeField] AudioClip engineMain;

    [SerializeField] bool invertControls = false; // Only used to invert rotation now

    Rigidbody rb;
    AudioSource thrustAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrustAudio = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void OnDisable()
    {
        thrust.Disable();
        rotation.Disable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrenght * Time.fixedDeltaTime);

            if (!thrustAudio.isPlaying)
            {
                thrustAudio.PlayOneShot(engineMain);
            }

            if (!MainBooster.isPlaying)
            {
                MainBooster.Play();
            }
        }
        else
        {
            thrustAudio.Stop();
            MainBooster.Stop();
        }
    }

    void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (invertControls)
        {
            rotationInput *= -1f; // Invert only rotation
        }

        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength);

            if (!RightBooster.isPlaying)
            {
                LeftBooster.Stop();
                RightBooster.Play();
            }
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);

            if (!LeftBooster.isPlaying)
            {
                RightBooster.Stop();
                LeftBooster.Play();
            }
        }
        else
        {
            RightBooster.Stop();
            LeftBooster.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        rb.transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        // rb.freezeRotation = false; // Uncomment if needed
    }
}
