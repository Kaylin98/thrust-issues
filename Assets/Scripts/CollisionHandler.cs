using UnityEngine;
using UnityEngine.InputSystem; // You don't actually need this namespace here anymore if you use the new Input System in Movement.cs, but it doesn't hurt.
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class CollisionHandler : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] float delayBeforeReload = 2f;

    [Header("Audio Assets")]
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;

    [Header("VFX Particles")]
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    bool isControllable = true;
    bool isCollidable = true;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        #if UNITY_EDITOR
            RespondToDebugKeys();
        #endif
    }

    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
        else if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ReloadLevel();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isControllable || !isCollidable) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with Friendly object.");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("Collided with Fuel object. Refueling!");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isControllable = false;
        StopAllAudio();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayBeforeReload);
    }

    void StartCrashSequence()
    {
        isControllable = false;
        StopAllAudio(); 
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayBeforeReload);
    }

    void StopAllAudio()
    {
        AudioSource[] allAudioSources = GetComponents<AudioSource>();
        foreach (AudioSource source in allAudioSources)
        {
            source.Stop();
        }
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }   
}