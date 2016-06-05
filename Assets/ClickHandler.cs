using UnityEngine;
using System.Collections;

public class ClickHandler : MonoBehaviour {
    public MasterController mc;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseOver(){
        
        if(Input.GetMouseButtonDown(0)){
            this.mc.HandleClickObject(this);
        }
    }
}
