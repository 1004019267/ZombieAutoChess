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


    class baseServerMsg
    {

        public virtual void shutdown()
        {

        }

        public e_baseMsg_user m_e_type = e_baseMsg_user.e_basemsg_none;
        public baseServerMsg()
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
            msgRoomSendToOtherPlayer.getMe().sendto(staff);

        }

        //发送给房间所有人
        public virtual void sendToALL(JObject data)
        {
            data.Add(new JProperty("tp", m_e_type));
            msgRoomSendToAll.getMe().sendto( data );
           
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