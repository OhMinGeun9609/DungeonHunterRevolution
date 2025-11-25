using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] public GameObject[] mapPrefabs;
    public StageInfo stageInfo;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.StageManager = this;
    }

    public void StageStart(int idx)
    {
        stageInfo = Instantiate(mapPrefabs[idx]).GetComponent<StageInfo>();
    }

    public void StageClear()
    {
        GameManager.Instance.StageClear();
    }
}
