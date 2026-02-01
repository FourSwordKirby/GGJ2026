using MaskGame.Character;
using Unity.Cinemachine;
using UnityEngine;

namespace MaskGame
{
    [RequireComponent(typeof(CinemachineCamera))]
    public class PlayerVCam : MonoBehaviour
    {
        public PlayerCharacter Player;
        public CinemachineCamera Camera;
        private void OnValidate()
        {
            Camera = GetComponent<CinemachineCamera>();
            Player = FindAnyObjectByType<PlayerCharacter>();
            Camera.Target.TrackingTarget = Player.transform;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}