using UnityEngine;
using System.Collections;
public class PassPeople : MonoBehaviour{

    bool IsTissueReceived;
    Vector2 IdentityLocation;
    ArrayList PassLocation = new ArrayList(); // 각 하위 클래스 start에서 이거 채워 넣어야 함(vector 2)
    // Use this for initialization
    void Start() {
        IsTissueReceived = false;
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