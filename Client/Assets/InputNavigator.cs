﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputNavigator : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    EventSystem _system;
    private bool _isSelect = false;

    void Start()
    {
        _system = EventSystem.current;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && _isSelect)
        {

            Selectable next = null;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                next = _system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
                //Bug：当按shift+tab的时候，会多次调用次方法
            }
            else
            {
                next = _system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            }
            if (next != null)
            {
                InputField inputfield = next.GetComponent<InputField>();
                if(inputfield != null)
                {
                    _system.SetSelectedGameObject(next.gameObject, new BaseEventData(_system));
                }
            }
            //else
            //{
            //    Debug.LogError("找不到下一个控件");
            //}
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        _isSelect = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _isSelect = false;
    }
}
