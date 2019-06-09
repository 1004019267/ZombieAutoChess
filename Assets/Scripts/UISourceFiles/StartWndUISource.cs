using UnityEngine;
using System.Collections;

///UISource File Create Data:3/6/2019 2:56:54 PM
public partial class StartWndUIController:BaseUI{

	public static readonly string ui_name = "StartWnd";

	public GameObject BG;
	public Vector3 UIOriginalPositionBG;

	public GameObject btnSingle;
	public Vector3 UIOriginalPositionbtnSingle;

	public GameObject btnDouble;
	public Vector3 UIOriginalPositionbtnDouble;

	public GameObject btnExit;
	public Vector3 UIOriginalPositionbtnExit;

	public GameObject textExit;
	public Vector3 UIOriginalPositiontextExit;

	public GameObject MainInterface;
	public Vector3 UIOriginalPositionMainInterface;

	void Awake() {
		BG = transform.Find("BG").gameObject;
		UIOriginalPositionBG = BG.transform.localPosition;

		btnSingle = transform.Find("btnSingle").gameObject;
		UIOriginalPositionbtnSingle = btnSingle.transform.localPosition;

		btnDouble = transform.Find("btnDouble").gameObject;
		UIOriginalPositionbtnDouble = btnDouble.transform.localPosition;

		btnExit = transform.Find("btnExit").gameObject;
		UIOriginalPositionbtnExit = btnExit.transform.localPosition;

		textExit = transform.Find("textExit").gameObject;
		UIOriginalPositiontextExit = textExit.transform.localPosition;

		MainInterface = transform.Find("MainInterface").gameObject;
		UIOriginalPositionMainInterface = MainInterface.transform.localPosition;

	}

}
