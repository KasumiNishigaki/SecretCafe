using UnityEngine;
using SimpleJSON;

public class CafeGameUtil : MonoBehaviour {
	
	// Resources直下にあるjsonファイルを読む.
	public static JSONNode LoadJSON (string fileName) {
		JSONNode json;
		var fileText = Resources.Load(fileName) as TextAsset;

		json = JSONNode.Parse(fileText.text);

		return json;
	}

	public static bool IsVisited (string charaName) {
		return ES2.Exists (charaName) && ES2.Load <bool> (charaName);
	}

	public static void SaveIsVisited (string charaName) {
		ES2.Save (true, charaName);
	}
}
