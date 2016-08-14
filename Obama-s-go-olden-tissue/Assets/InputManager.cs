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
    public void OnSelectionButtonClicked(Button button)
    {
        if (GameManager.Inst().getState() == State.IDLE && GameManager.Inst().getSelectionNum()[(int)button.GetComponentInChildren<PassPeople>().getPeople() - 2] > 0)
        {
            GameManager.Inst().setState(State.MOVING);
            GameManager.Inst().setMovingPeople(button.GetComponentInChildren<PassPeople>());
        }
    }
    public void OnBlankButtonClicked(Button button)
    {
        if (GameManager.Inst().getState() == State.MOVING && button.GetComponentInChildren<PassPeople>().people == People.NONE)
        {
            Image[] objects = button.GetComponentsInChildren<Image>();
            
            switch (GameManager.Inst().getMovingPeople().getPeople())
            {
                case People.ARABMALE:
                    objects[1].gameObject.AddComponent<PassPeople1>();
                    objects[1].GetComponent<PassPeople>().setPeople(People.ARABMALE);
                    GameManager.Inst().getMapData()[(int)objects[1].gameObject.transform.parent.GetComponent<PassPeople>().IdentityLocation.x,(int)objects[1].gameObject.transform.parent.GetComponent<PassPeople>().IdentityLocation.y] = People.ARABMALE;
                    break;
                case People.ASIANMALE:
                    objects[1].gameObject.AddComponent<PassPeople5>();
                    objects[1].GetComponent<PassPeople>().setPeople(People.ASIANMALE);
                    GameManager.Inst().getMapData()[(int)objects[1].gameObject.transform.parent.GetComponent<PassPeople>().IdentityLocation.x, (int)objects[1].gameObject.transform.parent.GetComponent<PassPeople>().IdentityLocation.y] = People.ASIANMALE;
                    break;
                case People.BLACKMALE:
                    objects[1].gameObject.AddComponent<PassPeople4>();
                    objects[1].GetComponent<PassPeople>().setPeople(People.BLACKMALE);
                    GameManager.Inst().getMapData()[(int)objects[1].gameObject.transform.parent.GetComponent<PassPeople>().IdentityLocation.x, (int)objects[1].gameObject.transform.parent.GetComponent<PassPeople>().IdentityLocation.y] = People.BLACKMALE;
                    break;
                case People.KOREANFEMALE:
                    objects[1].gameObject.AddComponent<PassPeople3>();
                    objects[1].GetComponent<PassPeople>().setPeople(People.KOREANFEMALE);
                    GameManager.Inst().getMapData()[(int)objects[1].gameObject.transform.parent.GetComponent<PassPeople>().IdentityLocation.x, (int)objects[1].gameObject.transform.parent.GetComponent<PassPeople>().IdentityLocation.y] = People.KOREANFEMALE;
                    break;
                case People.LATINFEMALE:
                    objects[1].gameObject.AddComponent<PassPeople6>();
                    objects[1].GetComponent<PassPeople>().setPeople(People.LATINFEMALE);
                    Debug.Log(objects[1].gameObject.transform.parent.GetComponent<PassPeople>().gameObject.name);
                    GameManager.Inst().getMapData()[(int)objects[1].gameObject.transform.parent.GetComponent<PassPeople>().IdentityLocation.x, (int)objects[1].gameObject.transform.parent.GetComponent<PassPeople>().IdentityLocation.y] = People.LATINFEMALE;
                    break;
                case People.WHITEFEMALE:
                    objects[1].gameObject.AddComponent<PassPeople2>();
                    objects[1].GetComponent<PassPeople>().setPeople(People.WHITEFEMALE);
                    GameManager.Inst().getMapData()[(int)objects[1].gameObject.transform.parent.GetComponent<PassPeople>().IdentityLocation.x, (int)objects[1].gameObject.transform.parent.GetComponent<PassPeople>().IdentityLocation.y] = People.WHITEFEMALE;
                    break;
            }
            --GameManager.Inst().getSelectionNum()[(int)GameManager.Inst().getMovingPeople().getPeople() - 2];
            GameManager.Inst().mapUpdate();
            GameManager.Inst().setState(State.IDLE);
        }
    }
}
