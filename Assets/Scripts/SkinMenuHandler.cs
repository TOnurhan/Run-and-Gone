using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinMenuHandler : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _player;
    [SerializeField] private GameObject _playerSkin;

    public void SkinSelect(GameObject skin)
    {
        _playerSkin.SetActive(false);
        _playerSkin = skin;
        _player.anim = _playerSkin.GetComponent<Animation>();
        skin.SetActive(true);
    }
}
