using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Settings.GamePlay.Input
{
	//Base calss for any input
	public abstract class AInputSettingsBase : ScriptableObject
	{
		public InputSchemeSettings inputSceme;
		public float speedDump;
		public float speedMultiplier;
		public float maxVelocityGain;
	}
}