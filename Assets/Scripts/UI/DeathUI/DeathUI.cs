using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DeathUI : MonoBehaviour
{
    private const string RewardId = "rebornReward";

    [Inject] private Player _player;

    [Header("Resume Panel Links")]
    [SerializeField] private CanvasGroup _resumePanel;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _applyButton;
    [SerializeField] private Button _refuseButton;

    [SerializeField] private ResultPanelUI _resultPanel;

    private void OnEnable()
    {
        _applyButton.onClick.AddListener(OnRebornPlayer);
        _refuseButton.onClick.AddListener(OnClickRefuseButton);

        _applyButton.onClick.AddListener(() => AudioManager.PlaySound("ButtonClick"));
        _refuseButton.onClick.AddListener(() => AudioManager.PlaySound("ButtonClick"));
    }

    private void OnDisable()
    {
        _applyButton.onClick.RemoveListener(OnRebornPlayer);
        _refuseButton.onClick.RemoveListener(OnClickRefuseButton);

        _applyButton.onClick.RemoveListener(() => AudioManager.PlaySound("ButtonClick"));
        _refuseButton.onClick.RemoveListener(() => AudioManager.PlaySound("ButtonClick"));
    }

    private void Awake()
    {
        _resumePanel.Deactivate();
    }

    private void OnClickRefuseButton() => _resultPanel.OpenMenu();

    private void OpenResumePanel()
    {
        _resumePanel.DOFade(1f, 1f)
           .SetEase(Ease.Linear, 1f)
           .SetUpdate(true);

        _resumePanel.Activate();

        PlayAnimationText(_description);
    }

    private void PlayAnimationText(TMP_Text text)
    {
        text.maxVisibleCharacters = 0;

        DOTween.To(() => text.maxVisibleCharacters,
                   x => text.maxVisibleCharacters = x,
                   text.text.Length,
                   0.5f)
               .SetEase(Ease.Linear);
    }

    private void OnRebornPlayer()
    {
        YandexManager.Instance.ShowRewardAdv(RewardId);
        _resumePanel.Deactivate();
    }

    public void OpenDeathPanel()
    {
        OpenResumePanel();
    }
}
