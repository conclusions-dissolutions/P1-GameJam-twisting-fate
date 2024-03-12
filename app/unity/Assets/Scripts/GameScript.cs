using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// All phases/modes the game can be in.
/// </summary>
[Serializable]
public enum GameState
{
    start = 0,
    mainMenu = 1,
    subjectSelect = 2,
    missionSelect = 3,
    mission = 4,
    // In mission States?
    decide = 5,
    confirmSave = 6,
    confirmDestroy = 7,
    ending = 8,
    gameover = 10,
    exit = 11,
    controls = 12,
    credits = 13
}

/// <summary>
/// Workaround for Dictionaries not being serializable by Unity
/// </summary>
[Serializable]
public class TransitionScreen
{
    public GameState state;
    public GameObject gameObject;
}

/// <summary>
/// Script to control main game interaction
/// </summary>
public class GameScript : MonoBehaviour
{
    /// <summary>
    /// All the CanvasGroups that hold all the elements in a specific state
    /// </summary>
    [SerializeField]
    public List<TransitionScreen> TransitionScreens = new List<TransitionScreen>();

    /// <summary>
    /// The previous State the game was in
    /// </summary>
    GameState LastGameState;

    /// <summary>
    /// The current state the game is in
    /// </summary>
    GameState CurrentGameState;

    /// <summary>
    /// Instance of a level loader to initiate transition to puzzle scene(s).
    /// </summary>
    public LevelLoader levelLoader;

    /// <summary>
    /// Instance of an animator to animate Earth endings
    /// </summary>
    public Animator earthAnimator;

    /// <summary>
    /// Fates canvas group to hide it during end game animation.
    /// </summary>
    public CanvasGroup fateCanvasGroup;

    /// <summary>
    /// In the middle of a transition.
    /// </summary>
    bool Transitioning;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        CurrentGameState = GameState.start;

        // If we arrive from another scene - initiate transition to a scene chosen by previous scene.
        if (PersistentVariables.gameState > 0)
        {
            ChangeState(PersistentVariables.gameState);
            PersistentVariables.gameState = 0;
        }
    }

    /// <summary>
    /// Takes in a new game state and begins the transition process
    /// </summary>
    /// <param name="newState">the next state to go to</param>
    public void ChangeState(int newState)
    {
        Transitioning = true;
        LastGameState = CurrentGameState;
        CurrentGameState = (GameState)newState;
    }
    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        if (Transitioning)
        {
            GameObject lastObject = TransitionScreens.FirstOrDefault(x => x.state == LastGameState)?.gameObject;
            GameObject currentObject = TransitionScreens.FirstOrDefault(x => x.state == CurrentGameState)?.gameObject;
            if (lastObject)
            {
                CanvasGroup canvasGroup = lastObject.GetComponent<CanvasGroup>();
                FadeScript fadeOutScript = lastObject.GetComponent<FadeScript>();
                if (canvasGroup.blocksRaycasts)
                {
                    canvasGroup.blocksRaycasts = false;
                    fadeOutScript.FadeOut();
                }

                bool isFadingOut = fadeOutScript.IsInTransition();
                if (!isFadingOut && currentObject)
                {
                    currentObject.GetComponent<FadeScript>().FadeIn();
                    currentObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    Transitioning = false;
                }
                else if (!isFadingOut)
                {
                    Transitioning = false;
                }
            }
            else
            {
                currentObject.GetComponent<FadeScript>().FadeIn();
                currentObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                Transitioning = false;
            }
        }
        else
        {
            switch (CurrentGameState)
            {
                case GameState.start:

                    break;

                case GameState.mainMenu:
                    PersistentVariables.isFreshStart = false;
                    break;

                case GameState.subjectSelect:

                    break;

                case GameState.missionSelect:

                    break;

                case GameState.mission:
                    levelLoader.LoadNextLevel(1);
                    break;

                case GameState.decide:

                    break;

                case GameState.confirmSave:

                    break;

                case GameState.confirmDestroy:

                    break;

                case GameState.ending:
                    StartCoroutine(LastGameState == GameState.confirmSave ? GameEnd("GoodEnd") : GameEnd("BadEnd"));
                    break;

                case GameState.gameover:
                    break;

                case GameState.exit:
                    // Clear and Save
                    Application.Quit();
                    break;

                case GameState.controls:

                    break;

                case GameState.credits:

                    break;

                default:
                    Debug.Log("Gamestate not set");
                    break;
            }
        }
    }

    /// <summary>
    /// Coroutine to play the game ending animation, wait a little, re-set the game state, and navigate back to main menu.
    /// </summary>
    /// <param name="endTrigger">Name of the trigger to start a corresponding game ending animation</param>
    /// <returns></returns>
    IEnumerator GameEnd(string endTrigger)
    {
        fateCanvasGroup.alpha = Mathf.MoveTowards(fateCanvasGroup.alpha, 0, Time.deltaTime);
        // Play end animation
        earthAnimator.SetTrigger(endTrigger);

        // Wait for transition to end
        yield return new WaitForSeconds(5);

        // After end animation is over - re-set values and re-load the scene.
        PersistentVariables.gameState = 1;
        PersistentVariables.forestPuzzleChoice = null;
        levelLoader.LoadNextLevel(0);
    }

    /// <summary>
    /// Function to quit the game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }


    /// <summary>
    /// Event listener for ChangeState
    /// </summary>
    /// <param name="sender">What component set the event</param>
    /// <param name="data"></param>
    public void ChangeState(Component sender, object data)
    {
        ChangeState((int)data);

        //if (data is int) ChangeState((int)data);
    }
}
