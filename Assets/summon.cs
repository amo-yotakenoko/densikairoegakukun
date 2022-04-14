using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class summon : MonoBehaviour
{
    // Start is called before the first frame update
    public Dropdown dropdown;
    public grip grip;
    void Start()
    {
        dropdown.ClearOptions();

        List<string> optionlist = new List<string>();

        optionlist.Add("");
        // optionlist.Add("廃線:line");
        optionlist.Add("パーツ:part");
        optionlist.Add("交流電源:E");
        optionlist.Add("抵抗:R");
        optionlist.Add("オシロ:oscillo");
        optionlist.Add("コンデンサ:C");
        optionlist.Add("コイル:L");
        optionlist.Add("電圧系:V");



        //リストを追加
        dropdown.AddOptions(optionlist);
        linebasesummon();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject instance;

    public void selected()
    {
        string selecteditem = dropdown.options[dropdown.value].text.Split(':')[1];
        // dropdown.value = 0;
        GameObject summoneditem = (GameObject)Resources.Load(selecteditem);
        instance = (GameObject)Instantiate(summoneditem);
        instance.name = selecteditem;
        instance.SetActive(false);
        // print(dropdown.options[dropdown.value].text.Split(':')[1]);
        Invoke("griping", 0.1f);

    }
    void griping()
    {
        grip.grabing = instance.transform;
        instance.SetActive(true);
        dropdown.value = 0;
    }
    public GameObject linebase;
    void linebasesummon()
    {
        for (int x = -10; x < 10; x++)
        {
            for (int z = -10; z < 10; z++)
            {
                // GameObject summoneditem = (GameObject)Resources.Load("linebase");
                GameObject instance = (GameObject)Instantiate(linebase);
                instance.transform.position = new Vector3(x, 0, z);
            }

        }

    }
}
