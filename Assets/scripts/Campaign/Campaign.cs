using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

// TODO: See campaign gameobject Hierarchy
// - On load, insert the loaded campaign assets in the GUI
// - Dev campaign GUI to be able to click on missions
// - Control locked/unlocked missions

public class Campaign : MonoBehaviour
{
	[SerializeField]
	string Name;

	[SerializeField]
	int NbMissions;

	private List<Mission> missions;

	void Awake()
	{
		LoadMissions();
	}

	void LoadMissions()
	{
		string path = "campaign/copyrighted/" + Name + "/";
		string currentPath;

		for (int i = 0; i < NbMissions; i++)
		{
			currentPath = path + "M" + i + "/brief/";

			Sprite card = Resources.Load<Sprite>(currentPath + "card");
			if (!card) Debug.Log(currentPath + "briefCard.png not found!");

			Sprite leftImage = Resources.Load<Sprite>(currentPath + "left_image");
			if (!leftImage) Debug.Log(currentPath + "left_image.png not found!");

			Sprite rightImage = Resources.Load<Sprite>(currentPath + "right_image");
			if (!rightImage) Debug.Log(currentPath + "right_image.png not found!");

			TextAsset objectives = Resources.Load<TextAsset>(currentPath + "objectives");
			if (!objectives) Debug.Log(currentPath + "objectives.txt not found!");

			TextAsset overview = Resources.Load<TextAsset>(currentPath + "overview");
			if (!overview) Debug.Log(currentPath + "overview.txt not found!");

			VideoClip video = Resources.Load<VideoClip>(currentPath + "video");
			if (!video) Debug.Log(currentPath + "video.mp4 not found!");

			missions.Add(new Mission(card, leftImage, rightImage, objectives.text, overview.text, video));
		}
	}
}
