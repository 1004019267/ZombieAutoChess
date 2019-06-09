using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
public class UISourceCreatorWindow : EditorWindow {
	[MenuItem("KLEditor/Window/UI Source Creator Window ")]
	static void init()
	{
		UISourceCreatorWindow _window = EditorWindow.GetWindow<UISourceCreatorWindow>();
	}
	
	GameObject selectGameObject;//选择对象
	List<GameObject> UIPrefabList;//预制体列表

	private void OnGUI()
	{
		GUILayout.Label("选择需要生成界面源文件的对象");
		if (GUILayout.Button("生成界面源文件"))
		{
			if (selectGameObject != null)
			{
				CreatUISourceFile();
			}
		}
		selectGameObject = GetSelectedPrefab();
		if (selectGameObject != null)
		{
			GUILayout.Label(selectGameObject.name);
		}
	}


	private void OnSelectionChange()
	{
		Repaint();
	}

	void CreatUISourceFile()
	{
		string GameObjectName = selectGameObject.name;
		string fileName = GameObjectName + "UISource";
		string classname = GameObjectName + "UIController";
		
		StreamWriter sw = new StreamWriter (Application.dataPath + "/Scripts/UISourceFiles/"+fileName+".cs");
		sw.WriteLine("using UnityEngine;\nusing System.Collections;\n");
		sw.WriteLine("///UISource File Create Data:" + System.DateTime.Now.ToString());
		sw.WriteLine("public partial class " + classname+":BaseUI{\n");
		sw.WriteLine("\tpublic static readonly string ui_name = \""+GameObjectName+"\";\n");
		
		
		foreach (Transform ta in selectGameObject.transform)
		{
			string childname = ta.gameObject.name;
			sw.WriteLine("\tpublic GameObject " + childname + ";");
			sw.WriteLine("\tpublic Vector3 UIOriginalPosition" + childname + ";\n");
		}
		sw.WriteLine("\tvoid Awake() {");
		foreach (Transform ta in selectGameObject.transform)
		{
			string childName = ta.gameObject.name;
			
			sw.WriteLine("\t\t"+childName+" = transform.Find(\""+childName+"\").gameObject;");
			sw.WriteLine("\t\tUIOriginalPosition"+childName+" = "+childName+".transform.localPosition;\n");
		}
		sw.WriteLine ("\t" + "}" + "\n");
		sw.WriteLine ("}");
		sw.Flush ();
		sw.Close ();
		
		StreamWriter sw_1 = new StreamWriter (Application.dataPath + "/Scripts/UIController/"+classname+".cs");
		sw_1.WriteLine("using UnityEngine;\nusing System.Collections;\n");
		sw_1.WriteLine("///UISource File Create Data:" + System.DateTime.Now.ToString());
		sw_1.WriteLine("public partial class " + classname+":BaseUI{\n");
		sw_1.WriteLine("\tpublic static void ShowOrHide(bool flag,Transform parent = null,object data = null){\n");
		sw_1.WriteLine("\t\tBaseUI.ShowOrHide("+classname+".ui_name,flag,parent,data);\n");
		sw_1.WriteLine("\t}\n");
		
		sw_1.WriteLine("\tpublic static "+classname+" getMe(){\n");
		sw_1.WriteLine("\t\treturn uiMgr.getMe().FindUI("+classname+".ui_name).GetComponent<"+classname+">();\n");
		sw_1.WriteLine("\t}\n");
		sw_1.WriteLine("\tpublic override bool onEnter(){\n");
		sw_1.WriteLine("\t\treturn true;\n");
		sw_1.WriteLine("\t}\n");
		sw_1.WriteLine("\tpublic override bool onExit(){\n");
		sw_1.WriteLine("\t\treturn true;\n");
		sw_1.WriteLine("\t}\n");
		sw_1.WriteLine("\tpublic override void Notify(){\n");
		sw_1.WriteLine("\t}\n");
		sw_1.WriteLine ("}");
		sw_1.Flush ();
		sw_1.Close ();
		AssetDatabase.Refresh();
	}
	
	GameObject GetSelectedPrefab()
	{
		if (Selection.activeGameObject != null)
		{
			return Selection.activeGameObject;
		}
		return null;
	}
}
