using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using ty;
using UnityEngine;
using UnityEngine.UI;
public  class BaseUI : MonoBehaviour
{
    public   string ui_name = "BaseUI";
    protected BasekeyState m_key;// = new BasekeyState();
    public BasekeyState GetBasekeyState()
    {
        return m_key;
    }

    /// <summary>
    /// ui层级深度不可修改
    /// </summary>
    [SerializeField]
    private int m_depth = 0;

    public int Depth {
        get
        {
            m_depth = transform.GetSiblingIndex();
            return m_depth;
        }
        set
        {
            transform.SetSiblingIndex(value);
            m_depth = value;
        }
    }

    public BaseUI()
    {
    
    }

    //获得当前界面焦点
    public Button getCurButton()
    {
       return m_key.m_key.node;
    }

    public virtual void Start()
    {
        m_key = new BasekeyState();
        m_key.init(this);
        onEnter();
    }

    // Use this for initialization
    void Awake()
    {
        m_depth = transform.GetSiblingIndex();
 
    }

    public static void ShowOrHide(string ui_name, bool flag, Transform parent = null)
    {


        if (parent == null)
        {
            parent = uiMgr.getMe().Canvas;
        }
        if (flag)
        {
            BaseUI _instance = uiMgr.getMe().AddUI(ui_name, parent).GetComponent<BaseUI>();
            _instance.ui_name = ui_name;
        }
        else
        {
            BaseUI ui_obj = uiMgr.getMe().FindUI(ui_name);
            if (ui_obj)
            {
                ui_obj.onExit();
                uiMgr.getMe().CloseUI(ui_name);
                Destroy(ui_obj.gameObject);
            }

        }
    }





    public virtual bool Left()
    {
        return true;
    }

    public virtual bool Right()
    {
        return true;
    }

    public virtual bool Up()
    {
        return true;
    }


    
    public virtual bool Down()
    {
        return true;
    }

    public virtual bool Ok()
    {
        return true;
    }


    /// <summary>
    /// 界面初始化
    /// </summary>
    /// <returns></returns>
    public virtual bool onEnter() { return true; }
    /// <summary>
    /// 界面离开
    /// </summary>
    /// <returns></returns>
    public virtual bool onExit() { return true; }

    /// <summary>
    /// 界面刷新
    /// </summary>
    public virtual void Notify() {   }
    /// <summary>
    /// 返回上一级界面
    /// </summary>
    /// <returns></returns>
    public  virtual bool onBack()
    {
        uiMgr.getMe().closeTopUI();//删除自己
        return true;
    }

    public virtual bool onBack(Action action)
    {
        uiMgr.getMe().closeTopUI();//删除自己
        return true;
    }

}
