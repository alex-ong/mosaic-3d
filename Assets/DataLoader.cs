using UnityEngine;
using System.Collections;

public class DataLoader : MonoBehaviour {
    public string fileToLoad;
	// Use this for initialization
	void Start () {
        WWW request = new WWW(fileToLoad);
        
        while(!request.isDone) {            
        }
        Debug.Log (request.error);
        Debug.Log("Data: " + request.text);
	}
	
}
