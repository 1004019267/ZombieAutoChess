using UnityEngine;
using System.Collections;


//������Ϣ������Ҫ���ɹ����
//һ���ǿ�����Ϣ
// 

public partial class CardConfig:GameConfigDataBase
{

	public int id;
	public string cardName;
	public int gold;
	public int skill;
	public int atk;
	public float hp;
	public float atkSpeed;
	public int atkRange;
	public int level;
	public int hpStar;
	public int hpHalfStar;
	public int atkStar;
	public int atkHalfStar;
	 protected override string getFilePath()
	{
		return "CardConfig";
	}
}
