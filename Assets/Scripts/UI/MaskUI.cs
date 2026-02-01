using MaskGame.Character;
using NUnit.Framework;
using UnityEngine;

public class MaskUI : MonoBehaviour
{
    public GameObject[] masks;
    private int localState = 0;


    // Update is called once per frame
    void Update()
    {
        var maskManager = GameManager.instance.player.GetComponent<PlayerMaskManager>();
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
