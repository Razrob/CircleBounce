using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CircleBounce
{
    public class MenuController : MonoBehaviour
    {
        public int levelCount;
        private LevelData[] levelDatas;


        public GameObject[] levelButtons;
        void Start()
        {
            levelDatas = new LevelData[levelCount];
            Time.timeScale = 1;


            LoadData();

            for (int i = 0; i < levelCount; i++)
            {
                if (levelDatas[i].isComplete)
                {
                    for (int x = 0; x < 3; x++) levelButtons[i].transform.GetChild(x).gameObject.GetComponent<Image>().color = new Color(0.2196079f, 0.2196079f, 0.2196079f, 1);
                }
            }

            for (int i = 0; i < levelButtons.Length; i++)
            {
                for (int x = 0; x < levelDatas[i].starCount; x++)
                {
                    levelButtons[i].transform.GetChild(x).gameObject.GetComponent<Image>().color = Color.yellow;
                }
            }
        }

        private void LoadData()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file;
            for (int i = 0; i < levelCount; i++)
            {
                if (!File.Exists(Application.persistentDataPath + $"/Level_{i}.dat"))
                {
                    levelDatas[i] = new LevelData(false, 0);
                    continue;
                }
                file = File.OpenRead(Application.persistentDataPath + $"/Level_{i}.dat");
                levelDatas[i] = (LevelData)binaryFormatter.Deserialize(file);
            }


            // for (int i = 0; i < levelCount; i++) if (PlayerPrefs.HasKey("Level" + i)) levelFinishStarCount[i] = PlayerPrefs.GetInt("Level" + i);
            // for (int i = 0; i < levelCount; i++) if (PlayerPrefs.HasKey("LevelIsFinished" + i)) levelIsFinished[i] = Convert.ToBoolean(PlayerPrefs.GetInt("LevelIsFinished" + i));
        }

        public void levelLoader(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
        }
    }
}