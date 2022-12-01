using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AbstractMap map;
    public bool mapWasCreated = false;

    private void Awake()
    {
        map.OnInitialized += MapWasCreated;
    }

    private void Update()
    {
        if (GPSLocalization.Instance.GpsData.HasValue && mapWasCreated)
        {
            var temp = GPSLocalization.Instance.GpsData.Value;
            UpdateWorldPosition(temp.Lan, temp.Long);
        }
    }

    public void UpdateWorldPosition(float lantitude, float longitude)
    {
        var localization = map.GeoToWorldPosition(new Vector2d(lantitude, longitude), false);
        transform.position = localization;
    }

    public void MapWasCreated()
    {
        mapWasCreated = true;
    }
}