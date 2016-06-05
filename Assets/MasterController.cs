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
        if (this.selectedCube != null) {
            bool animDone = this.selectedCube.GetComponent<TrueAspectRatio>().done;
            GetGrid gg = this.selectedCube.GetComponent<GetGrid>();
            if (gg != null && gg.done && animDone) {            
                this.HandleFinishSelect(gg.result);                
                ClickHandler hco = this.selectedCube.GetComponent<ClickHandler>();
                Destroy (hco);
                Destroy (gg);
                Destroy (this.selectedCube);
                this.selectedCube = null;                
            }
        }
    }
    
    private void HandleFinishSelect(Color[,] averages){
        int rows = averages.GetLength(0);
        int columns = averages.GetLength(1);
        
        float xInc = this.selectedCube.transform.localScale.x / (columns);
        float yInc = this.selectedCube.transform.localScale.y / (rows);
        float startX = xInc * -columns/2.0f +0.5f * xInc;
        float startY = yInc * -rows/2.0f+0.5f * yInc;
        Debug.Log (this.selectedCube.transform.localScale.x);
        Debug.Log (xInc * columns);
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < columns; x++) {
                GameObject go = this.CreateCube();
                Destroy(go.GetComponent<LoadImage>());
                Transform t = go.transform;
                t.localScale = new Vector3(xInc,yInc,1.0f);        
                t.localPosition = new Vector3(xInc * x +startX,
                                              yInc*y +startY,
                                                0.0f);
                t.SetParent(this.transform);
                go.SetActive(true);
                go.GetComponent<Renderer>().material.color = averages[y,x];
            }
        }
        
        
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
