using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.GamePlay.Settings
{
	[CreateAssetMenu (fileName = "New weapon settings", menuName = "Settings/Gameplay/Weapons/New weapon settings")]
	public class WeaponSettings : ScriptableObject
	{
		public float range;
		public float damage;
		[Tooltip ("Specify implementation and usage details for each weapon individually. Intended usage example:Increase damage by" +
			"channeling time.")]
		public float multiplier;
		[Tooltip ("Specify implementation and usage details for each weapon individually. Intended usage example:Charging")]
		public float channelTime;
		public float projectileSpeed;
		public float aoeRadius;

		public EAudioEventType launchSound;
		public EAudioEventType explodeSound;

		public Vector3 projectileLaunchOffset;
		public GameObject projectile;
		public GameObject crosshairPrefab;
	}
}