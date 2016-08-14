using UnityEngine;
using System.Collections;

public class PassPeople6 : PassPeople {

	// Use this for initialization
	void Start () {
        people = People.LATINFEMALE;
        PassLocation.Add(new Vector2(1, 1));
        PassLocation.Add(new Vector2(-1, 1));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
