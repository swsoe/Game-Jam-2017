using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class AdjustScoreEvent : UnityEvent<int>{}

[System.Serializable]
public class WaveEvent : UnityEvent{}

public class InputManager : MonoBehaviour {

	public static InputManager Instance;

	public AdjustScoreEvent adjustScoreEvent;
	public WaveEvent waveEvent;

	[Header("Character Data")]
	//public Transform[] characterPrefabs;
	public string[] pathGoodCSV;
	public string[] pathBadCSV;

	List<string> usedWords = new List<string> ();

	//string[] goodCSV;
	//string[] badCSV;

	string[] goodCSV0;
	string[] badCSV0;

	string[] goodCSV1;
	string[] badCSV1;

	string[] goodCSV2;
	string[] badCSV2;

	string[] goodCSV3;
	string[] badCSV3;

	string[] goodCSV4;
	string[] badCSV4;

	string[] goodCSV5;
	string[] badCSV5;

	string[] goodCSV6;
	string[] badCSV6;


	bool[] characterLikes;
	bool[] characterHates;

	int scoreSum = 0;


	[Header("Text Input")]

	public Text textInput;
	public Text holdText;

	CanvasGroup inputCanvas;
	CanvasGroup holdCanvas;

	Animator textAnim;

	string word = "";

	string[] letters =  {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","-","'","1","2","3","4","5","6","7","8","9"};

	bool toggleCarrot = true;

	bool deleteHeld = false;


	void Awake(){
		Instance = this;

	}

	// Use this for initialization
	void Start () {
		characterLikes = new bool[pathGoodCSV.Length];
		characterHates = new bool[pathGoodCSV.Length];
		textAnim = transform.parent.GetComponent<Animator> ();

		inputCanvas = textInput.transform.GetComponent<CanvasGroup> ();
		holdCanvas = holdText.transform.GetComponent<CanvasGroup> ();

		string baseFilePath = Application.streamingAssetsPath;

		Debug.Log ("Parsing " + baseFilePath + "/" + pathGoodCSV [0]);
		goodCSV0 = ParseCSV (baseFilePath + "/" + pathGoodCSV [0]);
		Debug.Log ("Parsing " + baseFilePath + "/" + pathBadCSV [0]);
		badCSV0 = ParseCSV (baseFilePath + "/" + pathBadCSV [0]);

		Debug.Log ("Parsing " + baseFilePath + "/" + pathGoodCSV [1]);
		goodCSV1 = ParseCSV (baseFilePath + "/" + pathGoodCSV [1]);
		Debug.Log ("Parsing " + baseFilePath + "/" + pathBadCSV [1]);
		badCSV1 = ParseCSV (baseFilePath + "/" + pathBadCSV [1]);

		Debug.Log ("Parsing " + baseFilePath + "/" + pathGoodCSV [2]);
		goodCSV2 = ParseCSV (baseFilePath + "/" + pathGoodCSV [2]);
		Debug.Log ("Parsing " + baseFilePath + "/" + pathBadCSV [2]);
		badCSV2 = ParseCSV (baseFilePath + "/" + pathBadCSV [2]);

		Debug.Log ("Parsing " + baseFilePath + "/" + pathGoodCSV [3]);
		goodCSV3 = ParseCSV (baseFilePath + "/" + pathGoodCSV [3]);
		Debug.Log ("Parsing " + baseFilePath + "/" + pathBadCSV [3]);
		badCSV3 = ParseCSV (baseFilePath + "/" + pathBadCSV [3]);

		Debug.Log ("Parsing " + baseFilePath + "/" + pathGoodCSV [4]);
		goodCSV4 = ParseCSV (baseFilePath + "/" + pathGoodCSV [4]);
		Debug.Log ("Parsing " + baseFilePath + "/" + pathBadCSV [4]);
		badCSV4 = ParseCSV (baseFilePath + "/" + pathBadCSV [4]);

		Debug.Log ("Parsing " + baseFilePath + "/" + pathGoodCSV [5]);
		goodCSV5 = ParseCSV (baseFilePath + "/" + pathGoodCSV [5]);
		Debug.Log ("Parsing " + baseFilePath + "/" + pathBadCSV [5]);
		badCSV5 = ParseCSV (baseFilePath + "/" + pathBadCSV [5]);

		Debug.Log ("Parsing " + baseFilePath + "/" + pathGoodCSV [6]);
		goodCSV6 = ParseCSV (baseFilePath + "/" + pathGoodCSV [6]);
		Debug.Log ("Parsing " + baseFilePath + "/" + pathBadCSV [6]);
		badCSV6 = ParseCSV (baseFilePath + "/" + pathBadCSV [6]);


//		for (int i = 0; i < pathGoodCSV.Length; i++) {
//			Debug.Log ("Parsing " + baseFilePath + "/" + pathGoodCSV [i]);
//			goodCSV = ParseCSV (baseFilePath + "/" + pathGoodCSV [i]);
//			Debug.Log ("Parsing " + baseFilePath + "/" + pathBadCSV [i]);
//			badCSV = ParseCSV (baseFilePath + "/" + pathBadCSV [i]);
//		}

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
		if(Input.GetKeyUp(KeyCode.Delete) || Input.GetKeyUp(KeyCode.Backspace)){
			deleteHeld = false;
			StopCoroutine ("DeleteDelay");
		}

		if(deleteHeld){
			Backspace ();
		}

		if(Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace)){
			Backspace ();
			StartCoroutine ("DeleteDelay");
		}

		//Submit Word
		if(Input.GetKeyDown(KeyCode.Return)){
			CheckWord (word);
			word = "";
			textInput.text = word;
		}

	}

