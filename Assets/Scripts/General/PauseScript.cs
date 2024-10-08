using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    bool buttonsActive = false;
    public void PauseGame(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            if (buttonsActive)
                UnPause();
            else
                Pause();
    }
    public void Pause()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            SwitchActivity(buttons[i]);
        }
        buttonsActive = true;
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            SwitchActivity(buttons[i]);
        }
        buttonsActive = false;
        Time.timeScale = 1;
    }
    private void SwitchActivity(GameObject obj)
    {
        if(obj.active)
            obj.SetActive(false);
        else
            obj.SetActive(true);
    }
}
