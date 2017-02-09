using UnityEngine;
using System.Collections;

public class LoadImage : MonoBehaviour
{
    public string filePath;
    public bool web = true;
    
    public bool loaded = false;
    
    IEnumerator Start ()
    {
        if (web) {
            filePath = filePath.Replace(" ", "%20");
        }
        FadeTwoTextures ftt = this.gameObject.AddComponent<FadeTwoTextures>();
        ftt.SetValue(1.0f);
        ftt.enabled = false;
        // Start a download of the given URL
        WWW www = new WWW (filePath);        
    
        // Wait for download to complete
        yield return www;
    
        // assign texture
        Renderer renderer = GetComponent<Renderer> ();
        Texture t = www.texture;
        renderer.material.mainTexture = t;
        if (t != null) {
            float aspect = t.width / (float)t.height;            
            if (aspect > 1.0f) { //width > height
                renderer.material.mainTextureScale = new Vector2 (1.0f / aspect, 1.0f);
                renderer.material.mainTextureOffset = new Vector2 (0.5f - (0.5f * 1.0f / aspect), 0.0f);
            } else {
                aspect = t.height / (float)t.width;
                renderer.material.mainTextureScale = new Vector2 (1.0f, 1.0f / aspect);
                renderer.material.mainTextureOffset = new Vector2 (0.0f, (0.5f - (0.5f * 1.0f / aspect)));
            }
        }
        this.loaded = true;
        ftt.delay = Random.Range(-3.0f,-0.5f);
        ftt.startValue = 1.0f;
        ftt.endValue = 0.0f;
        ftt.enabled = true;
    }
  
    // Update is called once per frame
    void Update ()
    {
    
    }
}
