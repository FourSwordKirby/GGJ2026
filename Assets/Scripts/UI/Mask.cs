using MaskGame.Character;
using NUnit.Framework;
using UnityEngine;

public class Mask : MonoBehaviour
{
    private PlayerCharacter player;
    public GameObject[] masks;
    private int localState = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var tmp = GameObject.Find("Player");
        if (tmp == null)
        {
            Debug.Log("Player not found in scene.");
        }
        else
        {

            player = tmp.GetComponent<PlayerCharacter>();
        }
        if (player == null)
        {
            Debug.Log("PlayerCharacter script not attached.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        var maskManager = player.GetComponent<PlayerMaskManager>();
        if (maskManager != null)
        {
            if(localState != (int)maskManager.CurrentMaskState)
            {
                foreach (var mask in masks)
                {
                    mask.SetActive(false);
                }
                masks[(int)maskManager.CurrentMaskState].SetActive(true);
                localState = (int)maskManager.CurrentMaskState;
            }
        }

    }
}
