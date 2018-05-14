using UnityEngine;
using UnityEngine.EventSystems;
using RPG.Characters;

namespace RPG.CameraUI {
    public class CameraRaycaster : MonoBehaviour {
        [SerializeField] Texture2D walkCursor = null;
        [SerializeField] Texture2D targetCursor = null;
        [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);

        const int WALKABLE_LAYER = 8;
        float maxRaycastDepth = 100f; // Hard coded value

        Rect screenRectAtStartPlay = new Rect(0, 0, Screen.width, Screen.height);

        public delegate void OnMouseOverTerrain(Vector3 destination);
        public event OnMouseOverTerrain onMouseOverTerrain;

        public delegate void OnMouseOverEnemy(Enemy enemy);
        public event OnMouseOverEnemy onMouseOverEnemy;

        public void UIButtonClicked() {
            print("Click");
        }

        void Update() {
            if (EventSystem.current.IsPointerOverGameObject()) {
                return;
            } else {
                PerformRaycasts();
            }
        }

        private void PerformRaycasts() {

            if (screenRectAtStartPlay.Contains(Input.mousePosition)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (RaycastForEnemy(ray)) {
                    return;
                }

                if (RaycastForTerrain(ray)) {
                    return;
                }
            }
        }

        bool RaycastForEnemy(Ray ray) {
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, maxRaycastDepth);
            var gameObjectHit = hitInfo.collider.gameObject;
            var enemyHit = gameObjectHit.GetComponent<Enemy>();
            if (enemyHit) {
                Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
                onMouseOverEnemy(enemyHit);
                return true;
            }
            return false;
        }

        bool RaycastForTerrain(Ray ray) {
            RaycastHit hitInfo;
            LayerMask terrainLayerMask = 1 << WALKABLE_LAYER;
            var terrainHit = Physics.Raycast(ray, out hitInfo, maxRaycastDepth, terrainLayerMask);
            if (terrainHit) {
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                onMouseOverTerrain(hitInfo.point);
                return true;
            }
            return false;
        }
    }
}
