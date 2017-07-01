using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovieOnUI : MonoBehaviour{
	
	void Start()
	{
		Handheld.PlayFullScreenMovie("cream.mov", Color.black, FullScreenMovieControlMode.CancelOnInput);
	}

	// Update is called once per frame
	void Update()
	{

	}

	}
