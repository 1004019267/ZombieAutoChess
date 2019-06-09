using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.VR.WSA.WebCam;

//无用
public abstract class BaseAction : MonoBehaviour
{
    public string info;

    /// <summary>
    /// 脚本在哪个目标对象上运行.支持GameObjectAgent
    /// </summary>
    public GameObject runTarget;

    internal static bool isWating = false;

    /// <summary>
    /// 是否暂时放弃这个节点.直接运行后续节点.
    /// </summary>
    public bool ignore;

    public float time = 0;
    public bool waitOver = true;
    public bool openNext = true;

    /// <summary>
    /// 当前已经消耗的时间.
    /// </summary>
    internal float duration;

    //	public Transform target;
    private bool _isStart = false;
    internal bool distoryOnOver;

    private void OnEnable()
    {
        if (isWating)
        {
            return;
        }

        if (_isStart)
        {
            return;
        }
        duration = 0;
        _isStart = true;
        if (!ignore)
        {
            onStart();
        }
        if (duration == 0 && time == 0)
        {
            over();
        }

        if (openNext && !waitOver)
        {
            openNextAction();
        }



    }
    [ExecuteInEditMode]
    private void OnDisable()
    {
        if (!_isStart)
        {
            return;
        }

        if (isWating)
        {
            return;
        }
        _isStart = false;
        if (openNext && waitOver)
        {
            openNextAction();
        }
        if (distoryOnOver)
        {
            DestroyObject(this);
        }
    }

    private void LateUpdate()
    {
        duration += Time.deltaTime;
        if (duration >= time)
        {
            over();
        }
    }

    public bool isStart
    {
        get { return _isStart; }
    }




    public new GameObject gameObject
    {
        get
        {
            if (runTarget == null)
            {
                return base.gameObject;
            }
            else
            {
                return GameObjectAgent.getAgentGameObject(base.gameObject, runTarget);
            }
        }
    }

    public new Transform transform
    {
        get
        {
            if (runTarget == null)
            {
                return base.transform;
            }
            else
            {
                return GameObjectAgent.getAgentTransform(base.gameObject, runTarget);
            }
        }
    }

    public float Progress
    {
        get { return duration / time; }
    }

    public void setMaxWait(float max)
    {
        if (time - duration > max)
        {
            duration = time - max;
        }
    }

    public void setWait(float wait)
    {
        duration = time - wait;
    }

    public void doover()
    {
        bool _opennext = this.openNext;
        _isStart = false;
        this.openNext = false;
        enabled = false;
        this.openNext = _opennext;
    }

    public GameObject getActionParent()
    {
        return base.gameObject;
    }



    public bool openNextAction()
    {
        bool find = false;
        BaseAction[] _list = base.gameObject.GetComponents<BaseAction>();
        foreach (var a in _list)
        {
            if (find)
            {
                a.enabled = true;
                return true;
            }
            else
            {
                if (a == this)
                {
                    find = true;
                }
            }
        }
        return false;
    }


    void over()
    {
        onOver();
        enabled = false;
    }


    public void copyData(BaseAction a)
    {
        a.enabled = enabled;
        a.info = info;
        a.time = time;
        a.openNext = openNext;
        a.waitOver = waitOver;
        a.ignore = ignore;
        a.runTarget = runTarget;
    }



    protected virtual void onStart()
    {
    }

    protected virtual void onOver()
    {

    }



    internal abstract void onCopyTo(BaseAction cloneto);
}
