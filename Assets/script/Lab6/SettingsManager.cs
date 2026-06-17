using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class SettingsManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject settingsPanel;

    [Header("Camera Control")]
    public MonoBehaviour cameraLookScript;

    [Header("Volume")]
    public Slider volumeSlider;

    [Header("Coin")]
    public TMP_Text coinText;

    [Header("Fullscreen")]
    public Toggle fullscreenToggle;

    [Header("Quality")]
    public TMP_Dropdown qualityDropdown;

    private bool isMenuOpen = false;

    private void Start()
{
    LoadSettings();

    if (volumeSlider != null)
        volumeSlider.onValueChanged.AddListener(SetVolume);

    if (fullscreenToggle != null)
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

    if (qualityDropdown != null)
        qualityDropdown.onValueChanged.AddListener(SetQuality);

    settingsPanel.SetActive(false);
    isMenuOpen = false;

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
}
    private void Update()
{
    if (Keyboard.current.escapeKey.wasPressedThisFrame)
    {
        if (isMenuOpen)
            ClosePanel();
        else
            OpenPanel();
    }
}

    #region Panel

    public void OpenPanel()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (cameraLookScript != null)
            cameraLookScript.enabled = false;

        isMenuOpen = true;
    }

    public void ClosePanel()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraLookScript != null)
            cameraLookScript.enabled = true;

        isMenuOpen = false;
    }

    #endregion

    #region Volume

    public void SetVolume(float value)
    {
        AudioListener.volume = value;

        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }

    #endregion

    #region Fullscreen

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    #endregion

    #region Quality

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        PlayerPrefs.SetInt("Quality", qualityIndex);
        PlayerPrefs.Save();
    }

    #endregion

    #region Coin

    public void AddCoin(int amount)
    {
        int currentCoin = PlayerPrefs.GetInt("Coin", 0);

        currentCoin += amount;

        PlayerPrefs.SetInt("Coin", currentCoin);
        PlayerPrefs.Save();

        UpdateCoinUI();
    }

    public void SpendCoin(int amount)
    {
        int currentCoin = PlayerPrefs.GetInt("Coin", 0);

        if (currentCoin >= amount)
        {
            currentCoin -= amount;

            PlayerPrefs.SetInt("Coin", currentCoin);
            PlayerPrefs.Save();

            UpdateCoinUI();
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            int coin = PlayerPrefs.GetInt("Coin", 0);
            coinText.text = coin.ToString();
        }
    }

    #endregion

    private void LoadSettings()
    {
        // Volume
        float volume = PlayerPrefs.GetFloat("Volume", 1f);

        if (volumeSlider != null)
            volumeSlider.value = volume;

        AudioListener.volume = volume;

        // Fullscreen
        bool fullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;

        if (fullscreenToggle != null)
            fullscreenToggle.isOn = fullscreen;

        Screen.fullScreen = fullscreen;

        // Quality
        int quality = PlayerPrefs.GetInt(
            "Quality",
            QualitySettings.GetQualityLevel());

        if (qualityDropdown != null)
            qualityDropdown.value = quality;

        QualitySettings.SetQualityLevel(quality);

        // Coin
        UpdateCoinUI();
    }
}