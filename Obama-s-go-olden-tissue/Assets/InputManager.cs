using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

	public GameObject popUpInstance;
	public float turnSpeed = 10f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Pop-up appears.
	// IsReset? - True: Reset / False: List
	public void PopUpAppear(bool isReset){
		RectTransform canvasRT = GameObject.Find("Canvas").GetComponent<RectTransform>();
		popUpInstance = Instantiate(Resources.Load("PopUpBackground"), canvasRT.pivot, Quaternion.identity) as GameObject;
		
		// Disable the buttons
		Button[] canvasButtons = GameObject.Find("Canvas").GetComponentsInChildren<Button>();
		Debug.Log(canvasButtons);
		foreach( Button iterator in canvasButtons){
			iterator.interactable = false;
		}

		// Setting properties of the Pop-up 
		popUpInstance.transform.SetParent((GameObject.Find("Canvas")).transform, false);
		if(isReset){
			// Message & "Yes" Button
			popUpInstance.transform.GetChild(0).GetComponent<Text>().text = "Reset Message";
			popUpInstance.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => ResetGame());
		}
		else{
			// Message & "Yes" Button
			popUpInstance.transform.GetChild(0).GetComponent<Text>().text = "List Message";
			popUpInstance.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => GotoList());
		}
		// "No" button
		popUpInstance.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => PopUpDisappear());  
	}
	// Pop-up Disappear (when No button is pressed)
	public void PopUpDisappear(){
		// Remove Pop-up
		Destroy(popUpInstance);

		// Enable the buttons
		Button[] canvasButtons = GameObject.Find("Canvas").GetComponentsInChildren<Button>();
		foreach( Button iterator in canvasButtons){
			iterator.interactable = true;
		}
	}
	public void ResetGame(){

	}
	public void GotoList(){
		
	}
	public void Update(){
		GameObject.Find("tissue").transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
	}
}
