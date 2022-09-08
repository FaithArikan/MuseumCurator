using System.Collections.Generic;
using UnityEngine;

namespace ArcadeIdle.Helpers
{
    public static class Utils
    {
        public static T FindComponentInChildWithTag<T> (GameObject parent, string tag) where T : Component {
            Transform t = parent.transform;
            foreach (Transform tr in t) {
                if (tr.CompareTag (tag)) {
                    return tr.GetComponent<T> ();
                }
            }
            return null;
        }

        public static List<T> FindComponentsInChildWithTag<T> (GameObject parent, string tag) where T : Component {
            List<T> children = new List<T> ();

            Transform t = parent.transform;
            foreach (Transform tr in t) {
                if (tr.CompareTag (tag)) {
                    children.Add (tr.GetComponent<T> ());
                }
            }

            return children;
        }

        public static void DestroyChildren(this Transform t)
        {
            foreach (Transform child in t)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public static void SetLayersRecursively(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            foreach (Transform t in gameObject.transform)
            {
                t.gameObject.SetLayersRecursively(layer);
            }
        }

        public static Vector2 ToVector2(this Vector3 input) => new Vector2(input.x, input.y);
        
        public static Vector3 Flat(this Vector3 input) => new Vector3(input.x, 0, input.y);
        
        public static Vector3Int ToVector3Int(this Vector3 vec3) => new Vector3Int((int) vec3.x, (int) vec3.y, (int) vec3.z);
        
        public static T Rand<T>(this IList<T> list)
        {
            return  list[Random.Range(0, list.Count)];
        }
        
        public static List<T> ShuffleList<T>(List<T> list)
        {
            var count = list.Count;
            for (var i = 0; i < count; i++)
            {
                var index = Random.Range(i, count);
                (list[i], list[index]) = (list[index], list[i]);
            }
            return list;
        }
        
        public static string FormatCash(double Value)
        {
            if (Value >= 1000000000)
                return (Value / 1000000000D).ToString("0.##B");
            if (Value >= 100000000)
                return (Value / 1000000D).ToString("0.#M");
            if (Value >= 1000000)
                return (Value / 1000000D).ToString("0.##M");
            if (Value >= 100000)
                return (Value / 1000D).ToString("0.#K");
            if (Value >= 1000)
                return (Value / 1000D).ToString("0.##K");
            return Value.ToString("#,0");
        }

        #region Vectors

        public static Vector2 Change2(this Vector2 vector, float? x = null, float? y = null)
        {
            if (x.HasValue) vector.x = x.Value;
            if (y.HasValue) vector.y = y.Value;
            return vector;
        }

        public static Vector3 Change3(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            if (x.HasValue) vector.x = x.Value;
            if (y.HasValue) vector.y = y.Value;
            if (z.HasValue) vector.z = z.Value;
            return vector;
        }
        
        public static Vector4 Change4(this Vector4 vector, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            if (x.HasValue) vector.x = x.Value;
            if (y.HasValue) vector.y = y.Value;
            if (z.HasValue) vector.z = z.Value;
            if (w.HasValue) vector.w = w.Value;
            return vector;
        }
        
        public static Vector2 RotateRad(this Vector2 v, float angleRad)
        {
            var sin = Mathf.Sin(angleRad);
            var cos = Mathf.Cos(angleRad);

            var tx = v.x;
            var ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);

            return v;
        }
        // Rotates a Vector2.
        public static Vector2 RotateDeg(this Vector2 v, float angleDeg)
        {
            return v.RotateRad(angleDeg * Mathf.Deg2Rad);
        }
        // Creates a Vector2 with a length of 1 pointing towards a certain angle.
        public static Vector2 CreateVector2AngleRad(float angleRad)
        {
            return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }
        // Creates a Vector2 with a length of 1 pointing towards a certain angle.
        public static Vector2 CreateVector2AngleDeg(float angleDeg)
        {
            return CreateVector2AngleRad(angleDeg * Mathf.Deg2Rad);
        }
        // Gets the rotation of a Vector2.
        public static float GetAngleRad(this Vector2 vector)
        {
            return Mathf.Atan2(vector.y, vector.x);
        }
        // Gets the rotation of a Vector2.
        public static float GetAngleDeg(this Vector2 vector)
        {
            return vector.GetAngleRad() * Mathf.Rad2Deg;
        }
        // Framerate-independent eased lerping to a target value, slowing down the closer it is.
        public static Vector2 EasedLerpVector2(Vector2 current, Vector2 target, float percentPerSecond, float deltaTime = 0f)
        {
            var t = MathUtils.EasedLerpFactor(percentPerSecond, deltaTime);
            return Vector2.Lerp(current, target, t);
        }
        // Framerate-independent eased lerping to a target value, slowing down the closer it is.
        public static Vector3 EasedLerpVector3(Vector3 current, Vector3 target, float percentPerSecond, float deltaTime = 0f)
        {
            var t = MathUtils.EasedLerpFactor(percentPerSecond, deltaTime);
            return Vector3.Lerp(current, target, t);
        }
        // Framerate-independent eased lerping to a target value, slowing down the closer it is.
        public static Vector4 EasedLerpVector4(Vector4 current, Vector4 target, float percentPerSecond, float deltaTime = 0f)
        {
            var t = MathUtils.EasedLerpFactor(percentPerSecond, deltaTime);
            return Vector4.Lerp(current, target, t);
        }
        #endregion

