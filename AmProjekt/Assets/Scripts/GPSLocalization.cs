using Mapbox.Unity.Location;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GPSLocalization : SingletonMonoBehaviour<GPSLocalization>
{
    private float lantitude;
    private float longitude;

    bool work;

    public (float Lan, float Long)? GpsData
    {
        get
        {
            if (!Input.location.isEnabledByUser)
                return null;
            return (lantitude, longitude);
        }
    }

    private void Start()
    {
        StartCoroutine(GPSLock());
    }

    private void Update()
    {
        if (!work)
        {
            work = true;
            StartCoroutine(GPSLock());
        }
    }
    private IEnumerator GPSLock()
    {
        Debug.Log("StartMethod");
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("gps disable");
            work = false;
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        Debug.Log("Start while");

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.Log("Time Out");
            work = false;

            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            work = false;
            Debug.Log("Faild");
            yield break;
        }
        else
        {
            Debug.Log("Running");
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }
    }

    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            lantitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            Debug.Log($"lan: {lantitude} Long: {longitude}");
        }
    }

}