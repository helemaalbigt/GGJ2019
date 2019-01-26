using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioMixerSnapshot menu;
    public AudioMixerSnapshot silence;
    public AudioMixerSnapshot flying;
    public ToggleDrift toggleDrift;

    [Space(25)] public GameState startMenuState;
    public GameState deathMenuState;
    public GameState flyState;
    public AudioSource flyIntro;
    public AudioSource flyLoop;

    void Start()
    {
        startMenuState.onStateEnabled += FadeToMenuMusic;
        deathMenuState.onStateEnabled += FadeToMenuMusic;
        flyState.onStateEnabled += FadeToSilence;
        toggleDrift.onPlayerEntered += StartFlyMusic;
    }

    private void FadeToMenuMusic()
    {
        menu.TransitionTo(0.2f);
    }

    private void FadeToSilence()
    {
        silence.TransitionTo(1f);
    }

    private void StartFlyMusic()
    {
        flying.TransitionTo(0.01f);

        StartCoroutine(PlayLoopSequence());
    }

    IEnumerator PlayLoopSequence()
    {
        flyLoop.Stop();
        flyIntro.Stop();

        flyIntro.Play();

        while(flyIntro.isPlaying)
            yield return null;

        flyLoop.Play();
    }
}
