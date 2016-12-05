using UnityEngine;
using System.Collections;
/**
 * @author mwan
 * Version: 1.0
 * Date: 2016/8/18
 * Description: 菜单管理类.
 * Copyright (c) 2016 OEPT inc. All rights reserved.
 */
public class MenuItemInstance : MonoBehaviour {

    public GameObject[] menuItemPre = new GameObject[2];
    public GameObject RJ45Pre;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //点击确认后在主场景中实例化所选设备
    public void InstanceItem()
    {
        for(int i = 0; i < menuItemPre.Length; i++)
        {
            if (menuItemPre[i].name == MenuItemsManager.selectedName)
            {
                if (MenuItemsManager.selectedName == "RJ45Cable")
                {
                    GameObject RJ45Start = (GameObject)Instantiate(RJ45Pre, new Vector3(2.0f, 3.0f, 2.3f), Quaternion.identity);
                    GameObject RJ45End = (GameObject)Instantiate(RJ45Pre, new Vector3(0.8f, 3.1f, -0.9f), Quaternion.identity);
                    menuItemPre[i].GetComponent<UltimateRope>().RopeStart = RJ45Start;
                    menuItemPre[i].GetComponent<UltimateRope>().RopeNodes[0].goNode = RJ45End;
                    Instantiate(menuItemPre[i], new Vector3(1.0f, 5.0f, 0.0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(menuItemPre[i], new Vector3(1.0f, 5.0f, 0.0f), Quaternion.identity);
                }
                
            }
        }
        
    }
}
