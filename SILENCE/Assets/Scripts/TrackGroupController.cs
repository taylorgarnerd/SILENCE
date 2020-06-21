using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGroupController : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float trackGroupVolume = 1f;

    AudioSource[] tracks;
    bool restart;

    // Start is called before the first frame update
    void Start()
    {
        tracks = GetComponentsInChildren<AudioSource>();
        restart = false;
    }

    // Update is called once per frame
    void Update()
    {
        int tracksCompleted = 0;

        foreach (AudioSource track in tracks)
        {
            track.volume = trackGroupVolume;

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
