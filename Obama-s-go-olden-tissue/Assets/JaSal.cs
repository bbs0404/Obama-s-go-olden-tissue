using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JaSal : MonoBehaviour {

    public void OnBlankButtonClicked(Button button)
    {
        if (GameManager.Inst().getState() == State.MOVING && button.GetComponentInChildren<PassPeople>().people == People.NONE)
        {
            Image[] objects = button.GetComponentsInChildren<Image>();

            switch (GameManager.Inst().getMovingPeople().getPeople())
            {
                case People.ARABMALE:
                    {
                        Vector2 tmpvecor = objects[0].GetComponent<PassPeople>().IdentityLocation;
                        //bool tmpbool = objects[0].GetComponent<PassPeople>().IsTissueReceived;
                        Destroy(objects[0].gameObject.GetComponent<PassPeople>());
                        PassPeople1 p = objects[0].gameObject.AddComponent<PassPeople1>();
                        p.IdentityLocation = tmpvecor;
                        GameManager.Inst().getMapData()[(int)tmpvecor.x, (int)tmpvecor.y] = People.ARABMALE;
                        GameManager.Inst().getMap()[(int)tmpvecor.x, (int)tmpvecor.y] = p;
                        p.setPeople(People.ARABMALE);
                        objects[0].color = new Color(47f / 255f, 121f / 255f, 60f / 255f, 0.5f);
                    break;
                    }
                case People.ASIANMALE:
                    {
                        Vector2 tmpvecor = objects[0].GetComponent<PassPeople>().IdentityLocation;
                        //bool tmpbool = objects[0].GetComponent<PassPeople>().IsTissueReceived;
                        Destroy(objects[0].gameObject.GetComponent<PassPeople>());
                        PassPeople5 p = objects[0].gameObject.AddComponent<PassPeople5>();
                        p.IdentityLocation = tmpvecor;
                        GameManager.Inst().getMapData()[(int)tmpvecor.x, (int)tmpvecor.y] = People.ASIANMALE;
                        GameManager.Inst().getMap()[(int)tmpvecor.x, (int)tmpvecor.y] = p;
                        p.setPeople(People.ASIANMALE);
                        objects[0].color = new Color(135f / 255f, 179f / 255f, 64f / 255f, 0.5f);
                    break;
                    }
                case People.BLACKMALE:
                    {
                        Vector2 tmpvecor = objects[0].GetComponent<PassPeople>().IdentityLocation;
                        //bool tmpbool = objects[0].GetComponent<PassPeople>().IsTissueReceived;
                        Destroy(objects[0].gameObject.GetComponent<PassPeople>());
                        PassPeople4 p = objects[0].gameObject.AddComponent<PassPeople4>();
                        p.IdentityLocation = tmpvecor;
                        GameManager.Inst().getMapData()[(int)tmpvecor.x, (int)tmpvecor.y] = People.BLACKMALE;
                        GameManager.Inst().getMap()[(int)tmpvecor.x, (int)tmpvecor.y] = p;
                        p.setPeople(People.BLACKMALE);
                        objects[0].color = new Color(255f / 255f, 158f / 255f, 87f / 255f, 0.5f);
                    break;
                    }
                case People.KOREANFEMALE:
                    {
                        Vector2 tmpvecor = objects[0].GetComponent<PassPeople>().IdentityLocation;
                        //bool tmpbool = objects[0].GetComponent<PassPeople>().IsTissueReceived;
                        Destroy(objects[0].gameObject.GetComponent<PassPeople>());
                        PassPeople3 p = objects[0].gameObject.AddComponent<PassPeople3>();
                        p.IdentityLocation = tmpvecor;
                        GameManager.Inst().getMapData()[(int)tmpvecor.x, (int)tmpvecor.y] = People.KOREANFEMALE;
                        GameManager.Inst().getMap()[(int)tmpvecor.x, (int)tmpvecor.y] = p;
                        p.setPeople(People.KOREANFEMALE);
                        objects[0].color = new Color(255f / 255f, 142f / 255f, 175f / 255f, 0.5f);
                    break;
                    }
                case People.LATINFEMALE:
                    {
                        Vector2 tmpvecor = objects[0].GetComponent<PassPeople>().IdentityLocation;
                        //bool tmpbool = objects[0].GetComponent<PassPeople>().IsTissueReceived;
                        Destroy(objects[0].gameObject.GetComponent<PassPeople>());
                        PassPeople6 p = objects[0].gameObject.AddComponent<PassPeople6>();
                        p.IdentityLocation = tmpvecor;
                        GameManager.Inst().getMapData()[(int)tmpvecor.x, (int)tmpvecor.y] = People.LATINFEMALE;
                        GameManager.Inst().getMap()[(int)tmpvecor.x, (int)tmpvecor.y] = p;
                        p.setPeople(People.LATINFEMALE);
                        objects[0].color = new Color(101f / 255f, 73f / 255f, 121f / 255f, 0.5f);
                    break;
                    }
                case People.WHITEFEMALE:
                    {
                        Vector2 tmpvecor = objects[0].GetComponent<PassPeople>().IdentityLocation;
                        //bool tmpbool = objects[0].GetComponent<PassPeople>().IsTissueReceived;
                        Destroy(objects[0].gameObject.GetComponent<PassPeople>());
                        PassPeople2 p = objects[0].gameObject.AddComponent<PassPeople2>();
                        p.IdentityLocation = tmpvecor;
                        GameManager.Inst().getMapData()[(int)tmpvecor.x, (int)tmpvecor.y] = People.WHITEFEMALE;
                        GameManager.Inst().getMap()[(int)tmpvecor.x, (int)tmpvecor.y] = p;
                        p.setPeople(People.WHITEFEMALE);
                        objects[0].color = new Color(19f / 255f, 105f / 255f, 164f / 255f, 0.5f);
                    break;
                    }
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
}
