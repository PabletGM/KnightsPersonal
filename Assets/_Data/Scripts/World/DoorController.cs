using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private DOTweenAnimation openAnimation;

    //animacion con tween de abrir puerta
    public void OpenDoor()
    {
        if (openAnimation != null)
        {
            openAnimation.DOPlay();
        }
    }
   
}
