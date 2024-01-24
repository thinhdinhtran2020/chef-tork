using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData{

    // Player Controller Variables
    public float walkSpeed;
    public float runSpeed;
    public float jumpStrength;
    public bool canControl;
    public float[] position;


    public PlayerData(PlayerController playercontroller)
    {
        walkSpeed = playercontroller.walkSpeed;
        runSpeed = playercontroller.runSpeed;
        jumpStrength = playercontroller.jumpStrength;
        canControl = playercontroller.canControl;

        position = new float[3];
        position[0] = playercontroller.transform.position.x;
        position[1] = playercontroller.transform.position.y;
        position[2] = playercontroller.transform.position.z;

    }

   
}
