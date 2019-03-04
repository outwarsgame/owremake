using UnityEngine.SceneManagement;

public class TestZone_Button : ButtonSoundAction
{
	protected override void DoIt()
	{
		SceneManager.LoadScene("TestZone");
	}
}
