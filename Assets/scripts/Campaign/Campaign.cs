using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.Diagnostics;

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
		missions = new List<Mission>();

		//LoadMissions();
		LoadOriginalMissionsFromCD();
	}

	// TELL THE USER TO INSTALL IMAGEMAGICK
	void LoadOriginalMissionsFromCD()
	{
		//Process.Start(Environment.CurrentDirectory + @"\Assets\converter.bat");
		Process.Start(Application.dataPath + "/tools/converter.bat");
	}

	//void LoadMissions()
	//{
	//	string path = "campaign/copyrighted/" + Name + "/";
	//	string currentPath = "";

	//	Sprite card, leftImage, rightImage;
	//	TextAsset objectives, overview;
	//	VideoClip video;

	//	try
	//	{
	//		for (int i = 0; i < NbMissions; i++)
	//		{
	//			currentPath = path + "M" + i + "/brief/";

	//			card = Resources.Load<Sprite>(currentPath + "card");
	//			if (card == null) throw new Exception(currentPath + "card not found");

	//			leftImage = Resources.Load<Sprite>(currentPath + "left_image");
	//			if (leftImage == null) throw new Exception(currentPath + "left_image not found");

	//			rightImage = Resources.Load<Sprite>(currentPath + "right_image");
	//			if (rightImage == null) throw new Exception(currentPath + "right_image not found");

	//			objectives = Resources.Load<TextAsset>(currentPath + "objectives");
	//			if (objectives == null) throw new Exception(currentPath + "objectives not found");

	//			overview = Resources.Load<TextAsset>(currentPath + "overview");
	//			if (overview == null) throw new Exception(currentPath + "overview not found");

	//			video = Resources.Load<VideoClip>(currentPath + "video");
	//			if (video == null) throw new Exception(currentPath + "video not found");

	//			missions.Add(new Mission(card, leftImage, rightImage, objectives.text, overview.text, video));
	//		}
	//	}
	//	catch (Exception e)
	//	{
	//		UnityEngine.Debug.Log("Exception " + e.Message + " occurred.");
	//	}
	//}
}



