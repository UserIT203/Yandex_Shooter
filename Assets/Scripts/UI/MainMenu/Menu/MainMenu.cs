using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MenuBaseUI
{
    protected override void CloseMenuAnimation()
    {
        OnCloseMenu();
    }

    protected override void OpenMenuAnimation()
    {
        OnOpenMenu();
    }
}
