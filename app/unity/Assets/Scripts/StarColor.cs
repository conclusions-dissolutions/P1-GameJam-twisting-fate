// A class that allows for editing the star color directly in the editor.
using UnityEngine;

[ExecuteInEditMode] // This attribute indicates that the script should be executed in the editor, allowing for real-time updates.
public class StarColor : MonoBehaviour
{
    /// <summary>
    /// The color of the star.
    /// </summary>
    public Color starColor = Color.white; // Default star color is white

    /// <summary>
    /// Reference to the bright star transform.
    /// </summary>
    private Transform _brightStar;

    /// <summary>
    /// Reference to the dim star transform.
    /// </summary>
    private Transform _dimStar;

    /// <summary>
    /// Called whenever the script is modified in the editor.
    /// </summary>
    private void OnValidate()
    {
        // Get references to the bright and dim star transforms from the GameObject's children
        _brightStar = transform.GetChild(0);
        _dimStar = transform.GetChild(1);

        // Check if both transforms are found
        if (_brightStar != null && _dimStar != null)
        {
            // Update the color of both bright and dim star sprites
            _brightStar.GetComponent<SpriteRenderer>().color = starColor;
            _dimStar.GetComponent<SpriteRenderer>().color = starColor;
        }
    }

    /// <summary>
    /// Update star color explicitly/programmatically.
    /// </summary>
    public void UpdateStarColor(Color color)
    {
        starColor = color;
        OnValidate();
    }
}