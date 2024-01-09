// A class that implements a twinkling effect for stars with improved performance and randomization.
using UnityEngine;
using Random = UnityEngine.Random;

public class StarTwinkle : MonoBehaviour
{
    /// <summary>
    /// The minimum size of the star during twinkling animation.
    /// </summary>
    public float minSize = 0.8f;

    /// <summary>
    /// The maximum size of the star during twinkling animation.
    /// </summary>
    public float maxSize = 2.0f;

    /// <summary>
    /// The amplitude of the twinkling oscillation.
    /// </summary>
    public float twinklingAmplitude = 0.4f; // Larger amplitude leads to more pronounced twinkling

    /// <summary>
    /// The frequency of the twinkling oscillation.
    /// </summary>
    public float twinklingFrequency = 0.25f; // Higher frequency leads to more frequent twinkling

    /// <summary>
    /// The bright star transform, referenced from the GameObject's child.
    /// </summary>
    private Transform _brightStar;

    /// <summary>
    /// The dim star transform, referenced from the GameObject's child.
    /// </summary>
    private Transform _dimStar;

    /// <summary>
    /// Flag indicating whether the twinkling animation is enabled.
    /// </summary>
    private bool _isTwinkling = true;

    /// <summary>
    /// The initial scale of the bright star, used for calculating the final scale during twinkling.
    /// </summary>
    private Vector3 _initialScale;

    /// <summary>
    /// Initializes the StarTwinkleImproved component and sets up the references to the bright and dim star transforms.
    /// </summary>
    private void Start()
    {
        // Get the bright and dim star transforms from the GameObject's children
        _brightStar = transform.GetChild(0);
        _dimStar = transform.GetChild(1);

        // Check if both transforms are found
        if (_brightStar == null || _dimStar == null)
        {
            // Log an error message if either transform is missing
            Debug.LogError("Bright and Dim Star Transforms not found on the GameObject.");
        }

        // Store the initial scale of the bright star for scaling calculations
        _initialScale = _brightStar.localScale;
    }

    /// <summary>
    /// Updates the twinkling animation if it's enabled.
    /// </summary>
    private void Update()
    {
        // Check if the twinkling animation is enabled
        if (!_isTwinkling) return;
        // Calculate the twinkling amplitude based on twinklingAmplitude and twinklingFrequency
        float amplitude = Mathf.Clamp(twinklingAmplitude * Mathf.Sin(Time.time * twinklingFrequency) + 1f, minSize, maxSize);

        amplitude += Random.Range(-amplitude * 0.2f, amplitude * 0.2f);

        // Clamp the amplitude within the range
        amplitude = Mathf.Clamp(amplitude, -maxSize, maxSize);

        // Randomize the target size based on the clamped amplitude
        float targetSize = _initialScale.x * (1.0f + amplitude);

        // Lerp between the current scale and the target scale
        float newSize = Mathf.Lerp(_brightStar.transform.localScale.x / _initialScale.x, targetSize, 1f);

        // Apply the new scale to both x and y scale
        Vector3 newScale = new Vector2(_initialScale.x * newSize, _initialScale.y * newSize);
        _brightStar.transform.localScale = newScale;
    }

    /// <summary>
    /// Enables the twinkling animation.
    /// </summary>
    public void EnableTwinkle()
    {
        // Set the twinkling flag to true
        _isTwinkling = true;
    }

    /// <summary>
    /// Disables the twinkling animation.
    /// </summary>
    public void DisableTwinkle()
    {
        // Set the twinkling flag to false
        _isTwinkling = false;
    }
}