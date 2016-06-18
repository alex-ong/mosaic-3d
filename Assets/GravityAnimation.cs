using UnityEngine;
using System.Collections;

public class GravityAnimation : DestroyCubeAnimation
{
    public float sideForce = 0.3f;

    protected override void OnFinishAnimation()
    {
        base.OnFinishAnimation();
    }

    protected override void OnStartAnimation()
    {
        foreach (GameObject go in this.items) {
            Rigidbody rb = go.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.AddForce(new Vector3(spreadRange(), 0.0f, spreadRange()));
            rb.AddRelativeTorque(new Vector3(10.0f, 0.0f, -10.0f));
        }
    }

    protected float spreadRange()
    {
        float result = Random.Range(sideForce * .5f, sideForce);
        result *= Random.Range(0, 1) > 0.5f ? 1f : -1f;
        return result;
    }

    protected override void UpdateAnimation(float perc)
    {
       
    }
}
