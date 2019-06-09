using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ty
{
    class syncObjectMgr
    {
        static syncObjectMgr g_msgMgr;
        public static syncObjectMgr getMe()
        {
            if (syncObjectMgr.g_msgMgr == null)
            {
                syncObjectMgr.g_msgMgr = new syncObjectMgr();
            }
            return syncObjectMgr.g_msgMgr;
        }

        public   Dictionary<int, baseSyncObject> m_roomObjMap = new Dictionary<int, baseSyncObject>();


        public void shutdown() {
            m_roomObjMap = new Dictionary<int, baseSyncObject>();
        }


    public     void addObject( baseSyncObject _baseSyncObject )
    {
            if(getObject(_baseSyncObject.m_objectId) != null)
            {

                logMgr.log("error addObject id 相同:"+ _baseSyncObject.m_objectId);
                return;
            }

            m_roomObjMap.Add( _baseSyncObject.m_objectId , _baseSyncObject);
   }


        public void removeObject(int objectid)
        {
             m_roomObjMap.Remove( objectid );
        }
        public void DestroyObject(int objectid)
        {
            baseSyncObject _baseSyncObject = getObject(objectid);
            if(_baseSyncObject)
            {
                GameObject.Destroy(_baseSyncObject.gameObject);
                m_roomObjMap.Remove(objectid);
            }
        }


        public baseSyncObject getObject(int objectid )
        {
            baseSyncObject _baseSyncObject;
            if (  m_roomObjMap.TryGetValue(objectid, out _baseSyncObject) )
            {
                return _baseSyncObject;
            }

            return null;
        }







    }
}
