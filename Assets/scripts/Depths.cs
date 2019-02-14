using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depths : MonoBehaviour
{
	[SerializeField]
	AudioSource dies;

	[SerializeField]
	UserControl uc;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.CompareTo("Player") == 0)
		{
			dies.Play();
			uc.enabled = false;
		}
	}
}
