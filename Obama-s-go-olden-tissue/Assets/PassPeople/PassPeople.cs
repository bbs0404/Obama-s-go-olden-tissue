using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum People {
    EMPTY,
    NONE,
    ASIANMALE,
    LATINFEMALE,
    WHITEFEMALE,
    ARABMALE,
    KOREANFEMALE,
    BLACKMALE,
}

public class PassPeople : MonoBehaviour{

    public bool IsTissueReceived;
    public bool IsPinned;
    public Vector2 IdentityLocation;
    public People people;
    public List<Vector2> PassLocation = new List<Vector2>(); // 각 하위 클래스 start에서 이거 채워 넣어야 함(vector 2)
    // Use this for initialization
    void Start() {
        IsTissueReceived = false;
    }
    //void OnMouseDown() {
    //    if(GameManager.Inst().getState() == State.IDLE) {
    //        if(!IsPinned) {
    //            GameManager.Inst().setMovingPeople(this);
    //            GameManager.Inst().setState(State.MOVING);
    //        }
    //    }
    //    if(GameManager.Inst().getState() == State.MOVING) {
    //        if(this.people == People.NONE && GameManager.Inst().getMovingPeopleList().Contains(this)) {
    //            this.people = GameManager.Inst().getMovingPeople().people;
    //            GameManager.Inst().setState(State.IDLE);
    //            GameManager.Inst().getMovingPeopleList().Remove(this);
    //            if(GameManager.Inst().getMovingPeopleList().Count <= 0)
    //            {
    //                GameManager.Inst().setMoveFinished(true);
    //            }
    //            Destroy(this);
    //        }
    //        GameManager.Inst().mapUpdate();
    //    }
    //}
    public void PassTissue() { // 티슈를 받았을 때 넘기는 행동
        if(!IsTissueReceived) {


            IsTissueReceived = true;
            
            bool[,] tempTissueMap = GameManager.Inst().getTissueMap();
            PassPeople[,] tempMap = GameManager.Inst().getMap();
            for(int i=0; i<PassLocation.Count; ++i)
            {
                Vector2 focus = new Vector2(IdentityLocation.x + PassLocation[i].x, IdentityLocation.y + PassLocation[i].y);
                if (focus.x < tempTissueMap.GetLength(0) && focus.y < tempTissueMap.GetLength(1) && focus.x>=0 && focus.y>=0 && tempMap[(int)focus.x, (int)focus.y] != null && !tempMap[(int)focus.x, (int)focus.y].IsTissueReceived)
                {
                    tempTissueMap[(int)focus.x, (int)focus.y] = true;
                }
            }
            //foreach (var item in PassLocation)
            //{
            //    Vector2 focus = new Vector2(IdentityLocation.x + item.x, IdentityLocation.y + item.y);
            //    if (focus.x < tempTissueMap.GetLength(0) && focus.y < tempTissueMap.GetLength(1) && !tempMap[(int)focus.x, (int)focus.y].IsTissueReceived)
            //    {
            //        tempTissueMap[(int)focus.x, (int)focus.y] = true;
            //    }
            //}
            tempTissueMap[(int)IdentityLocation.x, (int)IdentityLocation.y] = false;

        }
    }
    public People getPeople()
    {
        return people;
    }
    public void setPeople(People person)
    {
        people = person;
    }
}