using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Networking : MonoBehaviour {
    
    //Declare serializable classes

    [Serializable]
    public class Updated_Energy
    {
        public int value = 100;
    }

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

    public static Updated_Energy player_energy = new Updated_Energy();

    public static bool update = false;

	// Use this for initialization
	void Start () {
        // StartCoroutine(GetOxygen());
        //StartCoroutine(UpdateOxygen());
	}

    //Update energy level to server
    IEnumerator UpdateEnergy()
    {

        WWWForm form = new WWWForm();
        form.AddField("energy", player_energy.value);
        //form.AddField("players",["", "", "", "", "", "", ""]);
        form.AddField("shotsTaken", 1);

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


    // Update is called once per frame
    void Update () {
        if (update)
        {
            StartCoroutine(GetPlayerActions());
            StartCoroutine(UpdateEnergy());
            update = false;
        }
        Debug.Log(update.ToString());
	}

	public IEnumerator GetPlayerActions()
    {

        UnityWebRequest www = UnityWebRequest.Get("http://10.100.201.130:5000/get-actions");

        yield return www.SendWebRequest();

        string json = www.downloadHandler.text;

        JsonUtility.FromJsonOverwrite(json, playerActions);

    }

    //Oxygen management
    //Get Oxygen level from server
    // IEnumerator GetOxygen()
    // {
    //     UnityWebRequest www = UnityWebRequest.Get("http://10.100.201.130:5000/get-state");
    //     yield return www.SendWebRequest();

    //     if (www.isNetworkError || www.isHttpError)
    //     {
    //         Debug.Log(www.error);
    //     }
    //     else
    //     {
    //         // Show results as text
    //         Debug.Log(www.downloadHandler.text);

    //         // Or retrieve results as binary data
    //         byte[] results = www.downloadHandler.data;

    //         Oxygen o = new Oxygen();

    //         string json = www.downloadHandler.text;

    //         JsonUtility.FromJsonOverwrite(json, o);

    //         Debug.Log(o.oxygen);

    //        }
    // }
}
