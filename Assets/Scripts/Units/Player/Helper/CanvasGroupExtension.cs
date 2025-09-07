using UnityEngine;

public static class CanvasGroupExtension
{
    public static void Activate(this CanvasGroup group)
    {
        group.alpha = 1.0f;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    public static void Deactivate(this CanvasGroup group) 
    {
        group.alpha = 0f;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
}
