using Mapbox.Unity.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : SingletonMonoBehaviour<PointManager>
{
    [SerializeField]
    private Player player;

    [Space]
    [SerializeField]
    private GameObject pointPrefab;
    [SerializeField]
    private float minRange;
    [SerializeField]
    private float maxRange;

    [Space]
    [SerializeField]
    private Transform parret;
    [SerializeField]
    private AbstractMap map;

    private Point point;

    protected override void Awake()
    {
        base.Awake();
        map.OnInitialized += GenerateNextPoint;
    }

    private void Update()
    {
        if (point != null)
        {
            Debug.LogError(Mathf.Abs(Vector3.Distance(player.transform.position, point.transform.position)));

            if (Mathf.Abs(Vector3.Distance(player.transform.position, point.transform.position)) < 4)
            {
                GenerateNextPoint();
                Debug.LogError("new position");
            }
        }
    }

    public void GenerateNextPoint()
    {
        float x = player.transform.position.x + GenerateRange();
        float y = player.transform.position.y;
        float z = player.transform.position.z + GenerateRange();

        if (point == null)
        {
            point = Instantiate(pointPrefab, new Vector3(x, y, z), Quaternion.identity, parret).GetComponent<Point>();
        }
        else
        {
            point.transform.position = new Vector3(x, y, z);
        }
    }

    private float GenerateRange()
    {
        float randomNum = UnityEngine.Random.Range(minRange, maxRange);
        bool isPoitive = UnityEngine.Random.Range(0, 10) < 5;
        if (isPoitive)
        {
            return randomNum * -1;
        }

        return randomNum;
    }
    public void SetPointOnPlayer()
    {
        point.transform.position = player.transform.position;
    }
}