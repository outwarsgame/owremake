using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public struct Mission
{
	/* Sequence:
	* 1. metacard? => campaign?
	* 2. introvideo? => campaign?
	* 3. card
	* 4. Audio: "Standby for mission briefing"
	* 5. Briefing screen with:
	*		briefVideo (.mp4)
	*		ImageTopLeft (sprite)
	*		ImageTopRight (sprite)
	*		TextOverview (.txt)
	*		TextObjectives (.txt)
	*/

	Sprite card;
	Sprite leftImage;
	Sprite rightImage;
	string objectives;
	string overview;
	VideoClip video;

	public Mission(Sprite card, Sprite leftImage, Sprite rightImage, string objectives, string overview, VideoClip video)
	{
		this.card = card;
		this.leftImage = leftImage;
		this.rightImage = rightImage;
		this.objectives = objectives;
		this.overview = overview;
		this.video = video;
	}
}