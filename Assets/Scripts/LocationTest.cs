using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocationTest : MonoBehaviour
{
    public TextMeshProUGUI LocationInfo;


    public IEnumerator StartLocation()
    {
        // 检查是否开启定位
        if (!Input.location.isEnabledByUser)
        {
            LocationInfo.text = "Location is not enabled by user";
            yield break;
        }

        Input.location.Start();

        // 等待定位服务初始化
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            LocationInfo.text = "Timed out";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            LocationInfo.text = "Unable to determine device location";
        }
        else
        {
            LocationInfo.text = $"{Input.location.lastData.timestamp}: {Input.location.lastData.longitude},{Input.location.lastData.latitude} {Input.location.lastData.altitude}";
        }
    }

    public void StartLocationButton()
    {
        StartCoroutine(StartLocation());
    }

    public void StopLocation()
    {
        Input.location.Stop();
        LocationInfo.text = "Location service stopped";
    }
}