using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Threading;
using UnityEngine.AI;


namespace Core.AI
{
	public class ArtifitialIntelligenseBehavior : MonoBehaviour
	{
		private AIStateBase _current;

		public AIStateBase startState;
		public AIStateBase[] states;

		#region Monobehavior

		private void OnEnable()
		{
			_current = Instantiate (startState);
			_current.OnEnter (gameObject);
			if (UnityEngine.Random.value > 0.99f)
			{
				PlayerAudioBehaviour.PlaySound (EAudioEventType.zombieGrowl, transform.position);
			}
		}

		private void Update()
		{
			_current.Update ();
			if (_current.IsCompleted ())
			{
				_current.OnLeave ();
				var transitsTo = _current.TransitTo ();
				if (transitsTo != null && states.Contains (transitsTo))
				{
					Destroy (_current);
					_current = Instantiate (transitsTo);

					_current.OnEnter (gameObject);
				}
				else
				{
					Debug.LogError (string.Format ("Next logical step for {0} is oblivion!", gameObject.name));
					Destroy (gameObject);
				}
			}
		}

		private void OnDisable()
		{
			_current.OnLeave ();
			PlayerAudioBehaviour.PlaySound (EAudioEventType.zombieMoan, transform.position);
			GetComponent <NavMeshAgent> ().enabled = false;
		}

		private void OnValidate()
		{
			if (states != null && states.Length > 0 && startState != null)
			{
				for (int i = 0; i < states.Length; i++)
				{
					if (states [i] == startState)
					{
						return;
					}
				}


				Debug.LogError ("Start state must be in states list.");
			}
		}

		public void AnimationCallback()
		{
			_current.OnAnimationCallback ();
		}

		#endregion
	}

	public abstract class AIStateBase:ScriptableObject
	{
		protected GameObject _body;
		protected Animator _animator;

		public AIStateBase[] transitions;

		public abstract AIStateBase TransitTo();

		public abstract bool IsCompleted();

		public abstract void OnEnter(GameObject fragileBody);

		public abstract void Update();

		public abstract void OnLeave();

		public abstract void OnAnimationCallback();
	}
}