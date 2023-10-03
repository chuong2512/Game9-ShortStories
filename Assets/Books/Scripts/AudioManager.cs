using System;
using SingleApp;
using UnityEngine;

[DefaultExecutionOrder(-99)]
public class AudioManager : Singleton<AudioManager>
{
    public AudioSource musicSource;
    public float _timePlay = 0.6f;

    private AudioClip _audioClip;
    private bool _isPlaying;
    private int _crtId = -1;

    public bool IsPlaying => _isPlaying;

    public void SetTimeCount(float time)
    {
        Play();
        GameManager.SetTimeStop?.Invoke(time);
    }

    private void Start()
    {
        _isPlaying = false;
        _crtId = GameDataManager.Instance.currentID;

        var currentSong = GameDataManager.Instance.bookSo.GetBookWithID(_crtId);

        if (currentSong != null)
        {
            _audioClip = currentSong.song;
        }

        musicSource.loop = true;
    }

    public void PlaySong(int id)
    {
        GameDataManager.Instance.SetCurrentSongID(id);

        _crtId = id;
        _audioClip = GameDataManager.Instance.bookSo.GetBookWithID(_crtId).song;
        musicSource.clip = _audioClip;
        musicSource.Play();
        _isPlaying = true;
        GameManager.OnPlayMusic?.Invoke(_isPlaying);
    }

    public void ClickPlayBtn()
    {
        if (!_isPlaying)
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
            GameManager.SetTimeStop.Invoke(0);
        }

        _isPlaying = !_isPlaying;

        GameManager.OnPlayMusic?.Invoke(_isPlaying);
        Debug.Log($"PLaying : {_isPlaying}");
    }

    public void Stop()
    {
        musicSource.Stop();
        _isPlaying = false;

        GameManager.OnPlayMusic.Invoke(_isPlaying);
    }

    public void Play()
    {
        if (!_isPlaying)
        {
            musicSource.Play();
            _isPlaying = true;
        }

        GameManager.OnPlayMusic.Invoke(_isPlaying);
    }
}