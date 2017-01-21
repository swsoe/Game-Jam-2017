using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.IO;

[System.Serializable]
public class AdjustScoreEvent : UnityEvent<int>{}

public class InputManager : MonoBehaviour {

	public static InputManager Instance;

	public AdjustScoreEvent adjustScoreEvent;

	[Header("Character Data")]
	public Transform[] characterPrefabs;
	public string[] pathGoodCSV;
	public string[] pathBadCSV;

	string[] goodCSV;
	string[] badCSV;

	bool[] characterLikes;
	bool[] characterHates;

	void Awake(){
		Instance = this;

	}

	// Use this for initialization
	void Start () {
		characterLikes = new bool[characterPrefabs.Length];
		characterHates = new bool[characterPrefabs.Length];


		string baseFilePath = Application.streamingAssetsPath;

		for (int i = 0; i < characterPrefabs.Length; i++) {
			Debug.Log ("Parsing " + baseFilePath + "/" + pathGoodCSV [i]);
			goodCSV = ParseCSV (baseFilePath + "/" + pathGoodCSV [i]);
			Debug.Log ("Parsing " + baseFilePath + "/" + pathBadCSV [i]);
			badCSV = ParseCSV (baseFilePath + "/" + pathBadCSV [i]);
		}


	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space")){
			CheckWord ("dumb");
		}
		if(Input.GetKeyDown(KeyCode.Return)){
			CheckCharacter (0);
		}
	}

	//Parses the CSV Library for a character
	string[] ParseCSV(string filePath){

		string[] fileData = File.ReadAllLines (filePath);
		//string[] lines = fileData.Split("\n"[0]);
		//var lineData : String[] = (lines[0].Trim()).Split(","[0]);

		foreach (string s in fileData) {
			Debug.Log (s);
		}

		return fileData;
	}

	public void CheckWord(string enteredWord){

		enteredWord = "\"" + enteredWord + "\"";

		Debug.Log ("Checking " + enteredWord);

		for (int i = 0; i < characterPrefabs.Length; i++) {

			characterLikes [i] = (System.Array.IndexOf (goodCSV, enteredWord) != -1);
			//characterLikes[i] = goodCSV.Contains (enteredWord);
			if (characterLikes[i]) {
				Debug.Log ("Character " + characterPrefabs [i].name + " Likes " + enteredWord);
			}

			characterHates [i] = (System.Array.IndexOf (badCSV, enteredWord) != -1);
			//characterHates[i] = badCSV.Contains (enteredWord);
			if (characterHates[i]) {
				Debug.Log ("Character " + characterPrefabs[i].name + " Hates " + enteredWord);
			}
		}
	}

	//Character Checks to see how to react
	public void CheckCharacter(int characterIndex){
		if(characterLikes[characterIndex]){
			adjustScoreEvent.Invoke (1);
			Debug.Log ("LIKES");
		}
		if(characterHates[characterIndex]){
			adjustScoreEvent.Invoke (-1);
			Debug.Log ("HATES");
		}

	}
		
}
