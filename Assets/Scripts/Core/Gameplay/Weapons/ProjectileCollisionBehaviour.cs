using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.GamePlay.Weapons
{
	public class ProjectileCollisionBehaviour : MonoBehaviour
	{
		public GameObject explosionParticles;
		public float damage;
		public float aoeRadius;
		public EAudioEventType crashSound;

		private void OnEnable()
		{
			var body = GetComponent <Rigidbody> ();
			if (body != null)
			{
				body.velocity = Vector3.zero;
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			Explode ();
			gameObject.SetActive (false);
			PlayerAudioBehaviour.PlaySound (crashSound, transform.position);
			var health = collision.gameObject.GetComponentInChildren <HealthBehaviour> ();
			if (health != null)
			{
				health.SubstractHealth (damage);
			}

			if (aoeRadius > 0f)
			{
				var colliders = Physics.OverlapSphere (transform.position, aoeRadius);
				for (int i = 0; i < colliders.Length; i++)
				{
					health = colliders [i].gameObject.GetComponentInChildren <HealthBehaviour> ();
					if (health == null || health.tag == "Player")
					{
						continue;
					}
					health.SubstractHealth (damage);
				}
			}
		}

		private void Explode()
		{
			var newParticles = Instantiate (explosionParticles);
			newParticles.transform.position = transform.position;
			newParticles.SetActive (true);
		}
	}
}