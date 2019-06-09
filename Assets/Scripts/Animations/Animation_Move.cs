using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//无用
public class Animation_Move : MonoBehaviour
{
    public Transform target;
    public float time = 0.5f;
    public Vector3 endValue = Vector3.one;
    public Vector3 startValue = Vector3.zero;
    public int LoopCount = 1;
    public float delayTime = 0;
    public Ease type;
    public bool isplayStart = true;
    Transform _transform;

    void Awake()
    {
        _transform = target == null ? transform : target;
        _transform.position = startValue;
    }
    // Use this for initialization
    void Start()
    {
        if (!isplayStart)
            return;
        Active();

    }
    /// <summary>
    /// 激活使用
    /// </summary>
    void Active()
    {
        Tween t = transform.DOMove(endValue, time);
        t.SetLoops(LoopCount);
        t.SetEase(type);
        t.SetDelay(delayTime);
        t.SetUpdate(true);
    }
}
