using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehavior : MonoBehaviour
{
    /// <summary>
    /// <c>static</c> class that allows for playing or stopping audio in the game
    /// </summary>
    public static AudioBehavior instance { get; private set; }

    private AudioSource audioSource;

    private void Start()
    {
        instance = this;

        audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// Plays an audio clip once
    /// </summary>
    /// <param name="clip"></param>
    public void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    /// <summary>
    /// Plays the BGM of the game
    /// </summary>
    public void PlayBGM()
    {
        audioSource.Play();
    }
    /// <summary>
    /// Stops all audio in the game
    /// </summary>
    public void StopAudio()
    {
        audioSource.Stop();
    }
    /// <summary>
    /// Sets the master volume for the <c>AudioSource</c>
    /// </summary>
    /// <param name="volume">Master volume of the game</param>
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
