using UnityEngine;
using System.Collections;

public class LoadImage : MonoBehaviour
{
    public string filePath;

    IEnumerator Start ()
    {
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
    
    }
  
    // Update is called once per frame
    void Update ()
    {
    
    }
}
