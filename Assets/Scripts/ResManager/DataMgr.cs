using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using ES3Internal;
using ES3Types;
//



//数据存档类//whb

public class DataMgr
{
    
    private static DataMgr g_itemData;
    public static DataMgr getMe()
    {
        if (DataMgr.g_itemData == null)
        {
            DataMgr.g_itemData = new DataMgr();

        }
        return DataMgr.g_itemData;
    }

    private string DataKey = "GameData";
    [HideInInspector]
    public bool IsBackUp = false;//是否备份
    [HideInInspector]
    public bool Isenscript = false ;//是否加密

    private string secret = "xingjie";//加密密码
    string dirPath = Application.persistentDataPath + "/Save";//存档目录
    /// <summary>
    /// 玩家存档写入,自定义类型需要在assets/easysave3/openeasy3window/types添加
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void SavePlayerData<T>(T _data)
    {
        ES3Settings _settings = new ES3Settings();
        if (Isenscript)
        {
            _settings.encryptionType = ES3.EncryptionType.AES;
            _settings.encryptionPassword = secret;
        }
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        if (_data == null)
        {
            throw new UnityException("存储对象为空！");
        }
        ES3.Save<T>(DataKey,_data,dirPath+"/GameData.json",_settings);
        if (IsBackUp)
        {
            ES3.CreateBackup(dirPath+"/GameData.json",_settings);
        }
    }

    /// <summary>
    /// 玩家存档读取
    /// </summary>
    /// <param name="_data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="UnityException"></exception>
    public T LoadPlayerData<T>()where T:new()
    {
        ES3Settings _settings = new ES3Settings();
        if (Isenscript)
        {
            _settings.encryptionType = ES3.EncryptionType.AES;
            _settings.encryptionPassword = secret;
        }
        if (!ES3.FileExists(dirPath+"/GameData.json",_settings))
        {
            Debug.LogWarning("没有"+dirPath + "GameData.json！");
            if (!ES3.RestoreBackup(dirPath + "/GameData.json",_settings))
            {
                ES3.Save<T>(DataKey,new T(),dirPath+"/GameData.json",_settings);
                if (IsBackUp)
                {
                    ES3.CreateBackup(dirPath+"/GameData.json",_settings);
                }
            }
        }
        T data = default(T);
        try
        {
            data = ES3.Load<T>(DataKey,dirPath+"/GameData.json",_settings);
        }
        catch 
        {
            if (!ES3.RestoreBackup(dirPath + "/GameData.json",_settings))
            {
                ES3.Save<T>(DataKey,new T(),dirPath+"/GameData.json",_settings);
                if (IsBackUp)
                {
                    ES3.CreateBackup(dirPath+"/GameData.json",_settings);
                }
            }
            if (data == null)
            {
                if (Application.isEditor)
                {
                    throw new UnityException("GameData.json 没有");
                }
                Application.Quit();
            }
        }
        return data;
    }

    /// <summary>
    /// 用一个key来保存对应的gameobject
    /// </summary>
    /// <param name="key"></param>
    /// <param name="_obj"></param>
    /// <typeparam name="GameObject"></typeparam>
    public void SaveGameobjectOrPrefab<GameObject>(string key,GameObject _obj)
    {
        if (_obj == null)
        {
            throw new UnityException("存储对象为空！");
        }
        ES3.Save<GameObject>(key,_obj);
    }

    /// <summary>
    /// 加载物体
    /// </summary>
    /// <param name="key"></param>
    /// <param name="isself"></param>
    /// <param name="self"></param>
    /// <returns></returns>
    public GameObject loadGameObjectOrPrefab(string key,bool isself = false,GameObject self = null)
    {
        if (!ES3.KeyExists(key))
        {
            throw new UnityException("没有"+key + "存在！");
        }
        if (isself)
        {
            if (self != null)
            {
                ES3.LoadInto<GameObject>(key,self);
                return self;
            }
            Debug.LogWarning("当前对象没有重载数据！");
            return self;
        }
        return ES3.Load<GameObject>(key);
    }

    public void SaveJson<T>(string key,T _data,string fileName)
    {
        
        if (_data == null)
        {
            throw new UnityException("存储对象为空！");
        }
        ES3Settings _settings = new ES3Settings();
        if (Isenscript)
        {
            _settings.encryptionType = ES3.EncryptionType.AES;
            _settings.encryptionPassword = secret;
        }
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        if (fileName.IndexOf(".json") > -1)
        {
            fileName = fileName.Replace(".json", "");
        }
        string jsonpath = dirPath+"/" + fileName + ".json";
        _settings.format = ES3.Format.JSON;
        ES3.Save<T>(key,_data,jsonpath,_settings);
        if (IsBackUp)
        {
            ES3.CreateBackup(jsonpath,_settings);
        }
    }

 
    

    public T LoadJson<T>(string key,string fileName)where T:new ()
    {
        ES3Settings _settings = new ES3Settings();
        if (Isenscript)
        {
            _settings.encryptionType = ES3.EncryptionType.AES;
            _settings.encryptionPassword = secret;
        }
        if (fileName.IndexOf(".json") > -1)
        {
            fileName = fileName.Replace(".json", "");
        } 
        if (!ES3.FileExists(dirPath+"/"+fileName +".json",_settings))
        {
            Debug.LogWarning("没有"+dirPath +"/"+ fileName);
            if (!ES3.RestoreBackup(dirPath +"/" + fileName +".json",_settings))
            {
                ES3.Save<T>(DataKey,new T(),dirPath+"/"+fileName +".json",_settings);
                if (IsBackUp)
                {
                    ES3.CreateBackup(dirPath+"/"+fileName +".json",_settings);
                }
            }
        }
        string jsonpath = dirPath+"/" +fileName + ".json";
        if (!ES3.KeyExists(key,jsonpath,_settings))
        {
            throw new UnityException("没有"+key + "存在！");
        }
        _settings.format = ES3.Format.JSON;
        try
        {
            return ES3.Load<T>(key,jsonpath,_settings);
        }
        catch 
        {
            if (!ES3.RestoreBackup(jsonpath,_settings))
            {
                ES3.Save<T>(DataKey,new T(),dirPath+"/"+fileName +".json",_settings);
                if (IsBackUp)
                {
                    ES3.CreateBackup(dirPath+"/"+fileName +".json",_settings);
                }
            }
            throw new UnityException("no has "+dirPath+"/"+fileName +".json");
        }
    }
    
    

}