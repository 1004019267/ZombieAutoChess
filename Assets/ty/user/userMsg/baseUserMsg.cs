using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace ty
{
  public  enum e_baseMsg_user
    {
        e_basemsg_none = -1,
        e_basemsg_key = 13, //发键盘按键
        e_basemsg_SyncObject = 14, //同步物体位置
        e_basemsg_CreateObject = 15, //创建物体
        e_basemsg_startGame = 16, // 开始游戏
        e_basemsg_GameOver = 17 , // 结束游戏
        e_basemsg_SyncObjectRot = 18, //同步物体旋转


        e_basemsg_playin = 19, // 
        e_basemsg_playout = 20, // 
        e_basemsg_createscene = 21, // 


        e_basemsg_deleteObj = 22, // 

        e_basemsg_SnycAllObj = 23,
        e_basemsg_playerSyncObject = 24,
        e_basemsg_checkScene = 25,
        e_basemsg_physicobj = 26,
        e_basemsg_data = 27,

        e_basemsg_SyncObject_ctoc = 28, //同步物体位置
        e_basemsg_event = 29,//同步事件
    }


    public   class baseUserMsg
    {
        public virtual void shutdown()
        {

        }
        public e_baseMsg_user m_e_type = e_baseMsg_user.e_basemsg_none;
        public baseUserMsg()
        {
        }

        public virtual void handle(JObject jo)
        {

        }
        public virtual void loop()
        {

        }
        //发送给指定玩家
        public virtual void sentToPlayer(int targetplayid, JObject data)
        {
            data.Add(new JProperty("tp", m_e_type));
            msgRoomSendToPlayerOne.getMe().sendto( targetplayid , data );
        }
        //发送给除了自己的其他人
        public virtual void sentToOther(JObject staff)
        {
            staff.Add(new JProperty("tp", m_e_type));
            msgRoomSendToOtherPlayer.getMe().sendto( staff );
        }

        //发送给房间所有人
        public virtual void sendToALL(JObject data)
        {
            data.Add(new JProperty("tp", m_e_type));
            msgRoomSendToAll.getMe().sendto( data );
        }

        public virtual bool sendToServer(JObject data)
        {
            if (tyConofig.g_syncplayid == -2)
                return false;
            data.Add(new JProperty("tp", m_e_type));
            msgRoomSendToPlayerOne.getMe().sendto(tyConofig.g_syncplayid, data);
            return true;
        }

        public static byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter(); formatter.Serialize(ms, obj); return ms.GetBuffer();
            }
        }

        /// <summary> 
        /// 将一个序列化后的byte[]数组还原         
        /// </summary>
        /// <param name="Bytes"></param>         
        /// <returns></returns> 
        public static object BytesToObject(byte[] Bytes)
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                IFormatter formatter = new BinaryFormatter(); return formatter.Deserialize(ms);
            }
        }


    }
}