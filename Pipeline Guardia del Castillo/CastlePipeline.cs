using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
using Random = UnityEngine.Random;

public class CastlePipeline : MonoBehaviour
{
    private GameObject castelo;
    private GameObject knig;
    private Vector3 end;
    private Material fmat;
    private Material mIron;
    private Color glowC;
    private ComputeShader fshader;
    private bool standby;
    private animaçaoK walking;
    void Start()
    {
        //materiales
        fmat = Resources.Load<Material>("Doge texturas/Materials/dream doge");
        fshader = Resources.Load<ComputeShader>("Shaders/lmao");
        mIron = Resources.Load<Material>("Doge texturas/Materials/magicIron");
        glowC = new Color(0.237f,0.041f,0.043f);
        //castillo
        castelo = giantEnemyCrab.castelo(0, 0, 0);
        //caballero
        knig = knight(5, 3.43f, -10,90);
        walking = knig.GetComponent<animaçaoK>();
        //ruta del caballero
        end = new Vector3(-5, 3.43f, -10);
        standby = false;
        walking.sWalking();
    }

    
    void Update()
    {
        if (Vector3.Distance(knig.transform.position, end) != 0 && !standby)
        {
            knig.transform.position = Vector3.MoveTowards(knig.transform.position, end, Time.deltaTime*0.5f);
        }
        else if(!standby)
        {
            end = new Vector3(-end.x, end.y, end.z);
            standby = true;
            Invoke("sMode",3);
        }
        else
        {
            var r = Quaternion.LookRotation(knig.transform.position-end);
            knig.transform.rotation = Quaternion.Slerp(knig.transform.rotation,r,Time.deltaTime);
        }
        mIron.SetColor("_EmissionColor", glowC*(float)Math.Sin(Time.time)/2);
    }

    public void sMode()
    {
        standby = !standby;
        
    }
    
    public GameObject knight(float x, float y, float z, float r)
    {
        List<GameObject> steelRender = new List<GameObject>();
        GameObject knig = GameObject.CreatePrimitive(PrimitiveType.Cube);
        knig.transform.position = new Vector3(x, y, z);
        knig.transform.localScale = new Vector3(0.2f, 0.3f, 0.1f);
        knig.transform.Rotate(new Vector3(0, r, 0));
        knig.name = "knig";
        knig.AddComponent(typeof(Animation));
        knig.AddComponent(typeof(animaçaoK));
        //Casco
        GameObject careBalde = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        GameObject c1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject c2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        careBalde.transform.parent = knig.transform;
        c1.transform.parent = careBalde.transform;
        c2.transform.parent = careBalde.transform;
        careBalde.transform.localPosition = new Vector3(0, 0.7f, 0);
        c1.transform.localPosition = new Vector3(0, 0.25f, -0.45f);
        c2.transform.localPosition = new Vector3(0, 0.08f, -0.45f);
        careBalde.transform.localScale = new Vector3(0.5f, 0.2f, 1);
        c1.transform.localScale = new Vector3(0.5f, 0.1f, 0.1f);
        c2.transform.localScale = new Vector3(0.1f, 0.8f, 0.1f);
        careBalde.transform.localEulerAngles = new Vector3(0, 0, 0);
        c1.transform.localEulerAngles = new Vector3(0, 0, 0);
        c2.transform.localEulerAngles = new Vector3(0, 0, 0);
        c1.GetComponent<Renderer>().material.color = Color.black;
        c2.GetComponent<Renderer>().material.color = Color.black;
        steelRender.Add(careBalde);
        steelRender.Add(knig);
        //Hombreras
        GameObject sP = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        sP.transform.parent = knig.transform;
        sP.transform.localPosition = new Vector3(0, 0.43f, 0);
        sP.transform.localScale = new Vector3(1, 0.9f, 0.3333333333333f);
        sP.transform.localEulerAngles = new Vector3(90, 90, 0);
        //Brazos y Piernas
        yuca(-0.67f, knig, steelRender);
        yuca(0.67f, knig, steelRender);
        legpiece(-0.3f, knig, steelRender);
        legpiece(0.3f, knig, steelRender);
        //Arma
        strBuild(knig);
        //Texturas
        Texture2D texture = (Texture2D) Resources.Load ("Doge texturas/armor wm");
        Texture2D scPlate = (Texture2D) Resources.Load ("Doge texturas/5088");
        sP.GetComponent<Renderer>().material.mainTexture = scPlate;
        foreach (var plate in steelRender)
        {
            plate.GetComponent<Renderer>().material.mainTexture = texture;
            
        }
        return knig;
    }

