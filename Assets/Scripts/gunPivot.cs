using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPivot : MonoBehaviour
{
    public PlayerView playerView;

    // Start is called before the first frame update
    void Start()
    {
        playerView = GetComponentInParent<PlayerView>();
        if (playerView != null)
            Debug.Log("playerView load...complete");
    }

    // Update is called once per frame
    void Update()
    {
        view();
    }

    private void view()
    {
        this.transform.rotation = playerView.Gun_rotation;
        this.transform.localScale = new Vector3(playerView.playerScaleX, playerView.playerScaleX, 1);
    }
}
