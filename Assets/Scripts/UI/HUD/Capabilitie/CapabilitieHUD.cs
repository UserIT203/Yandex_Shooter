using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CapabilitieHUD : MonoBehaviour
{
    [SerializeField] private List<PlayerCapabilitieUI> _playerCapabilitieContainers;

    private PlayerCapabilities _playerCapabilitie;

    [Inject]
    public void Construct(Player player)
    {
        _playerCapabilitie = player.GetComponent<PlayerCapabilities>();
    }

    private void Start()
    {
        InitializedCapabilitieContainers();
    }

    private void InitializedCapabilitieContainers()
    {
        int currentContainer = Enum.GetValues(typeof(CapabilitiesType)).Length;
        Debug.Log(currentContainer);

        for (int i = 0; i < currentContainer; i++) 
        {
            _playerCapabilitieContainers[i].Initialized(
                _playerCapabilitie.GetCapabilite((CapabilitiesType)i));
        }
    }
}
