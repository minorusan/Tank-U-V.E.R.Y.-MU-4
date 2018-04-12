using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Settings.GamePlay.Input
{
	[SerializableAttribute]
	public struct SwapWeaponsGroup
	{
		public KeyCode swapForward;
		public KeyCode swapBack;
	}

	[CreateAssetMenu (fileName = "Player input scheme settings", menuName = "Settings/Gameplay/Input/Input scheme settings")]
	public class InputSchemeSettings : ScriptableObject
	{
		[Header ("Movement")]
		public KeyCode right;
		public KeyCode left;
		public KeyCode back;
		public KeyCode forward;
		public int beginRotateButtonIndex;

		[Header ("Swap weapons")]
		public SwapWeaponsGroup swapWeaponsSetup;
		public KeyCode shootKey;
	}
}