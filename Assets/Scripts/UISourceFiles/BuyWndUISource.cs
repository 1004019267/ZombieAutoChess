using UnityEngine;
using System.Collections;

///UISource File Create Data:3/7/2019 1:51:58 PM
public partial class BuyWndUIController:BaseUI{

	public static readonly string ui_name = "BuyWnd";

	public GameObject card;
	public Vector3 UIOriginalPositioncard;

	public GameObject choseCard;
	public Vector3 UIOriginalPositionchoseCard;

	void Awake() {
		card = transform.Find("card").gameObject;
		UIOriginalPositioncard = card.transform.localPosition;

		choseCard = transform.Find("choseCard").gameObject;
		UIOriginalPositionchoseCard = choseCard.transform.localPosition;

	}

}
