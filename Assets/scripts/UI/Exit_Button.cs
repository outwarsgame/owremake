using UnityEngine;

public class Exit_Button : ButtonSoundAction
{
	protected override void DoIt()
	{
		Application.Quit();
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}
}
