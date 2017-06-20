using UnityEngine.Networking;
using UnityEngine;

public class PlayerShoot : NetworkBehaviour {

    private const string PLAYER_TAG = "Player";

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

     void Start()
    {
        if( cam == null)
        {
            Debug.Log("PlayerShoot: No Camera Referenced!");
            this.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
         
        }
    }


    [Client]//Method only called on the CLient
    void Shoot()
    {
        RaycastHit _hit;
      
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            Debug.Log("We Hit " + _hit.collider.name);
            if(_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name);
            }
        }
    }

    [Command]//method only called on the server
    void CmdPlayerShot(string _ID)
    {
        Debug.Log(_ID + " Player has been Shot");
    }
}
