using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionMenu : MonoBehaviour {
    public Toggle soundToggle;
    public Slider volSlider;

    public bool soundBool;
    public float volFloat;

    public void toggleAct_sound(bool isActive)
    {
        soundBool = isActive;
    }

    public void sliderAct_volChange(float v)
    {
        volFloat = v;
    }
	// Use this for initialization
	void Start () {
        soundToggle.isOn = soundBool;
        volSlider.value = volFloat;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
