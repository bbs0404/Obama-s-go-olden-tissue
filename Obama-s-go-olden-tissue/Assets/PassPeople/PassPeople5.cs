using UnityEngine;
using System.Collections;

public class PassPeople5 : PassPeople {

	// Use this for initialization
	void Start () {
        people = People.ASIANMALE;
        PassLocation.Add(new Vector2(0, 1));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
