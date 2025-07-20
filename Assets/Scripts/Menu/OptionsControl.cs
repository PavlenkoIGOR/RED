using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsControl : MonoBehaviour
{
    [Header("AUDIO")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private Slider _sliderVolume;
    [SerializeField] private TMP_Text _textVolumeValue;
    
    private bool _optionsOpened = false;

    [Header("CONTROLS")]
    public Slider _controlSlider;

    [SerializeField] private Hero _hero;
    [SerializeField] private Button _pauseBttn;
    public Button startBttn;
    public Button restartBttn;
    void Start()
    {
        _controlSlider.value = 0; // 0- finger
        startBttn.gameObject.SetActive(true);
        restartBttn?.gameObject.SetActive(false);
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
        if (GameController.instance.isGameStarted)
        {
            _pauseBttn.gameObject.SetActive(_optionsOpened);
        }
        _optionsOpened = !_optionsOpened;
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
