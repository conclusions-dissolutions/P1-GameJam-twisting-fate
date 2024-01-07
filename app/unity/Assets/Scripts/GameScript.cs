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
    confirm = 6,
    gameover = 7,
    exit = 8
}

[Serializable]
public class TransitionScreen
{
    public GameState state;
    public GameObject gameObject;
}

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
    /// In the middle of a transition.
    /// </summary>
    bool Transitioning;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        CurrentGameState = GameState.start;
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
                    ChangeState(Convert.ToInt32(GameState.mainMenu));
                    break;

                case GameState.mainMenu:

                    break;

                case GameState.subjectSelect:

                    break;

                case GameState.missionSelect:

                    break;

                case GameState.mission:

                    break;

                case GameState.decide:

                    break;

                case GameState.confirm:

                    break;

                case GameState.gameover:

                    break;

                default:
                    Debug.Log("Gamestate not set");
                    break;
            }
        }
    }
}
