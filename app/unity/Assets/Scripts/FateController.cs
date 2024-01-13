using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the color of FATEs face depending on choices made.
/// </summary>
public class FateController : MonoBehaviour
{
    /// <summary>
    /// Called when the script object is initialized.
    /// </summary>
    private void Awake()
    {
        if (!PersistentVariables.forestPuzzleChoice.HasValue) return;

        Image fateImage = GetComponent<Image>();
        fateImage.color = new Color(
            PersistentVariables.forestPuzzleChoice.Value ? 0f : 1f,
            PersistentVariables.forestPuzzleChoice.Value ? 1f : 0f,
            0f,
            1f);
    }
}
