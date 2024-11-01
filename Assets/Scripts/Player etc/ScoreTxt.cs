using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTxt : MonoBehaviour
{
    private TMP_Text txt;
    private void Awake()
    {
        txt = GetComponent<TMP_Text>();
    }
    void Update()
    {
        txt.SetText(PlayerPrefs.GetInt("Score").ToString());
    }
}
