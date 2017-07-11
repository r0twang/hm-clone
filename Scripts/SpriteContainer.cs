using UnityEngine;
using System.Collections;

public class SpriteContainer : MonoBehaviour {
    public Sprite[] pUnarmedWalk, pGunWalk, pGunAttack, pPunch;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Sprite[] getPlayerUnarmedWalk()
    {
        return pUnarmedWalk;
    }

    public Sprite[] getPlayerPunch()
    {
        return pPunch;
    }

    public Sprite[] getWeapon (string weapon)
    {
        switch (weapon)
        {
            case "gun":
                return pGunAttack;
                break;
            default:
                return getPlayerPunch();
                break;
        }
    }

    public Sprite[] getWeaponWalk(string weapon)
    {
        switch (weapon)
        {
            case "gun":
                return pGunWalk;
                break;
            default:
                return getPlayerUnarmedWalk();
                break;
        }
    }
}
