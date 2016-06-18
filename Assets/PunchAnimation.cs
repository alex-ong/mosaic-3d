using UnityEngine;
using System.Collections;

public class PunchAnimation : DestroyCubeAnimation {
    public float punchForce = 100f;

    protected override void OnFinishAnimation()
    {
        base.OnFinishAnimation();
    }

    protected override void OnStartAnimation()
    {
        foreach (GameObject go in this.items) {
            Rigidbody rb = go.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.AddExplosionForce(punchForce,Vector3.zero,100f);
        }
    }
        

    protected override void UpdateAnimation(float perc)
    {

    }
}
