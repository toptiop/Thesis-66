using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _ambientSlider;
    [SerializeField] private Slider _cutSceneSlider;
    [SerializeField] private Slider _efxSlider;
    [SerializeField] private Slider _testSlider;

    [SerializeField] protected TMP_Text masterText;
    [SerializeField] protected TMP_Text soundAmText;
    [SerializeField] protected TMP_Text SoundCutText;
    [SerializeField] protected TMP_Text SoundEFXText;
    [SerializeField] protected TMP_Text SoundTestText;

    [SerializeField] AudioClip hoverClip;
    [SerializeField] AudioClip ClickClip;
    [SerializeField] AudioSource sourceBTN;

    private void Awake()
    {
        _masterSlider.onValueChanged.AddListener((Float) =>
        {
            MasterSlider();
        });

        _ambientSlider.onValueChanged.AddListener((Float) => 
        {
            AmbientSlider();
        });

        _cutSceneSlider.onValueChanged.AddListener((Float) =>
        {
            CutSceneSlider();
        });

        _efxSlider.onValueChanged.AddListener((Float) =>
        {
            EFXSlider();
        });

        _testSlider.onValueChanged.AddListener((Float) =>
        {
            TestSlider();
        });
    }
    private void Start()
    {
        LoadValues();
    }

    #region soundBTN

    public void OnHoverBTN()
    {
        sourceBTN.PlayOneShot(hoverClip);
    }

    public void OnClickBTN()
    {
        sourceBTN.PlayOneShot(ClickClip);
    }

    #endregion
    #region Sound Settings
    public void MasterSlider()
    {
        SetMasterVolume(_masterSlider.value);
    }

    public void AmbientSlider()
    {
        SetAmbientVolume(_ambientSlider.value);
    }

    public void CutSceneSlider()
    {
        SetCutSceneVolume(_cutSceneSlider.value);
    }

    public void EFXSlider()
    {
        SetEFXVolume(_efxSlider.value);
    }

    public void TestSlider()
    {
        SetTestVolume(_testSlider.value);
    }

    public void SetMasterVolume(float sliderMasterValue)
    {
        mixer.SetFloat("Master", Mathf.Log10(sliderMasterValue) * 20);
        masterText.text = sliderMasterValue.ToString("0.0"); 
        PlayerPrefs.SetFloat("MasterVolume", sliderMasterValue);

    }
    public void SetAmbientVolume(float sliderAmbientValue)
    {
        mixer.SetFloat("Am", Mathf.Log10(sliderAmbientValue) * 20);
        soundAmText.text = sliderAmbientValue.ToString("0.0");
        PlayerPrefs.SetFloat("AmVolume", sliderAmbientValue);
    }
    public void SetCutSceneVolume(float sliderCutSceneValue)
    {
        mixer.SetFloat("CutScene", Mathf.Log10(sliderCutSceneValue) * 20);
        SoundCutText.text = sliderCutSceneValue.ToString("0.0");
        PlayerPrefs.SetFloat("CutSceneVolume", sliderCutSceneValue);
    }

    public void SetEFXVolume(float sliderEFXValue)
    {
        mixer.SetFloat("Efx", Mathf.Log10(sliderEFXValue) * 20);
        SoundEFXText.text = sliderEFXValue.ToString("0.0");
        PlayerPrefs.SetFloat("EfxVolume", sliderEFXValue);
    }

    public void SetTestVolume(float sliderTestValue)
    {
        mixer.SetFloat("Test", Mathf.Log10(sliderTestValue) * 20);
        SoundTestText.text = sliderTestValue.ToString("0.0");
        PlayerPrefs.SetFloat("TestVolume", sliderTestValue);
    }


    public void LoadValues()
    {
        float MasterValue = PlayerPrefs.GetFloat("MasterVolume");
        _masterSlider.value = MasterValue;
        SetMasterVolume(MasterValue);

        float AmValue = PlayerPrefs.GetFloat("AmVolume");
        _ambientSlider.value = AmValue;
        SetAmbientVolume(AmValue);

        float CutValue = PlayerPrefs.GetFloat("CutSceneVolume");
        _cutSceneSlider.value = CutValue;
        SetCutSceneVolume(CutValue);

        float EfxValue = PlayerPrefs.GetFloat("EfxVolume");
        _efxSlider.value = EfxValue;
        SetEFXVolume(EfxValue);

        float TestValue = PlayerPrefs.GetFloat("TestVolume");
        _testSlider.value = TestValue;
        SetTestVolume(TestValue);   
    }
    #endregion
}
