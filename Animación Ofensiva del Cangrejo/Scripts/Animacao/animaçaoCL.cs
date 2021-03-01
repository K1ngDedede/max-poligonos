using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animaçaoCL : MonoBehaviour
{
    protected Animation a;
    protected AnimationClip ac;
    protected bool walking;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent(typeof(Animation));
        a = GetComponent<Animation>();
        ac = new AnimationClip();
        ac.legacy = true;
        walking = false;
        float ts = Random.Range(0f, 1f);
        float xAlpha = gameObject.transform.localPosition.x;
        Keyframe[] ks = new Keyframe[4];
        ks[0] = new Keyframe(ts, xAlpha);
        ks[1] = new Keyframe(ts+0.25f, xAlpha+0.1f);
        ks[2] = new Keyframe(ts+0.75f, xAlpha-0.1f);
        ks[3] = new Keyframe(ts+1f, xAlpha);
        AnimationCurve r1 = new AnimationCurve(ks);
        ac.SetCurve("",typeof(Transform),"localPosition.x",r1);
        ac.SetCurve("",typeof(Transform),"localPosition.y",AnimationCurve.Constant(ts, ts+1,gameObject.transform.localPosition.y));
        ac.SetCurve("",typeof(Transform),"localPosition.z",AnimationCurve.Constant(ts,ts+1,gameObject.transform.localPosition.z));
        a.AddClip(ac, "scuttle");
        //walkCycle(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void walkCycle(int sapo)
    {
        if (sapo == 1)
        {
            walking = true;
            a.wrapMode = WrapMode.Loop;
            a.Play("scuttle");
        }
        else
        {
            walking = false;
            a.wrapMode = WrapMode.Once;
        }
    }
}
