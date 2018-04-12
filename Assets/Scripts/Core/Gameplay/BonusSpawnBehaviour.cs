using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ObjectPooling;


namespace Core.GamePlay
{
	public class BonusSpawnBehaviour : MonoBehaviour
	{
		[Range (0f, 1f)]
		public float spawnChance;
		public GameObject bonus;

		private void Awake()
		{
			HealthBehaviour.didDieAtPositionGlobal += OnMonsterDied;
			PoolManager.Instance.CreatePool (bonus, 30);
		}

		void OnMonsterDied(Vector3 obj)
		{
			if (Random.value > (1f - spawnChance))
			{
				PoolManager.Instance.ReuseObject (bonus, obj, Quaternion.identity);
			}
		}

		private void OnDisable()
		{
			HealthBehaviour.didDieAtPositionGlobal -= OnMonsterDied;
		}
	}
}