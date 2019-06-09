using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://blog.csdn.net/xhyzdai/article/details/46799297
//定时销毁ui DoTween实现飘字的效果
public class BaseUIUIAnimaiton : MonoBehaviour
{
    private void Start()
    {
        startAnimation();
    }
    public Text text;
    public   float m_timedelete = 0.8f;//销毁的时间

    // Update is called once per frame
    protected virtual void Update()
    {
        m_timedelete -= Time.deltaTime;
        if( this.m_timedelete < 0 )
        {
            this.m_timedelete = 1;
            Destroy(this.gameObject);
            return ;
        }
    }
    public void startAnimation()
    {

        //Text text = this.gameObject.GetComponent<Text>();
        FlyTo(text);
    }
    public static void FlyTo(Graphic graphic)
    {
        RectTransform rt = graphic.rectTransform;
        Color c = graphic.color;
        c.a = 1;
        graphic.color = c;
        Sequence mySequence = DOTween.Sequence();
        Tweener move1 = rt.DOMoveY(rt.position.y + 50, 0.5f);
        Tweener move2 = rt.DOMoveY(rt.position.y + 100, 0.5f);
        Tweener alpha1 = graphic.DOColor(new Color(c.r, c.g, c.b, 1), 0.5f);
        Tweener alpha2 = graphic.DOColor(new Color(c.r, c.g, c.b, 0), 0.5f);
        mySequence.Append(move1);
        mySequence.Join(alpha1);
        mySequence.AppendInterval(1);
        mySequence.Append(move2);
        mySequence.Join(alpha2);

    }









}
