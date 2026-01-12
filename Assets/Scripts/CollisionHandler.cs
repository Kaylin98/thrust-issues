using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayBeforeReload = 2f;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;

    AudioSource audioSource;

    bool isControllable = true;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isControllable) { return; }

        string ObjectTag = collision.gameObject.tag;        
        switch (ObjectTag)
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

    private void StartSuccessSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayBeforeReload);
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayBeforeReload);
        
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