	IEnumerator DeleteDelay(){
		Debug.Log ("DELAY");
		yield return new WaitForSeconds (.2f);
		Debug.Log ("DONE DELAY");
		deleteHeld = true;
	}

	void Backspace(){
		//deleteHeld = true;

		if(word.Length > 0){
			bool hasCarrot = false;
			if(word[word.Length - 1] == '|'){
				word = word.Substring (0, word.Length - 1);
				hasCarrot = true;
			}
			if(word.Length > 0){
				word = word.Substring (0, word.Length - 1);
			}

			if(hasCarrot){word += "|";}

			textInput.text = word;
		}
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
		foreach (string s in fileData) {
			//Debug.Log (s);
		}

		return fileData;
	}

	IEnumerator HoldText(){
		yield return new WaitForSeconds (.2f);
		holdCanvas.alpha = 0f;
		inputCanvas.alpha = 1f;
	}

	//see if word exists
	public void CheckWord(string enteredWord){

		enteredWord = enteredWord.ToLower ();
		enteredWord = enteredWord.Replace("'", string.Empty);
		enteredWord = enteredWord.Replace("-", string.Empty);


		holdText.text = textInput.text;
		holdCanvas.alpha = 1f;
		inputCanvas.alpha = 0f;
		StartCoroutine ("HoldText");


		int likesSum = 0;

		if(enteredWord[enteredWord.Length - 1] == '|'){
			enteredWord = enteredWord.Substring (0, enteredWord.Length - 1);
		}

		enteredWord = "\"" + enteredWord + "\"";

		if(!usedWords.Contains(enteredWord)){

			for (int p = 0; p < pathGoodCSV.Length; p++) {
				characterLikes [p] = false;
				characterHates [p] = false;
			}
				
			Debug.Log ("Checking " + enteredWord);

			characterLikes [0] = (System.Array.IndexOf (goodCSV0, enteredWord) != -1);
			if (characterLikes[0]) {
				likesSum++;
				Debug.Log ("Character " + "0" + " Likes " + enteredWord);
			}

			characterHates [0] = (System.Array.IndexOf (badCSV0, enteredWord) != -1);
			if (characterHates[0]) {
				likesSum--;
				Debug.Log ("Character " + "0" + " Hates " + enteredWord);
			}
				 
			characterLikes [1] = (System.Array.IndexOf (goodCSV1, enteredWord) != -1);
			if (characterLikes[1]) {likesSum++;}

			characterHates [1] = (System.Array.IndexOf (badCSV1, enteredWord) != -1);
			if (characterHates[1]) {likesSum--;}

			characterLikes [2] = (System.Array.IndexOf (goodCSV2, enteredWord) != -1);
			if (characterLikes[2]) {likesSum++;}

			characterHates [2] = (System.Array.IndexOf (badCSV2, enteredWord) != -1);
			if (characterHates[2]) {likesSum--;}

			characterLikes [3] = (System.Array.IndexOf (goodCSV3, enteredWord) != -1);
			if (characterLikes[3]) {likesSum++;}

			characterHates [3] = (System.Array.IndexOf (badCSV3, enteredWord) != -1);
			if (characterHates[3]) {likesSum--;}

			characterLikes [4] = (System.Array.IndexOf (goodCSV4, enteredWord) != -1);
			if (characterLikes[4]) {likesSum++;}

			characterHates [4] = (System.Array.IndexOf (badCSV4, enteredWord) != -1);
			if (characterHates[4]) {likesSum--;}

			characterLikes [5] = (System.Array.IndexOf (goodCSV5, enteredWord) != -1);
			if (characterLikes[5]) {likesSum++;}

			characterHates [5] = (System.Array.IndexOf (badCSV5, enteredWord) != -1);
			if (characterHates[5]) {likesSum--;}

			characterLikes [6] = (System.Array.IndexOf (goodCSV6, enteredWord) != -1);
			if (characterLikes[6]) {likesSum++;}

			characterHates [6] = (System.Array.IndexOf (badCSV6, enteredWord) != -1);
			if (characterHates[6]) {likesSum--;}



			//usedWords.Add (enteredWord);
		}

		if(likesSum > 0){
			textAnim.SetTrigger ("Correct");
		}
		if(likesSum < 0){
			textAnim.SetTrigger ("Wrong");
		}
		if(likesSum == 0){
			textAnim.SetTrigger ("Enter");
		}

		waveEvent.Invoke ();



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
		Debug.Log ("ADDING " + scoreSum + " POINTS");
		adjustScoreEvent.Invoke (scoreSum);
		scoreSum = 0;
	}
		
}
