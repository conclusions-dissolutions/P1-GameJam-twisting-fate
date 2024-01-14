using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Handles transition between scenes.
/// </summary>
public class LevelLoader : MonoBehaviour
{
    /// <summary>
    /// Animator component that performs transition animation between scenes.
    /// </summary>
    public Animator transition;

    /// <summary>
    /// Transition time in seconds. 1 second, because that is the value set for the fade animation between scenes.
    /// </summary>
    public float transitionTime = 1f;

    /// <summary>
    /// Starts a coroutine to transition to another scene.
    /// </summary>
    /// <param name="levelIndex">Index of the scene to navigate to. See File -> Build Settings in Unity.</param>
    public void LoadNextLevel(int levelIndex)
    {
        //Navigate to "Decision" part of the main scene if we navigate there. Assuming that we always navigate there from puzzle levels
        if (levelIndex == 0 && PersistentVariables.forestPuzzleChoice.HasValue)
            PersistentVariables.gameState = 5;

        StartCoroutine(LoadLevel(levelIndex));
    }

    /// <summary>
    /// Start the fade animation between scenes. Waits for animation to complete before actually changing the scene.
    /// </summary>
    /// <param name="levelIndex">Index of the scene to navigate to. See File -> Build Settings in Unity.</param>
    /// <returns></returns>
    IEnumerator LoadLevel(int levelIndex)
    {
        // Play fade animation
        transition.SetTrigger("Start");

        // Wait for transition to end
        yield return new WaitForSeconds(transitionTime);

        // After fade animation is over - handle scene change.
        SceneManager.LoadScene(levelIndex);
    }
}
