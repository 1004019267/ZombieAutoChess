using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;
/**
	 * 用于播放声音的管理类. 针对游戏开发对声音进行了详细的分类管理.
	 * <br/>可以方便的对某一类声音进行停止,调整音量等操作.
	 * <br/>游戏中的声音可能分为背景音乐,音效,UI音效.
	 * @author Administrator
	 * 
	 */
public class SoundManager
{
    internal static Hashtable allSound = new Hashtable ();
    private static Hashtable allGroupInfo = new Hashtable ();
    private static float _volume = 1;
    internal static GameObject soundPlayerObject;
    private static SoundGroup _bg;
    private static SoundGroup _effect;
    private static AudioListener listener;

    private static void init()
    {
	    GameObject obj = GameObject.Find("SoundPlayerObject");
	    if (obj != null)
	    {
		    GameObject.DestroyObject(obj);
	    }
	    soundPlayerObject = new GameObject("SoundPlayerObject");
	    GameObject.DontDestroyOnLoad(soundPlayerObject);
	    soundPlayerObject.AddComponent<SoundRetriever>();
	    if (listener == null)
	    {
		    resetListener();
	    }
    }

    public static void resetListener()
    {
	    Camera[] _cameras = Camera.allCameras;
	    for (int i = 0; i < _cameras.Length; i++)
	    {
		    GameObject o = _cameras[i].gameObject;
		    AudioListener a = o.GetComponent<AudioListener>();
		    if (a != null)
		    {
			    setListener(a);
		    }
	    }

	    if (listener == null)
	    {
		    if (Camera.main)
		    {
			    setListener(Camera.main.gameObject);
		    }
		    else
		    {
			    setListener(soundPlayerObject);
		    }
	    }
	    
	    
	    
	    
    }
    
    public static void setListener(GameObject target)
    {

	    if (listener)
	    {
		    GameObject.Destroy(listener);
	    }
	    listener = target.AddComponent<AudioListener>();
    }

    public static void setListener(AudioListener _audioListener)
    {
	    if (listener)
	    {
		    GameObject.Destroy(listener);
	    }

	    listener = _audioListener;
    }

    
    public static SoundGroup bg
    {
	    get
	    {
		    if (_bg == null)
		    {
			    _bg = getSoundGroup ("bg");
		    }
		    return _bg;
	    }
    }

    public static SoundGroup effect
    {
	    get
	    {
		    if (_effect == null)
		    {
			    _effect = getSoundGroup ("effect");
		    }
		    return _effect;
	    }
    }

    public static bool onReleaseSound (string url)
    {
	    SoundPlayer sound = (SoundPlayer)allSound [url];
	    if (sound.soundCount <= 0)
	    {
		    allSound.Remove (url);
		    return true;
	    } else
	    {
		    return false;
	    }
    }
    /**
	     * 播放指定的声音.并且可以指定一个控制组. 
	     * @param url
	     * @param times 0值播一次.-1无限循环
	     * @param group
	     * @return 返回一个声音元素.
	     */
    public static SoundItem play (string url, bool loop =false, string group="effect")
    {
	    SoundGroup lb = getSoundGroup (group);
	    return lb.play (url, loop);
    }
    public static void setGroupVolume (string name, float value=1, int time=1000)
    {
	    getSoundGroup (name).volume = value;
    }
    
    public static SoundPlayer getSound(string url)
    {
	    SoundPlayer s = (SoundPlayer) allSound[url];
	    if (s == null)
	    {
		    s = new SoundPlayer(url);
		    allSound[url] = s;
	    }
	    return s;
    }

    public static SoundGroup getSoundGroup(string name = "effect")
    {
	    if (soundPlayerObject == null)
	    {
		    init();
	    }
	    SoundGroup s = (SoundGroup) allGroupInfo[name];
	    if (s == null)
	    {
		    s = new SoundGroup(name);
		    allGroupInfo[name] = s;
	    }
	    return s;
    }

    public static void stopSound(string url)
    {
	    SoundPlayer s = getSound(url);
	    if (s != null)
	    {
		    s.StopAll();
	    }
    }
    
    public static void stopGroup(string name)
    {
	    getSoundGroup(name).stopAll();
    }
    
    public static void stopAll ()
    {
	    foreach (SoundPlayer p in allSound.Values)
	    {
		    p.StopAll(); 
	    }
    }
    
    public static float  volume
    {
	    get
	    {
		    return _volume;
	    }
	    set
	    {
		    _volume = value;
		    AudioListener.volume = _volume;
	    }
    }

    private static bool _mute;
    public static bool mute
    {
	    get
	    {
		    return _mute;
	    }	
	    set
	    {
		    if (value != _mute)
		    {
			    _mute = value;
			    foreach (SoundGroup g in allGroupInfo.Values)
			    {
				    g.mute = value;
			    }
		    }
	    }
    }
    public static void gc ()
    {
	    foreach (SoundPlayer p in allSound.Values)
	    {
		    p.gc ();
	    }
    }
    
}