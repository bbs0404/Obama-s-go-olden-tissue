using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputManager : SingletonBehaviour<InputManager> {


	public GameObject popUpInstance;
	private float accuTime = 0.0f;
	private bool failedFlag = false;
	private int failedState = 0;
	private bool successFlag = false;
	private int successState = 0;

	private int levelBase = 1;
	private int levelLimit = 10;
	private GameObject[] levelDoors = new GameObject[6];
	
	public Sprite lockedSprite;
	public Sprite unlockedSprite;

	public Sprite backSprite;
	public Sprite resetSprite;
	
	private GameObject[,] targetMapTiles;
	private int mapSize;
	private PassPeople[,] map;

    [SerializeField]
    private AudioSource knock;

	// Use this for initialization
	void Start () {
		lockedSprite = Resources.Load <Sprite> ("stage/locked");
		unlockedSprite = Resources.Load <Sprite> ("stage/unlocked");
		backSprite = Resources.Load <Sprite> ("in-game/backimage");
		resetSprite = Resources.Load <Sprite> ("in-game/resetimage");
		for(int i=0;i<6;i++){
			levelDoors[i] = GameObject.Find("DoorButton" + (i+1).ToString());
		}
		if(levelDoors[0] != null)
			LevelButtonRenew();
	}
	
	// Pop-up appears.
	// IsReset? - True: Reset / False: List
	public void PopUpAppear(bool isReset){
		RectTransform canvasRT = GameObject.Find("Canvas").GetComponent<RectTransform>();
		popUpInstance = Instantiate(Resources.Load("PopUpBackground"), canvasRT.pivot, Quaternion.identity) as GameObject;
		
		// Disable the buttons
		Button[] canvasButtons = GameObject.Find("Canvas").GetComponentsInChildren<Button>();
		foreach( Button iterator in canvasButtons){
			iterator.interactable = false;
		}

		// Setting properties of the Pop-up 
		popUpInstance.transform.SetParent((GameObject.Find("Canvas")).transform, false);
		
		if(isReset){
			// Message & "Yes" Button
			popUpInstance.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => ResetGame());
			popUpInstance.GetComponent<Image>().sprite = resetSprite;
		}
		else{
			// Message & "Yes" Button
			popUpInstance.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => GotoList());
			popUpInstance.GetComponent<Image>().sprite = backSprite;
		}
		// "No" button
		popUpInstance.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => PopUpDisappear());  
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
		SceneManager.LoadScene("Main Scene");
	}
	public void GotoList(){
		SceneManager.LoadScene("List Scene");
	}
	public void GotoNextStage(){
		if( GameManager.Inst().getStage() == levelLimit ){
			SceneManager.LoadScene("List Scene");
		}
		else{
			GameManager.Inst().setStage(GameManager.Inst().getStage()+1);
			SceneManager.LoadScene("Main Scene");
		}
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
		// Set the image / onClick event of level buttons
		for(int i=0;i<6;i++){
			if(i+levelBase > levelLimit){
				levelDoors[i].transform.Find("Text").GetComponent<Text>().text = "";
				levelDoors[i].GetComponent<Image>().sprite = lockedSprite;
			}
			else{
				levelDoors[i].transform.Find("Text").GetComponent<Text>().text = (i+levelBase).ToString();
				levelDoors[i].GetComponent<Image>().sprite = unlockedSprite;
			}
			levelDoors[i].GetComponent<Button>().onClick.RemoveAllListeners();
			int tempInt = i+levelBase;
			levelDoors[i].GetComponent<Button>().onClick.AddListener(() => GoToStage(tempInt));
		}
		// Show the Left button or not
		if(levelBase == 1)
			GameObject.Find("LeftButton").GetComponent<Button>().interactable = false;
		else 
			GameObject.Find("LeftButton").GetComponent<Button>().interactable = true;
		// Show the Right button or not
		if(levelBase + 6 > levelLimit)
			GameObject.Find("RightButton").GetComponent<Button>().interactable = false;
		else 
			GameObject.Find("RightButton").GetComponent<Button>().interactable = true;
	}
	public void GoToStage(int idx){
		//Debug.Log("Called " + idx.ToString());
		if(idx>=1 && idx<=levelLimit){
			GameManager.Inst().setStage(idx);
			SceneManager.LoadScene("Main Scene");
		}
	}


	public void Update(){
		if(System.String.Compare(Application.loadedLevelName, "Title Scene", false) == 0){
			GameObject temp = GameObject.Find("TitlePaper");
			if(temp != null){
				accuTime += Time.deltaTime;
				while(accuTime > 0.6f){
					accuTime -= 0.6f;
				}
				float tempScale = 1.2f - accuTime / 0.6f * 0.2f;
				temp.transform.localScale = new Vector3(tempScale, tempScale, tempScale);
			}
		}
		else if(System.String.Compare(Application.loadedLevelName, "Main Scene", false) == 0){
			if(map != null){
				int mapX = (int)((Input.mousePosition.x-20.0f) / (680f / map.GetLength(0)));
				int mapY = (int)((720.0f - Input.mousePosition.x) / (680f / map.GetLength(0)));
				PassPeople movingPeople = GameManager.Inst().getMovingPeople();
				for(int i=0; i<mapSize; i++){
					for(int j=0;j<mapSize;j++){
						targetMapTiles[i,j].GetComponent<Image>().enabled = false; 
					}
				}
				if(mapX >= 0 && mapX < mapSize && mapY >= 0 && mapY < mapSize
					&& map[mapX,mapY] != null && movingPeople != null){
					foreach(var item in movingPeople.PassLocation){
						if((int)item.x + mapX >= 0 && (int)item.x + mapX < mapSize &&
							(int)item.y + mapY >= 0 && (int)item.y + mapY < mapSize &&
							map[(int)item.x + mapX,(int)item.y + mapY] != null)
							targetMapTiles[(int)item.x + mapX,(int)item.y + mapY].GetComponent<Image>().enabled = true;
					}
				}
			}
			else if(GameManager.Inst().getMap()!=null){
				map = GameManager.Inst().getMap();
				mapSize = map.GetLength(0);
				targetMapTiles = new GameObject[mapSize, mapSize];
				for(int i=0; i<mapSize; i++){
					for(int j=0;j<mapSize;j++){
						targetMapTiles[i,j] = Instantiate(Resources.Load("TargetTile")) as GameObject;
						targetMapTiles[i,j].transform.SetParent((GameObject.Find("Canvas")).transform, false);
						targetMapTiles[i,j].transform.position = new Vector2(20 + i * (680f / mapSize) + (680f / mapSize) / 2, 720 - (20 + j * (680f / mapSize) + (680f / mapSize) / 2));
						targetMapTiles[i,j].transform.localScale = new Vector2(6.80f / mapSize, 6.80f / mapSize);
						targetMapTiles[i,j].GetComponent<Image>().enabled = false;
					}
				}
			}

			// When Failed
			if(failedFlag){
				accuTime += Time.deltaTime;
				// While the first 0.5 sec, Failed Chocolate Background comes down.
				if(accuTime < 0.5){
					Image failedBG = GameObject.Find("FailedBackground").GetComponent<Image>();
					if(failedState == 0){
						failedBG.enabled = true;
						failedBG.transform.SetAsLastSibling();
						failedState = 1;
					}
					failedBG.transform.position = new Vector3(640, 1260 - accuTime/0.5f * 1080f, 0);
				}
				// While the next 0.5 sec, Failed Tr__p appears with increasing its alpha value. Also, Failed text comes down.
				else if(accuTime < 1.0){
					Image failedTr__p = GameObject.Find("FailedTr__p").GetComponent<Image>();
					if(failedState == 1){
						GameObject.Find("FailedBackground").GetComponent<Image>().transform.position = new Vector3(640, 180, 0);
						failedTr__p.enabled = true;
						failedTr__p.transform.SetAsLastSibling();
						failedTr__p.transform.position = new Vector3(640, 360, 0);
						failedState = 2;
					}
					Color colorTemp = failedTr__p.color;
					colorTemp.a = ((accuTime-0.5f)*2.1f > 1.0f) ? 1.0f : (accuTime-0.5f)*2.1f;
					failedTr__p.color = colorTemp;
					// Failed Text comes after Tr__p's alpha value is 0.5.
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
				// After 1 sec, Button appears.
				else if(failedState == 3){
					GameObject.Find("FailedText").GetComponent<Image>().transform.position = new Vector3(640, 480, 0);
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
				// Gol-den tissue spins constantly. It's awesome. 
				GameObject tissue = GameObject.Find("tissue");
				tissue.transform.RotateAround(tissue.transform.position, Vector3.up, 50.0f * Time.deltaTime);
				accuTime += Time.deltaTime;
				// While the first 0.2sec, Success Background appears by increasing its alpha value.
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
				// While the next 0.3 sec, Removes the original canvas and let the success canvas appears.
				// And then, Ribbon starts to come down.
				else if(accuTime<0.5f){
					Image successRibbon = GameObject.Find("SuccessRibbon").GetComponent<Image>();
					if(successState == 1){
						GameObject.Find("Canvas").SetActive(false);
						Canvas canvasSuc = GameObject.Find("CanvasSuc").GetComponent<Canvas>();
						canvasSuc.enabled = true;
						successState = 2;
					}
					// Tissue also comes down
					if(accuTime<0.4f)
						tissue.transform.position = new Vector3(0, 10 - (accuTime-0.2f)/0.2f * 12, 100);
					else
						tissue.transform.position = new Vector3(0, -2, 100);
					successRibbon.transform.position = new Vector3(640, 1080 - (accuTime-0.2f)/0.3f * 720, 100);
				}
				// After 0.5 sec, Button appears.
				else if(successState == 2){
					GameObject.Find("SuccessRibbon").GetComponent<Image>().transform.position = new Vector3(640, 360, 100);
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
	}
	public void Failed(){
		// When Failed, inactive all the buttons and raise the failedFlag.
		Button[] canvasButtons = GameObject.Find("Canvas").GetComponentsInChildren<Button>();
		foreach( Button iterator in canvasButtons){
			iterator.interactable = false;
		}
		failedFlag = true;
	}
	public void Success(){
		// When Failed, inactive all the buttons and raise the successFlag.
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
    public void OnPlacingButtonClicked()
    {
        for (int i=0; i<6; ++i)
        {
            if (GameManager.Inst().getSelectionNum()[i] > 0)
                return;
        }
        GameManager.Inst().setState(State.SELECTION);
    }
    public void LevelSelect(int level)
    {
        if (levelDoors[level-1].GetComponent<Image>().sprite == lockedSprite)
        {
            SoundManager.Inst().playAudio(knock);
        }
        else
        {
            GameManager.Inst().setStage(level);
            SceneManager.LoadScene("Main Scene");
        }
    }

}
