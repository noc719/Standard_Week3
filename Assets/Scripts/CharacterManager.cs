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
            instance = this; // instance�� �� ���ӿ�����Ʈ�� �ǰ�
            DontDestroyOnLoad(gameObject); //ĳ���͸Ŵ����� ���� ��Ȳ�� �����ϱ����� �� �̵��ÿ��� �ı����� ����
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
