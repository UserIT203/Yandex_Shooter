using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(Animator))]
public class BossUI : MonoBehaviour
{
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

        PlayAnimation();
    }

    private void PlayAnimation()
    {
        _animator.SetTrigger("Open");
    }

    public void StopAnimation()
    {
        Debug.Log("Boss Panel Close");
        onEndAnimation?.Invoke();
    }
}
