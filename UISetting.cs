using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UISetting : MonoBehaviour {

    [Header("Script")]
    [SerializeField] public MazeSpawner spawn;

    [Header("Another")]
    [SerializeField] public Toggle first_Toggle;
    [SerializeField] public GameObject pannel;

    [Header("Input")]
    [SerializeField] public TMP_InputField[] all_input;

    [Header("Hint")]
    [SerializeField] public Toggle togle_hint;
    [SerializeField] public GameObject hint;

    [Header("Slider")]
    [SerializeField] public Slider sliderCamera;
    [SerializeField] public Camera camera;

    [Header("Info")]
    [SerializeField] public int size_Weight = 5;
    [SerializeField] public int size_Height = 5;

    [SerializeField] public int startCellX = 0;
    [SerializeField] public int startCellY = 0;
    
    [SerializeField] public int typeGeneration = 0;

    private void Start() {
        sliderCamera.minValue = 3;
        sliderCamera.maxValue = 50;
        sliderCamera.value = 10;

        pannel.SetActive(false);

        hint.SetActive(false);
    }

    public void SetText(int index) {

        int num = 0;

        if(int.TryParse(all_input[index].text, out int numIf)) {
            num = numIf;
        } else {
            all_input[index].text = "0";
        }

        switch(index) {
            case 0:
                if (num < 0 || num > 100) {
                    num = 5;
                    all_input[index].text = "5";
                }
                size_Weight = num;
                break;

            case 1:
                if (num < 0 || num > 100) {
                    num = 5;
                    all_input[index].text = "5";
                }
                size_Height = num;
                break;

            case 2:
                if (num < 0 || num > size_Weight - 1) {
                    num = 0;
                    all_input[index].text = "0";
                }
                startCellX = num;
                break;

            case 3:
                if (num < 0 || num > size_Weight - 1) {
                    num = 0;
                    all_input[index].text = "0";
                }
                startCellY = num;
                break;
        }
    }

    public void ClickToggle_one() {
        if (first_Toggle.isOn) {
            typeGeneration = 0;
        } else {
            typeGeneration = 1;
        }
    }

    public void ZoomCamera() {
        camera.orthographicSize = sliderCamera.value;
    }

    public void StartGeneration() {
        spawn.CreateSettingAndMaze(this);
    }

    public void Hint() {
        if(togle_hint.isOn) {
            hint.SetActive(true);
        } else {
            hint.SetActive(false);
        }
    }

    public void ActiviteObject(bool set) {
        pannel.SetActive(set);
    }

    public void ExitApp() {
        Application.Quit();
    }
}
