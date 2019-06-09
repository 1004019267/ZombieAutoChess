using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

///
/// 信息中心类，用来处理GameObjects相互发消息。本质是观察者模式。
/// 通过AddObserver函数注册观察者，RemoveObserver注销观察者。
/// 内部通过哈希表对场景中所有的消息进行管理
///
public class NotificationCenter : MonoBehaviour
{

    private static NotificationCenter defaultCenter = null;
    ///
    /// 单例模式, 在场景中自动造一个挂有NotificationCenter脚本的Default Notification Center游戏物体，如果手动创建了一个则不再创建
    ///    
    public static NotificationCenter DefaultCenter()
    {
        if (null == defaultCenter)
        {
            GameObject notificationObject = new GameObject("Default Notification Center");
            defaultCenter = notificationObject.AddComponent<NotificationCenter>();
        }
        return defaultCenter;
    }

    // 哈希表包含了所有的发送的信息。其中每个键值对，表示的是【某一消息——该消息的所有观察者线性表】
    private Hashtable notifications = null;
    void Awake()
    {
        this.notifications = new Hashtable();
    }

    ///
    /// 注册观察者
    ///    
    public void AddObserver(Component observer, string name) { AddObserver(observer, name, null); }
    public void AddObserver(Component observer, string name, Component sender)
    {
        // 对观察者的名字进行检查
        if (name == null || name == "") { Debug.Log("在AddObserver函数中指定的是空名称!."); return; }
        // 哈希表中的值是List，new list
        if (null == this.notifications[name])
        {
            this.notifications[name] = new List<Component>();
        }
        // 该条消息的所有观察者，通过List将他拉出来对其操作
        List<Component> notifyList = this.notifications[name] as List<Component>;
        // 将观察者加入到哈希表中值LIST中去，也就是注册上了
        if (!notifyList.Contains(observer)) { notifyList.Add(observer); }
    }

    ///
    /// 注销观察者
    ///  
    public void RemoveObserver(Component observer, string name)
    {
        // 该条消息的所有观察者，通过List将他拉出来对其操作
        List<Component> notifyList = this.notifications[name] as List<Component>;

        if (null != notifyList)
        {
            // 删除这个已注册的观察者
            if (notifyList.Contains(observer)) { notifyList.Remove(observer); }
            // 如果这个消息没有观察者，则在哈希表中删除这个消息关键字
            if (notifyList.Count == 0) { this.notifications.Remove(name); }
        }
    }

    ///
    ///  事件源把发消息出去
    ///    
    public void PostNotification(Component aSender, string aName) { PostNotification(aSender, aName, null); }
    public void PostNotification(Component aSender, string aName, object aData) { PostNotification(new Notification(aSender, aName, aData)); }
    public void PostNotification(Notification aNotification)
    {

        if (aNotification.name == null || aNotification.name == "") { Debug.Log("Null name sent to PostNotification."); return; }
        // 该条消息的所有观察者，通过List将他拉出来对其操作
        List<Component> notifyList = this.notifications[aNotification.name] as List<Component>;
        if (null == notifyList) { Debug.Log("在PostNotification的通知列表中未找到: " + aNotification.name); return; }

        List<Component> observersToRemove = new List<Component>();
        foreach (Component observer in notifyList)
        {
            if (!observer)
            {
                observersToRemove.Add(observer);
            }
            else
            {
                // 最终以u3d api的SendMessage函数将消息发了过去,所以只能传递一个数据实参，受SendMessage函数本身限制
                observer.SendMessage(aNotification.name, aNotification, SendMessageOptions.DontRequireReceiver);
            }
        }
        // 清除所有无效的观察者
        foreach (Component observer in observersToRemove)
        {
            notifyList.Remove(observer);
        }
    }

}

///
/// 通信类是物体发送给接受物体的一个通信类型。这个类包含发送的游戏物体(U3D的Component类对象，
/// 而不是GameObject)，通信的名字(函数名)，可选的数据实参
///
public class Notification
{
    public Component sender;
    public string name;
    public object data;
    // 构造函数
    public Notification(Component aSender, string aName) { sender = aSender; name = aName; data = null; }
    public Notification(Component aSender, string aName, object aData) { sender = aSender; name = aName; data = aData; }
}
