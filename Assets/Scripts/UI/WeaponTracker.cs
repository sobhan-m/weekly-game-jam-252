using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTracker : MonoBehaviour
{
    [SerializeField] Sprite ketchupBottle;
    [SerializeField] Sprite mayoBottle;
    [SerializeField] Sprite mustardBottle;
    [SerializeField] Image weaponImage;

    private Gun gun;
    private WeaponTypes weaponType;

    // Start is called before the first frame update
    void Start()
    {
        gun = FindObjectOfType<Gun>();
        weaponType = gun.GetGunType();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGunType();
    }

    private void UpdateGunType()
    {
        if (weaponType != gun.GetGunType())
        {
            weaponType = gun.GetGunType();

            switch (weaponType)
            {
                case WeaponTypes.KETCHUP:
                    weaponImage.sprite = ketchupBottle;
                    break;
                case WeaponTypes.MAYONNAISE:
                    weaponImage.sprite = mayoBottle;
                    break;
                case WeaponTypes.MUSTARD:
                    weaponImage.sprite = mustardBottle;
                    break;
                default:
                    Debug.Log("WeaponTracker.UpdateGunType(): Reached default type.");
                    break;
            }
        }
    }
}
