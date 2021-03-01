using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animaçaoK : MonoBehaviour
{
    protected bool walking;
    private List<GameObject> legs;
    private float prevl;
    private float prevr;
    void Start()
    {
        walking = false;
        legs = giantEnemyCrab.gibLegsK(gameObject);
        prevl = -0.5f;
        prevr = 0.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (walking)
        {
            legs[0].transform.Rotate(prevl*Vector3.right,Space.Self);
            legs[1].transform.Rotate(prevr*Vector3.right,Space.Self);
            if (legs[0].transform.localEulerAngles.x >= 25 || legs[0].transform.localEulerAngles.x <= -25)
            {
                prevl = -prevl;
                prevr = -prevr;
            }
        }
    }
    
    public float goTo(float x, float z)
    {
        //Triangulación de posición y ángulo
        float sigma =
            gameObject.transform.eulerAngles.y + (float) Math.Atan((z - gameObject.transform.position.z) / (x - gameObject.transform.position.x));
        return sigma;

    }

    protected void walkCycle()
    {
        walking = !walking;
    }

    public void captain(Animation a)
    {
        AnimationClip ac = new AnimationClip();
        ac.legacy = true;
        //se voltea
        AnimationCurve rot = AnimationCurve.EaseInOut(18f, gameObject.transform.eulerAngles.y, 20f, gameObject.transform.eulerAngles.y-180);
        //Evento de animación de caminar 1
        AnimationEvent cWalk = new AnimationEvent();
        cWalk.time = 21;
        cWalk.functionName = "walkCycle";
        ac.AddEvent(cWalk);
        //camina
        AnimationCurve xgap = AnimationCurve.EaseInOut(21f, gameObject.transform.position.x, 27f, 2.6f);
        AnimationCurve zgap = AnimationCurve.EaseInOut(21f, gameObject.transform.position.z, 27f, 0.5f);
        AnimationCurve despawn = AnimationCurve.EaseInOut(25.35f, gameObject.transform.position.y, 29f, -2f);
        ac.SetCurve("",typeof(Transform),"localPosition.x",xgap);
        ac.SetCurve("",typeof(Transform),"localPosition.y",despawn);
        ac.SetCurve("",typeof(Transform),"localPosition.z",zgap);
        ac.SetCurve("",typeof(Transform),"localEulerAngles.y",rot);
        a.AddClip(ac, "capi");
        a.Play("capi");
    }

    public void order(Animation a, float x, float z, float alpha)
    {
        AnimationClip ac = new AnimationClip();
        ac.legacy = true;
        //Gira
        AnimationCurve rot = AnimationCurve.EaseInOut(alpha+30f, gameObject.transform.eulerAngles.y, alpha+30.5f, goTo(x,z));
        //Eventos de animación de caminar
        AnimationEvent cWalk = new AnimationEvent();
        cWalk.time = alpha+30.5f;
        cWalk.functionName = "walkCycle";
        ac.AddEvent(cWalk);
        cWalk = new AnimationEvent();
        cWalk.time = alpha+36f;
        cWalk.functionName = "walkCycle";
        ac.AddEvent(cWalk);
        //camina
        AnimationCurve xgap = AnimationCurve.EaseInOut(alpha+30.5f, gameObject.transform.position.x, alpha+36f, x);
        AnimationCurve zgap = AnimationCurve.EaseInOut(alpha+30.5f, gameObject.transform.position.z, alpha+36f, z);
        ac.SetCurve("",typeof(Transform),"localPosition.x",xgap);
        ac.SetCurve("",typeof(Transform),"localPosition.y",AnimationCurve.Constant(30, 50,gameObject.transform.localPosition.y));
        ac.SetCurve("",typeof(Transform),"localPosition.z",zgap);
        ac.SetCurve("",typeof(Transform),"localEulerAngles.y",rot);
        a.AddClip(ac, "order");
        a.Play("order");
    }
    
    public void orderA(Animation a, float x, float z, float alpha)
    {
        AnimationClip ac = new AnimationClip();
        ac.legacy = true;
        //Gira
        AnimationCurve rot = AnimationCurve.EaseInOut(alpha+30f, gameObject.transform.eulerAngles.y, alpha+30.5f, goTo(x,z));
        //Eventos de animación de caminar
        AnimationEvent cWalk = new AnimationEvent();
        cWalk.time = alpha+30.5f;
        cWalk.functionName = "walkCycle";
        ac.AddEvent(cWalk);
        cWalk = new AnimationEvent();
        cWalk.time = alpha+36f;
        cWalk.functionName = "walkCycle";
        ac.AddEvent(cWalk);
        //camina
        AnimationCurve xgap = AnimationCurve.EaseInOut(alpha+30.5f, gameObject.transform.position.x, alpha+36f, x);
        AnimationCurve zgap = AnimationCurve.EaseInOut(alpha+30.5f, gameObject.transform.position.z, alpha+36f, z);
        //Es aplastado
        AnimationCurve xarepa = AnimationCurve.EaseInOut(43f, gameObject.transform.localScale.x, 43.3f, 0.5f);
        AnimationCurve yarepa = AnimationCurve.EaseInOut(43f, gameObject.transform.localScale.y, 43.3f, 0.05f);
        AnimationCurve zarepa = AnimationCurve.EaseInOut(43f, gameObject.transform.localScale.z, 43.3f, 0.5f);
        AnimationCurve ygap = AnimationCurve.EaseInOut(43f, gameObject.transform.position.y, 43.3f, 0.1f);
        ac.SetCurve("",typeof(Transform),"localPosition.x",xgap);
        ac.SetCurve("",typeof(Transform),"localPosition.y",AnimationCurve.Constant(30, 50,gameObject.transform.localPosition.y));
        ac.SetCurve("",typeof(Transform),"localPosition.z",zgap);
        ac.SetCurve("",typeof(Transform),"localEulerAngles.y",rot);
        ac.SetCurve("",typeof(Transform),"localPosition.y",ygap);
        ac.SetCurve("",typeof(Transform),"localScale.x",xarepa);
        ac.SetCurve("",typeof(Transform),"localScale.y",yarepa);
        ac.SetCurve("",typeof(Transform),"localScale.z",zarepa);
        a.AddClip(ac, "order");
        a.Play("order");
    }
    
}