    protected GameObject yuca(float x, GameObject knig, List<GameObject> steelRender)
    {
        GameObject b1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        GameObject b2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        b1.transform.parent = knig.transform;
        b2.transform.parent = b1.transform;
        b1.transform.localPosition = new Vector3(x, 0.184f, 0);
        b2.transform.localPosition = new Vector3(0, -1.2833f, -1.0666f);
        b1.transform.localScale = new Vector3(0.3f, 0.3f, 0.6f);
        b2.transform.localScale = new Vector3(1, 1.3149f, 0.7f);
        b1.transform.localEulerAngles = new Vector3(0, 0, 0);
        b2.transform.localEulerAngles = new Vector3(78.65f, 0, 0);
        steelRender.Add(b1);
        steelRender.Add(b2);
        return b1;
    }

    protected GameObject legpiece(float x, GameObject knig, List<GameObject> steelRender)
    {
        GameObject p = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        p.name = "leg";
        p.transform.parent = knig.transform;
        p.transform.localPosition = new Vector3(x, -0.937f, 0);
        p.transform.localScale = new Vector3(0.35f, 0.5f, 0.65f);
        p.transform.localEulerAngles = new Vector3(0, 0, 0);
        steelRender.Add(p);
        return p;
    }

    protected GameObject strBuild(GameObject knig)
    {
        List<GameObject> iRender = new List<GameObject>();
        int cFlip = Random.Range(0, 2);
        GameObject r;
        GameObject fHost = new GameObject();
        Renderer lmao = fHost.AddComponent(typeof(MeshRenderer)) as Renderer;
        lmao.material = fmat;
        if (cFlip==0)
        {
            //halberd + escudo de torre
            GameObject towerShield = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject polearm = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            GameObject g1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject g2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            towerShield.transform.parent = knig.transform;
            polearm.transform.parent = knig.transform;
            g1.transform.parent = polearm.transform;
            g2.transform.parent = polearm.transform;
            towerShield.transform.localPosition = new Vector3(0.7f, -0.3666f, -1.691f);
            polearm.transform.localPosition = new Vector3(-0.65f, -0.09f, -1.5f);
            g1.transform.localPosition = new Vector3(0, 0.85f, -1.5f);
            g2.transform.localPosition = new Vector3(0, 0.875f, 0.8f);
            towerShield.transform.localScale = new Vector3(1.25f, 1.66666f, 0.2f);
            polearm.transform.localScale = new Vector3(0.1f, 1.3333f, 0.2f);
            g1.transform.localScale = new Vector3(0.25f, 0.25f, 2.5f);
            g2.transform.localScale = new Vector3(0.25f, 0.125f, 1);
            towerShield.transform.localEulerAngles = new Vector3(0, 0, 0);
            polearm.transform.localEulerAngles = new Vector3(0, 0, 0);
            g1.transform.localEulerAngles = new Vector3(0,0,0);
            g2.transform.localEulerAngles = new Vector3(0, 0, 0);
            fHost.transform.parent = g1.transform;
            fHost.transform.localPosition = new Vector3(0, 0, 0);
            iRender.Add(towerShield);
            iRender.Add(polearm);
            g1.GetComponent<Renderer>().material = mIron;
            iRender.Add(g2);
            r = polearm;
        }
        else
        {
            //claymore
            GameObject h = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            GameObject guard = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject bate = GameObject.CreatePrimitive(PrimitiveType.Cube);
            h.transform.parent = knig.transform;
            guard.transform.parent = h.transform;
            bate.transform.parent = h.transform;
            h.transform.localPosition = new Vector3(-0.655f, -0.1733f, -1.539f);
            guard.transform.localPosition = new Vector3(0,1.416f,0);
            bate.transform.localPosition = new Vector3(0, 7.2f, 0);
            h.transform.localScale = new Vector3(0.297f, 0.17f, 0.1215f);
            guard.transform.localScale = new Vector3(4.5f, 0.8333f, 1);
            bate.transform.localScale = new Vector3(3, 11.6666f, 0.4f);
            h.transform.localEulerAngles = new Vector3(0, 90, 17.357f);
            guard.transform.localEulerAngles = new Vector3(0,0,0);
            bate.transform.localEulerAngles = new Vector3(0, 0, 0);
            fHost.transform.parent = bate.transform;
            fHost.transform.localPosition = new Vector3(0, 0.5f, 0);
            bate.GetComponent<Renderer>().material = mIron;
            iRender.Add(guard);
            iRender.Add(h);
            r = h;
        }
        Texture2D texture = (Texture2D) Resources.Load ("Doge texturas/diron");
        foreach (var cast in iRender)
        {
            cast.GetComponent<Renderer>().material.mainTexture = texture;
        }
        Shaderhost temp = fHost.AddComponent(typeof(Shaderhost)) as Shaderhost;
        temp.material = fmat;
        temp.computeShader = fshader;
        return r;
    }
}
