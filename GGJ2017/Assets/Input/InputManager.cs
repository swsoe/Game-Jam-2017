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

	string[] goodCSV;
	string[] badCSV;

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

	string[] letters =  {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","-","'"};

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

		for (int i = 0; i < pathGoodCSV.Length; i++) {
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

			for (int i = 0; i < pathGoodCSV.Length; i++) {

				characterLikes [i] = (System.Array.IndexOf (goodCSV, enteredWord) != -1);
				//
				if (characterLikes[i]) {
					likesSum++;
					Debug.Log ("Character " + i + " Likes " + enteredWord);
				}

				characterHates [i] = (System.Array.IndexOf (badCSV, enteredWord) != -1);
				//
				if (characterHates[i]) {
					likesSum--;
					Debug.Log ("Character " + i + " Hates " + enteredWord);
				}
			}

			usedWords.Add (enteredWord);
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
		adjustScoreEvent.Invoke (scoreSum);
		scoreSum = 0;
	}
		
}
