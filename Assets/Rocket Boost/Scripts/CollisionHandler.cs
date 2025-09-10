using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float timeToDelay = 1.5f;
    [SerializeField] AudioClip engineCrash;
    [SerializeField] AudioClip engineReached;
    [SerializeField] ParticleSystem SuccessParticle;
    [SerializeField] ParticleSystem CrashParticle;

    bool isControllable = true;
    bool isCollidable = true;

    Movement moveScript;
    AudioSource audioSource;

    


    private void Start()
    {
        moveScript = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if ( Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
            Debug.Log("C key was pressed");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable)   { return; }

        switch (other.gameObject.tag)
        {
            case "Finish":
                StartSuccessSequence();
                break;

            case "Friendly":
                Debug.Log("hey");
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(engineReached, 0.25f /*Volume*/ );
        SuccessParticle.Play();
        moveScript.enabled = false;
        Invoke("LoadNextLevel", timeToDelay);
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(engineCrash, 0.2f /*Volume*/ );
        CrashParticle.Play();
        moveScript.enabled = false;
        Invoke("LoadLevel1", timeToDelay);
    }

    public void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            Invoke("LoadNextLevel", timeToDelay);
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    void LoadLevel1()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}