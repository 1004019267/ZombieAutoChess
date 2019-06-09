using System.IO;
using UnityEngine;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using LitJson;

public class TvSdk_Data
{
    public string JavaInterfaceFunc;
}

public class TvSdk_InitData : TvSdk_Data
{
    public int ServerLoadParamNums;
    public string key;
    public string cpId;
    public string gameId;
}

public class TvSdk_InitData_Callback
{
    public string[] ReturnData;
}

public class TvSdk_PayData : TvSdk_Data
{
    public string ProductId;
    public string OrderCode;
}

public class net_struct
{
    public string des;
    public string g_game_PrivateKey ;
    public string g_url_server_one ;
    public string g_url_server ;
    public string loginServerUrl ;
    public string pvpServerUrl;
    public string accServerUrl;
    public string g_gamaid;
}

public class IapMgr :Singleton<IapMgr>
{
    //public string url = "http://test.sjgame.net:81/OTTGame_test/";
    public string shenjian_url = "http://api.tvsdk.cn/OTTGame/";
    public string privateKey = "36DDA110-6F01-4C12-9840-33DAEC6A12FC";
    public string key = "5141060078055252970";
    public string cpId = "tvc103";
    public string gameId = "tvg103038";
    public string bossId = "1006";
    public string userId = "10003";//10dffsf
    public int maxPrice = 10000;
    public net_struct m_net_info;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        m_net_info      = new net_struct();
        string json_net =    IOHelper.LoadJson( Application.streamingAssetsPath + "/JsonData/net.json");
         if (json_net != null)
        {
            // m_net_info = JsonUtility.FromJson<net_struct>(json_net);
            //m_net_info = JsonMapper.ToObject(json_net);
        }
        privateKey = m_net_info.g_game_PrivateKey;
        shenjian_url = m_net_info.g_url_server;
        gameId = m_net_info.g_gamaid;
        Init();
    }

    public string CallJava4TvSdk(string str)
    {
        print("CallJava4TvSdk 被执行到了--"+ str);
        AndroidJavaClass jc = new AndroidJavaClass("com.holyblade.CyberSdk.UnityPlayerActivity");
        Debug.Log(jc);
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("m_instance");

        string ret = jo.Call<string>("CallJava4TvSdk", str);
        print("CallJava4TvSdk 执行回调 --" + ret);
        return ret;
    }

    public void Init()
    {
        TvSdk_InitData data = new TvSdk_InitData();
        data.JavaInterfaceFunc = "init";
        data.key = key;
        data.cpId = cpId;
        data.gameId = gameId;
        data.ServerLoadParamNums = 0;
        string str = JsonUtility.ToJson(data);
        //string str = JsonMapper.ToJson(data);
        string ret = this.CallJava4TvSdk(str);

        print("tvsdkinit ret :" + ret);

       // TvSdk_InitData_Callback d = JsonMapper.ToObject<TvSdk_InitData_Callback>(ret);
        TvSdk_InitData_Callback d = JsonUtility.FromJson<TvSdk_InitData_Callback>(ret);
        userId = d.ReturnData[0];
        maxPrice = int.Parse(d.ReturnData[1]);
        bossId = d.ReturnData[2];

        this.Login();
    }

    public void Login()
    {
        TvSdk_Data data = new TvSdk_Data();
        data.JavaInterfaceFunc = "login";

       // string str = JsonMapper.ToJson(data);
        string str = JsonUtility.ToJson(data);
        string ret = this.CallJava4TvSdk(str);

        print("tvsdk login ret :" + ret);
    }

    public void Pay(string shangpingid)
    {
        TvSdk_PayData data = new TvSdk_PayData();
        data.JavaInterfaceFunc = "Pay";
        data.ProductId = shangpingid;
        data.OrderCode = shangpingid;

        //string str = JsonMapper.ToJson(data);
        string str = JsonUtility.ToJson(data);
        string ret = this.CallJava4TvSdk(str);

        print("tvsdk Pay ret :" + ret);
    }

    public void PayCallBack(string result)
    {
        print("PayCallBack result:" + result);
    }

    public void Exit()
    {
        TvSdk_Data data = new TvSdk_Data();
        data.JavaInterfaceFunc = "Exit";

        //string str = JsonMapper.ToJson(data);
        string str = JsonUtility.ToJson(data);
        string ret = this.CallJava4TvSdk(str);

        print("tvsdk Exit ret :" + ret);
    }

//    public static string LoadJson(string path)
//    {
//        string rootpath = "";
//#if UNITY_EDITOR_WIN
//        rootpath = Application.dataPath;
//#elif UNITY_ANDROID
//        rootpath =  Application.persistentDataPath;
//#endif

//        string str = rootpath + path;
//        FileInfo fileinfo = new FileInfo(str);
//        if (fileinfo.Exists)
//        {
//            return File.ReadAllText(str, Encoding.UTF8);
//        }
//        Debug.Log("没有" + str);
//        return null;
//    }


