using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//无用
public class Animation_Alpa : MonoBehaviour
{

    public Image target;
    public float time = 0.5f;
    public float endValue = 1;
    public float startValue = 0;
    public int LoopCount = 1;
    public float delayTime = 0;
    public Ease type;
    public bool isplayStart = true;
    Image _image;
    // Use this for initialization
    void Awake()
    {
        _image = target == null ? transform.GetComponent<Image>() : target;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, startValue);
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
        Tween t = _image.DOFade(endValue, time);
        t.SetLoops(LoopCount);
        t.SetEase(type);
        t.SetDelay(delayTime);
        t.SetUpdate(true);
    }  
}
