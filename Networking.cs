using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Networking : MonoBehaviour {

    //Declare serializable classes

    [Serializable]
    public class Oxygen
    {
        public int oxygen;
    }

    [Serializable]
    public class Orientation
    {
        bool port;
        bool starboard;
    }


	// Use this for initialization
	void Start () {
        StartCoroutine(GetOxygen());
        //StartCoroutine(UpdateOxygen());
	}

    //Oxygen management
    //Get Oxygen level from server
    IEnumerator GetOxygen()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://10.100.201.130:5000/get-state");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;

            Oxygen o = new Oxygen();

            string json = www.downloadHandler.text;

            JsonUtility.FromJsonOverwrite(json, o);

            Debug.Log(o.oxygen);

           }
    }

    //Update oxygen level to server
    IEnumerator UpdateOxygen()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=100"));

        UnityWebRequest www = UnityWebRequest.Post("http://10.100.201.130:5000/get-state", formData);
        yield return www.SendWebRequest();
        

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Updated oxygen");
        }
    }
    // Update is called once per frame
    void Update () {
        //StartCoroutine(GetOrientation());
	}

    IEnumerator GetOrientation()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://10.100.201.130:5000/get-actions");
        yield return www.SendWebRequest();

        Orientation or = new Orientation();

        string json = www.downloadHandler.text;

        JsonUtility.FromJsonOverwrite(json, or);

        Debug.Log(or);
        
    }
}
