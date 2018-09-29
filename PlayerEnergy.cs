using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour {

    public static int updated_energy = 100;

    public static int frames = 0;

	// Use this for initialization
	void Start () {
        		
	}
	
	// Update is called once per frame
	void Update () {

        updated_energy--;
        Networking.player_energy.value = updated_energy;

        if (frames == 5)
        {
            updated_energy--;
            frames = 0;
            Networking.player_energy.value = updated_energy;
        }
        frames++;
    }
}