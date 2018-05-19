//Author: James Murphy
//Purpose: Show control mappings depending if a controller is selected
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayControlMappings : MonoBehaviour
{
    private Image controlMappingImage;
    [SerializeField]
    private Sprite controllerSprite, keyboardSprite;

    private void Update() //Get components and decide what image to use
    {
        controlMappingImage = GetComponent<Image>();

        //Detect if controller is connected and show the relevant controls
        if (Input.GetJoystickNames().Length != 0)
        {
            controlMappingImage.sprite = controllerSprite;
        }
        else
        {
            controlMappingImage.sprite = keyboardSprite;
        }
    }

}
