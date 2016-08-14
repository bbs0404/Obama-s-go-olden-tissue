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
        updateTime = 0f;
        objectList = null;
        MovingPeople = null;
        MoveFinished = false;
        isSuccess = false;

        if (stage == 0)
        {
            setSizeOfMap(5);
            mapdata[0, 0] = People.NONE;
            mapdata[0, 2] = People.ASIANMALE;
            mapdata[0, 3] = People.ASIANMALE;
            mapdata[0, 4] = People.ASIANMALE;
            mapdata[0, 1] = People.ASIANMALE;
            selectionNum[0] = 1;
            selectionNum[1] = 0;
            selectionNum[2] = 0;
            selectionNum[3] = 0;
            selectionNum[4] = 0;
            selectionNum[5] = 0;
            for (int i = 0; i < 6; ++i)
            {
                selectionText[i].text = ": " + selectionNum[i].ToString();
            }
            mapUpdate();
        }

        switch (stage)
        {
            case 1:

                setSizeOfMap(5);

                mapdata[0, 0] = People.EMPTY; mapdata[1, 0] = People.ARABMALE; mapdata[2, 0] = People.NONE; mapdata[3, 0] = People.EMPTY; mapdata[4, 0] = People.EMPTY;
                mapdata[0, 1] = People.EMPTY; mapdata[1, 1] = People.NONE; mapdata[2, 1] = People.ARABMALE; mapdata[3, 1] = People.NONE; mapdata[4, 1] = People.EMPTY;
                mapdata[0, 2] = People.ARABMALE; mapdata[1, 2] = People.NONE; mapdata[2, 2] = People.BLACKMALE; mapdata[3, 2] = People.ARABMALE; mapdata[4, 2] = People.ARABMALE;
                mapdata[0, 3] = People.EMPTY; mapdata[1, 3] = People.ARABMALE; mapdata[2, 3] = People.ARABMALE; mapdata[3, 3] = People.KOREANFEMALE; mapdata[4, 3] = People.EMPTY;
                mapdata[0, 4] = People.EMPTY; mapdata[1, 4] = People.NONE; mapdata[2, 4] = People.LATINFEMALE; mapdata[3, 4] = People.ARABMALE; mapdata[4, 4] = People.EMPTY;


                selectionNum[0] = 0;
                selectionNum[1] = 0;
                selectionNum[2] = 1;
                selectionNum[3] = 0;
                selectionNum[4] = 1;
                selectionNum[5] = 0;
                for (int i = 0; i < 6; ++i)
                {
                    selectionText[i].text = ": " + selectionNum[i].ToString();
                }
                mapUpdate();
                break;
            case 2:

                setSizeOfMap(5);

                mapdata[0, 0] = People.EMPTY; mapdata[1, 0] = People.ARABMALE; mapdata[2, 0] = People.EMPTY; mapdata[3, 0] = People.ARABMALE; mapdata[4, 0] = People.EMPTY;
                mapdata[0, 1] = People.EMPTY; mapdata[1, 1] = People.NONE; mapdata[2, 1] = People.NONE; mapdata[3, 1] = People.NONE; mapdata[4, 1] = People.EMPTY;
                mapdata[0, 2] = People.EMPTY; mapdata[1, 2] = People.ASIANMALE; mapdata[2, 2] = People.NONE; mapdata[3, 2] = People.ASIANMALE; mapdata[4, 2] = People.EMPTY;
                mapdata[0, 3] = People.EMPTY; mapdata[1, 3] = People.ASIANMALE; mapdata[2, 3] = People.ARABMALE; mapdata[3, 3] = People.NONE; mapdata[4, 3] = People.EMPTY;
                mapdata[0, 4] = People.EMPTY; mapdata[1, 4] = People.ARABMALE; mapdata[2, 4] = People.BLACKMALE; mapdata[3, 4] = People.ARABMALE; mapdata[4, 4] = People.EMPTY;


                selectionNum[0] = 1;
                selectionNum[1] = 0;
                selectionNum[2] = 1;
                selectionNum[3] = 0;
                selectionNum[4] = 1;
                selectionNum[5] = 0;
                for (int i = 0; i < 6; ++i)
                {
                    selectionText[i].text = ": " + selectionNum[i].ToString();
                }
                mapUpdate();
                break;
            case 3:

                setSizeOfMap(5);

                mapdata[0, 0] = People.EMPTY; mapdata[1, 0] = People.NONE; mapdata[2, 0] = People.NONE; mapdata[3, 0] = People.NONE; mapdata[4, 0] = People.EMPTY;
                mapdata[0, 1] = People.EMPTY; mapdata[1, 1] = People.ARABMALE; mapdata[2, 1] = People.ARABMALE; mapdata[3, 1] = People.ARABMALE; mapdata[4, 1] = People.EMPTY;
                mapdata[0, 2] = People.NONE; mapdata[1, 2] = People.NONE; mapdata[2, 2] = People.NONE; mapdata[3, 2] = People.NONE; mapdata[4, 2] = People.WHITEFEMALE;
                mapdata[0, 3] = People.EMPTY; mapdata[1, 3] = People.ARABMALE; mapdata[2, 3] = People.EMPTY; mapdata[3, 3] = People.ARABMALE; mapdata[4, 3] = People.ARABMALE;
                mapdata[0, 4] = People.WHITEFEMALE; mapdata[1, 4] = People.EMPTY; mapdata[2, 4] = People.BLACKMALE; mapdata[3, 4] = People.EMPTY; mapdata[4, 4] = People.KOREANFEMALE;


                selectionNum[0] = 0;
                selectionNum[1] = 0;
                selectionNum[2] = 1;
                selectionNum[3] = 0;
                selectionNum[4] = 1;
                selectionNum[5] = 1;
                for (int i = 0; i < 6; ++i)
                {
                    selectionText[i].text = ": " + selectionNum[i].ToString();
                }
                mapUpdate();
                break;
            case 4:

                setSizeOfMap(7);

                mapdata[0, 0] = People.EMPTY; mapdata[1, 0] = People.EMPTY; mapdata[2, 0] = People.EMPTY; mapdata[3, 0] = People.EMPTY; mapdata[4, 0] = People.EMPTY; mapdata[5, 0] = People.EMPTY; mapdata[6, 0] = People.EMPTY;
                mapdata[0, 1] = People.ARABMALE; mapdata[1, 1] = People.ASIANMALE; mapdata[2, 1] = People.ARABMALE; mapdata[3, 1] = People.NONE; mapdata[4, 1] = People.ASIANMALE; mapdata[5, 1] = People.EMPTY; mapdata[6, 1] = People.EMPTY;
                mapdata[0, 2] = People.EMPTY; mapdata[1, 2] = People.NONE; mapdata[2, 2] = People.NONE; mapdata[3, 2] = People.WHITEFEMALE; mapdata[4, 2] = People.ARABMALE; mapdata[5, 2] = People.EMPTY; mapdata[6, 2] = People.EMPTY;
                mapdata[0, 3] = People.EMPTY; mapdata[1, 3] = People.NONE; mapdata[2, 3] = People.NONE; mapdata[3, 3] = People.NONE; mapdata[4, 3] = People.ARABMALE; mapdata[5, 3] = People.EMPTY; mapdata[6, 3] = People.EMPTY;
                mapdata[0, 4] = People.EMPTY; mapdata[1, 4] = People.ARABMALE; mapdata[2, 4] = People.NONE; mapdata[3, 4] = People.BLACKMALE; mapdata[4, 4] = People.EMPTY; mapdata[5, 4] = People.ARABMALE; mapdata[6, 4] = People.EMPTY;
                mapdata[0, 5] = People.EMPTY; mapdata[1, 5] = People.NONE; mapdata[2, 5] = People.EMPTY; mapdata[3, 5] = People.NONE; mapdata[4, 5] = People.NONE; mapdata[5, 5] = People.EMPTY; mapdata[6, 5] = People.EMPTY;
                mapdata[0, 6] = People.EMPTY; mapdata[1, 6] = People.EMPTY; mapdata[2, 6] = People.NONE; mapdata[3, 6] = People.ARABMALE; mapdata[4, 6] = People.NONE; mapdata[5, 6] = People.EMPTY; mapdata[6, 6] = People.EMPTY;


                selectionNum[0] = 1;
                selectionNum[1] = 0;
                selectionNum[2] = 1;
                selectionNum[3] = 0;
                selectionNum[4] = 1;
                selectionNum[5] = 0;
                for (int i = 0; i < 6; ++i)
                {
                    selectionText[i].text = ": " + selectionNum[i].ToString();
                }
                mapUpdate();
                break;
            case 5:

                setSizeOfMap(7);

                mapdata[0, 0] = People.EMPTY; mapdata[1, 0] = People.EMPTY; mapdata[2, 0] = People.EMPTY; mapdata[3, 0] = People.EMPTY; mapdata[4, 0] = People.EMPTY; mapdata[5, 0] = People.EMPTY; mapdata[6, 0] = People.EMPTY;
                mapdata[0, 1] = People.EMPTY; mapdata[1, 1] = People.NONE; mapdata[2, 1] = People.ARABMALE; mapdata[3, 1] = People.NONE; mapdata[4, 1] = People.ARABMALE; mapdata[5, 1] = People.NONE; mapdata[6, 1] = People.EMPTY;
                mapdata[0, 2] = People.EMPTY; mapdata[1, 2] = People.NONE; mapdata[2, 2] = People.ASIANMALE; mapdata[3, 2] = People.ASIANMALE; mapdata[4, 2] = People.ASIANMALE; mapdata[5, 2] = People.NONE; mapdata[6, 2] = People.EMPTY;
                mapdata[0, 3] = People.ARABMALE; mapdata[1, 3] = People.NONE; mapdata[2, 3] = People.NONE; mapdata[3, 3] = People.NONE; mapdata[4, 3] = People.NONE; mapdata[5, 3] = People.NONE; mapdata[6, 3] = People.ARABMALE;
                mapdata[0, 4] = People.EMPTY; mapdata[1, 4] = People.EMPTY; mapdata[2, 4] = People.ARABMALE; mapdata[3, 4] = People.EMPTY; mapdata[4, 4] = People.ARABMALE; mapdata[5, 4] = People.EMPTY; mapdata[6, 4] = People.EMPTY;
                mapdata[0, 5] = People.EMPTY; mapdata[1, 5] = People.EMPTY; mapdata[2, 5] = People.ARABMALE; mapdata[3, 5] = People.EMPTY; mapdata[4, 5] = People.ARABMALE; mapdata[5, 5] = People.EMPTY; mapdata[6, 5] = People.EMPTY;
                mapdata[0, 6] = People.EMPTY; mapdata[1, 6] = People.EMPTY; mapdata[2, 6] = People.EMPTY; mapdata[3, 6] = People.EMPTY; mapdata[4, 6] = People.EMPTY; mapdata[5, 6] = People.EMPTY; mapdata[6, 6] = People.EMPTY;


                selectionNum[0] = 0;
                selectionNum[1] = 0;
                selectionNum[2] = 1;
                selectionNum[3] = 0;
                selectionNum[4] = 0;
                selectionNum[5] = 2;
                for (int i = 0; i < 6; ++i)
                {
                    selectionText[i].text = ": " + selectionNum[i].ToString();
                }
                mapUpdate();
                break;
            case 6:

                setSizeOfMap(7);

                mapdata[0, 0] = People.EMPTY; mapdata[1, 0] = People.ASIANMALE; mapdata[2, 0] = People.ARABMALE; mapdata[3, 0] = People.EMPTY; mapdata[4, 0] = People.EMPTY; mapdata[5, 0] = People.EMPTY; mapdata[6, 0] = People.EMPTY;
                mapdata[0, 1] = People.EMPTY; mapdata[1, 1] = People.NONE; mapdata[2, 1] = People.ARABMALE; mapdata[3, 1] = People.NONE; mapdata[4, 1] = People.EMPTY; mapdata[5, 1] = People.ASIANMALE; mapdata[6, 1] = People.EMPTY;
                mapdata[0, 2] = People.NONE; mapdata[1, 2] = People.ARABMALE; mapdata[2, 2] = People.NONE; mapdata[3, 2] = People.ARABMALE; mapdata[4, 2] = People.EMPTY; mapdata[5, 2] = People.ARABMALE; mapdata[6, 2] = People.EMPTY;
                mapdata[0, 3] = People.EMPTY; mapdata[1, 3] = People.EMPTY; mapdata[2, 3] = People.ASIANMALE; mapdata[3, 3] = People.ARABMALE; mapdata[4, 3] = People.NONE; mapdata[5, 3] = People.BLACKMALE; mapdata[6, 3] = People.EMPTY;
                mapdata[0, 4] = People.ARABMALE; mapdata[1, 4] = People.NONE; mapdata[2, 4] = People.BLACKMALE; mapdata[3, 4] = People.EMPTY; mapdata[4, 4] = People.EMPTY; mapdata[5, 4] = People.EMPTY; mapdata[6, 4] = People.EMPTY;
                mapdata[0, 5] = People.EMPTY; mapdata[1, 5] = People.EMPTY; mapdata[2, 5] = People.EMPTY; mapdata[3, 5] = People.ARABMALE; mapdata[4, 5] = People.EMPTY; mapdata[5, 5] = People.ARABMALE; mapdata[6, 5] = People.EMPTY;
                mapdata[0, 6] = People.EMPTY; mapdata[1, 6] = People.EMPTY; mapdata[2, 6] = People.ARABMALE; mapdata[3, 6] = People.EMPTY; mapdata[4, 6] = People.EMPTY; mapdata[5, 6] = People.EMPTY; mapdata[6, 6] = People.EMPTY;

                selectionNum[0] = 0;
                selectionNum[1] = 0;
                selectionNum[2] = 2;
                selectionNum[3] = 0;
                selectionNum[4] = 1;
                selectionNum[5] = 0;
                for (int i = 0; i < 6; ++i)
                {
                    selectionText[i].text = ": " + selectionNum[i].ToString();
                }
                mapUpdate();
                break;
            case 7:

                setSizeOfMap(7);

                mapdata[0, 0] = People.EMPTY; mapdata[1, 0] = People.EMPTY; mapdata[2, 0] = People.ARABMALE; mapdata[3, 0] = People.EMPTY; mapdata[4, 0] = People.EMPTY; mapdata[5, 0] = People.EMPTY; mapdata[6, 0] = People.EMPTY;
                mapdata[0, 1] = People.EMPTY; mapdata[1, 1] = People.NONE; mapdata[2, 1] = People.EMPTY; mapdata[3, 1] = People.NONE; mapdata[4, 1] = People.EMPTY; mapdata[5, 1] = People.NONE; mapdata[6, 1] = People.ARABMALE;
                mapdata[0, 2] = People.ARABMALE; mapdata[1, 2] = People.EMPTY; mapdata[2, 2] = People.BLACKMALE; mapdata[3, 2] = People.ARABMALE; mapdata[4, 2] = People.NONE; mapdata[5, 2] = People.WHITEFEMALE; mapdata[6, 2] = People.NONE;
                mapdata[0, 3] = People.EMPTY; mapdata[1, 3] = People.EMPTY; mapdata[2, 3] = People.NONE; mapdata[3, 3] = People.NONE; mapdata[4, 3] = People.ARABMALE; mapdata[5, 3] = People.NONE; mapdata[6, 3] = People.ASIANMALE;
                mapdata[0, 4] = People.EMPTY; mapdata[1, 4] = People.EMPTY; mapdata[2, 4] = People.ARABMALE; mapdata[3, 4] = People.ARABMALE; mapdata[4, 4] = People.NONE; mapdata[5, 4] = People.ARABMALE; mapdata[6, 4] = People.NONE;
                mapdata[0, 5] = People.EMPTY; mapdata[1, 5] = People.EMPTY; mapdata[2, 5] = People.EMPTY; mapdata[3, 5] = People.NONE; mapdata[4, 5] = People.ARABMALE; mapdata[5, 5] = People.NONE; mapdata[6, 5] = People.EMPTY;
                mapdata[0, 6] = People.EMPTY; mapdata[1, 6] = People.EMPTY; mapdata[2, 6] = People.EMPTY; mapdata[3, 6] = People.EMPTY; mapdata[4, 6] = People.NONE; mapdata[5, 6] = People.EMPTY; mapdata[6, 6] = People.ARABMALE;


                selectionNum[0] = 0;
                selectionNum[1] = 0;
                selectionNum[2] = 0;
                selectionNum[3] = 0;
                selectionNum[4] = 2;
                selectionNum[5] = 1;
                for (int i = 0; i < 6; ++i)
                {
                    selectionText[i].text = ": " + selectionNum[i].ToString();
                }
                mapUpdate();
                break;
            case 8:

                setSizeOfMap(7);

                mapdata[0, 0] = People.EMPTY; mapdata[1, 0] = People.NONE; mapdata[2, 0] = People.ARABMALE; mapdata[3, 0] = People.EMPTY; mapdata[4, 0] = People.EMPTY; mapdata[5, 0] = People.EMPTY; mapdata[6, 0] = People.EMPTY;
                mapdata[0, 1] = People.EMPTY; mapdata[1, 1] = People.NONE; mapdata[2, 1] = People.NONE; mapdata[3, 1] = People.NONE; mapdata[4, 1] = People.ARABMALE; mapdata[5, 1] = People.EMPTY; mapdata[6, 1] = People.EMPTY;
                mapdata[0, 2] = People.ARABMALE; mapdata[1, 2] = People.NONE; mapdata[2, 2] = People.BLACKMALE; mapdata[3, 2] = People.ARABMALE; mapdata[4, 2] = People.NONE; mapdata[5, 2] = People.EMPTY; mapdata[6, 2] = People.EMPTY;
                mapdata[0, 3] = People.ARABMALE; mapdata[1, 3] = People.NONE; mapdata[2, 3] = People.ARABMALE; mapdata[3, 3] = People.NONE; mapdata[4, 3] = People.WHITEFEMALE; mapdata[5, 3] = People.NONE; mapdata[6, 3] = People.EMPTY;
                mapdata[0, 4] = People.EMPTY; mapdata[1, 4] = People.NONE; mapdata[2, 4] = People.NONE; mapdata[3, 4] = People.NONE; mapdata[4, 4] = People.NONE; mapdata[5, 4] = People.ASIANMALE; mapdata[6, 4] = People.EMPTY;
                mapdata[0, 5] = People.EMPTY; mapdata[1, 5] = People.EMPTY; mapdata[2, 5] = People.NONE; mapdata[3, 5] = People.EMPTY; mapdata[4, 5] = People.EMPTY; mapdata[5, 5] = People.ARABMALE; mapdata[6, 5] = People.EMPTY;
                mapdata[0, 6] = People.EMPTY; mapdata[1, 6] = People.EMPTY; mapdata[2, 6] = People.EMPTY; mapdata[3, 6] = People.EMPTY; mapdata[4, 6] = People.EMPTY; mapdata[5, 6] = People.EMPTY; mapdata[6, 6] = People.EMPTY;

                selectionNum[0] = 0;
                selectionNum[1] = 1;
                selectionNum[2] = 1;
                selectionNum[3] = 0;
                selectionNum[4] = 1;
                selectionNum[5] = 0;
                for (int i = 0; i < 6; ++i)
                {
                    selectionText[i].text = ": " + selectionNum[i].ToString();
                }
                mapUpdate();
                break;
            case 9:

                setSizeOfMap(7);

                mapdata[0, 0] = People.EMPTY; mapdata[1, 0] = People.ARABMALE; mapdata[2, 0] = People.NONE; mapdata[3, 0] = People.EMPTY; mapdata[4, 0] = People.EMPTY; mapdata[5, 0] = People.EMPTY; mapdata[6, 0] = People.EMPTY;
                mapdata[0, 1] = People.EMPTY; mapdata[1, 1] = People.ARABMALE; mapdata[2, 1] = People.ARABMALE; mapdata[3, 1] = People.NONE; mapdata[4, 1] = People.EMPTY; mapdata[5, 1] = People.EMPTY; mapdata[6, 1] = People.EMPTY;
                mapdata[0, 2] = People.NONE; mapdata[1, 2] = People.NONE; mapdata[2, 2] = People.NONE; mapdata[3, 2] = People.NONE; mapdata[4, 2] = People.ARABMALE; mapdata[5, 2] = People.EMPTY; mapdata[6, 2] = People.EMPTY;
                mapdata[0, 3] = People.NONE; mapdata[1, 3] = People.EMPTY; mapdata[2, 3] = People.NONE; mapdata[3, 3] = People.WHITEFEMALE; mapdata[4, 3] = People.NONE; mapdata[5, 3] = People.EMPTY; mapdata[6, 3] = People.EMPTY;
                mapdata[0, 4] = People.EMPTY; mapdata[1, 4] = People.EMPTY; mapdata[2, 4] = People.ARABMALE; mapdata[3, 4] = People.NONE; mapdata[4, 4] = People.BLACKMALE; mapdata[5, 4] = People.NONE; mapdata[6, 4] = People.ARABMALE;
                mapdata[0, 5] = People.EMPTY; mapdata[1, 5] = People.EMPTY; mapdata[2, 5] = People.NONE; mapdata[3, 5] = People.ARABMALE; mapdata[4, 5] = People.EMPTY; mapdata[5, 5] = People.ARABMALE; mapdata[6, 5] = People.NONE;
                mapdata[0, 6] = People.EMPTY; mapdata[1, 6] = People.EMPTY; mapdata[2, 6] = People.NONE; mapdata[3, 6] = People.NONE; mapdata[4, 6] = People.LATINFEMALE; mapdata[5, 6] = People.EMPTY; mapdata[6, 6] = People.EMPTY;

                selectionNum[0] = 0;
                selectionNum[1] = 1;
                selectionNum[2] = 0;
                selectionNum[3] = 1;
                selectionNum[4] = 0;
                selectionNum[5] = 1;
                for (int i = 0; i < 6; ++i)
                {
                    selectionText[i].text = ": " + selectionNum[i].ToString();
                }
                mapUpdate();
                break;
            case 10:

                setSizeOfMap(7);

                mapdata[0, 0] = People.EMPTY; mapdata[1, 0] = People.ARABMALE; mapdata[2, 0] = People.NONE; mapdata[3, 0] = People.EMPTY; mapdata[4, 0] = People.EMPTY; mapdata[5, 0] = People.EMPTY; mapdata[6, 0] = People.EMPTY;
                mapdata[0, 1] = People.EMPTY; mapdata[1, 1] = People.NONE; mapdata[2, 1] = People.ARABMALE; mapdata[3, 1] = People.NONE; mapdata[4, 1] = People.EMPTY; mapdata[5, 1] = People.EMPTY; mapdata[6, 1] = People.EMPTY;
                mapdata[0, 2] = People.ARABMALE; mapdata[1, 2] = People.NONE; mapdata[2, 2] = People.BLACKMALE; mapdata[3, 2] = People.ARABMALE; mapdata[4, 2] = People.ARABMALE; mapdata[5, 2] = People.EMPTY; mapdata[6, 2] = People.EMPTY;
                mapdata[0, 3] = People.EMPTY; mapdata[1, 3] = People.ARABMALE; mapdata[2, 3] = People.ARABMALE; mapdata[3, 3] = People.KOREANFEMALE; mapdata[4, 3] = People.EMPTY; mapdata[5, 3] = People.EMPTY; mapdata[6, 3] = People.EMPTY;
                mapdata[0, 4] = People.EMPTY; mapdata[1, 4] = People.NONE; mapdata[2, 4] = People.LATINFEMALE; mapdata[3, 4] = People.ARABMALE; mapdata[4, 4] = People.EMPTY; mapdata[5, 4] = People.EMPTY; mapdata[6, 4] = People.EMPTY;
                mapdata[0, 5] = People.EMPTY; mapdata[1, 5] = People.NONE; mapdata[2, 5] = People.LATINFEMALE; mapdata[3, 5] = People.ARABMALE; mapdata[4, 5] = People.EMPTY; mapdata[5, 5] = People.EMPTY; mapdata[6, 5] = People.EMPTY;
                mapdata[0, 6] = People.EMPTY; mapdata[1, 6] = People.NONE; mapdata[2, 6] = People.LATINFEMALE; mapdata[3, 6] = People.ARABMALE; mapdata[4, 6] = People.EMPTY; mapdata[5, 6] = People.EMPTY; mapdata[6, 6] = People.EMPTY;

                selectionNum[0] = 0;
                selectionNum[1] = 0;
                selectionNum[2] = 0;
                selectionNum[3] = 0;
                selectionNum[4] = 0;
                selectionNum[5] = 0;
                for (int i = 0; i < 6; ++i)
                {
                    selectionText[i].text = ": " + selectionNum[i].ToString();
                }
                mapUpdate();
                break;


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
                updateTime = 0.7f;
            }
        }
    }

    public void setSizeOfMap(int size)
    {
        map = new PassPeople[size, size];
        tissueMap = new bool[size, size];
        for (int i = 0; i < size; ++i)
            for (int j = 0; j < size; ++j)
                tissueMap[i, j] = false;
        preTissueMap = new bool[size, size];
        mapdata = new People[size, size];
    }

    public void mapUpdate()
    {
        for (int i = 0; i < 6; ++i)
        {
            selectionText[i].text = ": " + selectionNum[i].ToString();
        }
        if (objectList == null)
        {
            objectList = new List<GameObject>();
            for (int i = 0; i < mapdata.GetLength(0); i++)
            {
                for (int j = 0; j < mapdata.GetLength(1); ++j)
                {
                    if (mapdata[i, j] != People.EMPTY)
                    {
                        GameObject tmp = Instantiate(objectPrefabs[0]);
                        if (!objectList.Contains(tmp))
                            objectList.Add(tmp);
                        tmp.transform.SetParent(gameCanvas.transform);
                        foreach (var item in tmp.GetComponentsInChildren<RectTransform>())
                        {
                            item.position = new Vector2(20 + i * (680f / map.GetLength(0)) + (680f / map.GetLength(0)) / 2, 720 - (20 + j * (680f / map.GetLength(1)) + (680f / map.GetLength(1)) / 2));
                            item.sizeDelta = new Vector2(680f / map.GetLength(0), 680f / map.GetLength(1));
                        }
                        Image[] images = tmp.GetComponentsInChildren<Image>();
                        Image image = images[1];
                        switch (mapdata[i, j])
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
                                if (map[i, j].IsTissueReceived)
                                {
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
        else
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); ++j)
                {
                    if (map[i, j] == null)
                        continue;
                    GameObject tmp = map[i, j].gameObject;
                    Image[] images = tmp.GetComponentsInChildren<Image>();
                    Image image = images[1];
                    switch (mapdata[i, j])
                    {
                        case People.NONE:
                            break;
                        default:
                            if (map[i, j].IsTissueReceived)
                            {
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
                if (preTissueMap[i, j] && map[i, j] != null)
                {
                    map[i, j].PassTissue();
                }
            }
        }
        mapUpdate();
        if (!IsEnded())
        {
            
        }
        else
        {
            checkResult();
            if (isSuccess)
            {
                InputManager.Inst().Success();
                Debug.Log("You success!");
            }
            else
            {
                Debug.Log("You failed!");
            }
            state = State.IDLE;
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
            for (int j = 0; j < map.GetLength(1) && map[i,j] != null && map[i,j].getPeople() != People.NONE; ++j)
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
    public void setStage(int stageNum)
    {
        stage = stageNum;
    }
    public int getStage()
    {
        return stage;
    }
}
