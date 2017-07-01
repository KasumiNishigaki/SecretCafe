using UnityEngine;
using System.Collections;

[System.Serializable] //これを書くとinspectorに表示される。エディタで設定する用。
public struct CharaStatus{
	public string Name;
	public int Bitter;
	//public int Dark;
	//public Sprite CharaImage;
	public Sprite[] BlinkImages;

	public int RequiredLevel;

	public int StartComingHour;
	public int EndComingHour;
}

public class Character {
	public string Name;
	public int Bitter;
	public Sprite[] BlinkImages;

	public int RequiredLevel;

	public int StartComingHour;
	public int EndComingHour;

	public Character (string name, int bitter,  Sprite[] blinkImages, int requiredLevel, int startComingHour, int endComingHour) {
		Name = name;
		Bitter = bitter;
		BlinkImages = blinkImages;

		RequiredLevel = requiredLevel;

		StartComingHour = startComingHour;
		EndComingHour = endComingHour;
	}
}


//Sprite charaImage,