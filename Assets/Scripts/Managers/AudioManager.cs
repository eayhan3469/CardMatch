using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip flipClip;
    [SerializeField] private AudioClip matchClip;
    [SerializeField] private AudioClip mismatchClip;
    [SerializeField] private AudioClip gameOverClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = 0;
    }

    private void OnEnable()
    {
        GameManager.OnFlip += PlayFlip;
        GameManager.OnMatched += PlayMatch;
        GameManager.OnMismatch += PlayMismatch;
        GameManager.OnGameEnded += PlayGameOver;
    }

    private void OnDisable()
    {
        GameManager.OnFlip -= PlayFlip;
        GameManager.OnMatched -= PlayMatch;
        GameManager.OnMismatch -= PlayMismatch;
        GameManager.OnGameEnded -= PlayGameOver;
    }

    private void PlayFlip() => Play(flipClip);
    private void PlayMatch() => Play(matchClip);
    private void PlayMismatch() => Play(mismatchClip);
    private void PlayGameOver() => Play(gameOverClip);

    private void Play(AudioClip clip)
    {
        if (clip != null)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}
