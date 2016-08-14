using UnityEngine;
using System.Collections;

public class PassPeople4 : PassPeople{

	// Use this for initialization
	void Start () {
        PassLocation.Add(new Vector2(0, 2));
        PassLocation.Add(new Vector2(2, 0));
        PassLocation.Add(new Vector2(0, -2));
        PassLocation.Add(new Vector2(-2, 0));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
