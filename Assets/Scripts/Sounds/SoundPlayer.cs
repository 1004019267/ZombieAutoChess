using System.Collections.Generic;
using UnityEngine;
/**
	 * 一个声音的播放器.只能播放一个声音文件. 
	 * @author Administrator
	 * 
	 */	
public class SoundPlayer
{
    internal int soundCount = 0;
    private bool _loadOver;
    private string urlStr;
    private bool isError = false;
    /**
    * 音量 
     */		
    private float _voi = 1;
    private Dictionary<SoundItem,SoundItem> allChannel = new Dictionary<SoundItem,SoundItem> ();
//	private List<AudioSource> usedPool;
    private List<AudioSource> unUsedPool;
    private AudioClip clip;
    internal GameObject playObject;

    public SoundPlayer(string url)
    {
        urlStr = url;
        clip = ResourcesManager.Load<AudioClip>(url);
        if (clip == null)
        {
            Debug.LogError("not find sound file:" + url);
            return;
        }
        unUsedPool = new List<AudioSource>();
        allChannel = new Dictionary<SoundItem, SoundItem>();
        playObject = new GameObject(url);
        playObject.transform.parent = SoundManager.soundPlayerObject.transform;
    }


    public string url
    {
        get { return urlStr; }
    }

    public bool loadOver
    {
        get { return _loadOver; }
    }

    public float volume
    {
        set
        {
            if(value != _voi)
                return;
            float off = value / _voi;
            foreach (KeyValuePair<SoundItem,SoundItem> kv in allChannel)
            {
                kv.Key.volume *= off;
            }
            _voi = value;
        }
        get { return _voi; }
    }
    
    public SoundItem playSound(bool loop = false, bool onLoadOver = true, GameObject obj = null)
    {
        if (!loop && onLoadOver && !loadOver)
        {
            return null;
        }
        SoundItem s = new SoundItem(loop,this,obj);
        if (s.volume != 1)
        {
            volume = s.volume;
        }
        allChannel[s] = s;
        return s;
    }

    internal void onSoundOver(SoundItem c, bool remove = true)
    {
        soundCount--;
        GameObject o = c.getSoundchanel().gameObject;
        if (o == playObject)
        {
            unUsedPool.Add(c.getSoundchanel());
        }
        else if (o.name == "SoundClip")
        {
            GameObject.DestroyObject(o);
        }
        else
        {
            GameObject.Destroy(c.getSoundchanel());
        }

        if (c.group != null)
        {
            c.group.onSoundOver(c);
        }

        if (remove)
        {
            allChannel.Remove(c);
        }
        
    }

    internal AudioSource getNextAudioSource(GameObject obj)
    {
        AudioSource source;
        if (obj)
        {
            source = obj.AddComponent<AudioSource>();
            source.volume = volume;
            source.minDistance = 0;
            source.maxDistance = 30;
            source.clip = clip;
            source.playOnAwake = false;
            source.rolloffMode = AudioRolloffMode.Linear;
            return source;
        }
        else
        {
            if (unUsedPool.Count != 0)
            {
                source = unUsedPool[unUsedPool.Count - 1];
                unUsedPool.RemoveAt(unUsedPool.Count - 1);
                source.enabled = true;
                return source;
            }
            source = playObject.AddComponent <AudioSource> ();
            source.clip = clip;
            source.volume = volume;
            source.playOnAwake = false;
            source.minDistance=10000;
            return source;
        }
        
        
    }

    public void gc()
    {
        if (allChannel.Count == 0)
        {
            return;
        }
        List<SoundItem> _list = new List<SoundItem>();
             
        foreach (KeyValuePair<SoundItem,SoundItem> kv in allChannel)
        {
            if (kv.Value.isOver())
            {
                _list.Add(kv.Value);
            }
            else if (!kv.Value.getSoundchanel().isPlaying)
            {
                _list.Add(kv.Value);
                onSoundOver(kv.Value,false);
            }
        }
        if (_list.Count != 0)
        {
            int length = _list.Count;
            for (int i = 0; i < length; i++)
            {
                allChannel.Remove(_list[i]);
            }
        }
    }

    public void StopAll()
    {
        foreach (KeyValuePair<SoundItem,SoundItem> kv in allChannel)
        {
            kv.Key.Stop();
        }
        allChannel.Clear();
    }
    
}