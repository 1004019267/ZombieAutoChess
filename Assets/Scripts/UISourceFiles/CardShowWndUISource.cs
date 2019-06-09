using UnityEngine;
using System.Collections;

///UISource File Create Data:4/3/2019 2:49:12 PM
public partial class CardShowWndUIController:BaseUI{

	public static readonly string ui_name = "CardShowWnd";

	public GameObject card;
	public Vector3 UIOriginalPositioncard;

	void Awake() {
		card = transform.Find("card").gameObject;
		UIOriginalPositioncard = card.transform.localPosition;

	}

}
