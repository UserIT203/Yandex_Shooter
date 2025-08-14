using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CapabilitiesManager: MonoBehaviour
{
    [SerializeField] private CapabilitieUI _capabilityUI;
    [SerializeField] private int _maxCapabilite;

    private PlayerCapabilities _player;
    private Queue<int> _indexQueue = new Queue<int>();
    private ICapabilitie[] _currentUpgradesCapabilitie;

    [Inject] 
    public void Construct(Player player)
    {
        _player = player.GetComponent<PlayerCapabilities>();
    }

    public void ShowUpgradeUI() 
    {
        _currentUpgradesCapabilitie = new ICapabilitie[_maxCapabilite];
        CapabilitiesType[] types = GetUniqueCapabilitie();

        for (int i = 0; i < _maxCapabilite; i++)
        {
            _currentUpgradesCapabilitie[i] = _player.GetCapabilite(types[i]);
        }

        _capabilityUI.Show(_currentUpgradesCapabilitie);
    }

    private CapabilitiesType[] GetUniqueCapabilitie()
    {
        var values = Enum.GetValues(typeof(CapabilitiesType)).Cast<CapabilitiesType>().OrderBy(x => UnityEngine.Random.value).ToArray();
        return values.Take(_maxCapabilite).ToArray();
    }
}