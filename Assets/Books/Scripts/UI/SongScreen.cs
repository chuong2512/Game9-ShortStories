using System;
using ABCBoard.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongScreen : BaseScreenWithModel<BookModel>
{
    private int _bookID;

    [SerializeField] private ScrollRect _scroll;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Button _buttonPlay;

    void Start()
    {
        _buttonPlay?.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        AudioManager.Instance.PlaySong(_bookID);
    }

    public override void BindData(BookModel model)
    {
        _bookID = model.bookID;
        SetImage();
        _scroll.verticalNormalizedPosition = 1;
    }

    private void SetImage()
    {
        var songSo = GameDataManager.Instance.bookSo;
        _name.text = songSo.GetBookWithID(_bookID).name;
        _text.text = songSo.GetBookWithID(_bookID).content;
    }

    public override ScreenType GetID() => ScreenType.BookScreen;
}

public class BookModel
{
    public int bookID;
}