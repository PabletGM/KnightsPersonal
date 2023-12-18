using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerAbilityCharacter player = other.GetComponent<PlayerAbilityCharacter>();
        if (player != null)
        {
            player.transform.position = Vector3.zero;
            SceneLoaderManager.instance.LoadRandomScene();
        }
    }
}
