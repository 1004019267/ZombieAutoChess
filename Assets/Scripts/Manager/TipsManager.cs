using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
/// <summary>
///  
/// 标准模板有个tip 挑一挑也有
/// </summary>
class TipsManager : Singleton<TipsManager>
{
    Transform parent;
    Transform tips;
    float posY = 200;
    List<Transform> existedTips = new List<Transform>();
    public void Init(Transform canvans)
    {
        parent = canvans.Find("TipsParent");
        tips = canvans.Find("TipsParent/Tips");
    }
    /// <summary>
    /// 系统提示
    /// </summary>
    /// <param name="sentence"></param>
    public void TipsShow(string sentence)
    {
        parent.SetAsLastSibling();
        var go = GameObject.Instantiate(tips);
        existedTips.Add(go);
        go.SetParent(parent, false);
        var text = go.GetComponent<Text>();
        text.text = sentence;
        go.gameObject.SetActive(true);

        TimerMgr.getMe().CreateTimer(TimeCallBack, 3);
        text.DOFade(0, 3);
        go.DOMove(Vector3.up * posY, 3).SetEase(Ease.InOutCubic).SetRelative();

    }

    private void TimeCallBack()
    {     
        Destroy(existedTips[0].gameObject);
        existedTips.Remove(existedTips[0]);
    }
}

