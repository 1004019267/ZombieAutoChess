using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//无用
public class Animation_Color : MonoBehaviour {

    public Image target;
    public float time = 0.5f;
    public Color endValue = Color.black;
    public Color startValue = Color.white;
    public int LoopCount = 1;
    public float delayTime = 0;
    public Ease type;
    public bool isplayStart = true;
    Image _image;
    // Use this for initialization
    void Awake()
    {
        _image = target == null ? transform.GetComponent<Image>() : target;
        _image.color = startValue;
    }
    private void Start()
    {
        if (!isplayStart)
            return;
        Active();
    }

    /// <summary>
    /// 激活使用
    /// </summary>
    public void Active()
    {
        Tween t = _image.DOColor(endValue, time);
        t.SetLoops(LoopCount);
        t.SetEase(type);
        t.SetDelay(delayTime);
        t.SetUpdate(true);
    }
}
