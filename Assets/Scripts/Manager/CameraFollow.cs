using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 无用
public class CameraFollow : MonoBehaviour {
    // Use this for initialization

    static public Rect m_rect;

    void Start ()
    {  
        float x = 5 * (float)Screen.width / (float)Screen.height;
        m_rect = new Rect(-x, -5 , 2*x , 10);

    }

    public static bool isRuning = true;
    private void LateUpdate()
    {
        if (!isRuning)
        {
            return;
        }
     
    }
}