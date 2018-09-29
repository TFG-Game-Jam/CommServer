using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Networking : MonoBehaviour {

    [Serializable]
    public class Oxygen
    {
        public int oxygen;
    }


	// Use this for initialization
	void Start () {
        StartCoroutine(GetText());
	}

    IEnumerator GetText()
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

           }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
