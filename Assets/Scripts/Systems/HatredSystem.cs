﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HatredSystem : MonoBehaviour
{

    //public int hateValueDecrement = 5;         //每秒仇恨值减少量

    //public int hateDecrementTime = 1;         //仇恨值减少间隔

    private Individual individual;

    //key 是仇恨来源ID ，value 是仇恨值
    Dictionary<int, int> hatredList = new Dictionary<int, int>();

    private void Awake()
    {
        individual = GetComponent<Individual>();
    }

    private void Start()
    {
        ////实例化时调用
        //StartCoroutine(HateDecrementTimer());
    }


    /// <summary>
    /// 增加仇恨值，若仇恨目标不在仇恨列表里，则先加入列表
    /// </summary>
    /// <param name="HateSource">仇恨目标</param>
    public void AddHateValue(int HateID)
    {

        Individual HateSource = LogicManager.GetIndividual(HateID);

        if (HateSource == null) Debug.Log("HateSource is null");

        //如果目标的势力和自己相同则不列入仇恨列表
        //if (HateSource.power == individual.power) 
        //{
        //    Debug.Log("因为目标是友军，所以不对友军产生仇恨");
        //    return;
        //}

        if (!hatredList.ContainsKey(HateSource.ID))
        {
            AddHatredList(HateSource);
        }
        else
        {
            hatredList[HateSource.ID] += HateSource.hatredValue;
        }

        Debug.Log(gameObject.name+"对ID为"+HateSource.gameObject.name+"的对象增加了"+hatredList[HateSource.ID]+"点仇恨值");
    }

    /// <summary>
    /// 获取当前仇恨值最高的目标
    /// </summary>
    /// <returns>返回仇恨值最高目标的Individual组件</returns>
    public Individual GetMostHatedTarget()
    {
        int maxValue = 0;
        int TargerID = 0;
        foreach(KeyValuePair<int, int> kvp in hatredList)
        {
            if (kvp.Value > maxValue)
            {
                maxValue = kvp.Value;
                TargerID = kvp.Key;
            }
        }

        return LogicManager.GetIndividual(TargerID);
    }

    //添加仇恨列表
    private void AddHatredList(Individual HateSource)
    {
        hatredList.Add(HateSource.ID, HateSource.hatredValue);
    }

    ////随时间流逝仇恨减少
    //private void HateDecrement()
    //{
    //    Dictionary<int, int>.KeyCollection HLkeys = hatredList.Keys;
    //    if(HLkeys.Count==0) 
    //    {
    //        StartCoroutine(HateDecrementTimer());
    //        return;
    //    }
    //    int[] keyArray = new int[HLkeys.Count];

    //    int index = 0;
    //    foreach (int key in HLkeys)
    //    {
    //        keyArray[index] = key;
    //    }


    //    for(int i=0;i<HLkeys.Count;i++)
    //    {
    //        //对基地的仇恨不用减少
    //        //if (pair.Key == 0) continue;

    //        //减少固定
    //        hatredList[keyArray[i]] -= hateValueDecrement;
    //        Debug.Log("减少了对" + LogicManager.GetIndividual(keyArray[i]).gameObject.name + "的" + hateValueDecrement + "点仇恨值，当前仇恨值是："+ hatredList[keyArray[i]]);

    //        //仇恨值小于0移除出仇恨表
    //        if (hatredList[keyArray[i]] <= 0)
    //        {
    //            hatredList.Remove(keyArray[i]);
    //        }
    //    }

    //    StartCoroutine(HateDecrementTimer());
    //}

    ////仇恨值每秒减少固定值，死循环
    //IEnumerator HateDecrementTimer()
    //{
    //    yield return new WaitForSeconds(hateDecrementTime);
    //    HateDecrement();
    //}
}

