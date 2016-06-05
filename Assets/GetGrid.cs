using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetGrid : MonoBehaviour {
    public int numColumns = 10;
    public float totalTimeAvailable;
    public float timeElapsed = 0.0f;
    private float mosaicsPerSecond;
    public int numMosaics;
    public int currentMosaic = 0;
    Color32[] data;
    
    private Vector2 imageSize;
    
    
    public Color[,] result; //2d array with result.
    public bool done = false;
    private List<Vector2> colStartEnd;
    private List<Vector2> rowStartEnd;
  
	// Use this for initialization
	void Start () {
	    Texture2D t = this.GetComponent<Renderer>().material.mainTexture as Texture2D;
        this.imageSize = new Vector2(t.width,t.height);
        
        this.data = t.GetPixels32(0);
        
        this.colStartEnd = new List<Vector2>();
        this.rowStartEnd = new List<Vector2>();
        
        float colSize = this.imageSize.x / numColumns;
        for (int i = 0; i < numColumns; i++) {
            colStartEnd.Add(new Vector2(Mathf.RoundToInt(i*colSize),Mathf.RoundToInt((i+1)*colSize)));
        }
        
        int numRows = Mathf.RoundToInt(this.imageSize.y / colSize);
        float rowSize = this.imageSize.y / numRows;
        for (int i = 0; i < numRows; i++) {
            rowStartEnd.Add(new Vector2(Mathf.RoundToInt(i*rowSize),Mathf.RoundToInt((i+1)*rowSize)));
        }
        
        result = new Color[numRows,numColumns];
        this.numMosaics = numColumns * numRows;
        this.mosaicsPerSecond = this.numMosaics /totalTimeAvailable;
    }
  
  
    // Update is called once per frame
	void Update () {
	    this.timeElapsed += Time.deltaTime;
        this.timeElapsed = Mathf.Clamp(this.timeElapsed,0.0f,this.totalTimeAvailable);
        
        int mosaicsDue = Mathf.FloorToInt(this.timeElapsed * mosaicsPerSecond) - currentMosaic;
        while (mosaicsDue > 0) {
            this.ProcessMosaic();
            mosaicsDue--;
        }
        if (this.currentMosaic == this.numMosaics && !this.done) {
            this.done = true;
        }
	}
    
    void ProcessMosaic()
    {
        int row = this.currentMosaic / this.numColumns;    
        int col = this.currentMosaic % this.numColumns;
        
        Vector2 xPixels = this.colStartEnd[col];
        Vector2 yPixels = this.rowStartEnd[row];
        
        int Rtotal= 0;
        int Gtotal= 0;
        int Btotal= 0;
        int count = 0;
        
        //Image is stored as columns.
        for (int y = (int)yPixels.x; y < (int)yPixels.y; y++) {
            for (int x = (int)xPixels.x; x < (int)xPixels.y; x++) {
                Color32 c = data[y*(int)imageSize.x + x];
                Rtotal += c.r;
                Gtotal += c.g;
                Btotal += c.b;
                count++;
            }
        }

        
        this.result[row,col] = new Color(Rtotal/count/255.0f,
                                         Gtotal/count/255.0f,
                                         Btotal/count/255.0f);
        this.currentMosaic++;
    }
    
    
}
