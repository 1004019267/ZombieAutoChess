using UnityEngine;
using System.Collections;

///UISource File Create Data:3/13/2019 2:22:00 PM
public partial class MenuWndUIController:BaseUI{

	public static readonly string ui_name = "MenuWnd";

	public GameObject UpLevel;
	public Vector3 UIOriginalPositionUpLevel;

	public GameObject Sold;
	public Vector3 UIOriginalPositionSold;

	public GameObject Recover;
	public Vector3 UIOriginalPositionRecycle;

	public GameObject Move;
	public Vector3 UIOriginalPositionMove;

	void Awake() {
		UpLevel = transform.Find("UpLevel").gameObject;
		UIOriginalPositionUpLevel = UpLevel.transform.localPosition;

		Sold = transform.Find("Sold").gameObject;
		UIOriginalPositionSold = Sold.transform.localPosition;

        Recover = transform.Find("Recover").gameObject;
		UIOriginalPositionRecycle = Recover.transform.localPosition;

		Move = transform.Find("Move").gameObject;
		UIOriginalPositionMove = Move.transform.localPosition;

	}

}
