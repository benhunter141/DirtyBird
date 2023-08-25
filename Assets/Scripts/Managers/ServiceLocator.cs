using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance { get; private set; }
    public BirdController birdController { get; private set; }
    public InputHandler inputHandler { get; private set; }
    public UIManager uiManager { get; private set; }
    public EnvironmentManager environmentManager { get; private set; }
    public ColorManager colorManager { get; private set; }
    public CameraController cameraController { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        birdController = GetComponentInChildren<BirdController>();
        inputHandler = GetComponentInChildren<InputHandler>();
        uiManager = GetComponentInChildren<UIManager>();
        environmentManager = GetComponentInChildren<EnvironmentManager>();
        colorManager = GetComponentInChildren<ColorManager>();
        cameraController = GetComponentInChildren<CameraController>();
    }


}
