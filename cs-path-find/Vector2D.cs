﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinding
{
    public interface IFVec2
    {
        float Length
        {
            get;
        }

        float SquaredLength
        {
            get;
        }

        float Radian
        {
            get;
        }

        float dotProduct(IFVec2 rhs);

        float dotProduct(IFVec2 lhs, IFVec2 rhs);

        float CrossLength(IFVec2 v);
        float z
        {
            set;
            get;
        }

        float x
        {
            get;
            set;
        }

        IFVec2 Clone();

        float GetDistanceTo(IFVec2 pt);
        float GetDistanceSqTo(IFVec2 pt);

        float Normalize();
        IFVec2 Rotate(float radian);

        IFVec2 Assign(IFVec2 rhs);
    }

    public class FVec2
    {
        //protected FVec3 mPoint = new FVec3();
        float mX;
        float mZ;
        public FVec2()
        {
            mX = 0;
            mZ = 0;
        }
        public FVec2(float _x, float _z)
        {
            mX = _x;
            mZ = _z;
        }

        public FVec2(FVec3 vec)
        {
            mX = vec.x;
            mZ = vec.z;
        }

        public float Length
        {
            get
            {
                return (float)Math.Sqrt(mX * mX + mZ * mZ);
            }
        }

        public float SquaredLength
        {
            get
            {
                return mX * mX + mZ * mZ;
            }
        }

        public float Radian
        {
            get
            {
                //return Geometry.PolarAngle(this);
                return (float)System.Math.Atan2(mZ, mX);
            }
        }

        public float dotProduct(FVec2 rhs)
        {
            return mX * rhs.x + mZ * rhs.z;
        }

        public static float dotProduct(FVec2 lhs, FVec2 rhs)
        {
            return lhs.x * rhs.x + lhs.z * rhs.z;
        }

        public float CrossLength(FVec2 v)
        {
            return Math.Abs(x * v.z - z * v.x);
        }

        public static FVec2 operator -(FVec2 lhs)
        {
            FVec2 result = lhs.Clone();
            result.x *= -1;
            result.z *= -1;
            return result;
        }

        public float z
        {
            set
            {
                mZ = value;
            }
            get
            {
                return mZ;
            }
        }

        public float x
        {
            get
            {
                return mX;
            }
            set
            {
                mX = value;
            }
        }

        internal FVec3 ToFVec3()
        {
            return new FVec3(x, 0, z);
        }

        public virtual FVec2 Clone()
        {
            return new FVec2(mX, mZ);
        }

        public float GetDistanceTo(FVec2 pt)
        {
            float dx = pt.x - mX;
            float dz = pt.z - mZ;
            return (float)System.Math.Sqrt(dx * dx + dz * dz);
        }

        public float GetDistanceSqTo(FVec2 pt)
        {
            float dx = pt.x - mX;
            float dz = pt.z - mZ;
            return dx * dx + dz * dz;
        }

        public static FVec2 operator *(FVec2 lhs, float scale)
        {
            FVec2 result = lhs.Clone();
            result.x *= scale;
            result.z *= scale;
            return result;
        }

        public static FVec2 operator *(float scale, FVec2 rhs)
        {
            FVec2 result = rhs.Clone();
            result.x *= scale;
            result.z *= scale;
            return result;
        }

        public static FVec2 operator -(FVec2 lhs, FVec2 rhs)
        {
            FVec2 result = lhs.Clone();
            result.x -= rhs.x;
            result.z -= rhs.z;
            return result;
        }

        public static FVec2 operator +(FVec2 lhs, FVec2 rhs)
        {
            FVec2 result = lhs.Clone();
            result.x += rhs.x;
            result.z += rhs.z;
            return result;
        }

        public float Normalize()
        {
            float length = Length;
            if (length == 0)
            {
                return 0;
            }
            mX /= length;
            mZ /= length;
            return length;
        }

        public FVec2 Rotate(float radian)
        {
            double cosine = System.Math.Cos(radian);
            double sine = System.Math.Sin(radian);
            FVec2 result = new FVec2();
            result.x = (float)(x * cosine - z * sine);
            result.z = (float)(x * sine + z * cosine);

            return result;
        }

        public FVec2 Assign(FVec2 rhs)
        {
            if (this == rhs) return this;
            x = rhs.x;
            z = rhs.z;
            return this;
        }

        public static int SidenessTest(FVec2 A, FVec2 B, FVec2 C)
        {
            float result = (B.x - A.x) * (C.z - A.z) - (B.z - A.z) * (C.x - A.x);
            if (result == 0) return 0;
            else if (result > 0) return 1;
            else return -1;
        }

        public override String ToString()
        {
            return String.Format("({0},{1})", x, z);
        }
    }

    public class IntVec2
    {
        //protected IntVec3 mPoint = new IntVec3();
        int mX;
        int mZ;
        public IntVec2()
        {
            mX = 0;
            mZ = 0;
        }
        public IntVec2(int _x, int _z)
        {
            mX = _x;
            mZ = _z;
        }


        public int Length
        {
            get
            {
                return (int)Math.Sqrt(mX * mX + mZ * mZ);
            }
        }

        public int SquaredLength
        {
            get
            {
                return mX * mX + mZ * mZ;
            }
        }

        public int Radian
        {
            get
            {
                //return Geometry.PolarAngle(this);
                return (int)System.Math.Atan2(mZ, mX);
            }
        }

        public int dotProduct(IntVec2 rhs)
        {
            return mX * rhs.x + mZ * rhs.z;
        }

        public static int dotProduct(IntVec2 lhs, IntVec2 rhs)
        {
            return lhs.x * rhs.x + lhs.z * rhs.z;
        }

        public int CrossLength(IntVec2 v)
        {
            return Math.Abs(x * v.z - z * v.x);
        }

        public static IntVec2 operator -(IntVec2 lhs)
        {
            IntVec2 result = lhs.Clone();
            result.x *= -1;
            result.z *= -1;
            return result;
        }

        public int z
        {
            set
            {
                mZ = value;
            }
            get
            {
                return mZ;
            }
        }

        public int x
        {
            get
            {
                return mX;
            }
            set
            {
                mX = value;
            }
        }

        public virtual IntVec2 Clone()
        {
            return new IntVec2(mX, mZ);
        }

        public int GetDistanceTo(IntVec2 pt)
        {
            int dx = pt.x - mX;
            int dz = pt.z - mZ;
            return (int)System.Math.Sqrt(dx * dx + dz * dz);
        }

        public int GetDistanceSqTo(IntVec2 pt)
        {
            int dx = pt.x - mX;
            int dz = pt.z - mZ;
            return dx * dx + dz * dz;
        }

        public static IntVec2 operator *(IntVec2 lhs, int scale)
        {
            IntVec2 result = lhs.Clone();
            result.x *= scale;
            result.z *= scale;
            return result;
        }

        public static IntVec2 operator *(int scale, IntVec2 rhs)
        {
            IntVec2 result = rhs.Clone();
            result.x *= scale;
            result.z *= scale;
            return result;
        }

        public static IntVec2 operator -(IntVec2 lhs, IntVec2 rhs)
        {
            IntVec2 result = lhs.Clone();
            result.x -= rhs.x;
            result.z -= rhs.z;
            return result;
        }

        public static IntVec2 operator +(IntVec2 lhs, IntVec2 rhs)
        {
            IntVec2 result = lhs.Clone();
            result.x += rhs.x;
            result.z += rhs.z;
            return result;
        }

        public int Normalize()
        {
            int length = Length;
            if (length == 0)
            {
                return 0;
            }
            mX /= length;
            mZ /= length;
            return length;
        }

        public IntVec2 Rotate(int radian)
        {
            double cosine = System.Math.Cos(radian);
            double sine = System.Math.Sin(radian);
            IntVec2 result = new IntVec2();
            result.x = (int)(x * cosine - z * sine);
            result.z = (int)(x * sine + z * cosine);

            return result;
        }

        public IntVec2 Assign(IntVec2 rhs)
        {
            if (this == rhs) return this;
            x = rhs.x;
            z = rhs.z;
            return this;
        }

        public static int SidenessTest(IntVec2 A, IntVec2 B, IntVec2 C)
        {
            int result = (B.x - A.x) * (C.z - A.z) - (B.z - A.z) * (C.x - A.x);
            if (result == 0) return 0;
            else if (result > 0) return 1;
            else return -1;
        }

        public override String ToString()
        {
            return String.Format("({0},{1})", x, z);
        }
    }
}
