using UnityEngine;

public class ResumeGame_Button : ButtonSoundAction
{
	[SerializeField]
	GameObject SoloCampaign;

	[SerializeField]
	GameObject SoloCampaignBrief;

	[SerializeField]
	AudioSource Music;

	protected override void DoIt()
	{
		SoloCampaignBrief.SetActive(true);
		Music.Stop();
		SoloCampaign.SetActive(false);
	}
}
