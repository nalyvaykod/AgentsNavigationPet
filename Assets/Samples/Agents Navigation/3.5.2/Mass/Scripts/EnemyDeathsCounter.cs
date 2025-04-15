using UnityEngine;
using UnityEngine.UI;

public class EnemyDeathsCounter : MonoBehaviour
{
    public int objectCount = 0; 
    public Text objectCountText; 

    void OnDestroy()
    {
        objectCount++; 
        UpdateObjectCountText(); 
    }

    void UpdateObjectCountText()
    {
        objectCountText.text = objectCount.ToString();
    }
}

