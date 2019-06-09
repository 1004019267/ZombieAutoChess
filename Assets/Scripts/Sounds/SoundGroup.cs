using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
	 * 声音的播放标签.里面包含了同一个标签下的所有声音的播放.<br/> 
	 * 操作播放标签类就可以对里面所有的声音进行统一的管理操作.<br/>
	 * @author Administrator
	 */
public class SoundGroup
{
    private float _voi = 1;
    private bool _mute = false;
    /**
         * 当前组中播放的声音,是否在声音加载完时候才可以播放.通常背景声音可以一边加载一边播放. 
         */		
    public bool loadOverToPlay;
    private Dictionary<SoundItem,SoundItem> allChannel = new Dictionary<SoundItem,SoundItem> ();
    /**
         * 是否启用单通道模式.如果是,则在短时间内,同一个声音只会被播放一次. <br>
         * <br>这里针对的是同一个声音.不同声音不受影响.<br>
         * 0:表示不启用单声道模式.
         * -1:表示当一个声音播放完毕后,才播放另一个声道.
         * >0表示在指定毫秒内,不再重复播放. 推荐100
         */		
    public float singleChangleDuration = 0f;
    private Hashtable singleChangleCatch = new Hashtable ();

    public SoundGroup(string name)
    {
        
    }

    public void stopAll()
    {
        foreach (KeyValuePair<SoundItem,SoundItem> kv in allChannel)
        {
            kv.Key.Stop();
        }
        allChannel.Clear();
    }

    internal void onSoundOver(SoundItem c)
    {
        allChannel.Remove(c);
        singleChangleCatch.Remove(c.player.url);
    }

    private void addSoundItem( SoundItem s)
    {
        allChannel[s] = s;
        s.group = this;
    }
    public float volume
    {
        get { return _voi; }
        set
        {
            if(_voi == value)
                return;
            _voi = value;
            foreach (KeyValuePair<SoundItem,SoundItem> kv in allChannel)
            {
                kv.Key.updateVolume();
            }

        }
    }

    public bool mute
    {
        get { return _mute; }
        set {
            if (_mute != value)
            {
                _mute = value;
                foreach (KeyValuePair<SoundItem,SoundItem> kv in allChannel)
                {
                    kv.Key.mute = value;
                }
            } 
        }
    }

    public SoundItem play(string url,Vector3 pos,bool loop=false)
    {
        GameObject obj = new GameObject ("SoundClip");
        obj.transform.position = pos;
        SoundItem s= play (url,loop,obj );
        if (s == null)
        {
            GameObject.DestroyObject (obj);
        } else
        {
            obj.transform.parent=s.player.playObject.transform;
        }
        return s;
    }
    public SoundItem play(string url, bool loop = false, GameObject obj = null)
    {
        if (string.IsNullOrEmpty(url))
        {
            return null;
        }

        if (singleChangleDuration != 0)
        {
            SoundItem I = (SoundItem) singleChangleCatch[url];
            if (I != null)
            {
                if (singleChangleDuration == -1 && !I.isOver())
                {
                    return null;
                }
                else if (Time.time - I.lastPlayTime < singleChangleDuration)
                {
                    return null;
                }
            }
        }
        SoundPlayer _player = SoundManager.getSound(url);
        SoundItem c = _player.playSound(loop, loadOverToPlay, obj);
        if (mute)
        {
            c.mute = mute;
        }

        addSoundItem(c);
        if (singleChangleDuration != 0)
        {
            singleChangleCatch[url] = c;
        }

        if (_voi != 1)
        {
            c.updateVolume();
        }
        return c;
    }


}