using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] audioSources;  // Array to hold both AudioSource components
    // Reference to the AudioSource component
    [SerializeField]
    private AudioSource audioSourceDrillStart;
    [SerializeField]
    private AudioSource audioSourceDrillStop;
    [SerializeField]
    public AudioSource audioSourceCorrect;



    void Awake()
    {
        // Get all AudioSource components attached to this GameObject
        audioSources = GetComponents<AudioSource>();

        if (audioSources.Length >= 1)
        {
            // Assign based on known order or identifiers
            audioSourceDrillStart = audioSources[0];  
            audioSourceDrillStop = audioSources[1];
            audioSourceCorrect = audioSources[2]; 
        }
        else
        {
            Debug.LogError("Not enough AudioSource components attached to the GameObject.");
        }
        
    }


    public void DrillSound(bool value)
    {
        // Define the actions to perform
        System.Action startAction = () =>
        {
            audioSourceDrillStart.Play();
            audioSourceDrillStop.Stop();
        };

        System.Action stopAction = () =>
        {
            audioSourceDrillStart.Stop();
            audioSourceDrillStop.Play();
        };

        // Use a lambda to choose the correct action based on the value
        System.Action action = value ? startAction : stopAction;

        // Invoke the selected action
        action.Invoke();
    }
}
