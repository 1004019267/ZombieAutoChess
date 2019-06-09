using UnityEngine;
using System.Collections;

///UISource File Create Data:3/8/2019 12:52:25 PM
public partial class BagWndUIController:BaseUI{
    //public static string ui_name = "BagWnd";

    public GameObject myCard;
	public Vector3 UIOriginalPositionmyCard;

	void Awake() {
		myCard = transform.Find("myCard").gameObject;
		UIOriginalPositionmyCard = myCard.transform.localPosition;

	}

}
