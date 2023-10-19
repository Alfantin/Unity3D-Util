using UnityEngine;
using UnityEngine.EventSystems;

public static class MobileInput {

    //pointer

    public static Vector3 PointerPosition {
        get => Input.mousePosition;
    }

    public static bool IsPointerOverUI {
        get => EventSystem.current.IsPointerOverGameObject() || Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
    }

    public static bool IsPointerDown {
        get => Input.GetMouseButtonDown(0) && !IsPointerOverUI;
    }

    public static bool IsPointerUp {
        get => Input.GetMouseButtonUp(0) && !IsPointerOverUI;
    }

    public static bool IsPointerHold {
        get => Input.GetMouseButton(0) && !IsPointerOverUI;
    }

    //keyboard

    public static bool IsKeyDown(KeyCode key) {
        return Input.GetKeyDown(key) && !IsPointerOverUI;
    }

    public static bool IsKeyUp(KeyCode key) {
        return Input.GetKeyUp(key) && !IsPointerOverUI;
    }

    public static bool IsKeyHold(KeyCode key) {
        return Input.GetKey(key) && !IsPointerOverUI;
    }

    public static bool IsAnyKeyDown() {
        return Input.anyKeyDown && !IsPointerOverUI;
    }

    public static bool IsAnyKeyHold() {
        return Input.anyKey && !IsPointerOverUI;
    }

    //viewport touch

    public static bool IsPointerHoldScreenLeft() {
        return IsKeyHold(KeyCode.LeftArrow) || (IsPointerHold && PointerPosition.x < Screen.width / 2f);
    }

    public static bool IsPointerHoldScreenRight() {
        return IsKeyHold(KeyCode.RightArrow) || (IsPointerHold && PointerPosition.x >= Screen.width / 2f);
    }

    public static bool IsPointerHoldScreenDown() {
        return IsKeyHold(KeyCode.DownArrow) || (IsPointerHold && PointerPosition.y < Screen.height / 2f);
    }

    public static bool IsPointerHoldScreenUp() {
        return IsKeyHold(KeyCode.UpArrow) || (IsPointerHold && PointerPosition.y >= Screen.height / 2f);
    }

    public static bool IsPointerDownScreenLeft() {
        return IsKeyDown(KeyCode.LeftArrow) || (IsPointerDown && PointerPosition.x < Screen.width / 2f);
    }

    public static bool IsPointerDownScreenRight() {
        return IsKeyDown(KeyCode.RightArrow) || (IsPointerDown && PointerPosition.x >= Screen.width / 2f);
    }

    public static bool IsPointerDownScreenDown() {
        return IsKeyDown(KeyCode.DownArrow) || (IsPointerDown && PointerPosition.y < Screen.height / 2f);
    }

    public static bool IsPointerDownScreenUp() {
        return IsKeyDown(KeyCode.UpArrow) || (IsPointerDown && PointerPosition.y >= Screen.height / 2f);
    }

    //swipe

    private static bool swipeStart = false;
    private static float minSwipe = 20f;
    private static Vector3 swipePos;

    private static void update() {
        if (!IsPointerOverUI) {
            if (!swipeStart && IsPointerDown) {
                swipeStart = true;
                swipePos = PointerPosition;
            }
            if (swipeStart && IsPointerUp) {
                swipeStart = false;
            }
        }
    }

    public static bool IsLeft() {
        if (!IsPointerOverUI) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                return true;
            }
            if (swipeStart) {
                var distance = PointerPosition.x - swipePos.x;
                if (distance < -minSwipe && Mathf.Abs(distance) > Mathf.Abs(PointerPosition.y - swipePos.y)) {
                    swipeStart = false;
                    return true;
                }
            }
        }
        return false;
    }

    public static bool IsRight() {
        if (!IsPointerOverUI) {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                return true;
            }
            if (swipeStart) {
                var distance = PointerPosition.x - swipePos.x;
                if (distance > +minSwipe && Mathf.Abs(distance) > Mathf.Abs(PointerPosition.y - swipePos.y)) {
                    swipeStart = false;
                    return true;
                }
            }
        }
        return false;
    }

    public static bool IsDown() {
        if (!IsPointerOverUI) {
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                return true;
            }
            if (swipeStart) {
                var distance = PointerPosition.y - swipePos.y;
                if (distance < -minSwipe && Mathf.Abs(distance) > Mathf.Abs(PointerPosition.x - swipePos.x)) {
                    swipeStart = false;
                    return true;
                }
            }
        }
        return false;
    }

    public static bool IsUp() {
        if (!IsPointerOverUI) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                return true;
            }
            if (swipeStart) {
                var distance = PointerPosition.y - swipePos.y;
                if (distance > +minSwipe && Mathf.Abs(distance) > Mathf.Abs(PointerPosition.x - swipePos.x)) {
                    swipeStart = false;
                    return true;
                }
            }
        }
        return false;
    }

}