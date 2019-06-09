using DG.Tweening;
using UnityEngine;


//无用
public class Animation_Scale : MonoBehaviour
{
        
    public Transform target;
    public float time = 0.5f;
    public Vector3 endValue = Vector3.one;
    public Vector3 startValue = Vector3.zero;
    public int LoopCount = 1;
    public float delayTime = 0;
    public Ease type;
    public bool isplayStart = true;
    private Transform _transform;
    
    private void Awake()
    {
        _transform = target == null ? transform : target;
        _transform.localScale = startValue;
    }

    private void Start()
    {
        if(!isplayStart)
            return;
        Active();
    }

    /// <summary>
    /// 激活使用
    /// </summary>
    public void Active()
    {
        Tween t = _transform.DOScale(endValue, time);
        t.SetLoops(LoopCount);
        t.SetEase(type);
        t.SetDelay(delayTime);
        t.SetUpdate(true);
    }
    
    
    
    
}