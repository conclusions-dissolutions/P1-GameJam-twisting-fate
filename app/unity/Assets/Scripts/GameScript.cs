using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    /// <summary>
    /// All phases/modes the game can be in.
    /// </summary>
    public enum GameState
    {
        start,
        mainMenu,
        subjectSelect,
        missionSelect,
        mission,
        // In mission States?
        decide,
        confirm,
        gameover,
        exit
    }

    /// <summary>
    /// All the CanvasGroups that hold all the elements in a specific state
    /// </summary>
    public Dictionary<GameState, CanvasGroup> CanvasGroups = new Dictionary<GameState, CanvasGroup>();

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
    void ChangeState(GameState newState)
    {
        Transitioning = true;
        LastGameState = CurrentGameState;
        CurrentGameState = newState;
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        if(Transitioning)
        {
            // Track elements that are in the middle of transitioning
            Transitioning = false;

            // To be replaced with FadeScript script
            if (CanvasGroups.ContainsKey(LastGameState))
            {
                CanvasGroups[LastGameState].alpha = 0;
            }
            if (CanvasGroups.ContainsKey(CurrentGameState))
            {
                CanvasGroups[CurrentGameState].alpha = 1;
            }
        }
        else
        {
            switch(CurrentGameState)
            {
                case GameState.start:
                    ChangeState(GameState.mainMenu);
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
