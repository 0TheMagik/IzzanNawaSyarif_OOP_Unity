using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;

    Weapon weapon;

    void Awake()
    {
        weapon = weaponHolder;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(weapon != null){
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            weapon.transform.SetParent(other.transform);
            TurnVisual(true);
            Debug.Log("Trigger");
        }
        else{
            Debug.Log("not trigger");
        }
        
    }

    void TurnVisual(bool on)
    {
        if(weapon != null){
            weapon.gameObject.SetActive(on);
        }
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        if(weapon != null){
            weapon.gameObject.SetActive(on);
        }
    }

    public bool GetWeapon()
    {
        return weaponHolder != null;
    }
}