        #region Transform
        public static void CopyPositionAndRotatationFrom(this Transform transform, Transform source)
        {
            transform.position = source.position;
            transform.rotation = source.rotation;
        }

        public static Transform SetPosition(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            transform.position = transform.position.Change3(x, y, z);
            return transform;
        }
        
        public static Transform SetLocalPosition(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            transform.localPosition = transform.localPosition.Change3(x, y, z);
            return transform;
        }
        
        public static Transform SetLocalScale(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            transform.localScale = transform.localScale.Change3(x, y, z);
            return transform;
        }
        
        public static Transform SetLossyScale(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            var lossyScale = transform.lossyScale.Change3(x, y, z);

            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(lossyScale.x / transform.lossyScale.x,
                lossyScale.y / transform.lossyScale.y,
                lossyScale.z / transform.lossyScale.z);

            return transform;
        }
        
        public static Transform SetEulerAngles(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            transform.eulerAngles = transform.eulerAngles.Change3(x, y, z);
            return transform;
        }
        
        public static Transform SetLocalEulerAngles(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            transform.localEulerAngles = transform.localEulerAngles.Change3(x, y, z);
            return transform;
        }
        #endregion

        #region Color
        public static Color Change(this Color color, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            if (r.HasValue) color.r = r.Value;
            if (g.HasValue) color.g = g.Value;
            if (b.HasValue) color.b = b.Value;
            if (a.HasValue) color.a = a.Value;
            return color;
        }
        
        public static void Fade(this SpriteRenderer renderer, float alpha)
        {
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }

        public static Color ChangeAlpha(this Color color, float a)
        {
            color.a = a;
            return color;
        }
        
        public static Color EasedLerpColor(Color current, Color target, float percentPerSecond, float deltaTime = 0f)
        {
            var t = MathUtils.EasedLerpFactor(percentPerSecond, deltaTime);
            return Color.Lerp(current, target, t);
        }
        // Calculates the average position of an array of Vector2.
        public static Vector2 CalculateCentroid(this Vector2[] array)
        {
            var sum = new Vector2();
            var count = array.Length;
            for (var i = 0; i < count; i++)
            {
                sum += array[i];
            }
            return sum / count;
        }
        // Calculates the average position of an array of Vector3.
        public static Vector3 CalculateCentroid(this Vector3[] array)
        {
            var sum = new Vector3();
            var count = array.Length;
            for (var i = 0; i < count; i++)
            {
                sum += array[i];
            }
            return sum / count;
        }
        // Calculates the average position of an array of Vector4.
        public static Vector4 CalculateCentroid(this Vector4[] array)
        {
            var sum = new Vector4();
            var count = array.Length;
            for (var i = 0; i < count; i++)
            {
                sum += array[i];
            }
            return sum / count;
        }
        // Calculates the average position of a List of Vector2.
        public static Vector2 CalculateCentroid(this List<Vector2> list)
        {
            var sum = new Vector2();
            var count = list.Count;
            for (var i = 0; i < count; i++)
            {
                sum += list[i];
            }
            return sum / count;
        }
        // Calculates the average position of a List of Vector3.
        public static Vector3 CalculateCentroid(this List<Vector3> list)
        {
            var sum = new Vector3();
            var count = list.Count;
            for (var i = 0; i < count; i++)
            {
                sum += list[i];
            }
            return sum / count;
        }
        // Calculates the average position of a List of Vector4.
        public static Vector4 CalculateCentroid(this List<Vector4> list)
        {
            var sum = new Vector4();
            var count = list.Count;
            for (var i = 0; i < count; i++)
            {
                sum += list[i];
            }
            return sum / count;
        }

        #endregion

