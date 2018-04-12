using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.GamePlay;


namespace Utils
{
	public class PlayerDeathEffectBehaviour : MonoBehaviour
	{
		private void Start()
		{
			GameGlobalsBehaviour.playerHealth.didDieAtPosition += OnPlayerDeath;
		}

		void OnPlayerDeath(Vector3 obj)
		{
			transform.position = obj;
		}

		private void OnDisable()
		{
			GameGlobalsBehaviour.playerHealth.didDieAtPosition -= OnPlayerDeath;
		}
	}

}
