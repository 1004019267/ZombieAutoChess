using UnityEngine;
using System.Collections;

public partial class test:GameConfigDataBase
{

	public int id;
	public string name;
	public int age;
	public float level;
	 protected override string getFilePath()
	{
		return "test";
	}
}
