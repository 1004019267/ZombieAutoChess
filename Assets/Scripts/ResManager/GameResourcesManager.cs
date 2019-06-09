using System.Resources;
using UnityEngine;
using Spine.Unity;
public class GameResourcesManager
{
    public static string uiPath = "Prefabs/UI/";
    public static string cardPath = "Texture/ZombieTexture";
    public static string zombiePath = "Prefabs/ZombieInfo";
    public static string zombieSpinePath = "Spine";
    public static string bulletPath = "Prefabs/Bullet/";
    public static string roleAvtPath = "Prefabs/GameObjs/RoleAvt/";
    public static string gameConfigPath = "GameConfigResources/";
    public static string sceneObjectPath = "Prefabs/SceneObjs/";
    public static string sceneXmlPath = "SceneResources/SceneXmls/";
    public static string materialPath = "Material/";
    public static string spritePath = "Texture/";
    public static GameObject GetSceneObject (string pName)
    {
        return ResourcesManager.Load<GameObject> (sceneObjectPath + pName);
    }

    public static string GetGameConfigText (string configName)
    {
        return ResourcesManager.Load<TextAsset> (gameConfigPath + configName).text;
    }

    public static string GetSceneXmlText(string sceneName)
    {
        return ResourcesManager.Load<TextAsset>(sceneXmlPath+sceneName+"Xml").text;
    }

    //
    public static GameObject GetUIPrefab (string pName)
    {
        return ResourcesManager.Load<GameObject> (uiPath + pName);
    }

    public static Sprite GetCardSprite(string name)
    {
        return ResourcesManager.Load<Sprite>(cardPath + name);
    }
    public static Sprite[] GetAllZombieCardSprite()
    {
        return Resources.LoadAll<Sprite>(cardPath);
    }

    public static GameObject [] GetAllZombiePrefab()
    {
        return Resources.LoadAll<GameObject>(zombiePath);
    }
    public static SkeletonDataAsset []GetAllZombieSpine()
    {
        return Resources.LoadAll<SkeletonDataAsset>(zombieSpinePath);
    }

    public static GameObject []GetAllBullet()
    {
        return Resources.LoadAll<GameObject>(bulletPath);
    }

    public static Material GetMaterial(string name)
    {
        return Resources.Load<Material>(materialPath + name);
    }

    public static Sprite GetSprite(string name)
    {
        return Resources.Load<Sprite>(spritePath + name);
    }
    /// <summary>
    /// 通过路径创建物体
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GameObject ClonePrefab(string path ,Transform _parent,Vector3 pos,Quaternion rot)
    {
        GameObject prefab = ResourcesManager.Load<GameObject> (path);
        GameObject obj_clone = GameObject.Instantiate<GameObject>(prefab,pos,rot,_parent);
        return obj_clone;
    }
    public static GameObject ClonePrefab(string path ,Vector3 pos,Quaternion rot)
    {
        GameObject prefab = ResourcesManager.Load<GameObject> (path);
        GameObject obj_clone = GameObject.Instantiate<GameObject>(prefab,pos,rot);
        return obj_clone;
    }
    
    public static GameObject ClonePrefab(string path ,Transform _parent)
    {
        GameObject prefab = ResourcesManager.Load<GameObject> (path);
        GameObject obj_clone = GameObject.Instantiate<GameObject>(prefab,_parent);
        return obj_clone;
    }
    


}