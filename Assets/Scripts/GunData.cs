using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/GunData", fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    //오디오 클립들은 추후에...
    /*
    public AudioClip shotClip;
    public AudioClip reloadClip;
    */

    public GameObject bullet;


    public int startAmmoRemain = 100;
    public int magCapacity = 25;

    public float timeBetFire = 0.12f;
    public float reloadTime = 1.8f;

}