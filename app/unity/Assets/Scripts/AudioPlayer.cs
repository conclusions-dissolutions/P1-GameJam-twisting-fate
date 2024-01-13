using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO: ADD DESCRIPTION OF THIS CLASS
/// 
/// Authors: Ethan Sewall
/// Created: Jan , 2024 TODO: ADD DATE WHEN CREATED
/// Last Modified: Jan 12, 2024
/// </summary>
public class AudioPlayer : MonoBehaviour
{
    /// <summary>
    /// Instance of the AudioPlayer.
    /// </summary>
    public static AudioPlayer instance;

    /// <summary>
    /// AudioSource var.
    /// </summary>
    [SerializeField] AudioSource TheAudioSource;
    /// <summary>
    /// Array of Music tracks var.
    /// </summary>
    [SerializeField] AudioClip[] BGM_Tracks;

    /// <summary>
    /// The order of the audio files in the above array and the order of filenames in the enumerator must be 
    /// in the same order for this to work. See the example.png in the audio folder.
    /// </summary>
    public enum BGMs { OST_NEUTRAL, OST_SAD, OST_LIGHT_02, OST_Menu, Forest_ambiance }

    /// <summary>
    /// Song that is currently playing var.
    /// </summary>
    BGMs CurrentPlaying = BGMs.OST_NEUTRAL;
    /// <summary>
    /// Song that should be playing var.
    /// </summary>
    [SerializeField] BGMs ShouldBePlaying = BGMs.OST_NEUTRAL;


    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        //If not instance, set instance.
        if (!instance)
        {
            instance = this;
        }
        //Else, destroy the gameobject.
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        // If the current song is not the song that should be playing, do this.
        if (CurrentPlaying != ShouldBePlaying)
        {
            //Stops current song playing.
            TheAudioSource.Stop();
            //Replaces current clip with next clip.
            TheAudioSource.clip = BGM_Tracks[(int)ShouldBePlaying];
            //Plays new song.
            TheAudioSource.Play();
            //Sets currentplaying with shouldbeplaying.
            CurrentPlaying = ShouldBePlaying;
        }
    }

    /// <summary>
    /// Sets shouldBePlaying var with BGMs playThis parameter.
    /// </summary>
    /// <param name="playThis"> desired BGMs to be set. </param>
    public void SetBGM(BGMs playThis)
    {
        ShouldBePlaying = playThis;
    }
    /// <summary>
    /// Sets shouldBePlaying var with int playThis parameter.
    /// </summary>
    /// <param name="playThis"> desired int to be set. </param>
    public void SetBGM(int playThis)
    {
        ShouldBePlaying = (BGMs)playThis;
    }
    /// <summary>
    /// Plays audioClip clip.
    /// </summary>
    /// <param name="clip"> desired clip to be played. </param>
    public void PlayClip(AudioClip clip)
    {
        TheAudioSource.PlayOneShot(clip);
    }
}
