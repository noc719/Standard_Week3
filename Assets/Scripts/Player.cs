using Unity.VisualScripting;
using UnityEngine;

public class Player :MonoBehaviour
{
    public PlayerController controller;
    private void Start()
    {
        CharacterManager.Instance.Player = this; //Player 오브젝트에 붙어있는 Player
        controller = GetComponent<PlayerController>();
    }
}