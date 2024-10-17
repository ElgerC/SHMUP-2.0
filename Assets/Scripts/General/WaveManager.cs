using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [System.Serializable]
    public class Waves
    {
        public Rounds[] rounds;
    }

    [System.Serializable]
    public class Rounds
    {
        public int enemy1C;
        public int enemy2C;
        public int enemy3C;
        public bool boss;
    }

    [SerializeField]
    private Waves[] waves;

    public int curWaveC;
    public int curRoundC;
    public int enemyC;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        
    }


}
