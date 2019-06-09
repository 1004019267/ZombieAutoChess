using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//无用
public class baseSuper: MonoBehaviour {

	private AudioSource audioSource;
	private AudioClip[] audio;
	protected GameObject heroObject;

	public void init(AudioSource _audioSource, AudioClip[] _audio, GameObject _heroObject)
	{
		audioSource = _audioSource;
		audio = _audio;
		heroObject = _heroObject;
		
		
	}

	protected void PlaySound()
	{
		
	}


	protected void Move()
	{
		
	}

	protected void Attack()
	{
		
	}

	protected void Hit()
	{
		
	}


	protected void Dead()
	{
		
	}

	protected void idle()
	{
		
	}
	public virtual void Action()
	{
		
	}
}
