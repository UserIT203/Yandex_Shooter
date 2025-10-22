using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Playables;
using YG;
using Zenject;
using System;

public class YandexManager : MonoBehaviour
{
    [Inject] private GameData _gameData;

    public static YandexManager Instance;

    private const string MoneyRewardId = "moneyReward";
    private const string RebornRewardId = "rebornReward";
    public const int MoneyReward = 250;

    private Player _player;
    private string _currentLanguage;

    public EnviroumentData EnviroumentData { get; private set; }
    private Dictionary<string, int> _languageDict = new Dictionary<string, int>();

    private void Awake()
    {
        InitializedStickBanner();
        InitializedLanguage();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    }

    public void InitializedPlayer(Player player) => _player = player;

    public void SaveData()
    {
        if (YG2.saves.Data == null) YG2.saves.Data = new Data();

        YG2.saves.Data.CharacterID = GeneralManager.Instance.GetCharacterID(_gameData.Character);
        YG2.saves.Data.Coins = _gameData.Coins;
        YG2.saves.Data.ShowTutorial = _gameData.ShowTutorial;

        YG2.SaveProgress();
    }

    #region Language
    private void InitializedLanguage()
    {
        EnviroumentData = new EnviroumentData(YG2.envir.deviceType, YG2.envir.language);
        _currentLanguage = YG2.envir.language;

        _languageDict.Add("en", 0);
        _languageDict.Add("ru", 1);
        _languageDict.Add("tr", 2);
    }

    public void SwitchLanguage(string language)
    {
        YG2.SwitchLanguage(language);
        _currentLanguage = language;
    }

    public int GetLanguageIndex() => _languageDict[_currentLanguage];
    #endregion

    #region Adv
    private void InitializedStickBanner()
    {
        YG2.StickyAdActivity(true);
    }

    private void OnRewardAddMoney()
    {
        _gameData.AddCoins(MoneyReward);
    }

    private void OnRewardRebornPlayer()
    {
        _player.Reborn();
    }

    public void ShowInterstitialAdv()
    {
        YG2.InterstitialAdvShow();
    }

    public void ShowRewardAdv(string rewardId)
    {
        YG2.RewardedAdvShow(rewardId, () =>
        {
            switch (rewardId) 
            { 
                case MoneyRewardId:
                    OnRewardAddMoney();
                    break;
                case RebornRewardId:
                    OnRewardRebornPlayer();
                    break;
            }
        });
    }

    public void ShowRewardAdv(string rewardId, Action onRewadrComplete = null)
    {
        YG2.RewardedAdvShow(rewardId, onRewadrComplete);
    }
    #endregion
}

public struct EnviroumentData
{
    public string Device;
    public string Language;

    public EnviroumentData(string device, string language)
    {
        Device = device;
        Language = language;
    }
}