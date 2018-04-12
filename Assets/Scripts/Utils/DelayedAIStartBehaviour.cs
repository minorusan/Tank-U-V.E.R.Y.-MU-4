using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using UnityEngine.AI;


namespace Utils
{
	public class DelayedAIStartBehaviour : MonoBehaviour
	{
		public NavMeshAgent behaviour;
		public float delay;

		private void OnEnable()
		{
			Invoke ("DelayedStart", delay);
		}

		private void DelayedStart()
		{
			behaviour.enabled = true;
		}
	}
}

