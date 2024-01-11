using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer instance;

    [SerializeField] AudioSource TheAudioSource;
    [SerializeField] AudioClip[] BGM_Tracks;
    //The order of the audio files in the above array and the order of filenames in the enumerator must be 
    //in the same order for this to work. See the example.png in the audio folder.
    public enum BGMs { OST_NEUTRAL, OST_SAD, OST_LIGHT_02, OST_Menu, Forest_ambiance }

    BGMs CurrentPlaying = BGMs.OST_NEUTRAL;
    [SerializeField] BGMs ShouldBePlaying = BGMs.OST_NEUTRAL;

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

    public void SetBGM(BGMs playThis)
    {
        ShouldBePlaying = playThis;
    }
    public void SetBGM(int playThis)
    {
        ShouldBePlaying = (BGMs)playThis;
    }

    public void PlayClip(AudioClip clip)
    {
        TheAudioSource.PlayOneShot(clip);
    }
}
