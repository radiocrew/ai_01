using System;
using UnityEngine;

namespace RM.Common
{
    public class RMMath
    {
        public const float M_PI_2 = (float)Math.PI * 2.0f;      // 360 도
        public const float M_PI_Q = (float)Math.PI / 2.0f;      // 90 도
        //public const float M_PI_H = (float)Math.PI * 0.2f;      // 180 도

        //public const float BA_DIRECTION = 90.0f;

        static public float Lerp(float start, float finish, float rate)
        {
            return (start * (1f - rate) + finish * rate);
        }

        static public float ToRadian(float degree)
        {
            // PI / 180
            return degree * 0.01745329252f;
        }

        static public float ToDegree(float radian)
        {
            // PI * 180
            return radian * 57.29577951f;
        }

        static public byte ToByte(float alpha)
        {
            return (byte)(255.0f * alpha);
        }


        static public Vector2 Lerp(Vector2 start, Vector2 finish, float rate)
        {
            return (start * (1f - rate) + finish * rate);
        }



        // 기준 각도로 반전
        static public float DegreeFlip(float datumAngle, float angle)
        {
            float flipAngle = 0.0f;

            if (datumAngle > angle)
            {
                float gap = datumAngle - angle;
                flipAngle = datumAngle + gap;
            }
            else
            {
                float gap = angle - datumAngle;
                flipAngle = datumAngle - gap;
            }

            if (360.0f < flipAngle)
            {
                flipAngle = flipAngle - 360.0f;
            }
            if (0.0f > flipAngle)
            {
                flipAngle = flipAngle + 360.0f;
            }

            return flipAngle;
        }

        static public float RadianFlip(float datumAngle, float angle)
        {
            float flipAngle = 0.0f;

            if (datumAngle > angle)
            {
                float gap = datumAngle - angle;
                flipAngle = datumAngle + gap;
            }
            else
            {
                float gap = angle - datumAngle;
                flipAngle = datumAngle - gap;
            }

            if (M_PI_2 < flipAngle)
            {
                flipAngle = flipAngle - M_PI_2;
            }
            if (0.0f > flipAngle)
            {
                flipAngle = flipAngle + M_PI_2;
            }

            return flipAngle;
        }



        static public float VecToDeg(Vector2 vec2)
        {
            //Mathf.PI
            return (float)(Math.Atan2(vec2.y, -vec2.x) - M_PI_Q) * Mathf.Rad2Deg;
        }

        static public float Vec3ToYDeg(Vector3 vec3)
        {
            //Mathf.PI
            return (float)(Math.Atan2(vec3.z, -vec3.x) - M_PI_Q) * Mathf.Rad2Deg;
        }

        static public float VecLength(float x1, float y1, float x2, float y2)
        {
            float x = x1 - x2;
            float y = y1 - y2;

            return (float)Math.Sqrt(x*x + y*y);
        }
        
        //static public Vector3 AngleToVector3(float degree)
        //{           
        //    float radian = ToRadian(degree) - M_PI_Q; // vector3(0,0,1) forward 기준으로

        //    Vector3 vec;
        //    vec.x = (float)Math.Cos(radian);
        //    vec.y = 0.0f;
        //    vec.z = -(float)Math.Sin(radian);
        //    return vec;
        //}

        static public Vector2 AngleToVector2(float degree)
        {
            float radian = ToRadian(degree) - M_PI_Q; // vector3(0,0,1) forward 기준으로

            Vector2 vec;
            vec.x = (float)Math.Cos(radian);
            vec.y = -(float)Math.Sin(radian);
            return vec;
        }

        static public float VecToDeg(float x, float y)
        {
            return (float)(Math.Atan2(y, -x) - M_PI_Q) * Mathf.Rad2Deg;
        }


        //public static float b2AngleToCocos(float dadian)
        //{
        //    return 
        //}

        //static public bool ContainsPoint(CCRect rt, CCPoint pt)ㅠ
        //{
        //    return rt.ContainsPoint(pt);
        //}

        static public bool IntersectCircleCircle(Vector2 a, float aRadius, Vector2 b, float bRadius)
        {
            return (Math.Pow(a.x - b.x, 2.0f) + Math.Pow(a.y - b.y, 2.0f)) < Math.Pow(aRadius + bRadius, 2.0f);
        }



        // 작은 쪽 각을 구함
        static public float NearAngleVectors(Vector2 vec1, Vector2 vec2)
        {
            vec1.Normalize();
            vec2.Normalize();

            float dot = Vector2.Dot(vec1, vec2);
            if (1.0f < dot)
            {
                dot = 1.0f;
            }
            if (-1.0f > dot)
            {
                dot = -1.0f;
            }
            return (float)Math.Acos((double)dot);
        }

        static public int Clamp(int num, int lower, int upper)
        {
            return Math.Max(lower, Math.Min(num, upper));
        }

        static public float Clamp(float num, float lower, float upper)
        {
            return Math.Max(lower, Math.Min(num, upper));
        }

        static public float BetweenRate(float min, float max, float value)
        {
            if (max < value)
            {
                return 1.0f;
            }
            if (min > value)
            {
                return 0.0f;
            }
            if (min > max)
            {
                System.Diagnostics.Debug.Assert(false);
                return 0.0f;
            }

            float gap = max - min;
            float value2 = value - min;
            return value2 / gap;
        }

        static public bool TestBit(uint test, ushort bit)
        {
            return bit == (test & bit);
        }


        static public int Random(int min, int max)
        {           
            return m_Random.Next(min, max);
        }

        static public int Random(int max)
        {
            return m_Random.Next(max);
        }

        static System.Random m_Random = new System.Random((int)DateTime.Now.Ticks);



        //static public Vector3 AngleToVector3(float degree)
        //{
        //    Vector3 direction = Vector3.forward;/* 기준이 되는 direction */;
        //    // 정면을 기준으로 한다면 transform.forward; 를 입렵하면 된다.

        //    var quaternion = Quaternion.Euler(0, degree, 0);
        //    Vector3 newDirection = quaternion * direction;

        //    return newDirection;
        //}

    }
}


