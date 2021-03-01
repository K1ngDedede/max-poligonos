using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animaçaoC : MonoBehaviour
{
    private List<Keyframe> allesX, allesY, allesZ, allesR;
    private AnimationClip ac;
    void Start()
    {
        
    }

    public void walkCycle(int sp)
    {
        List<GameObject> patas = giantEnemyCrab.gibLegs();
        foreach (GameObject p in patas)
        {
            animaçaoCL aCL = p.GetComponent<animaçaoCL>();
            aCL.walkCycle(sp);
        }
    }
    
    public float goTo(float x, float z)
    {
        //Triangulación de posición y ángulo
        float sigma =
            gameObject.transform.eulerAngles.y + (float) Math.Atan((z - gameObject.transform.position.z) / (x - gameObject.transform.position.x));
        return sigma;

    }
    
    public void unearth(Animation a)
    {
        ac = new AnimationClip();
        ac.legacy = true;
        allesX = new List<Keyframe>();
        allesY = new List<Keyframe>();
        allesZ = new List<Keyframe>();
        allesR = new List<Keyframe>();
        Vector3 ground0 = gameObject.transform.position;
        //Evento de animación de caminar 1
        AnimationEvent cWalk = new AnimationEvent();
        cWalk.intParameter = 1;
        cWalk.time = 0;
        cWalk.functionName = "walkCycle";
        ac.AddEvent(cWalk);
        //Se desentierra el cangrejo
        allesY.Add(new Keyframe(3, ground0.y));
        allesY.Add(new Keyframe(8, 1));
        //Se mueve a embestir la torre
        allesX.Add(new Keyframe(8, ground0.x));
        allesZ.Add(new Keyframe(8, ground0.z));
        allesR.Add(new Keyframe(8, gameObject.transform.eulerAngles.y));
        float sigma = goTo(30.1f, 17f);
        allesR.Add(new Keyframe(8.1f, sigma));
        allesX.Add(new Keyframe(9.8f, 30.1f));
        allesZ.Add(new Keyframe(9.8f, 17f));
        allesX.Add(new Keyframe(10f, 30.3f));
        allesZ.Add(new Keyframe(10f, 17.3f));
        //Evento para parar de caminar 1
        cWalk = new AnimationEvent();
        cWalk.intParameter = 0;
        cWalk.time = 10.2f;
        cWalk.functionName = "walkCycle";
        ac.AddEvent(cWalk);
        //Evento de animación de caminar 2
        cWalk = new AnimationEvent();
        cWalk.intParameter = 1;
        cWalk.time = 40;
        cWalk.functionName = "walkCycle";
        ac.AddEvent(cWalk);
        //Se mueve a aplastar al caballero
        allesR.Add(new Keyframe(40f, sigma));
        allesX.Add(new Keyframe(40f, 30.3f));
        allesZ.Add(new Keyframe(40f, 17.3f));
        sigma = goTo(11.8f, 8.8f);
        allesR.Add(new Keyframe(40.2f, sigma));
        allesX.Add(new Keyframe(42f, 12.2f));
        allesZ.Add(new Keyframe(42f, 11.2f));
        //Evento para parar de caminar 2
        cWalk = new AnimationEvent();
        cWalk.intParameter = 0;
        cWalk.time = 42f;
        cWalk.functionName = "walkCycle";
        ac.AddEvent(cWalk);
        //lmao esto se fue a la clase principal
        
        
        //Curvas y declaración de la animación
        Keyframe[] weltX = allesX.ToArray();
        Keyframe[] weltY = allesY.ToArray();
        Keyframe[] weltZ = allesZ.ToArray();
        Keyframe[] weltR = allesR.ToArray();
        AnimationCurve Xanima = new AnimationCurve(weltX);
        AnimationCurve Yanima = new AnimationCurve(weltY);
        AnimationCurve Zanima = new AnimationCurve(weltZ);
        AnimationCurve Ranima = new AnimationCurve(weltR);
        ac.SetCurve("",typeof(Transform),"localPosition.x",Xanima);
        ac.SetCurve("",typeof(Transform),"localPosition.y",Yanima);
        ac.SetCurve("",typeof(Transform),"localPosition.z",Zanima);
        ac.SetCurve("",typeof(Transform),"eulerAngles.x",AnimationCurve.Constant(0, 150,gameObject.transform.eulerAngles.x));
        ac.SetCurve("",typeof(Transform),"eulerAngles.y",Ranima);
        ac.SetCurve("",typeof(Transform),"eulerAngles.z",AnimationCurve.Constant(0, 150,gameObject.transform.eulerAngles.z));
        a.AddClip(ac, "unearth");
        a.Play("unearth");
    }

    
}