using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ObjectPooling;


[RequireComponent (typeof(AudioSource))]
public class PlayerAudioPlayerBehaviour : PoolObject
{
	private float _originalPitch;
	private AudioSource _source;

	private void Awake()
	{
		_source = GetComponent <AudioSource> ();
		_originalPitch = _source.pitch;
	}

	public override void OnObjectReuse(object parameters)
	{
		AudioSetting settingsForEvent = (AudioSetting)parameters;
		PlaySoundWithSettingsForEvent (settingsForEvent);
	}

	public void Update()
	{
		if (!_source.isPlaying)
		{
			gameObject.SetActive (false);
		}
	}

	private void PlaySoundWithSettingsForEvent(AudioSetting settingsForEvent)
	{
		_source.pitch = _originalPitch + Random.Range (-settingsForEvent.pitchVariety, settingsForEvent.pitchVariety);
		_source.volume = settingsForEvent.volume;
		_source.PlayOneShot (settingsForEvent.getClip ());
	}
}
