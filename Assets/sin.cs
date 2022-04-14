using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sin : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer lineRenderer;
    void Start()
    {
        var positions = new Vector3[360];
        for (int i = 0; i < 360; i++)
        {

            float y = Mathf.Sin(Mathf.PI * i / 180) * 1;
            positions[i] = new Vector3((i - 180) / 500f, 2, y / 4f);

        }
        lineRenderer.SetPositions(positions);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
