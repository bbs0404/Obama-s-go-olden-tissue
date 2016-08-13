using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum People
{
    NONE,
    Foo,
    Boo
}

public enum State
{
    IDLE,
    SIMULATING
}

public class GameManager : SingletonBehaviour<GameManager> {

    private float updateTime = 0.5f;
    private List<GameObject> objectList = null;
    [SerializeField]
    private List<GameObject> objectPrefabs;
    private People[,] map {
        get
        {
            return map;
        }
        set
        {
            map = value;
        }
    }
    private bool[,] tissueMap
    {
        get
        {
            return tissueMap;
        }
        set
        {
            tissueMap = value;
        }
    }
    private bool[,] preTissueMap;
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
        map = new People[size, size];
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
                Instantiate(objectPrefabs[(int)map[i,j] - 1]);
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
                    //passtissue 호출
                }
            }
        }
    }

}
