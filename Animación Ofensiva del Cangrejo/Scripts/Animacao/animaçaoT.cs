using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animaçaoT : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void crumble(Animation a)
    {
        AnimationClip ac = new AnimationClip();
        ac.legacy = true;
        //se derrumba la torre
        AnimationCurve und = AnimationCurve.EaseInOut(10f, 2f, 15f, -2f);
        //el siguiente algoritmo me lo inventé a último minuto para hacer que la torre caiga más chevere
        //lmao, no sirvió
        /*int max = 10;
        Keyframe[] ax = new Keyframe[max];
        Keyframe[] az = new Keyframe[max];
        float frac = 10f;
        for (int kneel = 0; kneel < max; kneel++)
        {
            float x = Random.Range(-0.05f, 0.05f);
            float z = Random.Range(-0.05f, 0.05f);
            ax[kneel] = new Keyframe(frac, gameObject.transform.position.x + x);
            az[kneel] = new Keyframe(frac, gameObject.transform.position.z + z);
            frac += 5 / max;
        }*/
        ac.SetCurve("",typeof(Transform),"localPosition.x",AnimationCurve.Constant(10, 15,gameObject.transform.localPosition.x));
        //ac.SetCurve("",typeof(Transform),"localPosition.x",new AnimationCurve(ax));
        ac.SetCurve("",typeof(Transform),"localPosition.y",und);
        ac.SetCurve("",typeof(Transform),"localPosition.z",AnimationCurve.Constant(10, 15,gameObject.transform.localPosition.z));
        //ac.SetCurve("",typeof(Transform),"localPosition.z",new AnimationCurve(az));
        a.AddClip(ac, "crumble");
        a.Play("crumble");
    }
    
}
