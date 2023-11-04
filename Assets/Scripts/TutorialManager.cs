using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<Slide> _slide;
    [SerializeField] private int _slideNumber = 1;
    [SerializeField] private Image _tvScreen;
    [SerializeField] private float brightness = 0f;

    public int GetSlideNumber { get { return _slideNumber; } }

    private bool _onStart = false;
    // Start is called before the first frame update

    private void OnEnable()
    {
        RenderSettings.reflectionIntensity = 0.1f;
        SetImage(_slideNumber);
    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        if (!_onStart)
        {
            _onStart = true;
        }
    }

    private void SetImage(int slideNumber)
    {
        if (_tvScreen != null)
        {
            _tvScreen.sprite = _slide[slideNumber-1].GetImage();
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
        if (_slideNumber < _slide.Count)
            _slideNumber++;
        SetImage(_slideNumber);

        if(_slideNumber == _slide.Count)
        {
            //characterController.enabled = true;
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
