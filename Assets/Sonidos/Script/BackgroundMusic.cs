using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip song1; // Asigna la primera canci�n en el Inspector
    public AudioClip song2; // Asigna la segunda canci�n en el Inspector
    private AudioSource audioSource;
    private bool firstSongFinished = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = song1;
        audioSource.Play();
    }

   private void Update()
    {
        if (!audioSource.isPlaying)
        {
            // La primera canci�n ha terminado y se reproducir� en bucle la segunda.
            audioSource.clip = song2;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    /*private void OnAudioFilterRead(float[] data, int channels)
    {
        if (!firstSongFinished && audioSource.clip == song1 && audioSource.timeSamples >= song1.samples)
        {
            // La primera canci�n ha terminado de reproducirse.
            firstSongFinished = true;
        }
    }*/
}