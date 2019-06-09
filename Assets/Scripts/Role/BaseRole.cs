using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRole : MonoBehaviour {

    //角色的状态列表
    protected virtual void Start () {
		
	}

    protected abstract void Loop();
	// Update is called once per frame
	 void Update () {
		Loop();
	}
}
