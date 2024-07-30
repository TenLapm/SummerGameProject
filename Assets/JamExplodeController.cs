using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamExplodeController : MonoBehaviour
{
    private GridManager gridManager;
    private InventoryPowerUps inventoryPowerUps;
    private PlayerControl playerControl;
    [SerializeField] private GameObject JamExplodeSound;
    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        inventoryPowerUps = GetComponentInParent<InventoryPowerUps>();
        playerControl = GetComponentInParent<PlayerControl>();
        JamExplodeSound.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        if (inventoryPowerUps.animator.GetBool("IsJamExplosion") == true)
        {
            JamExplodeSound.SetActive (true);
            Vector2Int gridPosition = gridManager.WorldToGridPosition(transform.position);
            gridManager.ChangeTileOwner(gridPosition, playerControl.player, 3);
        }
    }
}
