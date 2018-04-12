using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utils
{
	public class BonusMovementBehaviour : MonoBehaviour
	{
		private float _timePassed;
		public float heightCoef = 1f;

		private void OnEnable()
		{
			_timePassed = 0f;
		}

		private void LateUpdate()
		{
			var position = transform.localPosition;
			_timePassed += Time.deltaTime;
			position.y = 2 + Mathf.Sin (_timePassed) * heightCoef;
			transform.localPosition = position;
		}
	}
}

