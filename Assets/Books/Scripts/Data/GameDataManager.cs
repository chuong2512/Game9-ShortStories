using SingleApp;
using UnityEngine;
using UnityEngine.Serialization;

[DefaultExecutionOrder(-100)]
public class GameDataManager : PersistentSingleton<GameDataManager>
{
    /*----Scriptable data-----------------------------------------------------------------------------------------------*/
    [FormerlySerializedAs("songSo")] public BookSO bookSo;

    /*----Data variable-------------------------------------------------------------------------------------------------*/
    [HideInInspector] public PlayerData playerData;
    public int currentID = -1;

    private void OnEnable()
    {
        playerData = new GameObject(Constant.DataKey_PlayerData).AddComponent<PlayerData>();
        playerData.transform.parent = transform;
        playerData.Init();
    }

    private void Start()
    {
        Application.targetFrameRate = Mathf.Max(60, Screen.currentResolution.refreshRate);
    }

    public void ResetData()
    {
        playerData.ResetData();
    }

    public void SetCurrentSongID(int songID)
    {
        currentID = songID;
        GameManager.OnChooseSong.Invoke(currentID);
    }
}