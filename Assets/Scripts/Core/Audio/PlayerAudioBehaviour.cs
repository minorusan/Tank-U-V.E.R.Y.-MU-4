using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ObjectPooling;


public class PlayerAudioBehaviour : MonoBehaviour
{
	private static PlayerAudioBehaviour _instance;

	private float _originalPitch;

	public PlayerAudioPlayerBehaviour prefab;
	public AudioSettings settings;

	private void Start()
	{
		_instance = this;
		PoolManager.Instance.CreatePool (prefab.gameObject, 40);
	}

	public static void PlaySound(EAudioEventType type, Vector3 position)
	{
		_instance.PlaySoundAtPosition (type, position);
	}

	public void PlaySoundAtPosition(EAudioEventType type, Vector3 position)
	{
		var settingsForEvent = settings.settingsForEventType (type);
		PoolManager.Instance.ReuseObject (prefab.gameObject, position, Quaternion.identity, settingsForEvent);
	}

	public void PlaySound(EAudioEventType type)
	{
		var settingsForEvent = settings.settingsForEventType (type);
		PoolManager.Instance.ReuseObject (prefab.gameObject, transform.position, Quaternion.identity, settingsForEvent);
	}
}
