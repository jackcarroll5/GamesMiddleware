using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeolocationUnityTest : MonoBehaviour
{
    public string locale;
    public Text geolocationText;


    // Start is called before the first frame update
    IEnumerator Start()
    {
     if (!Input.location.isEnabledByUser)
    {
           
    }

        Input.location.Start();
        Debug.Log("Location status: " + Input.location.status);
       
        int maximumWait = 10;

        while (Input.location.status
               == LocationServiceStatus.Initializing && maximumWait > 0) {
            yield return new WaitForSeconds (1);
            maximumWait--;
        }
 
        if (maximumWait < 1) {
            print ("The service is timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed) {
            print ("Unable to determine the current device location");
            yield break;
        }
        else {
        locale = "Latitude: " + Input.location.lastData.latitude + "\nLongitude: " + Input.location.lastData.longitude + "\nAltitude: " + Input.location.lastData.altitude + "\nHorizontal Accuracy: " + Input.location.lastData.horizontalAccuracy + "\nTimestamp: " +
          Input.location.lastData.timestamp + "\nVertical Accuracy: " + Input.location.lastData.verticalAccuracy;
                                       
                   geolocationText.text = "Current GPS coordinates: \n" + "Lat: " + Input.location.lastData.latitude.ToString() + " Lon: " + Input.location.lastData.longitude.ToString() + "\n" + locale;
        }
        Input.location.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
