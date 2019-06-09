using UnityEngine;
using System.Collections;

///UISource File Create Data:3/6/2019 6:02:49 PM
public partial class CloseWndUIController:BaseUI{

	public static readonly string ui_name = "CloseWnd";

	public GameObject MessageBox;
	public Vector3 UIOriginalPositionMessageBox;

	void Awake() {
		MessageBox = transform.Find("MessageBox").gameObject;
		UIOriginalPositionMessageBox = MessageBox.transform.localPosition;

	}

}
