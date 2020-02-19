using UnityEngine;

public abstract class MouseUtils {
    /// Returns the vector's direction in radians
    public static float VectorToAngle(Vector2 dir) {
        return Mathf.Atan2(dir.y, dir.x);
    }

    public static Vector2 GetMousePosition() {
        Vector3 tmpPos = Input.mousePosition;
        tmpPos.z = 0 - Camera.main.transform.position.z; 
        return Camera.main.ScreenToWorldPoint(tmpPos);
    }

    public static float GetMouseAngle() {
        Vector2 pos = GetMousePosition();
        return Mathf.Atan(pos.y / pos.x);
    }

    public static Vector2 GetAimDirection(Vector2 pivot) {
        return (pivot - GetMousePosition()).normalized;
    }

    /// Returns the aiming direction angle in radians
    public static float GetAimAngle(Vector2 pivot) {
        Vector2 dir = (pivot - GetMousePosition()).normalized;
        return VectorToAngle(dir);
    }
}