
using System.Collections;
using System.Collections.Generic;
using ty;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 开始ui

public delegate int ui_MessageBoxTestConfig_func();
public class ui_MessageBoxTestConfig : BaseUIConfig
{
    static string _path = "ui_MessageBoxTestConfig";
    private static ui_MessageBoxTestConfig g_itemData;
    public static ui_MessageBoxTestConfig getMe()
    {
        return ui_MessageBoxTestConfig.g_itemData;
    }

    public static void ShowOrHide(bool flag)
    {
        BaseUI.ShowOrHide(ui_MessageBoxTestConfig._path, flag);
    }

    public Button queding_Btn;
    public Button quxiao_Btn;
    public Text _Text;



    ui_MessageBoxTestConfig_func _ui_MessageBoxTwoButton_Delegate_ok = null;
    ui_MessageBoxTestConfig_func _ui_MessageBoxTwoButton_Delegate_cansel = null;
    public override void Start()
    {
        base.Start();
        m_key.registerCallBack(queding_Btn, quedingClick);
        m_key.registerCallBack(quxiao_Btn, quxiaoClick);
    }



    void quedingClick()
    {
        if (_ui_MessageBoxTwoButton_Delegate_ok != null)
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
