using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utils
{
	public static class PhysicsHelpers
	{

		public static Vector3 velocityForBasketBallThrow(Vector3 origin, Vector3 target, float height)
		{
			float displacementY = target.y - origin.y;
			Vector3 displacementXZ = new Vector3 (target.x - origin.x, 0f, target.z - origin.z);
			float time = Mathf.Sqrt (-2f * height / Physics.gravity.y) + Mathf.Sqrt (2f * (displacementY - height) / Physics.gravity.y);
			Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * Physics.gravity.y * height);
			Vector3 velocityXZ = displacementXZ / time;

			return velocityXZ + velocityY * -Mathf.Sign (Physics.gravity.y);
		}
	}

}
