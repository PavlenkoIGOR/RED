using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsControl : MonoBehaviour
{
    [SerializeField]    private GameObject _menuPanel;
    [SerializeField]    private GameObject _optionsPanel;
    [SerializeField]    private Slider _sliderVolume;
    [SerializeField]    private TMP_Text _textVolumeValue;
    private bool _optionsOpened;

    void Start()
    {
        
    }

    private void Update()
    {
        _textVolumeValue.text = _sliderVolume.value.ToString("F1");
        AudioListener.volume = _sliderVolume.value;
    }

    public void OpenCloseOptions()
    {
        _optionsPanel.SetActive(!_optionsOpened);
        _menuPanel.SetActive(_optionsOpened);
        _optionsOpened = !_optionsOpened;
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
