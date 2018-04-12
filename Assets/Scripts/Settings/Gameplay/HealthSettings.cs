using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.GamePlay.Settings
{
	[CreateAssetMenu (fileName = "Player health settings", menuName = "Settings/Gameplay/Health settings")]
	public class HealthSettings : ScriptableObject
	{
		public float health;
		[Range (0f, 1f)]
		public float defence;
	}
}