using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Core.GamePlay;


namespace Core.AI
{
	[CreateAssetMenu (fileName = "New wandering state", menuName = "AIStates/Wandering state")]
	public sealed class WanderingStateDefinition : AIStateBase
	{
		private NavMeshAgent _agent;
		private Vector3 _prevDestination;

		public float stoppingDistance;

		#region implemented abstract members of AIStateBase

		public override void OnAnimationCallback()
		{
			
		}

		public override AIStateBase TransitTo()
		{
			return transitions [0];
		}

		public override bool IsCompleted()
		{
			var dist = Vector3.Distance (_body.transform.position, GameGlobalsBehaviour.player.transform.position);
			return dist < stoppingDistance;
		}

		public override void OnEnter(GameObject fragileBody)
		{
			_body = fragileBody;

			_animator = _body.GetComponentInChildren <Animator> ();
			_agent = _body.GetComponent <NavMeshAgent> ();

			_animator.SetBool ("walks", true);
		}

		public override void Update()
		{
			if (_agent.enabled)
			{
				_agent.isStopped = false;
				var playerPos = GameGlobalsBehaviour.player.transform.position;

				if (Vector3.Distance (playerPos, _prevDestination) > stoppingDistance)
				{
					_agent.SetDestination (playerPos);
					if (Random.value > 0.99f)
					{
						PlayerAudioBehaviour.PlaySound (EAudioEventType.zombieGrowl, _body.transform.position);
					}
					_prevDestination = playerPos;
				}
			}
		}

		public override void OnLeave()
		{
			if (_agent.enabled && _agent.isOnNavMesh)
			{
				_agent.isStopped = true;
			}

			_animator.SetBool ("walks", false);
		}

		#endregion
		
	}
}