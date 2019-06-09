using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//无用
public class Animation_Rotate : MonoBehaviour {
    public Transform target;
    public float time = 0.5f;
    public Vector3 endValue = Vector3.one;
    public Vector3 startValue = Vector3.zero;
    public int LoopCount = 1;
    public float delayTime = 0;
    public Ease type;
    public bool isplayStart = true;
    Transform _transform;
	// Use this for initialization
	void Awake () {
        _transform = target == null ? transform : target;
        _transform.Rotate(startValue);
	}
	
	void Start()
    {
        if (!isplayStart)
            return;
        
    }

    public void Active()
    {
        Tween t = _transform.DORotate(endValue,time);
        t.SetLoops(LoopCount);
        t.SetEase(type);
        t.SetDelay(delayTime);
        t.SetUpdate(true);
    }
}
