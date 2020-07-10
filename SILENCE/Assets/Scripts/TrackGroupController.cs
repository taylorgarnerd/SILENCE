using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TrackGroupController : MonoBehaviour
{
    //[SerializeField] [Range(0, 1)] float trackGroupVolume = 1f;
    [SerializeField] float fadeDuration = 1f;
    [SerializeField] AudioMixer audioMixer;
    //[SerializeField] String exposedVolumeParameter;
    [SerializeField] AudioMixerSnapshot FadeInSnapshot;
    [SerializeField] AudioMixerSnapshot FadeOutSnapshot;

    AudioSource[] tracks;
    bool tracksPlaying;
    //AudioMixerSnapshot FadeOut;
    //AudioMixerSnapshot FadeIn;

    Enemy[] enemies;

    IEnumerator FadeIn;
    IEnumerator FadeOut;

    BeatCounter beatCounter;

    // Start is called before the first frame update
    void Start()
    {
        tracks = GetComponentsInChildren<AudioSource>();
        tracksPlaying = true;
        //FadeIn = FadeMixerGroup.StartFade(audioMixer, exposedVolumeParameter, fadeDuration, trackGroupVolume);
        //FadeOut = FadeMixerGroup.StartFade(audioMixer, exposedVolumeParameter, fadeDuration, 0);
        beatCounter = GetComponentInChildren<BeatCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        RestartCheck();

        //Update enemies array with all active enemies in this group
        enemies = GetComponentsInChildren<Enemy>();

        //UpdateBeatCounterObservers();
        //This time i may need to check for *NEW* enemies each update and then add them to the observers

        FadeCheck();
    }

    private void UpdateBeatCounterObservers()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            beatCounter.observers[i] = enemies[i].gameObject;
        }
    }

    private void FadeCheck()
    {
        if (tracksPlaying && enemies.Length <= 0)
        {
            //Debug.Log("Tracks are playing, but no enemies found. Fade Out...");
            FadeOutAllTracks();
        }

        if (!tracksPlaying && enemies.Length > 0)
        {
            //Debug.Log("Tracks are not playing, but enemies found. Fade In...");
            FadeInAllTracks();
        }
    }

    private void FadeInAllTracks()
    {
        //foreach (AudioSource track in tracks)
        //{
        //    Debug.Log("Stopping Fade Out coroutine for " + track.gameObject.name);
        //    StopCoroutine(FadeOut);
        //    Debug.Log("Starting Fade In coroutine for " + track.gameObject.name);
        //    StartCoroutine(FadeIn);
        //    //StartCoroutine(FadeMixerGroup.StartFade(audioMixer, exposedVolumeParameter, fadeDuration, trackGroupVolume));
        //}
        FadeInSnapshot.TransitionTo(fadeDuration);

        tracksPlaying = true;
    }

    private void FadeOutAllTracks()
    {
        //foreach (AudioSource track in tracks)
        //{
        //    Debug.Log("Stopping Fade In coroutine for " + track.gameObject.name);
        //    StopCoroutine(FadeIn);
        //    Debug.Log("Starting Fade Out coroutine for " + track.gameObject.name);
        //    StartCoroutine(FadeOut);
        //    //StartCoroutine(FadeMixerGroup.StartFade(audioMixer, exposedVolumeParameter, fadeDuration, 0));
        //}
        FadeOutSnapshot.TransitionTo(fadeDuration);

        tracksPlaying = false;
    }

    private void RestartCheck()
    {
        int tracksCompleted = 0;

        foreach (AudioSource track in tracks)
        {
            //track.volume = trackGroupVolume;

            //Increment the above variable if a track isn't playing anymore
            if (!track.isPlaying)
            {
                tracksCompleted++;
            }
        }

        //If the above variable equals the length of tracks[], then play all tracks
        if (tracksCompleted == tracks.Length)
        {
            RestartTracks();
        }
    }

    private void RestartTracks()
    {
        foreach (AudioSource track in tracks)
        {
            track.Play();
        }
    }
}
