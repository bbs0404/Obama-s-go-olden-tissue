using UnityEngine;
using System.Collections;

public enum People {
    EMPTY,
    NONE,
    Foo,
    Boo
}

public class PassPeople : MonoBehaviour{

    bool IsTissueReceived;
    public bool IsPinned;
    Vector2 IdentityLocation;
    People people;
    ArrayList PassLocation = new ArrayList(); // 각 하위 클래스 start에서 이거 채워 넣어야 함(vector 2)
    // Use this for initialization
    void Start() {
        IsTissueReceived = false;
    }
    void OnMouseDown() {
        if(GameManager.Inst().getState() == State.IDLE) {
            if(!IsPinned) {
                GameManager.Inst().setMovingPeople(this);
                GameManager.Inst().setState(State.MOVING);
            }
        }
        if(GameManager.Inst().getState() == State.MOVING) {
            if(this.people == People.NONE && GameManager.Inst().getMovingPeopleList().Contains(this)) {
                this.people = GameManager.Inst().getMovingPeople().people;
                GameManager.Inst().setState(State.IDLE);
                GameManager.Inst().getMovingPeopleList().Remove(this);
                if(GameManager.Inst().getMovingPeopleList().Count <= 0)
                {
                    GameManager.Inst().setMoveFinished(true);
                }
                Destroy(this);
            }
            GameManager.Inst().mapUpdate();
        }
    }
    public void PassTissue() { // 티슈를 받았을 때 넘기는 행동
        IsTissueReceived = true;
        bool[,] tempTissueMap = GameManager.Inst().getTissueMap();
        foreach(Vector2 Loc in PassLocation) {
            Vector2 focus = new Vector2(IdentityLocation.x + Loc.x, IdentityLocation.y + Loc.y);
            if(focus.x < tempTissueMap.GetLength(0) && focus.y < tempTissueMap.GetLength(1) && !tempTissueMap[(int)focus.x, (int)focus.y]) {
                tempTissueMap[(int)focus.x, (int)focus.y] = true;
            }
        }
        GameManager.Inst().setTissueMap(tempTissueMap);
    }
}