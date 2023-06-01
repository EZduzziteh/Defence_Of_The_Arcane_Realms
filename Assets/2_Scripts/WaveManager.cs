namespace Gameplay
{
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;


    public class WaveManager : MonoBehaviour
    {

        int currentWaveIndex = 0;
        public List<WaveData> waveData = new List<WaveData>();

        [SerializeField]
        float timeBetweenWaves = 10.0f;
        [SerializeField]
        float timeUntilNextWave = 10.0f;

        float timeLeft;

        TextMeshProUGUI timeBetweenWavesText;
        public bool readyToStart = false;
        float width = 0.75f;

        public int enemiesLeft = 0;
        int enemiesInWave = 0;

        bool isWaveInProgress;

        private void Start()
        {

            timeBetweenWavesText = FindObjectOfType<UI_Wave_Display>().GetComponent<TextMeshProUGUI>();

            foreach (WaveData data in GetComponentsInChildren<WaveData>())
            {
                waveData.Add(data);
            }

            timeLeft = timeUntilNextWave;
        }

        public void Update()
        {
            if (readyToStart)
            {

                if (isWaveInProgress)
                {
                    if (enemiesLeft <= 0)
                    {
                        EndWave();
                    }
                }


                if (timeUntilNextWave > 0)
                {
                    timeUntilNextWave -= Time.deltaTime;
                    if (timeUntilNextWave > 0)
                    {

                        if ((Mathf.Round(timeUntilNextWave * 100) / 100) < timeLeft)
                        {
                            timeLeft = Mathf.Round(timeUntilNextWave * 100.0f) * 0.01f;
                        }

                        string textString = "Wave: " + (currentWaveIndex + 1) + "/" + waveData.Count;
                        textString += ", Next wave in: " + timeLeft.ToString(".00");
                        timeBetweenWavesText.horizontalAlignment = HorizontalAlignmentOptions.Center;

                        timeBetweenWavesText.verticalAlignment = VerticalAlignmentOptions.Middle;
                        timeBetweenWavesText.enableAutoSizing = true;

                        timeBetweenWavesText.SetText($"<mspace={width}em>{textString}");
                    }
                    else
                    {
                        if (currentWaveIndex < waveData.Count)
                        {
                            timeBetweenWavesText.text = "Wave: " + (currentWaveIndex + 1) + "/" + waveData.Count;
                        }
                        else
                        {
                            timeBetweenWavesText.text = "Final Wave: " + (currentWaveIndex + 1) + "/" + waveData.Count;

                        }
                        StartWave();
                    }
                }
            }
        }
        public void StartWave()
        {
            waveData[currentWaveIndex].spawning = true;
            isWaveInProgress = true;

            enemiesInWave = waveData[currentWaveIndex].totalAmountToSpawn;
            enemiesLeft = enemiesInWave;
        }

        public void EndWave()
        {
            isWaveInProgress = false;
            waveData[currentWaveIndex].spawning = false;
            FindObjectOfType<PlayerController>().AddGold(waveData[currentWaveIndex].waveGoldBonus);
            currentWaveIndex++;

            if (currentWaveIndex >= waveData.Count)
            {
                timeBetweenWavesText.text = "Level Complete! all waves finished!";
                // FindObjectOfType<PlayerController>().EndLevel();
                FindObjectOfType<UI_Manager>().HandleLevelComplete();
            }
            else
            {
                timeUntilNextWave = timeBetweenWaves;
                timeLeft = timeBetweenWaves;
            }
        }


    }
}