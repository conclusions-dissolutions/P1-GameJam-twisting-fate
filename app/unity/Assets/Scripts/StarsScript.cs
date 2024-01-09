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
    public int maxStarsPerSector = 10;

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
    /// Goes through coordinates of the main game screen sector from bottom left corner to top right corner.
    /// Populates small sectors of background with a random number of star prefabs. 0-5 stars per sector.
    /// </summary>
    public void Awake()
    {
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
                    PlaceStar(
                        x: Random.Range(x, x + sectorWidth),
                        y: Random.Range(y, y + sectorHeight),
                        starIndex: Random.Range(0, starPrefabs.Count),
                        starScale: Random.Range(0.05f, 0.10f));
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
    }
}
