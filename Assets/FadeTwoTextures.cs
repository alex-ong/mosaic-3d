using UnityEngine;
using System.Collections;

public class FadeTwoTextures : MonoBehaviour {
    public float delay = 0.0f;
    private float animationTime = 0.5f;
    private float timer = 0.0f;
    public float startValue;
    public float endValue;

	// Use this for initialization
	void Start () {
        this.timer = delay;
	}
	
    
	// Update is called once per frame
	void Update () {
        this.timer += Time.deltaTime;
        float perc = Mathf.InverseLerp(0.0f, animationTime, timer);
        float value = Mathf.Lerp(startValue, endValue, perc);

        Material m = this.GetComponent<Renderer>().material;
        m.SetFloat("_BlendAlpha", value);
	}

    public void SetValue(float value)
    {
        Material m = this.GetComponent<Renderer>().material;
        m.SetFloat("_BlendAlpha", value);
    }
}
