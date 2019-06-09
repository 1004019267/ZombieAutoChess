using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//  数字跳动
public class uiTip : BaseUIUIAnimaiton
{
    static string _path = "ui_tip";
    public static void ShowOrHide(string code)
    {
        //GameObject obj = Resources.Load<GameObject>("Prefabs/" + _path);
        //GameObject clone_UI = Instantiate(obj, uiMgr.getCanvas());

        GameObject clone_UI = GameResourcesManager.ClonePrefab(GameResourcesManager.uiPath + _path,  uiMgr.getMe().Canvas );
        uiTip _UI_NumJump = clone_UI.GetComponent<uiTip>();
        _UI_NumJump.text.text = "" + code;
    }



}
