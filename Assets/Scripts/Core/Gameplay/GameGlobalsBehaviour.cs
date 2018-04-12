using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.GamePlay
{
	public class GameGlobalsBehaviour : MonoBehaviour
	{
		public static GameGlobalsBehaviour instance
		{
			get;
			private set;
		}

		public static GameObject player
		{
			get;
			private set;
		}

		public static HealthBehaviour playerHealth
		{
			get;
			private set;
		}

		private void OnEnable()
		{
			instance = this;
			player = GameObject.FindGameObjectWithTag ("Player");
			playerHealth = player.GetComponentInChildren <HealthBehaviour> ();
		}

		private void OnDisable()
		{
			
		}
	}
}