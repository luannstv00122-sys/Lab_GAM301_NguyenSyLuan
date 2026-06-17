using UnityEngine;

public class SaveData : MonoBehaviour
{
    void Start()
    {
        // Đọc dữ liệu đã lưu
        int volume = PlayerPrefs.GetInt("Sound", 100);
        int score = PlayerPrefs.GetInt("Score", 0);

        Debug.Log("Volume = " + volume);
        Debug.Log("Score = " + score);
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Sound", 80);
        PlayerPrefs.SetInt("Score", 500);

        PlayerPrefs.Save();

        Debug.Log("Đã lưu dữ liệu");
    }
}