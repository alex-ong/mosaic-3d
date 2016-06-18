using UnityEngine;
using System.Collections;

public class FadeColour : MonoBehaviour
{
    public float delay = 0.0f;
    public float animationTime = 0.5f;
    private float timer = 0.0f;
    public Color startValue;
    public Color endValue;

    // Use this for initialization
    void Start()
    {
        this.timer = delay;
    }


    // Update is called once per frame
    void Update()
    {
        this.timer += Time.deltaTime;
        float perc = Mathf.InverseLerp(0.0f, animationTime, timer);
        Color value = Color.Lerp(startValue, endValue, perc);

        this.SetValue(value);
        if (perc >= 1.0f) {
            Destroy(this);
        }
    }

    public Color getValue()
    {
        return this.GetComponent<Renderer>().material.color;
    }

    public void SetValue(Color value)
    {
        Material m = this.GetComponent<Renderer>().material;
        m.SetColor("_Color", value);
    }
}
