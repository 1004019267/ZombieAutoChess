using UnityEngine;
using System.Collections;
using UnityEngine.UI;
///UISource File Create Data:3/6/2019 6:02:49 PM
public delegate void MessageBoxShowYes();
public delegate void MessageBoxShowNo();
public partial class CloseWndUIController : BaseUI
{

    Button btnYes;
    Button btnNo;
    public MessageBoxShowYes messageBoxShowYes;
    public MessageBoxShowNo messageBoxShowNo;   
    public string tip;
    public static void ShowOrHide(bool flag, Transform parent = null)
    {

        BaseUI.ShowOrHide(CloseWndUIController.ui_name, flag, parent);

    }

    public static CloseWndUIController getMe()
    {

        return uiMgr.getMe().FindUI(CloseWndUIController.ui_name).GetComponent<CloseWndUIController>();

    }

    public override bool onEnter()
    {
        btnYes = this.MessageBox.transform.Find("btnYes").GetComponent<Button>();
        btnNo = this.MessageBox.transform.Find("btnNo").GetComponent<Button>();
        m_key.registerCallBack(btnYes, btnYesClick);
        m_key.registerCallBack(btnNo, btnNoClick);
        btnYes.image.sprite = btnYes.spriteState.highlightedSprite;
        return true;
    }

    public override bool onExit()
    {

        return true;

    }

    public override void Notify()
    {
        SetFirstBtnOnNode();
    }
    void btnYesClick()
    {
        if (messageBoxShowYes != null)
        {
            messageBoxShowYes();

            messageBoxShowYes = null;
        }
        onBack();
    }
    void btnNoClick()
    {
        if (messageBoxShowNo != null)
        {
            messageBoxShowNo();

            messageBoxShowNo = null;
        }
        SetFirstBtnOnNode();
        gameObject.SetActive(false);        
    }

    public override bool onBack()
    {
        btnNoClick();        
        return true;
    }

    /// <summary>
    /// 设置焦点为第一个btn 重置其他btn
    /// </summary>
    public void SetFirstBtnOnNode()
    {
        m_key.setCurKey(btnYes, false);
        btnYes.image.sprite = btnYes.spriteState.highlightedSprite;
        btnNo.image.sprite = btnNo.spriteState.pressedSprite;
    }
}
