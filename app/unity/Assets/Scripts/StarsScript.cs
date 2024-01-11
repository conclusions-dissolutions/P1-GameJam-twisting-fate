using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that places star prefab clones in the background object.
/// </summary>
public class StarsScript : MonoBehaviour
{
    /// <summary>
    /// The background object to populate with stars.
    /// </summary>
    public GameObject background;

    /// <summary>
    /// The list of star prefabs. Populated in Unity Inspector.
    /// </summary>
    public List<Object> starPrefabs;

    /// <summary>
    /// Seed for the stars generation. If less than 0 - generate stars randomly.
    /// </summary>
    public int seed = -1;

    /// <summary>
    /// Maximum amount of stars per sector.
    /// </summary>
    public int maxStarsPerSector = 15;

    /// <summary>
    /// Percentage of stars that should be the smallest and not twinkling.
    /// </summary>
    public int smallStarsPercentage = 95;

    /// <summary>
    /// Percentage of stars that should be medium sized and twinkling.
    /// </summary>
    public int mediumStarsPercentage = 4;

    /// <summary>
    /// Percentage of stars that should be the the largest and twinkling.
    /// </summary>
    public int largeStarsPercentage = 1;

    /// <summary>
    /// The X coordinate of the lower left corner of the game screen.
    /// </summary>
    private readonly float lowerLeftCornerX = -8.8f;

    /// <summary>
    /// The Y coordinate of the lower left corner of the game screen.
    /// </summary>
    private readonly float lowerLeftCornerY = -4.9f;

    /// <summary>
    /// Somewhat reasonable width of the sector of the background to populate with starts to achieve relatively spread-out population.
    /// Screen width can be split into 10 equal segments with sectorWidth as width.
    /// </summary>
    private readonly float sectorWidth = 1.76f;

    /// <summary>
    /// Somewhat reasonable height of the sector of the background to populate with starts to achieve relatively spread-out population.
    /// Screen height can be split into 5 equal segments with sectorHeight as height.
    /// </summary>
    private readonly float sectorHeight = 1.96f;

    /// <summary>
    /// Scale for the smallest stars. Should be the majority, but is configurable.
    /// </summary>
    private readonly float minStarScale = 0.04f;

    /// <summary>
    /// List of possible colors for stars in HEX format. Source: https://clarkvision.com/articles/color-of-stars/
    /// Used Color Picker on an image with stars of all colors.
    /// </summary>
    private readonly List<string> starColors = new()
    {
        "#668AC8",
        "#768CC8",
        "#7992C8",
        "#6F8FC8",
        "#7394C7",
        "#7495C8",
        "#82A6C8",
        "#8AAEC8",
        "#84A6C9",
        "#94B0C8",
        "#97AFC7",
        "#A1B8C8",
        "#A4BFC8",
        "#B8C8C8",
        "#BFC9C0",
        "#C4C5C0",
        "#C1C8C0",
        "#C8C8C8",
        "#BFC8AD",
        "#C8B5A6",
        "#C8C8A4",
        "#C8C3BD",
        "#C8B9A2",
        "#C8B386",
        "#C8996F",
        "#C8A263",
        "#C98642",
        "#C88D49",
        "#C88954",
        "#C77836",
        "#C8352E"
    };

    /// <summary>
    /// Goes through coordinates of the main game screen sector from bottom left corner to top right corner.
    /// Populates small sectors of background with a random number of star prefabs. Number of stars per sector is configurable.
    /// </summary>
    public void Awake()
    {
        if (smallStarsPercentage + mediumStarsPercentage + largeStarsPercentage != 100)
            throw new System.Exception("Please make sure that the sum of small, medium, and large star percentage values is equal to 100.");


        if (seed >= 0)
            Random.InitState(seed);

        // Traverse the X coordinate. The last X coordinate is located 1 xStep away from the right edge of the screen.
        for (float x = lowerLeftCornerX; x <= lowerLeftCornerX * (-1) - sectorWidth; x += sectorWidth)
        {
            // Traverse the Y coordinate. The last Y coordinate is located 1 yStep away from the right edge of the screen.
            for (float y = lowerLeftCornerY; y <= lowerLeftCornerY * (-1) - sectorHeight; y += sectorHeight)
            {
                // Place from 0 to configurable number of stars per sector to have a somewhat realistic-looking starry view.
                int starsPerSector = Random.Range(0, maxStarsPerSector + 1);
                for (int i = 0; i < starsPerSector; i++)
                {
                    // Random number between 0 and 100 to decide the scale of the star.
                    int starPercentage = Random.Range(0, 101);
                    PlaceStar(
                        x: Random.Range(x, x + sectorWidth),
                        y: Random.Range(y, y + sectorHeight),
                        starIndex: Random.Range(0, starPrefabs.Count),
                        starScale: starPercentage > smallStarsPercentage + mediumStarsPercentage
                        ? Random.Range(0.07f, 0.1f)
                        : starPercentage > smallStarsPercentage ? Random.Range(0.04f, 0.07f) : minStarScale);
                }
            }
        }
    }


    /// <summary>
    /// Creates an instance out of one of listed prefabs and places it inside of the background game object.
    /// </summary>
    /// <param name="x">x coordinate of the star placement.</param>
    /// <param name="y">y coordinate of the star placement.</param>
    /// <param name="starIndex">An index in the list to get a star prefab.</param>
    /// <param name="starScale">A scale of the star to be rendered used for both X and Y values.</param>
    private void PlaceStar(float x, float y, int starIndex, float starScale)
    {
        GameObject star = Instantiate(starPrefabs[starIndex], new Vector3(x, y, 0), Quaternion.identity, background.transform) as GameObject;
        star.transform.localScale = new Vector3(starScale, starScale);

        StarColor starColor = star.GetComponent<StarColor>();
        if (starColor)
        {
            ColorUtility.TryParseHtmlString(starColors[Random.Range(0, starColors.Count)], out Color color);
            starColor.UpdateStarColor(color);
        }

        StarTwinkle twinkle = star.GetComponent<StarTwinkle>();
        if (!twinkle) return;

        if (starScale == minStarScale)
            twinkle.DisableTwinkle();
        else
        {
            float halfDefaultFrequency = twinkle.twinklingFrequency / 2;
            float halfDefaultAmplitude = twinkle.twinklingFrequency / 2;
            twinkle.twinklingFrequency = Random.Range(halfDefaultFrequency, twinkle.twinklingFrequency + halfDefaultFrequency);
            twinkle.twinklingAmplitude = Random.Range(halfDefaultAmplitude, twinkle.twinklingAmplitude + halfDefaultAmplitude);
        }
    }
}
