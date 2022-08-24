using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reloading
    }

    public State state { get; private set; }

    public Transform fireTransform;
    private Vector3 fire_Pos;

    public PlayerView playerView;

    private LineRenderer bulletLineRenderer;
    private AudioSource gunAudioPlayer;

    public GunData gunData;

    public int ammoRemain = 100;
    public int magAmmo;

    private float lastFireTime;


    private void Awake()
    {
        playerView = GetComponentInParent<PlayerView>();
        if(playerView != null)
        {
            Debug.Log("loaded");
        }
    }

    // Update is called once per frame
    private void OnEnable()
    {
        ammoRemain = gunData.startAmmoRemain;
        magAmmo = gunData.magCapacity;

        state = State.Ready;
        lastFireTime = 0;
    }

    public void Fire()
    {
        if(state == State.Ready && Time.time >= lastFireTime + gunData.timeBetFire)
        {
            lastFireTime = Time.time;
            Shot();
        }
    }

    private void Shot()
    {
        fire_Pos = new Vector3(fireTransform.position.x, fireTransform.position.y, fireTransform.position.z);

        Instantiate(gunData.bullet, fire_Pos, playerView.Gun_rotation);

        magAmmo--;
        if(magAmmo <= 0)
        {
            state = State.Empty;
        }
    }

    public bool Reload()
    {
        if (state == State.Reloading || ammoRemain <= 0 || magAmmo >= gunData.magCapacity)
        {
            return false;
        }

        StartCoroutine(ReloadRoutine());
        return true;
    }

    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;

        yield return new WaitForSeconds(gunData.reloadTime);

        int ammoToFill = gunData.magCapacity - magAmmo;

        if(ammoRemain < ammoToFill)
        {
            ammoToFill = ammoRemain;
        }
        
        magAmmo += ammoToFill;
        //ammoRemain -= ammoToFill;
        //무한대로 하려고 기존 코드에서 이것만 바꾸면 됨

        state = State.Ready;
    }
}
