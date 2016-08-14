using UnityEngine;
using System.Collections;

public class PassPeople3 : PassPeople {

	// Use this for initialization
	void Start () {
        PassLocation.Add(new Vector2(0, 1));
        PassLocation.Add(new Vector2(1, 0));
        PassLocation.Add(new Vector2(0, -1));
        PassLocation.Add(new Vector2(-1, 0));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
