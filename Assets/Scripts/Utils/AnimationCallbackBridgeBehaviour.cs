using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Utils
{
	public class AnimationCallbackBridgeBehaviour : MonoBehaviour
	{
		public UnityEvent unityEvent;

		public void OnAnimationFrame()
		{
			unityEvent.Invoke ();
		}
	}
}