using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum State
{
    IDLE,
    MOVING,
    SIMULATING
}

public class GameManager : SingletonBehaviour<GameManager> {

    private float updateTime = 0.5f;
    private List<GameObject> objectList = null;
    [SerializeField]
    private List<GameObject> objectPrefabs;
    private PassPeople[,] map;
    private bool[,] tissueMap;
    private bool[,] preTissueMap;
    private PassPeople MovingPeople = null;
    private bool MoveFinished = false;
    [SerializeField]
    private List<PassPeople> MovingPeopleList;
    private bool isSuccess;

    private State state
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
        }
    }
    void Start()
    {
        updateTime = 0.5f;
        objectList = null;
        MovingPeople = null;
        MoveFinished = false;
        isSuccess = false;
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
    }

    public void mapUpdate()
    {
        if (objectList != null) {
            objectList.Clear();
        }
        objectList = new List<GameObject>();
        for (int i=0; i<map.GetLength(0); i++)
        {
            for (int j=0; j<map.GetLength(1); ++j)
            {
                objectList.Add(Instantiate(objectPrefabs[(int)map[i,j].getPeople() - 1]));
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
                if (preTissueMap[i, j])
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
}
