using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

namespace CircleBounce
{
    public class GameManager : MonoBehaviour
    {
        private int collisionCount = 0;

        private Text bounceCountText;

        public GameObject finishWindow;
        public GameObject[] starsObjs;
        public GameObject pauseWindow;
        public LevelProperties levelProps;



        void Start()
        {
            bounceCountText = GameObject.FindGameObjectWithTag("BounceCountText").GetComponent<Text>();

            Time.timeScale = 1;

            Values.requireBounceCount = levelProps.requireBounceCount;
            Values.starsCounts = levelProps.starsCounts;

        }

        public void collisionCountChange()
        {
            collisionCount++;
            bounceCountText.text = collisionCount.ToString();
        }

        public void ObstacleCollision()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void levelFinish()
        {
            int starCount = 0;
            for (int i = levelProps.starsCounts.Length - 1; i >= 0; i--)
            {
                if (levelProps.starsCounts[i] >= collisionCount)
                {
                    starCount = i + 1;
                    break;
                }
            }
            for (int i = 0; i < starCount; i++) starsObjs[i].GetComponent<Image>().color = Color.yellow;

            LevelSave(starCount);

            Time.timeScale = 0;
            finishWindow.SetActive(true);
        }

        private void LevelSave(int starCount)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file;
            if (File.Exists(Application.persistentDataPath + $"/Level_{SceneManager.GetActiveScene().buildIndex - 1}.dat"))
            {
                file = File.Open(Application.persistentDataPath + $"/Level_{SceneManager.GetActiveScene().buildIndex - 1}.dat", FileMode.Open, FileAccess.ReadWrite);
                LevelData ld = (LevelData)binaryFormatter.Deserialize(file);
                file.Close();
                if (ld.starCount > starCount) return;
                file = File.Open(Application.persistentDataPath + $"/Level_{SceneManager.GetActiveScene().buildIndex - 1}.dat", FileMode.Open, FileAccess.ReadWrite);
            }
            else file = File.Create(Application.persistentDataPath + $"/Level_{SceneManager.GetActiveScene().buildIndex - 1}.dat");


            LevelData levelData = new LevelData(true, starCount);
            binaryFormatter.Serialize(file, levelData);
            file.Close();

            //if (PlayerPrefs.GetInt("Level" + (SceneManager.GetActiveScene().buildIndex - 1)) < starCount) PlayerPrefs.SetInt("Level" + (SceneManager.GetActiveScene().buildIndex - 1), starCount);
            //PlayerPrefs.SetInt("LevelIsFinished" + (SceneManager.GetActiveScene().buildIndex - 1).ToString(), 1);
        }

        public void levelPause()
        {
            pauseWindow.SetActive(!pauseWindow.activeSelf);
            Time.timeScale = Convert.ToInt32(!pauseWindow.activeSelf);
        }

        public void toMenu()
        {
            SceneManager.LoadScene(0);
        }
        public void sceneSkip()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    [Serializable]
    public class LevelProperties
    {
        public int requireBounceCount;
        public int[] starsCounts = new int[3];

    }

    [Serializable]
    public class LevelData
    {
        public bool isComplete;
        public int starCount;
        public LevelData(bool _isComplete, int _starCount)
        {
            isComplete = _isComplete;
            starCount = _starCount;
        }
    }

    public class Values
    {
        public static int requireBounceCount;
        public static int currentSkinIndex;
        public static int[] starsCounts = new int[3] { 0, 1, 2 };
    }
}