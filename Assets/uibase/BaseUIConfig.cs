using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using ty;
using UnityEngine;
using UnityEngine.UI;
public  class BaseUIConfig : BaseUI
{
    public override void Start()
    {
        m_key = new BasekeyState_config();
        m_key.init(this);
        onEnter();
    }


}
