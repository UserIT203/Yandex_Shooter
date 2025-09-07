using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Zenject;

[RequireComponent(typeof(Animator))]
public class BossUI : MonoBehaviour
{
    [Inject] private GameTimeManager _timeManager;

    [SerializeField] private Image _bossImage;
    [SerializeField] private TMP_Text _bossText;

    private Animator _animator;

    public event Action onEndAnimation;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Initialized(Boss boss)
    {
        _bossImage.sprite = boss.BossIcon;
        _bossText.text = boss.BossName;
        _timeManager.Pause();

        PlayAnimation();
    }

    private void PlayAnimation()
    {
        _animator.SetTrigger("Open");
    }

    public void StopAnimation()
    {
        Debug.Log("Boss Panel Close");
        _timeManager.Resume();
        onEndAnimation?.Invoke();
    }
}
