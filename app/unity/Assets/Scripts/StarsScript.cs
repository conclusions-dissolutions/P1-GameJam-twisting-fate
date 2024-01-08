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
    /// Goes through coordinates of the main game screen area from bottom left corner to top right corner.
    /// Populates small areas of background with a random number of star prefabs. 0-5 stars per area.
    /// </summary>
    public void Awake()
    {
        float xStep = 1.76f;
        float yStep = 1.96f;
        for (float x = -8.8f; x <= 8.8f - xStep; x += xStep)
        {
            for (float y = -4.9f; y <= 4.9f - yStep; y += yStep)
            {
                int starsPerAre = Random.Range(0, 6);
                for (int i = 0; i < starsPerAre; i++)
                {
                    PlaceStar(Random.Range(x, x + xStep), Random.Range(y, y + yStep), Random.Range(0, starPrefabs.Count));
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
    private void PlaceStar(float x, float y, int starIndex)
    {
        Instantiate(starPrefabs[starIndex], new Vector3(x, y, 0), Quaternion.identity, background.transform);
    }
}
