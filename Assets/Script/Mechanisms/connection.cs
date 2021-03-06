﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class connection : MonoBehaviour
{
    public enum checkState //Список элементов
    {
        nothing = 0,
        leverArm = 1,
        mediator = 2,
        listener = 3,
        button = 4
    }
    public checkState status;
    public bool isActive = false; //активация элемента в сети
    public bool animateEnabled = false;

    public GameObject connector;  //объект для работы света
    [HideInInspector]
    public int list; // скрытая переменная для проверки размерности переменной
    private int buffer; //буфер
    private bool youIn = false; //перменная, которая активна, когда персонаж внутри коллайдера

    public List<mediator> med = new List<mediator>(); //вывод класса mediator на инспектор

    void OnTriggerEnter2D(Collider2D collision) //Проверки на то, внутри коллайдера ли персонаж
    {
        youIn = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        youIn = false;
    }



    void Start()
    {
        list = med.Count; //проверка размерности массива
    }

    void Update()
    {
        switch (status) //статус выпадающего списка
        {
            case checkState.nothing:
                isActive = true;
            break;//этот статус отключает механизм
            case checkState.leverArm: //этот статус выполняет функцию рычага, возвращает значение активности isActive
                if (Input.GetKeyDown(KeyCode.E) && youIn) 
                    isActive = !isActive;
                break;
            case checkState.mediator: //этот статус принимает значения активности от рычагов или других посредников в зависимости от размера массива
                for (int i = 0; i < list; i++) //проверка активности всех ячеек массива через буфер
                {
                    if (med[i].chain_element.GetComponent<connection>().isActive) med[i].connect = true;
                    else med[i].connect = false;
                    if (med[i].connect) buffer += 1;
                }
                if (buffer == list) //если в конце цикла буфер равен значению массива, то isActive принимает значение true, а буфер обнуляется, иначе наоборот (так же с обнулением буфера)
                {
                    isActive = true;
                    buffer = 0;
                }
                else
                {
                    isActive = false;
                    buffer = 0;
                }
                break;
            case checkState.listener: //статус света. Включает свет, если поступившее значение isActive из connector истинно
                if (connector.GetComponent<connection>().isActive)
                    isActive = true;
                else
                    isActive = false;
                break;
            case checkState.button:
                if(!isActive && Input.GetKeyDown(KeyCode.E) && youIn)
                    StartCoroutine(button());
                break;
        }
        if (animateEnabled && isActive)
            GetComponent<Animator>().SetBool("Active", true);
        else if (animateEnabled && !isActive)
            GetComponent<Animator>().SetBool("Active", false);
    }

    IEnumerator button()
    {
        isActive = true;
        yield return new WaitForSeconds(1);
        isActive = false;
    }
    
}

[Serializable]
public class mediator //класс, который используется в качестве массива для посредника
{
    public bool connect; //активность одного из подключённых элементов
    public GameObject chain_element; //элемент цепи
}

/*На самом деле ещё много чего можно улучшить в этом скрипте: вынести ненужные команды в другие скрипты, избавиться от статуса light и использовать только mediator. Думаю, это можно поправить в будущем.
 А пока довёл код более ли менее до ума, оставляю так.*/

