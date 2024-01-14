/// <summary>
/// Static class to hold variables to be used by multiple scenes.
/// </summary>
public static class PersistentVariables
{
    /// <summary>
    /// When transitioning to the main scene - we might want to transition to a specific game state, e.g. to Decision state. 
    /// Puzzle levels set this variable and decide to which part to transition.
    /// </summary>
    public static int gameState = 0;

    /// <summary>
    /// Choice taken in the Forest puzzle.
    /// </summary>
    public static bool? forestPuzzleChoice;

    /// <summary>
    /// Indicator if game is freshly started or scenes has been reloading as part of an in-game mechanics.
    /// </summary>
    public static bool isFreshStart = true;
}
