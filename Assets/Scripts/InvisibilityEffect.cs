using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class InvisibilityEffect : MonoBehaviour
{
    [SerializeField] private List<GameObject> GOtoHit;
    [SerializeField] private List<Image> ImagestoHit;

    private PhotonView pv;
    
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    public void InvisibilitySwitch(bool _bool)
    {
        foreach (GameObject GO in GOtoHit)
        {
            GO.GetComponent<SpriteRenderer>().enabled = _bool;
        }
        
        foreach (Image image in ImagestoHit)
        {
            image.GetComponent<Image>().enabled = _bool;
        }
        
    }

    public void Called(bool _switch)
    { 
        pv.RPC("RPC_HideShowPlayer", RpcTarget.Others, _switch);
    }
    
    [PunRPC] 
    public void RPC_HideShowPlayer(bool _switch)
    {
        if (pv.IsMine) return;
        
        this.InvisibilitySwitch(_switch);
    }

}
