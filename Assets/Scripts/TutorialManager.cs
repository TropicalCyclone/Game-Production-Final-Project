using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<Slide> _slideshow;
    [SerializeField] private int _slideNumber = 1;
    [SerializeField] private Image _tvScreen;
    //[SerializeField] private float brightness = 0f;
    [SerializeField] private List<GameObject> TutorialObjectHider;

    public int GetSlideNumber { get { return _slideNumber; } }

    // Start is called before the first frame update

    private void OnEnable()
    {
       
        
        SetImage(_slideNumber);
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
      if(_slideNumber == _slideshow.Count)
        {
            HideAllObjects();
        }
    }

    public void SetLightBrightness(float value)
    {
        RenderSettings.ambientLight = new Color(0.1f * value, 0.1f * value, 0.1f * value);
    }

    private void SetImage(int slideNumber)
    {
        if (_tvScreen != null)
        {
            _tvScreen.sprite = _slideshow[slideNumber-1].GetImage();
        }
    }

    public void PreviousSlide()
    {
        if (_slideNumber > 1)
        {
            _slideNumber--;
            SetImage(_slideNumber);
        }
    }

    public void NextSlide()
    {
        if (_slideNumber < _slideshow.Count)
            _slideNumber++;
        SetImage(_slideNumber);

        if(_slideNumber == _slideshow.Count)
        {
            //characterController.enabled = true;
        }
    }

    public void HideAllObjects()
    {
        foreach (GameObject obj in TutorialObjectHider)
        {
            obj.SetActive(false);
        }
    }
}


[System.Serializable]
public class Slide
{
    public string name;
    [SerializeField] private Sprite _slideshowImage;
    public Sprite GetImage()
    {
        return _slideshowImage;
    }
}
