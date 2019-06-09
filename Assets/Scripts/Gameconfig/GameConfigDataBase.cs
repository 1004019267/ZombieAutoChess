using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class GameConfigDataBase : MonoBehaviour {

	protected virtual string getFilePath()
	{
		return "";
	}
	static Dictionary<Type,Dictionary<string,GameConfigDataBase>> dataDic = new Dictionary<Type, Dictionary<string, GameConfigDataBase>> ();

	public static T GetConfigData<T>(string key)where T:GameConfigDataBase
	{
		Type setT = typeof(T);
		if (!dataDic.ContainsKey(setT))
		{
			ReadConfigData<T>();
		}

		Dictionary<string, GameConfigDataBase> objdic = dataDic[setT];
		if (!objdic.ContainsKey(key))
		{
			throw new Exception("no this config");
		}
		return (T) objdic[key];
	}
	public static List<T> GetConfigDatas<T>() where T : GameConfigDataBase
	{
		Type setT = typeof(T);
		List<T> _list = new List<T>();
		if (!dataDic.ContainsKey(setT))
		{
			ReadConfigData<T>();
		}
		Dictionary<string, GameConfigDataBase> objdic = dataDic[setT];
		foreach (KeyValuePair<string,GameConfigDataBase> kvp in objdic)
		{
			_list.Add((T)kvp.Value);
		}
		return _list;
	}
		
	
	


	static void ReadConfigData<T>() where T : GameConfigDataBase
	{
		T obj = Activator.CreateInstance<T> ();
		string fileName = obj.getFilePath ();
		string getString = GameResourcesManager.GetGameConfigText (fileName);
		CsvReaderByString csr = new CsvReaderByString (getString);
		Dictionary<string,GameConfigDataBase> objDic = new Dictionary<string, GameConfigDataBase>();
		
		FieldInfo[] _fileInfos = new FieldInfo[csr.ColCount];
		for (int i = 1; i < csr.ColCount + 1; i++)
		{
			_fileInfos[i - 1] = typeof(T).GetField(csr[1, i]);
		}
		for (int rowNum = 3; rowNum < csr.RowCount+1; rowNum++)
		{
			T configObj = Activator.CreateInstance<T>();
			for (int colNum = 0; colNum < _fileInfos.Length; colNum++)
			{
				string fieldValue = csr [rowNum, colNum + 1];
				object setvalue = new object();
				switch (_fileInfos[colNum].FieldType.ToString())
				{
					case "System.Int32":
						setvalue = int.Parse (fieldValue);
						break;
					case "System.Int64":
						setvalue = long.Parse (fieldValue);
						break;
					case "System.String":
						setvalue = fieldValue;
						break;
					case "System.Single":
						setvalue = float.Parse(fieldValue);
						break;
					default:
						Debug.Log ("error data type");
						break;
				}
				_fileInfos[colNum].SetValue(configObj,setvalue);
				if (_fileInfos[colNum].Name == "key" || _fileInfos[colNum].Name == "id")
				{
					objDic.Add(setvalue.ToString(),configObj);
				}
			}
		}
		dataDic.Add(typeof(T),objDic);
	}
	
}
