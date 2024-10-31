using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;

    public static CharacterManager Instance 
    { get 
        {
            if (instance == null)
            {
                instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return instance;
        } 
    }

    private Player player;
    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this; // instance는 이 게임오브젝트가 되고
            DontDestroyOnLoad(gameObject); //캐릭터매니저가 없는 상황을 예방하기위해 씬 이동시에도 파괴되지 않음
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }   
        }
    }
}
