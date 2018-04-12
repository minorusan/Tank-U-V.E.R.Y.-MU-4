using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.GamePlay;


namespace Utils.UI
{
	public class GameEndedTextBehavior : MonoBehaviour
	{
		private void Start()
		{
			GameGlobalsBehaviour.playerHealth.didDieAtPosition += OnPlayerDeath;
		}

		void OnPlayerDeath(Vector3 obj)
		{
			transform.GetChild (0).gameObject.SetActive (true);
		}

		private void OnDisable()
		{
			GameGlobalsBehaviour.playerHealth.didDieAtPosition -= OnPlayerDeath;
		}
	}
}

