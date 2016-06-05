using UnityEngine;
using System.Collections;

public class MasterController : MonoBehaviour {
    public GameObject selectedCube;
    public GameObject sampleCube;
	// Use this for initialization
	void Start () {
	    GameObject go = CreateCube();
        go.SetActive(true);
	}
    
	protected GameObject CreateCube() {
        GameObject go = GameObject.Instantiate(this.sampleCube);
        go.transform.SetParent(this.transform);
        return go;
    }
   
	// Update is called once per frame
	void Update () {
	
	}
    
    public void HandleClickObject(ClickHandler ch) {
        if (this.selectedCube != null) {
            return;
        }
        this.selectedCube = ch.gameObject;
        this.selectedCube.GetComponent<TrueAspectRatio>().enabled = true;
        this.selectedCube.GetComponent<GetGrid>().enabled = true;
    }
}
