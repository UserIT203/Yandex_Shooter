using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class UltimateBase : ScriptableObject, IUltimate
{
    [field: SerializeField] public string UltimateName { get; protected set; }

    [field: SerializeField] public float Cooldown { get; protected set; }

    [field: SerializeField] public float UltimateDuration { get; protected set; }

    protected bool _isStarted = false;
    protected Player _player;

    private float _timer;

    protected virtual void Execute()
    {
        Debug.Log("Use Ultimate: " + UltimateName);
        _timer = Cooldown;
        _player.StartCoroutine(ExecutionProcess());
    }

    public virtual void Initialized(Player player)
    {
        _player = player;
        _isStarted = false;
    }

    public void TryUse()
    {
        if(_timer < 0 && _isStarted == false)
        {
            Execute();
        }
    }

    public void Update()
    {
        _timer -= Time.deltaTime;
    }

    protected virtual void CleanUp() { }

    private IEnumerator ExecutionProcess()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(UltimateDuration);
        _isStarted = true;

        yield return waitingTime;

        _isStarted = false;
        CleanUp();
    }
}
