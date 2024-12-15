using UnityEngine;

public static class EPFunctionLibrary {

    // ===== GAMEPLAY =====
    public static void ClearChilds(this Transform transform) {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }

    // ===== UI ======
    public static void Toggle(this CanvasGroup canvasGroup, bool enable) {
        canvasGroup.alpha = enable ? 1f : 0f;
        canvasGroup.blocksRaycasts = enable;
        canvasGroup.interactable = enable;
    }
}