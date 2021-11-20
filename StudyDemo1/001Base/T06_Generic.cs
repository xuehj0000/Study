using System;
using System.Collections.Generic;
using System.Text;

namespace StudyDemo1
{
    /// <summary>
    /// 泛型：不同类型的同一个操作,极大提高数据的重用性。类型安全的。性能比较好
    /// ref : 强制要求参数按照引用传递
    /// </summary>
    public class T06_Generic
    {

    }

    /// <summary>
    /// 泛型类
    /// </summary>
    public class MyGenericArray<T>
    {
        private T[] array;
        public MyGenericArray()
        {

        }
        public MyGenericArray(int size)
        {
            array = new T[size + 1];
        }

        public T GetItem(int index)
        {
            return array[index];
        }

        public void SetItem(int index, T value)
        {
            array[index] = value;
        }

        /// <summary>
        /// 泛型方法
        /// </summary>
        public void GenericMethod<T>(T value)
        {
            Console.WriteLine(value);
        }

        /// <summary>
        /// ref 使用泛型
        /// </summary>
        public static void Swap<T>(ref T l, ref T r)
        {
            T temp;
            temp = l;
            l = r;
            r = temp;
        }


    }

    /// <summary>
    /// 多重泛型， 像Dictionary<>
    /// 泛型约束：泛型类型限制。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    public class MyGeneric<T, K> where T:struct
    {

    }

    /// <summary>
    /// 泛型类，继承
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SubClass<T> : MyGenericArray<T> where T:struct
    {

    }

}
