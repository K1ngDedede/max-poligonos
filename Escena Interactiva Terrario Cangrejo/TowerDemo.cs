using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TowerDemo : MonoBehaviour
{
    protected Camera mainc;
    protected RaycastHit RayHit;
    protected Ray r;
    protected Vector3 Hitpoint = Vector3.zero;
    protected Vector3 crabloc = Vector3.zero;
    protected Vector3 crabrot = Vector3.zero;
    protected GameObject crav;
    protected GameObject cravHost;
    protected Heap<GameObject> spires;
    protected Material magma;
    protected Color glowC;
    protected Light licht;
    protected bool standby;
    protected bool sleepFlag;

    void Start()
    {
        mainc = Camera.main;
        spires = new Heap<GameObject>(8);
        //Host del cangrejo
        cravHost = new GameObject();
        cravHost.transform.position = new Vector3(20, 2, 20);
        cravHost.transform.eulerAngles = Vector3.zero;
        cravHost.name = "CHost";
        crav = giantEnemyCrab.letThereBeCrab(20,1,20,1.5f, 90);
        crav.transform.parent = cravHost.transform;
        spires.updateCrab(cravHost.transform);
        //Plano invisible sobre el cangrejo para detectar colisiones
        GameObject crabUmbrella = GameObject.CreatePrimitive(PrimitiveType.Quad);
        crabUmbrella.name = "chitbox";
        crabUmbrella.transform.SetParent(crav.transform);
        crabUmbrella.transform.localPosition = new Vector3(1,0,0);
        crabUmbrella.transform.localEulerAngles = new Vector3(0,-90,0);
        crabUmbrella.transform.localScale = new Vector3(4, 5, 1);
        Material crabHitbox = crabUmbrella.GetComponent<Renderer>().material;
        crabHitbox.SetColor("_Color",Color.clear);
        crabHitbox.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        crabHitbox.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        crabHitbox.SetInt("_ZWrite", 0);
        crabHitbox.DisableKeyword("_ALPHATEST_ON");
        crabHitbox.EnableKeyword("_ALPHABLEND_ON");
        crabHitbox.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        crabHitbox.renderQueue = 3000;
        //Iluminación del cangrejo
        GameObject cLight = new GameObject("crablight");
        cLight.transform.position = cravHost.transform.position;
        cLight.transform.parent = crav.transform;
        licht = cLight.AddComponent<Light>();
        licht.color = new Color(0.7f,0.35f,0.2f);
        licht.type = LightType.Point;
        standby = true;
        sleepFlag = false;
        magma = (Material) Resources.Load("Doge texturas/Materials/crabMat");
        glowC = new Color(0.783f,0.388f,0.055f,0.8f);
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            r = mainc.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(r, out RayHit))
            {
                GameObject oh = RayHit.transform.gameObject;
                if (oh.name != "chitbox" && oh.name != "CHost")
                {
                    Hitpoint = RayHit.point;
                    spires.Insert(giantEnemyCrab.theTower(Hitpoint.x, Hitpoint.z));
                    if (standby && !sleepFlag)
                    {
                        sMode();
                        sleepFlag = true;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //DEBUG
        }

        if (!spires.IsEmpty && !standby)
        {
            crabloc = Vector3.MoveTowards(cravHost.transform.position, spires.Peek().transform.position, 5f*Time.deltaTime);
            cravHost.transform.position = crabloc;
            crabrot = spires.Peek().transform.position - cravHost.transform.position;
            crabrot.y = 0;
            Quaternion rot = Quaternion.LookRotation(crabrot);
            cravHost.transform.rotation = Quaternion.Slerp(cravHost.transform.rotation, rot, 5f*Time.deltaTime);
            if (Vector3.Distance(crabloc, spires.Peek().transform.position)<3f)
            {
                GameObject crumble = spires.Remove();
                animaçaoT k = crumble.GetComponent<animaçaoT>();
                Animation towAnim = crumble.GetComponent<Animation>();
                k.instCrumble(towAnim);
                sMode();
                Invoke("sMode",2);
            }
        }
        magma.SetColor("_EmissionColor", glowC*(float)Math.Sin(Time.time)/3);
        licht.intensity = (float) Math.Sin(Time.time);
    }

    private void sMode()
    {
        standby = !standby;
        walking();
    }
    

    public void walking()
    {
        int sp = standby ? 0 : 1;
        List<GameObject> patas = gibLegs();
        foreach (GameObject p in patas)
        {
            animaçaoCL aCL = p.GetComponent<animaçaoCL>();
            aCL.walkCycle(sp);
        }
    }
    
    public List<GameObject> gibLegs()
    {
        List<GameObject> patas = new List<GameObject>();
        foreach (Transform hijo in crav.transform)
        {
            if (hijo.name.Equals("pata"))
            {
                patas.Add(hijo.gameObject);
            }
        }
        return patas;
    }
}
