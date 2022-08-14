using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformSpawner_Spb : MonoBehaviour
{
    [SerializeField]
    private PlatformShape[] allPlatforms;

    [SerializeField]
    private Transform lastPlatform;

    public int SpawnPlatfroms()
    {
        Transform[] platforms = SetupPlatformFigure();

        int platformCount = SetupPlatformCount();

        var indexs = SetupStartAndEndIndex(platforms);

        for (int i = 0; i < platformCount; ++i)
        {
            Transform platform = Instantiate(platforms[Random.Range(indexs.Item1, indexs.Item2)]);

            platform.position = new Vector3(0, -i * 0.5f, 0);
            platform.eulerAngles = new Vector3(0, -i * 5, 0);

            if (i != 0 && i % 5 == 0 && Random.Range(0, 2) == 1)
                platform.eulerAngles += Vector3.up * 180;
            
            platform.SetParent(transform);
        }

        lastPlatform.position = new Vector3(0, -platformCount * 0.5f, 0);

        return platformCount;
    }

    private Transform[] SetupPlatformFigure()
    {
        int index = Random.Range(0, allPlatforms.Length);

        Transform[] selectedPlatforms = new Transform[allPlatforms[index].platforms.Length];
        for (int i = 0; i < allPlatforms[index].platforms.Length; ++i)
            selectedPlatforms[i] = allPlatforms[index].platforms[i];

        return selectedPlatforms;
    }
    
    private (int, int) SetupStartAndEndIndex(Transform[] platforms)
    {
        int level = PlayerPrefs.GetInt("LEVEL");

        float startDureaction = 0.05f;
        float endDuration = 0.1f;

        // level 0 ~ 19 : 0
        // level 20 ~ 39 : 1
        // level 40 ~ 59 : 2
        int startIndex = Mathf.Min((int) (level * startDureaction), platforms.Length - 1);
        
        
        // level 0 ~ 9 : 2
        // level 10 ~ 19 : 3
        // level 20 ~ 29 : 4
        int endIndex = Mathf.Min((int) (level * endDuration + 2), platforms.Length);

        return (startIndex, endIndex);
    }
    
    private int SetupPlatformCount()
    {
        int level = PlayerPrefs.GetInt("Level");
        int baseCount = 20;

        int platformCount = baseCount * ((level + 10) / 10) + (int) (level % 10 * 1.5f);

        return platformCount;
    }

    [System.Serializable]
    private struct PlatformShape
    {
        public Transform[] platforms;
    }
    

}
