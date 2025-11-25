using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DisplayPlayerStatus : MonoBehaviour
{
    private PlayerStatus status;
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private TextMeshProUGUI atkTxt;
    [SerializeField] private TextMeshProUGUI defTxt;
    [SerializeField] private TextMeshProUGUI hpTxt;
    [SerializeField] private TextMeshProUGUI mpTxt;
    [SerializeField] private TextMeshProUGUI expTxt;

    private void Start()
    {
        if(GameManager.Instance.PlayerInfo != null)
        {
            status = GameManager.Instance.PlayerInfo.status;
        }
    }

    public void OnStatusPanel()
    {
        if (status == null) return;

        levelTxt.text = status.playerLevel.ToString();
        atkTxt.text = status.playerAtk.ToString();
        defTxt.text = status.playerDef.ToString();
        hpTxt.text = status.playerCurrentHp.ToString() + " / " + status.playerMaxHp.ToString();
        mpTxt.text = status.playerCurrentMp.ToString() + " / " + status.playerMaxMp.ToString();
        expTxt.text = status.playerExp.ToString();
    }
}
