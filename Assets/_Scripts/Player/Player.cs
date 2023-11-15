using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;
    public PlayerData playerData;
    public PlayerInputActions playerInputActions;

    private void Awake()
    {
        if(playerMovement == null)
            TryGetComponent<PlayerMovement>(out playerMovement);

        if(playerCombat == null)
            TryGetComponent<PlayerCombat>(out playerCombat);

        if(playerInputActions == null)
            playerInputActions = new PlayerInputActions();

    }
}
