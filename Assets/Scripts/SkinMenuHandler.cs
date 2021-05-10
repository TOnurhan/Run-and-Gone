using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    public void SkinSelect(GameObject skin)
    {
        Destroy(_player.transform.GetChild(0).gameObject);
        skin = Instantiate(skin,_player.transform.position,Quaternion.identity);
        skin.transform.parent = _player.transform;
    }
}
