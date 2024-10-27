using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace HG
{
    public static class Utils
    {
        public static bool IsInLayerMask(this GameObject obj, LayerMask layerMask)
        {
            return ((layerMask.value & (1 << obj.layer)) > 0);
        }

        public static T GetRandom<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }
        public static T GetRandomInRange<T>(this T[] array, int min, int max)
        {
            min = Mathf.Clamp(min, 0, array.Length);
            max = Mathf.Clamp(max, 0, array.Length);
            return array[Random.Range(min, max)];
        }
        public static T GetRandom<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static float GetRandom(this Vector2 range)
        {
            return Random.Range(range.x, range.y);
        }

        public static Vector3 RandomInsideSphere(float y)
        {
            var random = Random.insideUnitSphere;
            random.y = y;
            return random;
        }

        public static string GetCurrencyText(this string s)
        {
            return s + "<sprite=0>";
        }

        public static int GetInsideRange(this float[] range, float current)
        {
            for (int i = range.Length - 1; i >= 0; i--)
            {
                if (range[i] < current)
                {
                    return i + 1;
                }
            }
            return 0;
        }
        public static string RandomString(int length)
        {
            var random = new System.Random(Random.Range(0, 1000));
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static bool GetRandom(float percentTrue)
        {
            return Random.Range(0, 100) < percentTrue;
        }

        public static Vector3 RandomPoint(this Collider2D collider)
        {
            Vector3 randomPoint;
            Vector3 minBound = collider.bounds.min;
            Vector3 maxBound = collider.bounds.max;

            do
            {
                randomPoint =
                    new Vector3(
                        Random.Range(minBound.x, maxBound.x),
                        Random.Range(minBound.y, maxBound.y),
                        Random.Range(minBound.z, maxBound.z)
                    );
            } while (!collider.OverlapPoint(randomPoint));

            return randomPoint;
        }
        public static int GetRandomPercent(this int[] percents)
        {
            int range = 0;
            for (int i = 0; i < percents.Length; i++)
            {
                range += percents[i];
            }

            int rd = Random.Range(0, range);
            int value = 0;
            for (int i = 0; i < percents.Length; i++)
            {
                value += percents[i];
                if (rd < value)
                    return i;
            }

            return 0;
        }
        public static int GetValue(float[] percents)
        {
            float range = 0;
            for (int i = 0; i < percents.Length; i++)
            {
                range += percents[i];
            }

            float rd = Random.Range(0, range);
            float value = 0;
            for (int i = 0; i < percents.Length; i++)
            {
                value += percents[i];
                if (rd < value)
                    return i;
            }

            return 0;
        }

        public static IEnumerable<Vector3> EvaluateSlerpPoints(Vector3 start, Vector3 end, Vector3 center, int count = 10)
        {
            var startRelativeCenter = start - center;
            var endRelativeCenter = end - center;

            var f = 1f / count;

            for (var i = 0f; i < 1 + f; i += f)
            {
                yield return Vector3.Slerp(startRelativeCenter, endRelativeCenter, i) + center;
            }
        }
        public static Coroutine StartCor(this IEnumerator coroutine, MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.StartCoroutine(coroutine);
        }

        public static void ClearChild(this Transform parent)
        {
            Transform[] child = new Transform[parent.childCount];
            for (int i = 0; i < child.Length; i++)
            {
                child[i] = parent.GetChild(i);
            }

            for (int i = 0; i < child.Length; i++)
            {
                Object.Destroy(child[i].gameObject);
            }
        }
        public static void ClearChildImmediate(this Transform parent)
        {
            Transform[] child = new Transform[parent.childCount];
            for (int i = 0; i < child.Length; i++)
            {
                child[i] = parent.GetChild(i);
            }

            for (int i = 0; i < child.Length; i++)
            {
                Object.DestroyImmediate(child[i].gameObject);
            }
        }

        public static float GetAngle(float x, float y)
        {
            var tan = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
            return tan;
        }


        public static GameObject Instantiate(this GameObject go, Vector3 pos, Quaternion quaternion, float time = 0)
        {
            var obj = Object.Instantiate(go, pos, quaternion);
            if (time > 0)
            {
                Object.Destroy(obj, time);
            }

            return obj;
        }
        public static Vector3 RandomPointInBounds(this Bounds bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
        public static string GetUntilOrEmpty(this string text, string stopAt = "-")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }
        public static string GetUntil(this string text, string stopAt = "-")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return text;
        }
    }
}

