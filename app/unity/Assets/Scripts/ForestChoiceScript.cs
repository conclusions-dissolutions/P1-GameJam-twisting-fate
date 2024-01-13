using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script to be attached to the forest interactive elements to transition back to the main scene, saving the choice.
/// </summary>
public class ForestChoiceScript : MonoBehaviour
{
    /// <summary>
    /// Instance of a level loader to initiate transition to main menu scene.
    /// </summary>
    public LevelLoader levelLoader;

    /// <summary>
    /// Instance of animator to trigger the leaver pull animation.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Instance of audio source to play the leaver pull sound.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// The boolean choice associated with collision.
    /// </summary>
    public bool choice;

    /// <summary>
    /// The boolean to track if the choice was already made to prevent multiple triggers.
    /// </summary>
    private bool isChoiceMade = false;

    /// <summary>
    /// Default event function to handle collision with the object to which this script is attached.
    /// Save the choice in a persistent static variable to reuse in different scenes and transition back to the main scene.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isChoiceMade) return;

        isChoiceMade = true;
        audioSource.Play();
        animator.SetTrigger("Pull");
        PersistentVariables.forestPuzzleChoice = choice;
        levelLoader.LoadNextLevel(0);
    }
}
