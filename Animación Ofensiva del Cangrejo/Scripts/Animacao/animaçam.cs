using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animaçam : MonoBehaviour
{
    private Animation a;
    void Start()
    {
        gameObject.AddComponent(typeof(Animation));
        gameObject.transform.position = new Vector3(0, 2, 0);
        a = GetComponent<Animation>();
        //se crean las escenas
        stage1();
        stage2();
        stage3();
        a.PlayQueued("stage1", QueueMode.PlayNow);
        a.PlayQueued("stage2", QueueMode.CompleteOthers);
        a.PlayQueued("stage3", QueueMode.CompleteOthers);
    }

    // Update is called once per frame
    void stage1()
    {
        AnimationClip ac = new AnimationClip();
        ac.legacy = true;
        Keyframe[] ax = new Keyframe[3];
        Keyframe[] az = new Keyframe[3];
        Keyframe[] ar = new Keyframe[3];
        ax[0] = new Keyframe(8,24);
        az[0] = new Keyframe(8, 24);
        ar[0] = new Keyframe(8, 75);
        ax[1] = new Keyframe(10, 22.8f);
        az[1] = new Keyframe(10, 18.5f);
        ar[1] = new Keyframe(10, 100);
        ax[2] = new Keyframe(15, 22.8f);
        az[2] = new Keyframe(15, 18.5f);
        ar[2] = new Keyframe(15, 100);
        AnimationCurve cx = new AnimationCurve(ax);
        AnimationCurve cz = new AnimationCurve(az);
        AnimationCurve cr = new AnimationCurve(ar);
        ac.SetCurve("",typeof(Transform),"localPosition.x",cx);
        ac.SetCurve("",typeof(Transform),"localPosition.y",AnimationCurve.Constant(0, 10,gameObject.transform.position.y));
        ac.SetCurve("",typeof(Transform),"localPosition.z",cz);
        ac.SetCurve("",typeof(Transform),"localEulerAngles.y",cr);
        a.AddClip(ac, "stage1");
    }
    
    void stage2()
    {
        AnimationClip ac = new AnimationClip();
        ac.legacy = true;
        Keyframe[] ax = new Keyframe[3];
        Keyframe[] ay = new Keyframe[3];
        Keyframe[] az = new Keyframe[3];
        Keyframe[] ar = new Keyframe[2];
        ax[0] = new Keyframe(1, -1);
        ay[0] = new Keyframe(1, 5);
        az[0] = new Keyframe(1, -2.5f);
        ar[0] = new Keyframe(1, 50);
        ax[1] = new Keyframe(2, 3);
        ay[1] = new Keyframe(2, 4.5f);
        az[1] = new Keyframe(2, 1.25f);
        ax[2] = new Keyframe(11, 3);
        ay[2] = new Keyframe(11, 4.5f);
        az[2] = new Keyframe(11, 1.25f);
        ar[1] = new Keyframe(11, 50);
        AnimationCurve cx = new AnimationCurve(ax);
        AnimationCurve cy = new AnimationCurve(ay);
        AnimationCurve cz = new AnimationCurve(az);
        AnimationCurve cr = new AnimationCurve(ar);
        ac.SetCurve("",typeof(Transform),"localPosition.x",cx);
        ac.SetCurve("",typeof(Transform),"localPosition.y",cy);
        ac.SetCurve("",typeof(Transform),"localPosition.z",cz);
        ac.SetCurve("",typeof(Transform),"localEulerAngles.y",cr);
        a.AddClip(ac, "stage2");
    }
    
    void stage3()
    {
        AnimationClip ac = new AnimationClip();
        ac.legacy = true;
        Keyframe[] ax = new Keyframe[3];
        Keyframe[] ay = new Keyframe[3];
        Keyframe[] az = new Keyframe[3];
        Keyframe[] ar = new Keyframe[3];
        ax[0] = new Keyframe(2, 6);
        ay[0] = new Keyframe(2, 7);
        az[0] = new Keyframe(2, 13);
        ar[0] = new Keyframe(2, -130);
        ax[1] = new Keyframe(4, 4);
        ay[1] = new Keyframe(4, 3);
        az[1] = new Keyframe(4, 14);
        ar[1] = new Keyframe(4, -170);
        ax[2] = new Keyframe(11, 8);
        ay[2] = new Keyframe(11, 2);
        az[2] = new Keyframe(11, 15);
        ar[2] = new Keyframe(11, -200);
        AnimationCurve cx = new AnimationCurve(ax);
        AnimationCurve cy = new AnimationCurve(ay);
        AnimationCurve cz = new AnimationCurve(az);
        AnimationCurve cr = new AnimationCurve(ar);
        ac.SetCurve("",typeof(Transform),"localPosition.x",cx);
        ac.SetCurve("",typeof(Transform),"localPosition.y",cy);
        ac.SetCurve("",typeof(Transform),"localPosition.z",cz);
        ac.SetCurve("",typeof(Transform),"localEulerAngles.y",cr);
        a.AddClip(ac, "stage3");
    }
}
