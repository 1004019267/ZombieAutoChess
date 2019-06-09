
using DG.Tweening;
using UnityEngine;

//ÎÞÓÃ
public class ActionStartGame : BaseAction
{
    protected override void onStart()
    {
        //DOTween.Sequence().AppendCallback(() =>
        //{
        //    PanelMainUIController.Instance.AddUI(PanelMainUIController.UILayer.L_Buttom, ui_startUIController.name);
        //}).SetDelay(0.2f);
        Debug.Log("zzzzzzzzzz");

    }




    internal override void onCopyTo(BaseAction cloneto)
    {
        
    }
}