using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject A;
    public GameObject corner;
    public GameObject B;
    public LineRenderer lineRenderer;
    public GameObject Adot;
    public GameObject Bdot;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Apos = A.transform.position;
        Vector3 Bpos = B.transform.position;
        Vector3 cornerpos = new Vector3(Apos.x, 0, Bpos.z);
        corner.transform.position = cornerpos;
        var positions = new Vector3[]{
       Apos+ new Vector3(0, 1, 0) ,
       cornerpos+ new Vector3(0,1, 0),
        Bpos+ new Vector3(0, 1, 0),
    };

        lineRenderer.SetPositions(positions);

        int Acount = 0;
        Acount += connecting(Apos, new Vector3(1, 0, 0)) ? 1 : 0;
        Acount += connecting(Apos, new Vector3(-1, 0, 0)) ? 1 : 0;
        Acount += connecting(Apos, new Vector3(0, 0, 1)) ? 1 : 0;
        Acount += connecting(Apos, new Vector3(0, 0, -1)) ? 1 : 0;
        // print(Acount);
        Adot.SetActive(Acount >= 3);
        int Bcount = 0;
        Bcount += connecting(Bpos, new Vector3(1, 0, 0)) ? 1 : 0;
        Bcount += connecting(Bpos, new Vector3(-1, 0, 0)) ? 1 : 0;
        Bcount += connecting(Bpos, new Vector3(0, 0, 1)) ? 1 : 0;
        Bcount += connecting(Bpos, new Vector3(0, 0, -1)) ? 1 : 0;
        // print(Bcount);
        Bdot.SetActive(Bcount >= 3);


    }
    bool connecting(Vector3 AorB, Vector3 direction)
    {
        Ray ray = new Ray(AorB + direction, direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // もしRayを投射して何らかのコライダーに衝突したら
        {
            string name = hit.collider.gameObject.name;
            if (hit.collider.gameObject.name == "A" || hit.collider.gameObject.name == "B" || hit.collider.gameObject.name == "corner")
            {

                Debug.DrawRay(ray.origin, ray.direction * 30, Color.red, 0.1f);
                return true;
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 30, Color.blue, 0.1f);
        return false;
    }
    public void cornerPositionChange()
    {
        Vector3 Apos = A.transform.position;
        Vector3 Bpos = B.transform.position;
        A.transform.position = Bpos;
        B.transform.position = Apos;
    }

}

