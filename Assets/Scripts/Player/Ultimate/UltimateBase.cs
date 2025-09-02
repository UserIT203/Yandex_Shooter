using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class UltimateBase : ScriptableObject, IUltimate
{
    [field: SerializeField] public Image UltimateIcon { get; protected set; }
    [field: SerializeField] public string UltimateName { get; protected set; }

    [field: SerializeField] public float Cooldown { get; protected set; }

    [field: SerializeField] public float UltimateDuration { get; protected set; }

    public bool IsStarted { get; protected set; }

    protected Player _player;
    protected Boss _boss;

    private float _timer;

    public event Action onUltimateEnd;

    public virtual void Initialized(Player player)
    {
        _player = player;
        IsStarted = false;
    }

    public void TryUse()
    {
        if(CanUse())
        {
            Execute();
        }
    }

    public bool CanUse() => _timer < 0 && IsStarted == false;

    public void SetBoss(Boss boss) => _boss = boss;

    public void Update()
    {
        _timer -= Time.deltaTime;
    }

    protected virtual void Execute()
    {
        Debug.Log("Use Ultimate: " + UltimateName);
        _timer = Cooldown;
        _player.StartCoroutine(ExecutionProcess());
    }

    protected virtual void CleanUp() 
    {
        onUltimateEnd?.Invoke();
    }

    private IEnumerator ExecutionProcess()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(UltimateDuration);
        IsStarted = true;

        yield return waitingTime;

        IsStarted = false;
        CleanUp();
        Debug.Log("END Ultimate: " + UltimateName);
    }
}
