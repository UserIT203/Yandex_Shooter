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

    private const string MoneyRewardId = "moneyReward";
    public const int MoneyReward = 250;

    public static YandexManager Instance;

    private void Awake()
    {
        InitializedStickBanner();

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
            }
        });
    }
}