using UnityEngine;
using System.Collections;

public class MasterController : MonoBehaviour {
    public GameObject selectedCube;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void HandleClickObject(ClickHandler ch) {
        if (this.selectedCube != null) {
            return;
        }
        //TODO: handle click.
    }
}
