using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main controller of all sounds and music.
/// </summary>
public class AudioPlayer : MonoBehaviour
{
    /// <summary>
    /// Holds a single instance of AudioPlayer to using in the AudioPlayer.
    /// </summary>
    public static AudioPlayer instance;

    /// <summary>
    /// Plays the music and sounds
    /// </summary>
    [SerializeField] AudioSource TheAudioSource;

    /// <summary>
    /// The order of the audio files in the array and the order of filenames in the enumerator must be 
    /// in the same order for this to work. See the example.png in the audio folder.
    /// </summary>
    [SerializeField] AudioClip[] BGM_Tracks;

    /// <summary>
    /// Enum of music tracks.
    /// </summary>
    public enum BGMs { OST_NEUTRAL, OST_SAD, OST_LIGHT_02, OST_Menu, Forest_ambiance }

    BGMs CurrentPlaying = BGMs.OST_NEUTRAL;
    [SerializeField] BGMs ShouldBePlaying = BGMs.OST_NEUTRAL;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    void Update()
    {
        if (CurrentPlaying != ShouldBePlaying)
        {
            TheAudioSource.Stop();
            TheAudioSource.clip = BGM_Tracks[(int)ShouldBePlaying];
            TheAudioSource.Play();
            CurrentPlaying = ShouldBePlaying;
        }
    }

    /// <summary>
    /// This sets up the track but does not play it.
    /// </summary>
    /// <param name="playThis">enum value of the track to play</param>
    public void SetBGM(BGMs playThis)
    {
        ShouldBePlaying = playThis;
    }

    /// <summary>
    /// This sets up the track but does not play it.
    /// </summary>
    /// <param name="playThis">int value of the track to play</param>
    public void SetBGM(int playThis)
    {
        ShouldBePlaying = (BGMs)playThis;
    }

    /// <summary>
    /// Plays sound files.
    /// </summary>
    /// <param name="clip">what sound file to play</param>
    public void PlayClip(AudioClip clip)
    {
        TheAudioSource.PlayOneShot(clip);
    }
}
