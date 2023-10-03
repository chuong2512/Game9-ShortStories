using System.Collections.Generic;
using UnityEngine;

public class BookCollection : MonoBehaviour
{
    [SerializeField] private SongButton _songButton;
    [SerializeField] private Transform _content;

    private SongButton _currentSong;
    private List<SongButton> _songButtons = new List<SongButton>();

    void Start()
    {
        ShowAllSongButtons();
        GameManager.OnChooseSong += OnChooseBook;
    }

    private void OnChooseBook(int i)
    {
        ChooseItem(i);
    }

    private void ShowAllSongButtons()
    {
        var count = GameDataManager.Instance.bookSo.bookInfors.Length;

        for (int i = 0; i < count; i++)
        {
            var song = Instantiate(_songButton, _content);
            song.SetID(i);
            _songButtons.Add(song);
        }

        GetCurrentSong();
    }

    public SongButton GetCurrentSong()
    {
        _currentSong = _songButtons.Find(song => song.BookID == GameDataManager.Instance.currentID);
        return _currentSong;
    }

    public void Refresh()
    {
        foreach (var song in _songButtons)
        {
            song.Refresh();
        }
    }

    public void ChooseItem(int songID)
    {
        _currentSong?.Choose(false);
        GetCurrentSong();
        _currentSong?.Choose(true);
    }
}