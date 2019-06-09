using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using UnityEngine;
/**
	 * 一个声音播放通道. 
	 * @author Administrator
	 * 
	 */
public class SoundItem
{
    private SoundGroup _group;
    private SoundPlayer _player;
    private float _weaken = 2000;
    private float _x = 0;
    private float _y = 0;
    public bool autoRemove;
    private float _volume = 1;
    private AudioSource chanel;
    private bool loop = false;
    private float _lastPlayTime;

    public SoundItem(bool loop,SoundPlayer player,GameObject obj)
    {
        _lastPlayTime = Time.time;//getTimer();
//			if(times<=0)
//			{
//				times=int.MaxValue;
//			}
        this.loop = loop;
        this.chanel = player.getNextAudioSource (obj);
        this.chanel.loop = loop;
        this.chanel.Play ();
        _player = player;
        volume = 1;//chanel.soundTransform.volume;
        if (chanel == null)
        {
//				onPlayOver(new Event(Event.SOUND_COMPLETE));
//				Debug.Log(SoundItem+"声道用完"+player.url);
        } else
        {
//				chanel.addEventListener(Event.SOUND_COMPLETE,onPlayOver);
        }

    }

    public float  lastPlayTime
    {
        get
        {
            return _lastPlayTime;
        }
    }
    public void Stop()
    {
        if (chanel != null)
        {
            chanel.Stop();
        }
    }

    protected void Destroy()
    {
        chanel = null;
        _group = null;
        _player = null;
    }

    public bool isOver()
    {
        return chanel == null;
    }

    public void updateVolume()
    {
        if(chanel)
            return;
        if (_group != null)
        {
            chanel.volume = _volume * _group.volume;

        }
        else
        {
            chanel.volume = _volume;
        }
    }
    /// <summary>
    /// 设置音量
    /// </summary>
    public float volume
    {
        set
        {
            _volume = value;
            updateVolume();
        }
        get { return _volume; }
    }




    public SoundPlayer player
    {
        get { return _player; }
    }

    internal SoundGroup group
    {
        set { _group = value; }
        get { return _group; }
    }

    public AudioSource getSoundchanel()
    {
        return chanel;
    }

    public float x
    {
        get { return _x; }
        set { _x = value; }
    }

    public float y
    {
        get { return _y; }
        set { _y = value; }
    }

    public float weaken
    {
        get { return _weaken; }
        set { _weaken = value; }
    }

    public bool mute
    {
        set
        {
            if (!isOver())
            {
                chanel.mute = value;
            }
        }
        get
        {
            if (isOver())
            {
                return false;
            }
            else
            {
                return chanel.mute;
            }
        }
    }
}