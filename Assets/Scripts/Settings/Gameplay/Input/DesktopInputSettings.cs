using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Settings.GamePlay.Input
{
	[CreateAssetMenu (fileName = "Player input settings", menuName = "Settings/Gameplay/Input/Input settings")]
	public class DesktopInputSettings : AInputSettingsBase
	{
		public float mouseSensevity = 1f;
		public float mouseDragTrashHold = 0.01f;
	}
}