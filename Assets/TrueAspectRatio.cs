using UnityEngine;
using System.Collections;

public class TrueAspectRatio : MonoBehaviour {
    public float blowupSize = 10.0f;
	// Use this for initialization
    
    private Vector2 startTextureScale;
    private Vector2 startTextureOffset;
    
    private Vector3 startScale;
    private Vector3 targetScale;
    
    private Vector3 startPosition;    
    
    public float animationTime = 3.0f;
    private float animTimer = 0.0f;
    
    void Start () {
	    Renderer r = this.gameObject.GetComponent<Renderer>();
        Texture t = r.material.mainTexture;
        float aspect = t.width/(float)t.height;
        this.targetScale = new Vector3(blowupSize*aspect,blowupSize*(1.0f/aspect),1.0f);
        this.startScale = this.transform.localScale;
        
        this.startTextureScale = r.material.mainTextureScale;
        this.startTextureOffset = r.material.mainTextureOffset;    
        this.startPosition = this.transform.position;
    }
  
	// Update is called once per frame
	void Update () {
	    animTimer += Time.deltaTime;
        float perc = animTimer/animationTime;
        perc = Mathf.Clamp01(perc);
        
        Renderer r = this.gameObject.GetComponent<Renderer>();
        r.material.mainTextureScale = Vector2.Lerp(startTextureScale,Vector2.one,perc);
        r.material.mainTextureOffset = Vector2.Lerp(startTextureOffset,Vector2.zero,perc);
        
        this.transform.localScale = Vector3.Lerp(startScale,targetScale,perc);
        this.transform.position = Vector3.Lerp(this.startPosition,Vector3.zero,perc);
    
	}
}
