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

    public int levelIndex;

    public PlayerData(PlayerController playercontroller, int currentLevelIndex)
    {
        walkSpeed = playercontroller.walkSpeed;
        runSpeed = playercontroller.runSpeed;
        jumpStrength = playercontroller.jumpStrength;
        canControl = playercontroller.canControl;

        levelIndex = currentLevelIndex;

    }

   
}
