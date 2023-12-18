using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script que controla la camara con velocidad y va siguiendo al jugador
public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField]
    private float followSpeed;
    [SerializeField]
    private Vector3 offset;

    private GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.gameObject;    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = Vector3.Lerp(transform.position, (player.transform.position + offset), Time.deltaTime * followSpeed);
    }
}
