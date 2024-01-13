using System;
using UnityEngine;

/// <summary>
/// Controls the FATEs face color depending on players proximity to the ending leavers.
/// </summary>
public class FatePuzzleController : MonoBehaviour
{
    /// <summary>
    /// Reference to FATEs face sprite to change color.
    /// </summary>
    public SpriteRenderer fateSprite;

    /// <summary>
    /// Reference to Lumberjacks leaver to calculate players proximity to leavers.
    /// </summary>
    public Transform lumberjacksLeaver;

    /// <summary>
    /// Reference to Activists leaver to calculate players proximity to leavers.
    /// </summary>
    public Transform activistsLeaver;

    /// <summary>
    /// Reference to Players leaver to calculate players proximity to leavers.
    /// </summary>
    public Transform player;

    /// <summary>
    /// Max value of a color component.
    /// </summary>
    private readonly float colorMax = 255f;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        //Get the positive distance between players position and leavers position
        float lumberjacksDelta = Math.Abs(lumberjacksLeaver.position.x - player.position.x);
        float activistsDelta = Math.Abs(activistsLeaver.position.x - player.position.x);

        float fateRed = colorMax; // Can remain max value
        float fateGreen = colorMax; // Can remain max value
        float fateBlue; // Always changed

        // The maximum distance a player can have between each leaver.
        float hundredPercentDistance = (Math.Abs(lumberjacksLeaver.position.x) + Math.Abs(activistsLeaver.position.x)) / 2;
        if (lumberjacksDelta < activistsDelta)
        {
            // If player is closer to Lumberjacks leaver 
            // Calculate distance between the leaver and player compared to the max distance in percentages.
            float currentPercentage = lumberjacksDelta * 100 / hundredPercentDistance;
            //Reduce Blue and Green parts of the color, making FATE more Red.
            float gbValue = colorMax * currentPercentage / 100;
            fateBlue = gbValue;
            fateGreen = gbValue;
        }
        else
        {
            // If player is closer to Activists leaver 
            // Calculate distance between the leaver and player compared to the max distance in percentages.
            float currentPercentage = activistsDelta * 100 / hundredPercentDistance;
            //Reduce Red and Blue parts of the color, making FATE more Green.
            float rbValue = colorMax * currentPercentage / 100;
            fateRed = rbValue;
            fateBlue = rbValue;
        }
        // Adjust FATEs color using Color32 class that is able to work with 0-255 values. Regular Color class expects values between 0.0f and 1.0f
        fateSprite.color = new Color32((byte)fateRed, (byte)fateGreen, (byte)fateBlue, (byte)colorMax);
    }
}
