using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

[CreateAssetMenu(fileName = "Fugue", menuName = "Boss Ultimate/Fugue")]
public class FugueUltimate : UltimateBase, IBulletObserver
{
    [Header("Fugue Settings")]
    [SerializeField] private float _freezeTime;
    [SerializeField] private float _delayToSpawn;
    [SerializeField] private FreezeBall _fuguePrefab;

    private Coroutine _coroutine;
    private List<FreezeBall> _fugueObject = new List<FreezeBall>();

    protected override void Execute()
    {
        base.Execute();
        _fugueObject.Clear();

        _coroutine = _player.StartCoroutine(CreateFugue());
    }

    protected override void CleanUp()
    {
        base.CleanUp();

        foreach (FreezeBall fugue in _fugueObject)
        {
            if(fugue != null) 
                Destroy(fugue.gameObject);
        }

        _player.StopCoroutine(_coroutine);
        _fugueObject.Clear();
    }

    private IEnumerator CreateFugue()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(_delayToSpawn);

        while (true)
        {
            FreezeBall fugue = Instantiate(_fuguePrefab);
            fugue.SetTraget(_player, _freezeTime, this);
            fugue.transform.position = _boss.transform.position;

            _fugueObject.Add(fugue);

            yield return waitingTime;
        }
    }

    public void HandleHit()
    {
        CleanUp();
    }
}
