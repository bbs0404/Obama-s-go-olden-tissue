using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum State
{
    IDLE,
    MOVING,
    SELECTION,
    SIMULATING
}

public class GameManager : SingletonBehaviour<GameManager> {
    [SerializeField]
    private Canvas gameCanvas;
    [SerializeField]
    private float updateTime = 0.5f;
    [SerializeField]
    private int stage = 0;
    [SerializeField]
    private List<GameObject> objectList = null;
    [SerializeField]
    private List<GameObject> objectPrefabs;
    [SerializeField]
    private PassPeople[,] map;
    [SerializeField]
    private People[,] mapdata;
    private bool[,] tissueMap;
    private bool[,] preTissueMap;
    private PassPeople MovingPeople = null;
    private bool MoveFinished = false;
    [SerializeField]
    private List<PassPeople> MovingPeopleList;
    private bool isSuccess;
    [SerializeField]
    private Sprite[] characters = new Sprite[12];
    [SerializeField]
    private State state;
    [SerializeField]
    private int[] selectionNum = new int[6];
    [SerializeField]
    private Text[] selectionText = new Text[6];

    void Start()
    {
        updateTime = 0.5f;
        objectList = null;
        MovingPeople = null;
        MoveFinished = false;
        isSuccess = false;
        if (stage == 0){
            setSizeOfMap(5);
            mapdata[0, 1] = People.ARABMALE;
            mapdata[0, 0] = People.ASIANMALE;
            mapdata[1, 3] = People.BLACKMALE;
            mapdata[1, 1] = People.KOREANFEMALE;
            mapdata[1, 2] = People.NONE;
            selectionNum[0] = 0;
            selectionNum[1] = 1;
            selectionNum[2] = 2;
            selectionNum[3] = 3;
            selectionNum[4] = 4;
            selectionNum[5] = 5;
            mapUpdate();
        }
    }

    void Update()
    {
        if (state == State.SIMULATING)
        {
            if (updateTime > 0)
            {
                updateTime -= Time.deltaTime;
            }
            else
            {
                updateTurn();
                updateTime = 0.5f;
            }
        }
    }

    public void setSizeOfMap(int size)
    {
        map = new PassPeople[size, size];
        tissueMap = new bool[size, size];
        preTissueMap = new bool[size, size];
        mapdata = new People[size, size];
    }

    public void mapUpdate()
    {
        for (int i = 0; i < 6; ++i)
        {
            selectionText[i].text = ": " + selectionNum[i].ToString();
        }
        if (objectList != null) {
            foreach (var item in objectList)
            {
                Destroy(item);
            }
            objectList.Clear();
        }
        objectList = new List<GameObject>();
        for (int i = 0; i < mapdata.GetLength(0); i++)
        {
            for (int j = 0; j < mapdata.GetLength(1); ++j)
            {
                if (mapdata[i, j] != People.EMPTY) {
                    GameObject tmp = Instantiate(objectPrefabs[0]);
                    objectList.Add(tmp);
                    tmp.transform.SetParent(gameCanvas.transform);
                    foreach (var item in tmp.GetComponentsInChildren<RectTransform>())
                    {
                        item.position = new Vector2(20 + i * (680f / map.GetLength(0)) + (680f / map.GetLength(0)) / 2, 720 - ( 20 + j * (680f / map.GetLength(1)) + (680f / map.GetLength(1)) / 2 ));
                        item.sizeDelta = new Vector2(680f / map.GetLength(0), 680f / map.GetLength(1));
                    }
                    Image[] images = tmp.GetComponentsInChildren<Image>();
                    Image image = images[1];
                    switch (mapdata[i,j])
                    {
                        case People.NONE:
                            tmp.AddComponent<PassPeople>();
                            tmp.GetComponent<PassPeople>().setPeople(People.NONE);
                            break;
                        case People.ARABMALE:
                            tmp.AddComponent<PassPeople1>();
                            tmp.GetComponent<PassPeople>().setPeople(People.ARABMALE);
                            break;
                        case People.ASIANMALE:
                            tmp.AddComponent<PassPeople5>();
                            tmp.GetComponent<PassPeople>().setPeople(People.ASIANMALE);
                            break;
                        case People.BLACKMALE:
                            tmp.AddComponent<PassPeople4>();
                            tmp.GetComponent<PassPeople>().setPeople(People.BLACKMALE);
                            break;
                        case People.KOREANFEMALE:
                            tmp.AddComponent<PassPeople3>();
                            tmp.GetComponent<PassPeople>().setPeople(People.KOREANFEMALE);
                            break;
                        case People.LATINFEMALE:
                            tmp.AddComponent<PassPeople6>();
                            tmp.GetComponent<PassPeople>().setPeople(People.LATINFEMALE);
                            break;
                        case People.WHITEFEMALE:
                            tmp.AddComponent<PassPeople2>();
                            tmp.GetComponent<PassPeople>().setPeople(People.WHITEFEMALE);
                            break;
                    }
                    tmp.GetComponent<PassPeople>().IdentityLocation = new Vector2(i, j);
                    map[i, j] = tmp.GetComponent<PassPeople>();
                    switch (mapdata[i, j])
                    {
                        case People.NONE:
                            break;
                        default:
                            if(map[i, j].IsTissueReceived) {
                                image.sprite = characters[((int)map[i, j].getPeople() - 2) * 2 + 1];
                                images[0].color = new Color(230f / 255f, 181f / 255f, 78f / 255f);
                            }
                            else
                                image.sprite = characters[((int)map[i, j].getPeople() - 2) * 2];
                            break;
                    }
                }
            }
        }
    }

    private void updateTurn()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); ++j)
            {
                preTissueMap[i, j] = tissueMap[i, j];
            }
        }
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); ++j)
            {
                if (preTissueMap[i, j])
                {
                    map[i, j].PassTissue();
                }
            }
        }
        if (IsEnded())
        {
            checkResult();
            if (isSuccess)
            {
                Debug.Log("You success!");
            }
            else
            {
                Debug.Log("You failed!");
            }
            state = State.IDLE;
        }
        else
        {
            mapUpdate();
        }
    }
    
    public bool IsEnded()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); ++j)
            {
                if (tissueMap[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void checkResult()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); ++j)
            {
                if (!map[i,j].IsTissueReceived)
                {
                    isSuccess = false;
                    return;
                }
            }
        }
        isSuccess = true;
    }

    public PassPeople[,] getMap()
    {
        return map;
    }
    public void setMap(PassPeople[,] newmap)
    {
        map = newmap;
    }
    public bool[,] getTissueMap()
    {
        return tissueMap;
    }
    public void setTissueMap(bool[,] newtissuemap)
    {
        tissueMap = newtissuemap;
    }
    public State getState()
    {
        return state;
    }
    public void setState(State newState)
    {
        state = newState;
    }
    public PassPeople getMovingPeople() {
        return MovingPeople;
    }
    public void setMovingPeople(PassPeople newPeople) {
        MovingPeople = newPeople;
    }
    public bool isMoveFinished()
    {
        return MoveFinished;
    }
    public void setMoveFinished(bool isFinished)
    {
        MoveFinished = isFinished;
    }
    public List<PassPeople> getMovingPeopleList()
    {
        return MovingPeopleList;
    }
    public People[,] getMapData()
    {
        return mapdata;
    }
    public void setMapData(People[,] newmapdata)
    {
        mapdata = newmapdata;
    }
    public int[] getSelectionNum()
    {
        return selectionNum;
    }
}
