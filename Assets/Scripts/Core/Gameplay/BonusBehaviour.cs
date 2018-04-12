using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.GamePlay
{
	public class BonusBehaviour : MonoBehaviour
	{
		public float healAmount = 20f;
		public float timeExists = 2f;

		private void OnEnable()
		{
			Invoke ("DeactivateDelayed", timeExists);
		}

		private void OnCollisionEnter(Collision col)
		{
			col.gameObject.GetComponentInChildren <HealthBehaviour> ().Heal (healAmount);
			gameObject.SetActive (false);
		}

		private void DeactivateDelayed()
		{
			gameObject.SetActive (false);
		}
	}
}

