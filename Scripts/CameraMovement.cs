using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    GameObject Player;
    public bool followPlayer = true;


	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (followPlayer == true)
        {
            CamFollowPlayer();
        }
	}

    public void SetFollowPlayer(bool val)
    {
        followPlayer = val;
    }

    void CamFollowPlayer()
    {
        Vector3 newPos = new Vector3(Player.transform.position.x, Player.transform.position.y, this.transform.position.z);
        this.transform.position = newPos;
    }
}
