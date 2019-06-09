using System.IO;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class CreatConfigDataFile : EditorWindow
{
    static string writePath="/Scripts/GameConfig/";
    static Object selectObj;
    [MenuItem("KLEditor/Window/Config Creator Window")]
    public static void Bywindow()
    {
        CreatConfigDataFile _window = EditorWindow.GetWindow<CreatConfigDataFile>();
    }
    
    void OnSelectionChange()
    {
        Repaint ();
    }

    private void OnGUI()
    {
        GUILayout.Label("设定配置数据文件生成路径");
        GUILayout.TextField(writePath);
        GUILayout.Label ("选择需要生成配置数据的CSV文件");
        if (GUILayout.Button("生成"))
        {
            if (selectObj != null)
            {
                CreatConfigFile();
            }
        }

        if (Selection.activeObject != null)
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path.Substring(path.Length -4,4) == ".csv")
            {
                selectObj = Selection.activeObject;
                GUILayout.Label(path);
            }
        }
        
    }

    void CreatConfigFile()
    {
        string fileName = selectObj.name;
        string className = fileName;
        
        StreamWriter sw = new StreamWriter(Application.dataPath + writePath +className +".cs");
        
        sw.WriteLine("using UnityEngine;\nusing System.Collections;\n");
        sw.WriteLine("public partial class " + className + ":GameConfigDataBase");
        sw.WriteLine("{\n");
        string filePath = AssetDatabase.GetAssetPath(selectObj);
        CsvStreamReader csv = new CsvStreamReader(filePath);
        for (int i = 1; i < csv.ColCount+1; i++)
        {
            string fieldname = csv[1, i];
            string fieldtype = csv[2, i];
            sw.WriteLine("\tpublic " + fieldtype+" " + fieldname + ";");
        }
        sw.WriteLine("\t protected override string getFilePath()");
        
        sw.WriteLine ("\t" + "{");
//		filePath=filePath.Replace("Assets/Resources/","");
//		filePath=filePath.Substring(0,filePath.LastIndexOf('.'));
        sw.WriteLine ("\t\t" + "return " + "\"" + fileName + "\";");
        sw.WriteLine ("\t" + "}");
        sw.WriteLine ("}");
		
        sw.Flush ();
        sw.Close ();
        AssetDatabase.Refresh();
    }
}