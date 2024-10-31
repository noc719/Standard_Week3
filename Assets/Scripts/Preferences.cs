using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Preferences : MonoBehaviour
{
    public Image Image;

    public void Start()
    {
        Image = GetComponentInChildren<Image>();
    }
    public void OnPreferences(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (CharacterManager.Instance.Player.controller.canLook)
            {
                CharacterManager.Instance.Player.controller.canLook = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                CharacterManager.Instance.Player.controller.canLook = true;
                Cursor.lockState = CursorLockMode.None;
            }

            //커서 잠금 푸는건 Cursor.lockState = CursorLockMode.None;
            //화면 전환 잠금은 Look 메서드가 실행되는 곳에 조건을 붙이면 됨
        }
    }
}