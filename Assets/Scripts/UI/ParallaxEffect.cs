using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float _speed;
    private RawImage _rawImage;

    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();
    }

    private void LateUpdate()
    {
        PlayParallax();
    }

    private void PlayParallax()
    {
        Rect newRect = new Rect(_rawImage.uvRect);
        newRect.x += _speed * Time.deltaTime;

        if (newRect.x > 1000)
            newRect.x = 0;

        _rawImage.uvRect = newRect;
    }
}
