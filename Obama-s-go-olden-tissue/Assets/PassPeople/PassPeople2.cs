using UnityEngine;
using System.Collections;

public class PassPeople2 : PassPeople {

	// Use this for initialization
	void Start () {
        people = People.WHITEFEMALE;
        PassLocation.Add(new Vector2(1, 1));
        PassLocation.Add(new Vector2(1, -1));
        PassLocation.Add(new Vector2(-1, -1));
        PassLocation.Add(new Vector2(-1, 1));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
