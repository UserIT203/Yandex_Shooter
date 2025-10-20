using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardButton : MonoBehaviour
{
    private const string RewardId = "moneyReward";

    private Button _button;

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnReward);
        _button.onClick.AddListener(() => AudioManager.PlaySound("ButtonClick"));
    }

    private void OnReward()
    {
        YandexManager.Instance.ShowRewardAdv(RewardId);
    }
}
