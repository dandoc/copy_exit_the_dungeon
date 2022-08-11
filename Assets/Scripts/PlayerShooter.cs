using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;
    public Transform gunPivot;

    private PlayerInput2 playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput2>();
    }

    private void OnEnable()
    {
        gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        gun.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInput.fire)
        {
            gun.Fire();
        }

        else if (playerInput.reload)
        {
            if (gun.Reload())
            {
                Debug.Log("Reload");
            }
        }

        //UpdateUI();
    }

    //private void UpdateUI()
    //{
    //    if(gun != null && UIManager.instance != null)
    //    {
    //        UIManager.instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
    //    }
    //}
}