//    public static void SaveJson(string path,string content)
//    {
//        string rootpath = "";
//#if UNITY_EDITOR_WIN
//        rootpath = Application.dataPath;
//#elif UNITY_ANDROID
//        rootpath =  Application.persistentDataPath;
//#endif
//        string str = rootpath + path;
//        FileInfo fileinfo = new FileInfo(str);
//        if (fileinfo.Exists)
//        {
//            File.WriteAllText(str, content, Encoding.UTF8);
//        }
//        else {
//            StreamWriter sw = new StreamWriter(str,false, Encoding.UTF8);
//            content = Regex.Unescape(content);//litJson Unicode转码
//            sw.Write(content);
//            sw.Close();
//        }
//    }
}




 public static class WebUtility
   {
       // Fields
   
       private static char[] _htmlEntityEndingChars = new char[] { ';', '&' };
       private const char HIGH_SURROGATE_START = '\ud800';
       private const char LOW_SURROGATE_END = '\udfff';
      private const char LOW_SURROGATE_START = '\udc00';
      private const int UNICODE_PLANE00_END = 0xffff;
      private const int UNICODE_PLANE01_START = 0x10000;
      private const int UNICODE_PLANE16_END = 0x10ffff;
      private const int UnicodeReplacementChar = 0xfffd;
 
  
 
     private static void ConvertSmpToUtf16(uint smpChar, out char leadingSurrogate, out char trailingSurrogate)
      {
          int num = ((int)smpChar) - 0x10000;
         leadingSurrogate = (char) ((num / 0x400) + 0xd800);
         trailingSurrogate = (char) ((num % 0x400) + 0xdc00);
     }

    private static int HexToInt(char h)
     {
         if ((h >= '0') && (h <= '9'))
          {
              return (h - '0');
          }
         if ((h >= 'a') && (h <= 'f'))
         {
              return ((h - 'a') + 10);
          }
          if ((h >= 'A') && (h <= 'F'))
          {
             return ((h - 'A') + 10);
          }
          return -1;
      }
  
  
 
 
 
      private static char IntToHex(int n)
      {
          if (n <= 9)
         {
              return (char) (n + 0x30);
          }
          return (char) ((n - 10) + 0x41);
      }
  
      private static bool IsUrlSafeChar(char ch)
    {
         if ((((ch >= 'a') && (ch <= 'z')) || ((ch >= 'A') && (ch <= 'Z'))) || ((ch >= '0') && (ch <= '9')))
         {
            return true;
         }
          switch (ch)
          {
          case '(':
          case ')':
          case '*':
          case '-':
          case '.':
         case '_':
          case '!':
              return true;
          }
         return false;
      }
  
      public static string UrlEncode(string value)
      {
          if (value == null)
          {
              return null;
          }
          byte[] bytes = Encoding.UTF8.GetBytes(value);
          return Encoding.UTF8.GetString(UrlEncode(bytes, 0, bytes.Length, false));
      }
      private static bool ValidateUrlEncodingParameters(byte[] bytes, int offset, int count)
      {
          if ((bytes == null) && (count == 0))
          {
            return false;
         }
         if (bytes == null)
        {
             //throw new ArgumentNullException("bytes");
          }
          if ((offset< 0) || (offset > bytes.Length))
         {
              //throw new ArgumentOutOfRangeException("offset");
         }
        if ((count< 0) || ((offset + count) > bytes.Length))
         {
             //throw new ArgumentOutOfRangeException("count");
        }
         return true;
     }

 

     private static byte[] UrlEncode(byte[] bytes, int offset, int count)
     {
         if (!ValidateUrlEncodingParameters(bytes, offset, count))
         {
             return null;
         }
         int num = 0;
         int num2 = 0;
         for (int i = 0; i<count; i++)
         {
             char ch = (char)bytes[offset + i];
             if (ch == ' ')
             {
                 num++;
             }
             else if (!IsUrlSafeChar(ch))
             {
                 num2++;
            }
         }
         if ((num == 0) && (num2 == 0))
         {
            return bytes;
         }
         byte[] buffer = new byte[count + (num2 * 2)];
         int num4 = 0;
         for (int j = 0; j<count; j++)
        {
             byte num6 = bytes[offset + j];
             char ch2 = (char)num6;
             if (IsUrlSafeChar(ch2))
             {
                 buffer[num4++] = num6;
             }
            else if (ch2 == ' ')
             {
                 buffer[num4++] = 0x2b;
             }
             else
            {
                 buffer[num4++] = 0x25;
                buffer[num4++] = (byte) IntToHex((num6 >> 4) & 15);
                 buffer[num4++] = (byte) IntToHex(num6 & 15);
            }
        }
         return buffer;
     }

     private static byte[] UrlEncode(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
     {
         byte[] buffer = UrlEncode(bytes, offset, count);
         if ((alwaysCreateNewReturnValue && (buffer != null)) && (buffer == bytes))
         {
             return (byte[]) buffer.Clone();
         }
         return buffer;
    }


     public static byte[] UrlEncodeToBytes(byte[] value, int offset, int count)     {
         return UrlEncode(value, offset, count, true);
     }
 
 
  }

