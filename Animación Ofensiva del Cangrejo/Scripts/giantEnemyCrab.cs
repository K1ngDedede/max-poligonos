using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class giantEnemyCrab : MonoBehaviour
{
    protected static GameObject crab;

    protected static GameObject t1;

    protected static GameObject castel;

    protected static GameObject knig1;

    protected static GameObject arepa;
    
    // Start is called before the first frame update
    void Start()
    {
        //skybox
        Material skydoge = Resources.Load("skydoge", typeof(Material)) as Material;
        RenderSettings.skybox = skydoge;
        //terreno y luz
        terrainGen();
        //gran cangrejo enemigo
        crab = letThereBeCrab(32, -2, 26, 1.5f, -80);
        //castillo
        castel = castelo(-8,2,-120);
        //torre que se derrumba
        t1 = theTower(29, 14.15f);
        //caballeros
        /*knight(5,0.43f,8.8f,-115);
        knight(6,0.43f,12.7f,-48);
        knight(2.65f,0.43f,7.6f,-147);
        knight(2.7f,0.43f,10.8f,-90);
        knight(5.9f,0.43f,5.1f,-170);
        knight(0.5f,0.43f,11.9f,-90);
        knight(3.74f,0.43f,14,-35);
        knight(-0.7f,3.43f,9.4f,-116);
        knight(0.46f,3.43f,7.14f,-116);
        knight(0.82f,3.43f,6.4f,-116);*/
        knig1 = knight(3.85f,4.4f,1.5f,-140);
        //Animación
        sceneplay();
    }

    public static GameObject letThereBeCrab(float x, float y, float z, float m, float r)
    {
        List<GameObject> crabRender = new List<GameObject>();
        //Cuerpo
        GameObject crabsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        crabsule.name = "cangreijo";
        crabsule.AddComponent(typeof(Animation));
        crabsule.AddComponent(typeof(animaçaoC));
        crabsule.transform.position = new Vector3(x, y, z);
        crabsule.transform.localScale = new Vector3(0.8f*m, 1*m, 1.3f*m);
        crabsule.transform.Rotate(0, r, 90);
        crabRender.Add(crabsule);
        //Ojos
        crabRender.Add(oculi(0.4f, -0.5f, 0.331f, crabsule));
        crabRender.Add(oculi(0.4f, 0.344f, 0.331f, crabsule));
        //Patas
        patas(-0.151f, -1, -0.377f, 5.5f, -198.9f, crabsule, crabRender);
        patas(-0.181f, -1, -0.19f, 4.58f, -180.81f, crabsule, crabRender);
        patas(-0.205f, -1, -0.0618f, 4.58f, -177.5f, crabsule, crabRender);
        patas(-0.258f, -1, 0.351f, 5.58f, -149.6f, crabsule, crabRender);
        patas(-0.082f, 1, -0.36f, 1.72f, 36.23f, crabsule, crabRender);
        patas(-0.143f, 1, -0.079f, 3.05f, 7.968f, crabsule, crabRender);
        patas(-0.2f, 1, 0.168f, 3.101f, 4.59f, crabsule, crabRender);
        patas(-0.227f, 1, 0.362f, 1.96f, -13.17f, crabsule, crabRender);
        //Pinzas (van a otro método porque acá es mal agüero)
        timeForCrab(crabsule, crabRender);
        //Texturas
        Texture2D texture = (Texture2D) Resources.Load ("Doge texturas/magma");
        foreach (var palmito in crabRender)
        {
            palmito.GetComponent<Renderer>().material.mainTexture = texture;
        }

        return crabsule;
    }

    protected static GameObject oculi(float x, float y, float z, GameObject f)
    {
        GameObject oc1 = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        GameObject oc2 = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        oc1.transform.parent = f.transform;
        oc2.transform.parent = oc1.transform;
        oc1.transform.localPosition = new Vector3(x, y, z);
        oc2.transform.localPosition = new Vector3(0, 0.29f, 0.49f);
        oc1.transform.localScale = new Vector3(0.1f, 0.125f, 0.076f);
        oc2.transform.localScale = new Vector3(0.6f, 0.6f, 0.5f);
        oc1.transform.localEulerAngles = new Vector3(0, 0, -90);
        oc2.GetComponent<Renderer>().material.color = Color.black;
        return oc1;
    }

    protected static GameObject patas(float x, float y, float z, float r1, float r2, GameObject f, List<GameObject> crabRender)
    {
        GameObject p1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        GameObject p2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        p1.name = "pata";
        p1.transform.parent = f.transform;
        p2.transform.parent = p1.transform;
        p1.transform.localPosition = new Vector3(x, y, z);
        p2.transform.localPosition = new Vector3(-0.09f, 1.17f, 2.52f);
        p1.transform.localScale = new Vector3(0.0789f, 0.292f, 0.123f);
        p2.transform.localScale = new Vector3(1, 2.858f, 0.338f);
        p1.transform.localEulerAngles = new Vector3(r1, -96, r2);
        p2.transform.localEulerAngles = new Vector3(82.76f, -360, -360);
        crabRender.Add(p1);
        crabRender.Add(p2);
        p1.AddComponent(typeof(animaçaoCL));
        return p1;
    }

    protected static GameObject timeForCrab(GameObject crab, List<GameObject> crabRender)
    {
        GameObject r1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        GameObject r2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        GameObject l1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        GameObject l2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        r1.transform.parent = crab.transform;
        r2.transform.parent = crab.transform;
        l1.transform.parent = crab.transform;
        l2.transform.parent = l1.transform;
        l1.name = "cArm";
        l1.AddComponent(typeof(Animation));
        r1.transform.localPosition = new Vector3(0.2735f, -1.0518f, 0.4122f);
        r2.transform.localPosition = new Vector3(0.265f, -0.865f, 0.803f);
        l1.transform.localPosition = new Vector3(0.2635f, 1, 0.473f);
        l2.transform.localPosition = new Vector3(0.76f, 0.74f, 0.66f);
        r1.transform.localScale = new Vector3(0.1763f, 0.4391f, 0.2416f);
        r2.transform.localScale = new Vector3(0.079f, 0.4962f, 0.1208f);
        l1.transform.localScale = new Vector3(0.1861f, 0.420f, 0.2390f);
        l2.transform.localScale = new Vector3(0.544f, 1.443f, 0.661f);
        r1.transform.localEulerAngles = new Vector3(13.581f, -79.163f, -130.327f);
        r2.transform.localEulerAngles = new Vector3(13.581f, -79.163f, -190.52f);
        l1.transform.localEulerAngles = new Vector3(10.99f, -75.68f, -52.35f);
        l2.transform.localEulerAngles = new Vector3(28.63f, -30.54f, 90.84f);
        crabHammer(0.12f, -1.1f, 0.4f, -4.52f, 102.48f, -78.87f, 0.088f, 1.779f, 0.783f, r2, crabRender);
        crabHammer(-0.2245f, -1.03f, -0.219f, 18.609f, 79.595f, -108.468f, 0.5158f, 3.3817f, 0.7587f, l2, crabRender).name = "crabhammer";
        crabRender.Add(r1);
        crabRender.Add(r2);
        crabRender.Add(l1);
        crabRender.Add(l2);
        return l1;
    }

    protected static GameObject crabHammer(float x, float y, float z, float r1, float r2, float r3, float s1, float s2,
        float s3, GameObject arm, List<GameObject> crabRender)
    {
        GameObject basal = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject c1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject c2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject c3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject c4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject c5 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        basal.transform.parent = arm.transform;
        c1.transform.parent = basal.transform;
        c2.transform.parent = basal.transform;
        c3.transform.parent = basal.transform;
        c4.transform.parent = basal.transform;
        c5.transform.parent = basal.transform;
        basal.transform.localPosition = new Vector3(x, y, z);
        c1.transform.localPosition = new Vector3(2.0799f, -0.4f, 0);
        c2.transform.localPosition = new Vector3(2.7298f, 0.374f, 0);
        c3.transform.localPosition = new Vector3(3.6998f, 0.31f, 0);
        c4.transform.localPosition = new Vector3(4.3998f, 0.176f, 0);
        c5.transform.localPosition = new Vector3(4.4798f, -0.282f, 0);
        basal.transform.localScale = new Vector3(s1, s2, s3);
        c1.transform.localScale = new Vector3(0.2f, 5, 1);
        c2.transform.localScale = new Vector3(0.6f, 5, 1);
        c3.transform.localScale = new Vector3(0.4f, 5, 1);
        c4.transform.localScale = new Vector3(0.2f, 5, 1);
        c5.transform.localScale = new Vector3(0.2f, 2, 1);
        basal.transform.localEulerAngles = new Vector3(r1, r2, r3);
        c1.transform.localEulerAngles = new Vector3(0, 0, -90);
        c2.transform.localEulerAngles = new Vector3(0, 0, -90);
        c3.transform.localEulerAngles = new Vector3(0, 0, -90);
        c4.transform.localEulerAngles = new Vector3(0, 0, -90);
        c5.transform.localEulerAngles = new Vector3(0, 0, -90);
        crabRender.Add(basal);
        crabRender.Add(c1);
        crabRender.Add(c2);
        crabRender.Add(c3);
        crabRender.Add(c4);
        crabRender.Add(c5);
        return basal;
    }

    public static GameObject knight(float x, float y, float z, float r)
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

    protected static GameObject yuca(float x, GameObject knig, List<GameObject> steelRender)
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

    protected static GameObject legpiece(float x, GameObject knig, List<GameObject> steelRender)
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

    protected static GameObject strBuild(GameObject knig)
    {
        List<GameObject> iRender = new List<GameObject>();
        int cFlip = Random.Range(0, 2);
        GameObject r;
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
            iRender.Add(towerShield);
            iRender.Add(polearm);
            iRender.Add(g1);
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
            iRender.Add(bate);
            iRender.Add(guard);
            iRender.Add(h);
            r = h;
        }
        Texture2D texture = (Texture2D) Resources.Load ("Doge texturas/diron");
        foreach (var cast in iRender)
        {
            cast.GetComponent<Renderer>().material.mainTexture = texture;
        }

        return r;
    }

    public static GameObject castelo(float x, float z, float r)
    {
        List<GameObject> stoneRender = new List<GameObject>();
        GameObject fortress = GameObject.CreatePrimitive(PrimitiveType.Cube);
        fortress.transform.position = new Vector3(x,3, z);
        fortress.transform.localScale = new Vector3(12, 6, 5.5f);
        fortress.transform.Rotate(0, r, 0);
        stoneRender.Add(fortress);
        fortress.name = "castelo";
        //Techo y torre
        GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject spire = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        tile.transform.parent = fortress.transform;
        spire.transform.parent = fortress.transform;
        tile.transform.localPosition = new Vector3(0, 0.5f, 0);
        spire.transform.localPosition = new Vector3(0, 0.333f, -0.4345f);
        tile.transform.localScale = new Vector3(0.6f, 0.6618f, 0.95f);
        spire.transform.localScale = new Vector3(0.329f, 0.9166f, 0.7272f);
        tile.transform.localEulerAngles = new Vector3(0,0,45);
        spire.transform.localEulerAngles = new Vector3(0,0,0);
        BattlementsT(spire, 16);
        stoneRender.Add(spire);
        //Muros y torres
        castleWalls(fortress, stoneRender);
        //Texturas
        Texture2D texture = (Texture2D) Resources.Load ("Doge texturas/tijolo");
        Texture2D tTile =(Texture2D) Resources.Load ("Doge texturas/btile");
        tile.GetComponent<Renderer>().material.mainTexture = tTile;
        foreach (var bas in stoneRender)
        {
            bas.GetComponent<Renderer>().material.mainTexture = texture;
        }

        return fortress;
    }

    protected static void castleWalls(GameObject castelo, List<GameObject> stoneRender)
    {
        List<GameObject> bast = new List<GameObject>();
        for (int sapo = 0; sapo < 4; sapo++)
        {
            GameObject w = GameObject.CreatePrimitive(PrimitiveType.Cube);
            w.transform.parent = castelo.transform;
            w.transform.localScale = sapo % 2 == 0 ? new Vector3(1, 0.5f, 0.18f) : new Vector3(2.2f, 0.5f, 0.082f);
            w.transform.localEulerAngles = sapo % 2 == 0 ? new Vector3(0, 0, 0) : new Vector3(0, 90, 0);
            BattlementsW(w);
            stoneRender.Add(w);
            bast.Add(w);
        }
        bast[0].transform.localPosition = new Vector3(0, -0.25f, -1.8f);
        bast[1].transform.localPosition = new Vector3(0.55f, -0.25f, -0.6f);
        bast[2].transform.localPosition = new Vector3(0, -0.25f, 0.6f);
        bast[3].transform.localPosition = new Vector3(-0.55f, -0.25f, -0.6f);
        //Puertas
        GameObject p1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject p2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        p1.name = "eingang";
        p2.name = "eingang";
        p1.transform.parent = bast[0].transform;
        p2.transform.parent = bast[0].transform;
        p1.transform.localPosition = new Vector3(-0.04f, -0.16666f, -0.46f);
        p2.transform.localPosition = new Vector3(0.04f, -0.16666f, -0.46f);
        p1.transform.localScale = new Vector3(0.082f, 0.666f, 0.4f);
        p2.transform.localScale = new Vector3(0.082f, 0.666f, 0.4f);
        p1.transform.localEulerAngles = new Vector3(0, 10, 0);
        p2.transform.localEulerAngles = new Vector3(0, -10, 0);
        //Torres
        theTower(0.55f, -1.8f, castelo);
        theTower(-0.55f, -1.8f, castelo);
        theTower(0.55f, 0.6f, castelo);
        theTower(-0.55f, 0.6f, castelo);
        //Texturas
        Texture2D wTile =(Texture2D) Resources.Load ("Doge texturas/wood");
        p1.GetComponent<Renderer>().material.mainTexture = wTile;
        p2.GetComponent<Renderer>().material.mainTexture = wTile;
    }

    public static GameObject theTower(float x, float z, GameObject castelo=null)
    {
        GameObject spire = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        spire.AddComponent(typeof(Animation));
        spire.AddComponent(typeof(animaçaoT));
        spire.name = "spire";
        if (castelo != null)
        {
            spire.transform.parent = castelo.transform;
            spire.transform.localScale = new Vector3(0.1648f, 0.333f, 0.363f);
            spire.transform.localPosition = new Vector3(x, -0.16666f, z);
        }
        else
        {
            spire.transform.localScale = new Vector3(2,2,2);
            spire.transform.localPosition = new Vector3(x, 2, z);
        }
        spire.transform.localEulerAngles = new Vector3(0, 0, 0);
        //Battlements
        BattlementsT(spire, 16);
        //Textura
        Texture2D texture = (Texture2D) Resources.Load ("Doge texturas/tijolo");
        spire.GetComponent<Renderer>().material.mainTexture = texture;
        return spire;
    }

    protected static void BattlementsT(GameObject spire, int iterator)
    {
        Texture2D texture = (Texture2D) Resources.Load ("Doge texturas/tijolo");
        for (int sapo = 1; sapo <= iterator; sapo++)
        {
            float alpha = (float) (sapo * 2 * Math.PI / iterator);
            float xalpha = (float) Math.Sin(alpha)*0.46f;
            float zalpha = (float) Math.Cos(alpha)*0.46f;
            GameObject divot = GameObject.CreatePrimitive(PrimitiveType.Cube);
            divot.transform.parent = spire.transform;
            divot.transform.localScale = sapo % 2 == 0 ? new Vector3(0.15f, 0.3f, 0.05f) : new Vector3(0.15f, 0.15f, 0.05f);
            divot.transform.localPosition = new Vector3(xalpha,1,zalpha);
            divot.transform.localEulerAngles = new Vector3(0,(sapo*360/iterator),0);
            divot.GetComponent<Renderer>().material.mainTexture = texture;
        }
    }

    protected static void BattlementsW(GameObject cwall)
    {
        Texture2D texture = (Texture2D) Resources.Load ("Doge texturas/tijolo");
        for (float sapo = -0.5f; sapo <= 0.5; sapo += 0.1f)
        {
            for (int sapa = -1; sapa <= 1; sapa += 2)
            {
                GameObject divot = GameObject.CreatePrimitive(PrimitiveType.Cube);
                divot.transform.parent = cwall.transform;
                divot.transform.localScale = new Vector3(0.05f, 0.3f, 0.2f);
                divot.transform.localEulerAngles = new Vector3(0, 0, 0);
                divot.transform.localPosition = new Vector3(sapo,0.5f, sapa*0.45f);
                divot.GetComponent<Renderer>().material.mainTexture = texture;
            }
        }
    }

    public static void terrainGen()
    {
        GameObject marsh = GameObject.CreatePrimitive(PrimitiveType.Plane);
        marsh.transform.position = new Vector3(0, 0, 0);
        Texture2D texture = (Texture2D) Resources.Load ("Doge texturas/pentano");
        marsh.GetComponent<Renderer>().material.mainTexture = texture;
        marsh.GetComponent<Renderer>().material.mainTextureScale = new Vector2(10,10);
        marsh.transform.localScale = new Vector3(20, 1, 20);
        GameObject sun = new GameObject("The Sun");
        Light licht = sun.AddComponent<Light>();
        licht.color = new Color(221,198,158);
        licht.range = 50;
        licht.intensity = 0.01f;
        licht.type = LightType.Point;
        sun.transform.localPosition = new Vector3(21.5f, 12, 12.3f);
        int iterator = 32;
        for (int sapo = 1; sapo <= iterator; sapo++)
        {
            int cFlip = Random.Range(0, 10);
            float r = Random.Range(24f, 64f);
            float alpha = (float) (sapo * 2 * Math.PI / iterator);
            float xalpha = (float) Math.Sin(alpha)*r;
            float zalpha = (float) Math.Cos(alpha)*r;
            if (cFlip>=5 && cFlip <8)
            {
                theTower(xalpha,zalpha);
            }else if (cFlip >= 8)
            {
                float dx = Random.Range(-8f, 8f);
                float dz = Random.Range(-8f, 8f);
                theTower(xalpha, zalpha);
                r = Random.Range(16f, 64f);
                xalpha = (float) Math.Sin(alpha)*r;
                zalpha = (float) Math.Cos(alpha)*r;
                theTower(xalpha+dx,zalpha+dz);
            }
        }
    }

    public static List<GameObject> gibLegs()
    {
        List<GameObject> patas = new List<GameObject>();
        foreach (Transform hijo in crab.transform)
        {
            if (hijo.name.Equals("pata"))
            {
                patas.Add(hijo.gameObject);
            }
        }
        return patas;
    }
    
    public static List<GameObject> gibLegsK(GameObject k)
    {
        List<GameObject> patas = new List<GameObject>();
        foreach (Transform hijo in k.transform)
        {
            if (hijo.name.Equals("leg"))
            {
                patas.Add(hijo.gameObject);
            }
        }
        return patas;
    }

    public static GameObject gibArm()
    {
        foreach (Transform hijo in crab.transform)
        {
            if (hijo.name.Equals("cArm"))
            {
                return hijo.gameObject;
            }
        }

        return null;
    }

    protected void sceneplay()
    {
        Animation crabAnim = crab.GetComponent<Animation>();
        Animation towerAnim = t1.GetComponent<Animation>();
        Animation knigAnim = knig1.GetComponent<Animation>();
        crab.GetComponent<animaçaoC>().unearth(crabAnim);
        t1.GetComponent<animaçaoT>().crumble(towerAnim);
        knig1.GetComponent<animaçaoK>().captain(knigAnim);
        for (int sapo = 0; sapo < 7; sapo++)
        {
            GameObject crusader = knight(1,0.43f,7f,-120);
            Animation crusAnim = crusader.GetComponent<Animation>();
            float r = Random.Range(0f, 3f);
            float x = Random.Range(4f,8f);
            float z = Random.Range(6f,10f);
            crusader.GetComponent<animaçaoK>().order(crusAnim, x, z, r);
        }
        arepa = knight(1, 0.43f, 7, -115);
        Animation areAnim = arepa.GetComponent<Animation>();
        arepa.GetComponent<animaçaoK>().orderA(areAnim,10,11,5);
        GameObject cArm = giantEnemyCrab.gibArm();
        //aplastar
        Animation armAnim = cArm.GetComponent<Animation>();
        AnimationClip ac = new AnimationClip();
        ac.legacy = true;
        Keyframe[] ks = new Keyframe[3];
        ks[0] = new Keyframe(43f, 0.3f);
        ks[1] = new Keyframe(43.3f, -0.2f);
        ks[2] = new Keyframe(43.5f, 0.35f);
        AnimationCurve Xanima = new AnimationCurve(ks);
        ac.SetCurve("",typeof(Transform),"localPosition.x",Xanima);
        ac.SetCurve("",typeof(Transform),"localPosition.y",AnimationCurve.Constant(43, 50,cArm.transform.localPosition.y));
        ac.SetCurve("",typeof(Transform),"localPosition.z",AnimationCurve.Constant(43, 50,cArm.transform.localPosition.z));
        armAnim.AddClip(ac, "smash");
        armAnim.Play("smash");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
