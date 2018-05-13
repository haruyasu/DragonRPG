using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
using RPG.CameraUI;

namespace RPG.Characters {
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AICharacterControl))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class PlayerMovement : MonoBehaviour {
        ThirdPersonCharacter thirdPersonCharacter = null;   // A reference to the ThirdPersonCharacter on the object
        AICharacterControl aiCharacterControl = null;
        GameObject walkTarget = null;

        void Start() {
            CameraRaycaster cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
            thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
            aiCharacterControl = GetComponent<AICharacterControl>();
            walkTarget = new GameObject("walkTarget");

            cameraRaycaster.onMouseOverTerrain += OnMouseOverTerrain;
            cameraRaycaster.onMouseOverEnemy += OnClickEnemy;
        }

        void OnMouseOverTerrain(Vector3 destination) {
            if (Input.GetMouseButton(0)) {
                walkTarget.transform.position = destination;
                aiCharacterControl.SetTarget(walkTarget.transform);
            }
        }

        void OnClickEnemy(Enemy enemy) {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
                aiCharacterControl.SetTarget(enemy.transform);
            }
        }

        void ProcessDirectMovement() {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // calculate camera relative direction to move:
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

            thirdPersonCharacter.Move(movement, false, false);
        }
    }

}

