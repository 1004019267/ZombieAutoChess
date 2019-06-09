
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Net;

//链接loddy服务器  服务器代码 
namespace ty
{
    public class tyConnectRoom
    {
        static tyConnectRoom g_websocketMgr;
        public static tyConnectRoom getMe()
        {

            if (tyConnectRoom.g_websocketMgr == null)
            {
                tyConnectRoom.g_websocketMgr = new tyConnectRoom();
            }
            return tyConnectRoom.g_websocketMgr;
        }

        public void shutdown()
        {
            OnClose();
        }


        WebSocket   m_ws = null;
        public bool m_connent = false;
        public bool m_connenting = false;

        public void connent(string loginServerIp, int loginServerPort)
        {


            if (m_connent == true)
            {
                return;
            }
            if (m_connenting == true)
            {
                return;
            }

            string _url = "ws://" + loginServerIp + ":" + loginServerPort;
            logMgr.log("连接房间服务器 : " + _url);
            using (var ws = new WebSocket(_url))
                m_ws = ws;

            m_connenting = true;
            // To set the WebSocket events.
            m_ws.OnOpen += (sender, e) =>
            {
                this.OnConnect();
            };

            m_ws.OnMessage += (sender, e) =>
            {
                this.OnMessage(e.Data);
            };


            m_ws.OnError += (sender, e) =>
            {
                this.OnError(sender, e);
            };


            m_ws.OnClose += (sender, e) =>
            {
                this.OnClose();
            };

            m_ws.ConnectAsync();
        }

        void OnError(object sender, ErrorEventArgs e)
        {
            logMgr.log(e.Message);
            m_connent = false;
            m_connenting = false;
            logMgr.log( "连接房间失败！"+ e.Message );
            tyInterface.getMe().onjointRoomServerError( "连接房间失败！" );

        }

        void OnConnect()
        {
            m_connent = true;
            m_connenting = false;
            logMgr.log("连接房间成功！");
            msgRoomlogin.getMe().login();
         
        }

        void OnClose()
        {
            if (m_ws == null)
                return;
         
                m_connent = false;
                m_connenting = false;
                if(m_ws!=null)
                {
                    m_ws.CloseAsync();
                    m_ws = null;
                }
                tyConnectRoom.g_websocketMgr = new tyConnectRoom();

            tyInterface.getMe().onjointRoomServerError("");

           
        }

        void OnMessage(string data)
        {

            this.addMsg(data);

        }

        Queue<string> m_Queue_msg = new Queue<string>();


        public virtual void addMsg(string msg)
        {
            lock (m_Queue_msg)
            {
                m_Queue_msg.Enqueue(msg);
            }
        }


        public virtual void loop_msg()
        {
            while (m_Queue_msg.Count > 0)
            {
                lock (m_Queue_msg)
                {
                    string msg = m_Queue_msg.Dequeue();
                    tyRoommsgMgr.getMe().handle(msg);
                }
            }
        }


        //float m_time_login = 1;
        public void loop( )//场景
        {
            tyRoommsgMgr.getMe().loop();

            this.loop_msg();
            //m_time_login -= Time.deltaTime ;
            //if (m_time_login > 0)
            //{
            //    return;
            //}
            //m_time_login = 10;

            //if (this.m_connent == false)
            //{
            //    if (this.m_connenting == false)
            //    {
            //        this.connent( tyConofig.g_ty_loddyServerIp , tyConofig.g_ty_loddyServerPort );
            //    }
            //}

        }


        public bool send(string data)
        {
            data = data.Replace("\r\n", "");
            if (m_ws != null)
            {

                if (this.m_connent == false)
                    return false;

                //m_ws.Send(data);
                m_ws.Send(data);
                return true;
            }
            return false;
        }



    }
}