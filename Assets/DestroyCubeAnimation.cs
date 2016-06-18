using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class DestroyCubeAnimation : MonoBehaviour
{
    public Action OnFinish;
    public float time = 3.0f;
    private float timer = 0.0f;
    protected List<GameObject> items;

    public void StartAnimation(List<GameObject> items)
    {
        this.items = items;
        this.timer = 0.0f;
        this.OnStartAnimation();
    }

    protected virtual void OnStartAnimation()
    {
        //fill me in.    
    }
	
    // Update is called once per frame
    void Update()
    {
        this.timer += Time.deltaTime;
        float perc = timer / time;
        perc = Mathf.Clamp01(perc);
        this.UpdateAnimation(perc);
        if (perc >= 1.0f) {
            this.OnFinishAnimation();
            if (this.OnFinish != null)
                this.OnFinish();
            this.enabled = false;
        }
    }

    protected virtual void UpdateAnimation(float perc)
    {
    }

    protected virtual void OnFinishAnimation()
    {
    }
}
