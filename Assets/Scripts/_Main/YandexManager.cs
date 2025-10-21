using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Playables;
using YG;
using Zenject;

public class YandexManager : MonoBehaviour
{
    [Inject] private GameData _gameData;

    public static YandexManager Instance;

    private const string MoneyRewardId = "moneyReward";
    private const string RebornRewardId = "rebornReward";
    public const int MoneyReward = 250;

    private Player _player;

    public EnviroumentData EnviroumentData { get; private set; }

    private void Awake()
    {
        InitializedStickBanner();

        EnviroumentData  = new EnviroumentData(YG2.envir.deviceType, YG2.envir.language);

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

    public void SwitchLanguage(string language)
    {
        YG2.SwitchLanguage(language);
    }

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