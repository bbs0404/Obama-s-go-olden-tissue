using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

	public GameObject popUpInstance;
	//public float turnSpeed = 10.0f;
	private float accuTime = 0.0f;
	private bool failedFlag = false;
	private int failedState = 0;
	public bool successFlag = false;
	private int successState = 0;

	private int levelBase = 1;
	private int levelLimit = 10;
	private GameObject[] levelDoors = new GameObject[6];
	
	public Sprite lockedSprite;
	public Sprite unlockedSprite;

	// Use this for initialization
	void Start () {
		for(int i=0;i<6;i++){
			levelDoors[i] = GameObject.Find("DoorButton" + (i+1).ToString());
		}
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
		Application.LoadLevel("List Scene");
	}

	public void LevelLeft(){
		if(levelBase == 1)
			levelBase = (levelLimit + 5)/6*6 - 5; 
		else
			levelBase -= 6;
		LevelButtonRenew();
	}
	public void LevelRight(){
		if(levelBase+6 > levelLimit)
			levelBase = 1; 
		else
			levelBase += 6;
		LevelButtonRenew();
	}
	public void LevelButtonRenew(){
		for(int i=0;i<6;i++){
			if(i+levelBase > levelLimit){
				levelDoors[i].transform.Find("Text").GetComponent<Text>().text = "";
				levelDoors[i].GetComponent<Image>().sprite = lockedSprite;
			}
			else{
				levelDoors[i].transform.Find("Text").GetComponent<Text>().text = (i+levelBase).ToString();
				levelDoors[i].GetComponent<Image>().sprite = unlockedSprite;
			}
			levelDoors[i].GetComponent<Button>().onClick.AddListener(() => GoToStage(i+levelBase));
		}
	}
	public void GoToStage(int idx){
		if(idx>=1 && idx<=levelLimit){
			Application.LoadLevel("Main Scene");
		}
	}


	public void Update(){
		GameObject temp = GameObject.Find("TitlePaper");
		if(temp != null){
			accuTime += Time.deltaTime;
			while(accuTime > 0.6f){
				accuTime -= 0.6f;
			}
			float tempScale = 1.2f - accuTime / 0.6f * 0.2f;
			temp.transform.localScale = new Vector3(tempScale, tempScale, tempScale);
		}
		// When Failed
		if(failedFlag){
			accuTime += Time.deltaTime;
			if(accuTime < 0.5){
				Image failedBG = GameObject.Find("FailedBackground").GetComponent<Image>();
				if(failedState == 0){
					failedBG.enabled = true;
					failedBG.transform.SetAsLastSibling();
					failedState = 1;
				}
				failedBG.transform.position = new Vector3(640, 1260 - accuTime/0.5f * 1080f, 0);
			}
			else if(accuTime < 1.0){
				Image failedTr__p = GameObject.Find("FailedTr__p").GetComponent<Image>();
				if(failedState == 1){
					failedTr__p.enabled = true;
					failedTr__p.transform.SetAsLastSibling();
					failedTr__p.transform.position = new Vector3(640, 360, 0);
					failedState = 2;
				}
				Color colorTemp = failedTr__p.color;
            	colorTemp.a = (accuTime-0.5f)*2.0f;
            	failedTr__p.color = colorTemp;
				if(accuTime>=0.75){
					Image failedText = GameObject.Find("FailedText").GetComponent<Image>();
					if(failedState == 2){
						failedText.enabled = true;
						failedText.transform.SetAsLastSibling();
						failedState = 3;
					}
					failedText.transform.position = new Vector3(640, 1080 - (accuTime-0.75f)/0.25f * 600f, 0);
				}
			}
			else if(failedState == 3){
				GameObject failedRestart = GameObject.Find("FailedRestartButton");
				Image failedRestartImage = failedRestart.GetComponent<Image>();
				failedRestartImage.enabled = true;
				failedRestartImage.transform.SetAsLastSibling();
				failedRestartImage.transform.position = new Vector3(640, 120, 0);
				Button failedRestartButton = failedRestart.GetComponent<Button>();
				failedRestartButton.interactable = true;
				failedState = 4;
			}
		}
		// When Success
		if(successFlag){
			GameObject tissue = GameObject.Find("tissue");
			tissue.transform.RotateAround(tissue.transform.position, Vector3.up, 50.0f * Time.deltaTime);
			accuTime += Time.deltaTime;
			if(accuTime<0.2f){
				Image successBackground = GameObject.Find("SuccessBackground").GetComponent<Image>();
				if(successState == 0){
					successBackground.enabled = true;
					successBackground.transform.SetAsLastSibling();
					successState = 1;
				}
				Color colorTemp = successBackground.color;
            	colorTemp.a = (accuTime * 6.0f > 1.0f) ? 1.0f : (accuTime * 6.0f);
            	successBackground.color = colorTemp;
			}
			else if(accuTime<0.5f){
				Image successRibbon = GameObject.Find("SuccessRibbon").GetComponent<Image>();
				if(successState == 1){
					GameObject.Find("Canvas").SetActive(false);
					Canvas canvasSuc = GameObject.Find("CanvasSuc").GetComponent<Canvas>();
					canvasSuc.enabled = true;
					successState = 2;
				}
				if(accuTime<0.4f){
					tissue.transform.position = new Vector3(0, 10 - (accuTime-0.2f)/0.2f * 12, 100);
				}
				successRibbon.transform.position = new Vector3(640, 1080 - (accuTime-0.2f)/0.3f * 720, 100);
			}
			else if(successState == 2){
				GameObject successRestart = GameObject.Find("SuccessRestartButton");
				Image successRestartImage = successRestart.GetComponent<Image>();
				successRestartImage.enabled = true;
				Button successRestartButton = successRestart.GetComponent<Button>();
				successRestartButton.interactable = true;
				GameObject successNext = GameObject.Find("SuccessNextButton");
				Image successNextImage = successNext.GetComponent<Image>();
				successNextImage.enabled = true;
				Button successNextButton = successNext.GetComponent<Button>();
				successNextButton.interactable = true;
			}
		}
	}
	public void Failed(){
		Button[] canvasButtons = GameObject.Find("Canvas").GetComponentsInChildren<Button>();
		foreach( Button iterator in canvasButtons){
			iterator.interactable = false;
		}
		failedFlag = true;
	}
	public void Success(){
		Button[] canvasButtons = GameObject.Find("Canvas").GetComponentsInChildren<Button>();
		foreach( Button iterator in canvasButtons){
			iterator.interactable = false;
		}
		successFlag = true;
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
        if (GameManager.Inst().getState() == State.SELECTION)
        {
            Image[] objects = button.GetComponentsInChildren<Image>();
            GameManager.Inst().getTissueMap()[(int)objects[1].gameObject.transform.parent.GetComponent<PassPeople>().IdentityLocation.x, (int)objects[1].gameObject.transform.parent.GetComponent<PassPeople>().IdentityLocation.y] = true;
            GameManager.Inst().setState(State.SIMULATING);
        }
    }
    public void OnPlacingButtonClicked()
    {
        for (int i=0; i<6; ++i)
        {
            if (GameManager.Inst().getSelectionNum()[i] > 0)
                return;
        }
        GameManager.Inst().setState(State.SELECTION);
    }
}
