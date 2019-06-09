using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using ty;

public class CreateUUIDWidows : EditorWindow
{
    [MenuItem("创建UUID/创建UUID")]
    static void Create()
    {
        baseSyncObject[] networkSyncObject =  GameObject.FindObjectsOfType<baseSyncObject>();
        
        foreach (var item in networkSyncObject)
        {
            Undo.RecordObject(item, "networkSyncObject");
            //  item.m_Index = (int)CreateUUID.CreateID();
            item.m_objectId = msgRoomCreateObject.getMe().createObjectId(  );
        }
        //foreach (var item in netWorkSyncObj_DaTaoSha)
        //{
        //    Undo.RecordObject(item, "networkSyncObject_dataosha");
        //    item.m_Index = CreateUUID.CreateID();
        //    item.name = item.m_Index.ToString();
        //}
        //foreach (var item in networkSyncObject_4V4)
        //{
        //    Undo.RecordObject(item, "networkSyncObject_4v4");
        //    item.m_Index =(int) CreateUUID.CreateID();
        //    item.name = item.m_Index.ToString();
        //}
        Undo.RecordObjects(networkSyncObject, "ID1 ");
        //Undo.RecordObjects(netWorkSyncObj_DaTaoSha, "ID2 ");
        //Undo.RecordObjects(networkSyncObject_4V4, "ID3 ");
    }
      
}