        #region Rect
        // Extends/shrinks the rect by extendDistance to each side and gets a random position from the resulting rect.
        public static Vector2 RandomPosition(this Rect rect, float extendDistance = 0f)
        {
            return new Vector2(Random.Range(rect.xMin - extendDistance, rect.xMax + extendDistance),
                               Random.Range(rect.yMin - extendDistance, rect.yMax + extendDistance));
        }
        // Gets a random subrect of the given width or height inside this rect.
        public static Rect RandomSubRect(this Rect rect, float width, float height)
        {
            width = Mathf.Min(rect.width, width);
            height = Mathf.Min(rect.height, height);

            var halfWidth = width / 2f;
            var halfHeight = height / 2f;

            var centerX = Random.Range(rect.xMin + halfWidth, rect.xMax - halfWidth);
            var centerY = Random.Range(rect.yMin + halfHeight, rect.yMax - halfHeight);

            return new Rect(centerX - halfWidth, centerY - halfHeight, width, height);
        }
        // Extends/shrinks the rect by extendDistance to each side and then restricts the given vector to the resulting rect.
        public static Vector2 Clamp2(this Rect rect, Vector2 position, float extendDistance = 0f)
        {
            return new Vector2(Mathf.Clamp(position.x, rect.xMin - extendDistance, rect.xMax + extendDistance),
                               Mathf.Clamp(position.y, rect.yMin - extendDistance, rect.yMax + extendDistance));
        }
        // Extends/shrinks the rect by extendDistance to each side and then restricts the given vector to the resulting rect.
        public static Vector3 Clamp3(this Rect rect, Vector3 position, float extendDistance = 0f)
        {
            return new Vector3(Mathf.Clamp(position.x, rect.xMin - extendDistance, rect.xMax + extendDistance),
                               Mathf.Clamp(position.y, rect.yMin - extendDistance, rect.yMax + extendDistance),
                               position.z);
        }
        // Extends/shrinks the rect by extendDistance to each side.
        public static Rect Extend(this Rect rect, float extendDistance)
        {
            var copy = rect;
            copy.xMin -= extendDistance;
            copy.xMax += extendDistance;
            copy.yMin -= extendDistance;
            copy.yMax += extendDistance;
            return copy;
        }
        // Extends/shrinks the rect by extendDistance to each side and then checks if a given point is inside the resulting rect.
        public static bool Contains(this Rect rect, Vector2 position, float extendDistance)
        {
            return (position.x > rect.xMin + extendDistance) &&
                   (position.y > rect.yMin + extendDistance) &&
                   (position.x < rect.xMax - extendDistance) &&
                   (position.y < rect.yMax - extendDistance);
        }
        // Creates an array containing the four corner points of a Rect.
        public static Vector2[] GetCornerPoints(this Rect rect)
        {
            return new[]
                       {
                           new Vector2(rect.xMin, rect.yMin),
                           new Vector2(rect.xMax, rect.yMin),
                           new Vector2(rect.xMax, rect.yMax),
                           new Vector2(rect.xMin, rect.yMax)
                       };
        }
        #endregion

        #region PlayerPrefs
        // Returns the value corresponding to the key in the preference file if it exists.
        public static bool PlayerPrefsGetBool(string key, bool defaultValue = false)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }
        // Sets the value of the preference entry identified by the key.
        public static void PlayerPrefsSetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        #endregion

        #region LayerMask
        // Is a specific layer actived in the given LayerMask?
        public static bool ContainsLayer(this LayerMask mask, int layer)
        {
            return (mask.value & (1 << layer)) != 0;
        }

        #endregion

        #region Physics
        // Creates a Bounds encapsulating all given colliders bounds.
        public static Bounds CombineColliderBounds(Collider[] colliders)
        {
            var bounds = colliders[0].bounds;

            foreach (var colliderComponent in colliders)
            {
                bounds.Encapsulate(colliderComponent.bounds);
            }

            return bounds;
        }

        public static void GetCapsuleCastData(CharacterController characterController, Vector3 origin, out Vector3 point1, out Vector3 point2, out float radius)
        {
            var scale = characterController.transform.lossyScale;
            radius = characterController.radius * scale.x;
            var height = characterController.height * scale.y - (radius * 2);
            var center = characterController.center;
            center.Scale(scale);
            point1 = origin + center + Vector3.down * (height / 2f);
            point2 = point1 + Vector3.up * height;
        }

        #endregion

        #region Camera
        // Calculates the size of the viewport at a given distance from a perspective camera.
        public static Vector2 CalculateViewportWorldSizeAtDistance(this Camera camera, float distance, float aspectRatio = 0)
        {
            if (aspectRatio == 0)
            {
                aspectRatio = camera.aspect;
            }

            var viewportHeightAtDistance = 2.0f * Mathf.Tan(0.5f * camera.fieldOfView * Mathf.Deg2Rad) * distance;
            var viewportWidthAtDistance = viewportHeightAtDistance * aspectRatio;

            return new Vector2(viewportWidthAtDistance, viewportHeightAtDistance);
        }

        #endregion

        #region Random
        // Gets a random Vector2 of length 1 pointing in a random direction.
        public static Vector2 RandomOnUnitCircle
        {
            get
            {
                var angle = Random.Range(0f, Mathf.PI * 2);
                return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            }
        }
        // Returns -1 or 1 with equal change.
        public static int RandomSign
        {
            get { return (Random.value < 0.5f) ? -1 : 1; }
        }
        // Returns true or false with equal chance.
        public static bool RandomBool
        {
            get { return Random.value < 0.5f; }
        }
        #endregion    

    }
}