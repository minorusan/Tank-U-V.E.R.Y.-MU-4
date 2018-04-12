using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ObjectPooling;
using Core.GamePlay;


namespace Core.AI
{
	public class MobsSpawnerBehaviour : MonoBehaviour
	{
		private List<GameObject> _activeMonsters = new List<GameObject> ();

		public int maxCountOnScene;
		public GameObject[] monsters;
		public Transform spawnCenter;
		public float spawnRadius;

		void Start()
		{
			for (int i = 0; i < monsters.Length; i++)
			{
				PoolManager.Instance.CreatePool (monsters [i], 10);
			}
		}

		void Update()
		{
			if (_activeMonsters.Count <= maxCountOnScene)
			{
				var candidate = monsters [Random.Range (0, monsters.Length)];
				var position = Vector3.zero;
				do
				{
					position = RandomCircle (spawnCenter.position, spawnRadius);
				} while (Vector3.Distance (position, GameGlobalsBehaviour.player.transform.position) < 50);
				_activeMonsters.Add (PoolManager.Instance.ReuseObject (candidate, position, Quaternion.identity));
			}

			for (int i = 0; i < _activeMonsters.Count; i++)
			{
				if (!_activeMonsters [i].activeInHierarchy)
				{
					_activeMonsters.RemoveAt (i);
				}
			}
		}

		Vector3 RandomCircle(Vector3 center, float radius)
		{
			float ang = Random.value * 360f;
			Vector3 pos = Vector3.zero;
			pos.x = center.x + radius * Mathf.Sin (ang * Mathf.Deg2Rad);
			pos.z = center.z + radius * Mathf.Cos (ang * Mathf.Deg2Rad);
			pos.y = center.y;
			return pos;
		}
	}
}