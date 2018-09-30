using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Communication : MonoBehaviour {

    //This class is responsible for communication between Unity and Server

    //Player's actions management
    
    //Class with directions, actions and coloured shots
    [Serializable]
    public class Actions
    {
        public float aimAngle;
        public bool fixGenerator;
        public bool loadCyan;
        public bool loadGreen;
        public bool loadPurple;
        public bool loadWhite;
        public bool port;
        public int[] roomState;
        public bool starboard;
    }

    public static Actions playerActions = new Actions();

    public int update = 0; 

    //Method to get the class's atribute values
    public IEnumerator GetPlayerActions()
    {

        UnityWebRequest www = UnityWebRequest.Get("http://10.100.201.130:5000/get-actions");

        yield return www.SendWebRequest();

        string json = www.downloadHandler.text;

        JsonUtility.FromJsonOverwrite(json, playerActions);

        Debug.Log("port");
        Debug.Log(playerActions.port);
        Debug.Log("starboard");
        Debug.Log(playerActions.starboard);
        Debug.Log("fixGenerator");
        Debug.Log(playerActions.fixGenerator);
        Debug.Log("loadCyan");
        Debug.Log(playerActions.loadCyan);
        Debug.Log("loadGreen");
        Debug.Log(playerActions.loadGreen);
        Debug.Log("loadPurple");
        Debug.Log(playerActions.loadPurple);
        Debug.Log("loadWhite");
        Debug.Log(playerActions.loadWhite);
        Debug.Log("aim angle");
        Debug.Log(playerActions.aimAngle.ToString());
        //Debug.Log("room state");
        //Debug.Log(playerActions.roomState.ToString());
    }

    //Shots Management

    //Class with atributes of taken shots and fired shots
    [Serializable]
    public class Shots
    {
        public int taken = 0;
        public int fired = 0;
    }

    public static Shots playerShots = new Shots();

    //Method to warn Server of fired and taken shots
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
            Debug.Log("Updated Player Shots");
            Debug.Log("shots taken");
            Debug.Log(playerShots.taken);
            Debug.Log("shots fired");
            Debug.Log(playerShots.fired);
        }

    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per fixed frame time
    // Gets player's actions and Update Server on shots info
    private void FixedUpdate()
    {
        if (update == 3)
        {
            StartCoroutine(GetPlayerActions());
            update = 0;
        }
        else
        {
            update++;
        }
        if (shot)
        {
            StartCoroutine(UpdateShotsTaken());
        }
    }
}
