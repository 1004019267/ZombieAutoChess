using UnityEngine;
using System.Collections;

public partial class SkillConfig:GameConfigDataBase
{

	public int id;
	public string profile;
	public int probability;
	public int skillL1;
	public int skillL2;
	public int skillL3;
	public float Percentage1L1;
	public float Percentage1L2;
	public float Percentage1L3;
	public float Percentage2;
	public float cd;
	public float duration;
	 protected override string getFilePath()
	{
		return "SkillConfig";
	}
}
