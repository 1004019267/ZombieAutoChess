using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

 
 
    /// <summary>
    /// 打印输出管理
    /// </summary>
    class logMgr
    {
        static logMgr g_logMgr;
        string fullPath;
        public static logMgr getMe()
        {

            if (logMgr.g_logMgr == null)
            {
                logMgr.g_logMgr = new logMgr();
                logMgr.g_logMgr.InitLogger();
            }
            return logMgr.g_logMgr;
        }

        private void InitLogger()
        {

            string filename = "/"  + "tylog.txt";


            fullPath = Application.dataPath + filename;
            if (File.Exists(fullPath))
                File.Delete(fullPath);
            Debug.Log(fullPath.Replace(filename, ""));
            if (Directory.Exists(fullPath.Replace(filename, "")))
            {
                FileStream fs = File.Create(fullPath);
                fs.Close();
                // Application.RegisterLogCallback(logCallBack);
            }
            else
            {
                Debug.LogError("directory is not exist");
            }
        }

        public void logCallBack(string condition)
        {
            if (File.Exists(fullPath))
            {
                using (StreamWriter sw = File.AppendText(fullPath))
                {
                    sw.WriteLine(condition);
                }
            }
        }

        public void shutdown()
        {

        }

        public static void log(object log)
        {
            Debug.Log(log);
            Console.WriteLine(log);
        }

    }
 