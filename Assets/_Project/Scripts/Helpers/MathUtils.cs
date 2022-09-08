using UnityEngine;

namespace ArcadeIdle.Helpers
{
    public static class MathUtils
    {
        #region Mapping
        // Mathf.Lerp(targetFrom, targetTo, Mathf.InverseLerp(sourceFrom, sourceTo, sourceValue)).
        public static float MapClamped(float sourceValue, float sourceFrom, float sourceTo, float targetFrom, float targetTo)
        {
            var sourceRange = sourceTo - sourceFrom;
            var targetRange = targetTo - targetFrom;
            var percent = Mathf.Clamp01((sourceValue - sourceFrom) / sourceRange);
            return targetFrom + targetRange * percent;
        }
        // Applies a deadzone [-deadzone..deadzone] in which the value will be set to 0.       
        public static float ApplyJoystickDeadzone(float value, float deadzone, bool fullRangeBetweenDeadzoneAndOne = false)
        {
            if (Mathf.Abs(value) <= deadzone)
                return 0;

            if (fullRangeBetweenDeadzoneAndOne && (deadzone > 0f))
            {
                if (value < 0)
                {
                    return MapClamped(value, -1f, -deadzone, -1f, 0f);
                }
                else
                {
                    return MapClamped(value, deadzone, 1f, 0f, 1f);
                }
            }

            return value;
        }
        // Maps a joystick input from [sourceFrom..sourceTo] to [-1..1] with clamping.
        public static float MapClampedJoystick(float sourceValue, float sourceFrom, float sourceTo, float deadzone = 0f, bool fullRangeBetweenDeadzoneAndOne = false)
        {
            var percent = MapClamped(sourceValue, sourceFrom, sourceTo, -1, 1);

            if (deadzone > 0)
                percent = ApplyJoystickDeadzone(percent, deadzone, fullRangeBetweenDeadzoneAndOne);

            return percent;
        }
        #endregion

        #region Angles
        // Returns the closer center between two angles.
        public static float GetCenterAngleDeg(float angle1, float angle2)
        {
            return angle1 + Mathf.DeltaAngle(angle1, angle2) / 2f;
        }
        // Normalizes an angle between 0 (inclusive) and 360 (exclusive).
        public static float NormalizeAngleDeg360(float angle)
        {
            while (angle < 0)
                angle += 360;

            if (angle >= 360)
                angle %= 360;

            return angle;
        }
        // Normalizes an angle between -180 (inclusive) and 180 (exclusive).
        public static float NormalizeAngleDeg180(float angle)
        {
            while (angle < -180)
                angle += 360;

            while (angle >= 180)
                angle -= 360;

            return angle;
        }
        #endregion

        #region Framerate-Independent Lerping
        public static float EasedLerpFactor(float factor, float deltaTime = 0f)
        {
            if (deltaTime == 0f)
                deltaTime = Time.deltaTime;

            return 1 - Mathf.Pow(1 - factor, deltaTime);
        }
        
        public static float EasedLerp(float current, float target, float percentPerSecond, float deltaTime = 0f)
        {
            var t = EasedLerpFactor(percentPerSecond, deltaTime);
            return Mathf.Lerp(current, target, t);
        }
        #endregion
    }
}