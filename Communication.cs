using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Communication : MonoBehaviour {

    [Serializable]
    public class Actions
    {
        public bool port;
        public bool starboard;
        public bool fixGenerator;
        public bool loadCyan;
        public bool loadGreen;
        public bool loadPurple;
        public bool loadWhite;
    }

    public static Actions playerActions = new Actions();

    public IEnumerator GetPlayerActions()
    {

        UnityWebRequest www = UnityWebRequest.Get("http://10.100.201.130:5000/get-actions");

        yield return www.SendWebRequest();

        string json = www.downloadHandler.text;

        JsonUtility.FromJsonOverwrite(json, playerActions);

    }

    [Serializable]
    public class Shots
    {
        public int taken = 0;
        public int fired = 0;
    }

    public static Shots playerShots = new Shots();

    IEnumerator UpdateShotsTaken()
    {
        WWWForm form = new WWWForm();
        form.AddField("shotsTaken", playerShots.taken);
        form.AddField("shotsFired", playerShots.fired);

        UnityWebRequest www = UnityWebRequest.Post("http://10.100.201.130:5000/set-state", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            if (www.isNetworkError)
                Debug.Log("Network erros");
            else
                Debug.Log("isHttpError");
            Debug.Log(www.responseCode.ToString());
        }
        else
        {
            Debug.Log("Updated Player Energy");
        }

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
