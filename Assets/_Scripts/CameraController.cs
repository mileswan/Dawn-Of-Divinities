using UnityEngine;
using System.Collections;
/**
 * @author mwan
 * Version: 1.0
 * Date: 2016/8/8
 * Description: 相机管理类.
 * Copyright (c) 2016 OEPT inc. All rights reserved.
 */
public class CameraController : MonoBehaviour {

    public float MouseRotateSpeed = 0.3f;
    public float MouseTranslateSpeed = 0.1f;
    public float camDepthSmooth = 5f;

    private Vector3 m_v3MousePosition;

    // Use this for initialization
    void Start () {
        m_v3MousePosition = Input.mousePosition;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(1) == false)
        {
            this.transform.Rotate(-(Input.mousePosition.y - m_v3MousePosition.y) * MouseRotateSpeed, 0.0f, 0.0f);
            this.transform.RotateAround(this.transform.position, Vector3.up, (Input.mousePosition.x - m_v3MousePosition.x) * MouseRotateSpeed);
        }

        if (Input.GetMouseButton(2) && Input.GetMouseButtonDown(2) == false)
        {
            this.transform.Translate(-(Input.mousePosition.x - m_v3MousePosition.x) * MouseTranslateSpeed, -(Input.mousePosition.y - m_v3MousePosition.y) * MouseTranslateSpeed, 0.0f);
        }

        if ((Input.mouseScrollDelta.y < 0 && Camera.main.fieldOfView >= 3) || Input.mouseScrollDelta.y > 0 && Camera.main.fieldOfView <= 80)
        {
            Camera.main.fieldOfView += Input.mouseScrollDelta.y * camDepthSmooth * Time.deltaTime;
        }
       // m_v3MousePosition = Input.mousePosition;

        //拾取设备
        
        if (Input.GetMouseButton(0) && Input.GetMouseButtonDown(0) == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//从摄像机发出到点击坐标的射线
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject gameObj = hitInfo.collider.gameObject;
                Debug.Log("click object name is " + gameObj.name);
                 if (gameObj.tag == "Connector1")//当射线碰撞目标为Connector类型的物品 ，执行拾取操作
                 {
                    Debug.Log("pick up!");
                    gameObj.transform.Translate(-(Input.mousePosition.x - m_v3MousePosition.x) * MouseTranslateSpeed, -(Input.mousePosition.y - m_v3MousePosition.y) * MouseTranslateSpeed, 0.0f);
                }
             }
         }

        m_v3MousePosition = Input.mousePosition;
    }
}
