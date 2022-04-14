using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grip : MonoBehaviour
{
    // Start is called before the first frame update
    public Plane ground;
    public Transform grabing;
    public GameObject selectline;
    public GameObject lastselectline;
    public Vector3 lasttappos;
    void Start()
    {
        ground = new Plane(Vector3.up, Vector3.up);
        lasttappos = tappos();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

        foreach (RaycastHit hit in hits)

        {
            if (Input.GetMouseButtonDown(0) && hit.collider.tag == "item")
            {

                grabing = hit.collider.transform;

            }
            if (Input.GetMouseButtonDown(1) && hit.collider.tag == "item")
            {
                hit.collider.transform.Rotate(0, 90, 0);
                print("回転");
                if (hit.collider.transform.parent.name == "line")
                {
                    hit.collider.transform.parent.GetComponent<line>().cornerPositionChange();
                }
            }

            if (Input.GetKey(KeyCode.LeftShift) && hit.collider.tag == "line")
            {

                selectline = hit.collider.gameObject;

                if (lastselectline != null)
                {

                    Vector3 direction = selectline.transform.position - lastselectline.transform.position;
                    if (direction != new Vector3(0, 0, 0))
                    {
                        print(direction + "に進む");
                        if (Input.GetMouseButton(0))
                        {

                            enableline(lastselectline, selectline, direction, true);
                        }
                        if (Input.GetMouseButton(1))
                        {

                            enableline(lastselectline, selectline, direction, false);
                        }
                    }
                }

                lastselectline = selectline;

            }


            if (Input.GetMouseButton(0) && grabing == null && !Input.GetKey(KeyCode.LeftShift))
            {
                this.transform.position += lasttappos - tappos();
            }

        }
        // if (selectline != null)
        // {
        //     // print(selectline.transform.position + "," + tappos());
        //     if (selectline.transform.position != RoundVector(tappos()))
        //     {
        //         print("伸ばす");
        //         GameObject newline = (GameObject)Instantiate(selectline);
        //         newline.transform.position += RoundVector(tappos()) - selectline.transform.position;
        //     }

        // }

        if (Input.GetMouseButtonUp(0))
        {
            grabing = null;
            selectline = null;
        }
        float scroll = Input.mouseScrollDelta.y;
        this.GetComponent<Camera>().orthographicSize += scroll * -0.1f;



        if (grabing != null)
        {

            grabing.position = RoundVector(tappos());


        }
        lasttappos = tappos();

    }
    void enableline(GameObject A, GameObject B, Vector3 direction, bool enable)
    {

        foreach (Transform Aline in A.transform)
        {
            if (Aline.name != "dot")
            {
                if (Aline.transform.localPosition == direction / 2)
                {
                    Aline.GetComponent<LineRenderer>().enabled = enable;
                }
            }
        }
        foreach (Transform Bline in B.transform)
        {
            if (Bline.name != "dot")
            {
                if (Bline.transform.localPosition == direction / 2 * -1)
                {
                    Bline.GetComponent<LineRenderer>().enabled = enable;
                }
            }
        }
        dotenable(A);
        dotenable(B);
    }
    void dotenable(GameObject line)
    {
        int count = 0;
        foreach (Transform l in line.transform)
        {
            if (l.name != "dot")
            {
                count += l.GetComponent<LineRenderer>().enabled ? 1 : 0;
            }
            else
            {
                l.GetComponent<MeshRenderer>().enabled = count > 2 ? true : false;
            }
        }
    }
    Vector3 tappos()
    {
        Ray groray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        float rayDistance = 10;
        ground.Raycast(groray, out rayDistance);

        return groray.GetPoint(rayDistance);
    }
    public Vector3 RoundVector(Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x), 0, Mathf.Round(v.z));
    }
}