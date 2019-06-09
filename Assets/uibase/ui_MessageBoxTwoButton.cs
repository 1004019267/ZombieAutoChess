
using System.Collections;
using System.Collections.Generic;
using ty;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 开始ui

public delegate int ui_MessageBoxTwoButton_Delegate(  );
public class ui_MessageBoxTwoButton : BaseUI
{
    static string _path = "ui_MessageBoxTwoButton";
    private static ui_MessageBoxTwoButton g_itemData;
    public static ui_MessageBoxTwoButton getMe()
    {
        return ui_MessageBoxTwoButton.g_itemData;
    }

    public static void ShowOrHide(bool flag)
    {
        BaseUI.ShowOrHide(ui_MessageBoxTwoButton._path,flag);
    }

    public Button queding_Btn;
    public Button quxiao_Btn;
    public Text   _Text;

 

    ui_MessageBoxTwoButton_Delegate _ui_MessageBoxTwoButton_Delegate_ok = null;
    ui_MessageBoxTwoButton_Delegate _ui_MessageBoxTwoButton_Delegate_cansel=null;
    public override void Start()
    {
        base.Start();
        m_key.registerCallBack(queding_Btn, quedingClick);
        m_key.registerCallBack(quxiao_Btn, quxiaoClick);
    }

 

    void quedingClick()
    {
       if(_ui_MessageBoxTwoButton_Delegate_ok!=null)
        {
            _ui_MessageBoxTwoButton_Delegate_ok();
        }
    }
    void quxiaoClick()
    {
        if (_ui_MessageBoxTwoButton_Delegate_cansel != null)
        {
            _ui_MessageBoxTwoButton_Delegate_cansel();
        }
        ui_MessageBoxTwoButton.ShowOrHide(false);
    }
 

}
