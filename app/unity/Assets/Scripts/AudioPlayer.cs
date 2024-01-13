using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum of music tracks.
/// </summary>
[Serializable]
public enum BGMs { OST_NEUTRAL, OST_SAD, OST_LIGHT_02, OST_Menu, Forest_ambiance }

/// <summary>
/// 
/// </summary>
public enum Sounds { }

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
    /// 
    /// </summary>
    private bool ShouldPlay = true;

    /// <summary>
    /// Plays the music and sounds
    /// </summary>
    [SerializeField] AudioSource TheAudioSource;

    /// <summary>
    /// In game sound tracks.
    /// The order of the audio files in the array and the order of filenames in the enumerator must be 
    /// in the same order for this to work. See the example.png in the audio folder.
    /// </summary>
    [SerializeField] AudioClip[] BGM_Tracks;

    /// <summary>
    /// All in game sound files
    /// </summary>
    [SerializeField] AudioClip[] SoundFiles;

    /// <summary>
    /// What the player is currently playing
    /// </summary>
    BGMs CurrentPlaying = BGMs.OST_NEUTRAL;

    /// <summary>
    /// What the player should be playing
    /// </summary>
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
        if (!ShouldPlay && TheAudioSource.isPlaying)
        {
            TheAudioSource.Stop();
        }
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="data"></param>
    public void SetBGM(Component sender, object data)
    {
        if (data is not int) return;

        ShouldBePlaying = (BGMs)data;
        ShouldPlay = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="data"></param>
    public void StopBGM(Component sender, object data)
    {
        ShouldPlay = false;
    }

    public void PlaySound(Component sender, object data)
    {
        if (data is not int) return;
        int soundIndex = (int)data;

        if (soundIndex > SoundFiles.Length) return; //TODO: THROW ERROR

        TheAudioSource.PlayOneShot(SoundFiles[soundIndex]);
    }
}
