using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//无用

public class actionplay : BaseAction {

    // Use this for initialization
    protected override void onStart()
    {
        //DOTween.Sequence().AppendCallback(() =>
        //{
        //    PanelMainUIController.Instance.AddUI(PanelMainUIController.UILayer.L_Buttom, ui_startUIController.name);
        //}).SetDelay(0.2f);
        Debug.Log("sssssssssssssssss");

    }
    protected override void onOver()
    {
        Debug.Log("over");
    }

    // Update is called once per frame
    internal override void onCopyTo(BaseAction cloneto)
    {

    }
}
