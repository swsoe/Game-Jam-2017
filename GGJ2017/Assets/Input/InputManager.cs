using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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

	int scoreSum = 0;


	[Header("Text Input")]

	public Text textInput;
	string word = "";

	string[] letters =  {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","-","'"};

	bool toggleCarrot = true;




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

		InvokeRepeating("FlashCarrot", 1f, .5f);


	}
	
	// Update is called once per frame
	void Update () {

		//Check for valid text input
		for(int i = 0; i < letters.Length; i++){
			//Debug.Log("CHECKING " + letters[i]);
			if(Input.GetKeyDown(letters[i])){
				UpdateWord (letters [i]);
			}
		}
			
		if(Input.GetKeyDown(KeyCode.Space)){
			UpdateWord(" ");
		}

		//Backspace
		if(Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace)){
			if(word.Length > 0){
				bool hasCarrot = false;
				if(word[word.Length - 1] == '|'){
					word = word.Substring (0, word.Length - 1);
					hasCarrot = true;
				}
				word = word.Substring (0, word.Length - 1);

				if(hasCarrot){word += "|";}

				textInput.text = word;
			}
		}

		//Submit Word
		if(Input.GetKeyDown(KeyCode.Return)){
			CheckWord (word);
			word = "";
			textInput.text = word;
		}
		


//		if(Input.GetKeyDown("space")){
//			CheckWord ("dumb");
//		}
//		if(Input.GetKeyDown(KeyCode.Return)){
//			CheckCharacter (0);
//		}
	}

	void UpdateWord(string newLetter){
		if(word.Length < 40){
			
			bool hasCarrot = false;

			if(word.Length > 0){
				if(word[word.Length - 1] == '|'){
					word = word.Substring (0, word.Length - 1);
					hasCarrot = true;
				}
			}

			word += newLetter;

			if(hasCarrot){word += "|";}

			textInput.text = word;
		}

	}

	void FlashCarrot(){
		if(toggleCarrot){
			word += "|";
			textInput.text = word;
		}
		else{
			if(word.Length > 0){
				word = word.Substring (0, word.Length - 1);
				textInput.text = word;
			}
		}
		toggleCarrot = !toggleCarrot;
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

	//see if word exists
	public void CheckWord(string enteredWord){

		if(enteredWord[enteredWord.Length - 1] == '|'){
			enteredWord = enteredWord.Substring (0, enteredWord.Length - 1);
		}

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
	public int CheckCharacter(int characterIndex){
		if(characterLikes[characterIndex]){
			scoreSum++;
			Debug.Log ("LIKES");
			return 1;
		}
		if(characterHates[characterIndex]){
			scoreSum--;
			Debug.Log ("HATES");
			return -1;
		}
		return 0;

	}

	//send the total score after the wave completes
	public void SendScore(){
		adjustScoreEvent.Invoke (scoreSum);
		scoreSum = 0;
	}
		
}
