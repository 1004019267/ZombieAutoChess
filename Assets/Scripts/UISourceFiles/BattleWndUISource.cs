using UnityEngine;
using System.Collections;

///UISource File Create Data:3/6/2019 5:11:00 PM
public partial class BattleWndUIController:BaseUI{

	public static readonly string ui_name = "BattleWnd";

	//public GameObject BG;
	//public Vector3 UIOriginalPositionBG;

	public GameObject Ground;
	public Vector3 UIOriginalPositionGround;

	//public GameObject btnExit;
	//public Vector3 UIOriginalPositionbtnExit;

	public GameObject UpData;
	public Vector3 UIOriginalPositionUpData;

	public GameObject RightData;
	public Vector3 UIOriginalPositionDownData;

	void Awake() {
		//BG = transform.Find("BG").gameObject;
		//UIOriginalPositionBG = BG.transform.localPosition;

		Ground = transform.Find("Ground").gameObject;
		UIOriginalPositionGround = Ground.transform.localPosition;

		//btnExit = transform.Find("btnExit").gameObject;
		//UIOriginalPositionbtnExit = btnExit.transform.localPosition;

		UpData = transform.Find("UpData").gameObject;
		UIOriginalPositionUpData = UpData.transform.localPosition;

		RightData = transform.Find("RightData").gameObject;
		UIOriginalPositionDownData = RightData.transform.localPosition;

	}

}
