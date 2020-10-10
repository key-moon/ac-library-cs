﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtCoder
{
    public static class StlFunction
    {
        #region NextPermutation
        public struct NextPermutationEnumerator<T> : IEnumerator<T[]>, IEnumerable<T[]> where T : IComparable<T>
        {
            internal readonly IEnumerable<T> _orig;
            internal NextPermutationEnumerator(IEnumerable<T> orig)
            {
                _orig = orig;
                Current = null;
            }

            public T[] Current { get; private set; }

            public bool MoveNext()
            {
                if (Current == null)
                {
                    Current = _orig.ToArray();
                    return true;
                }

                var arr = Current;

                int i;
                for (i = arr.Length - 2; i >= 0; i--)
                    if (arr[i].CompareTo(arr[i + 1]) < 0)
                        break;
                if (i < 0)
                    return false;
                int j;
                for (j = arr.Length - 1; j >= 0; j--)
                    if (arr[i].CompareTo(arr[j]) < 0)
                        break;
                (arr[i], arr[j]) = (arr[j], arr[i]);
                Array.Reverse(arr, i + 1, arr.Length - i - 1);
                return true;
            }
            public void Reset() => Current = null;
            object IEnumerator.Current => Current;
            void IDisposable.Dispose() { }
            public NextPermutationEnumerator<T> GetEnumerator() => this;
            IEnumerator<T[]> IEnumerable<T[]>.GetEnumerator() => this;
            IEnumerator IEnumerable.GetEnumerator() => this;
        }

        /// <summary>
        /// 辞書順によるその次の順列を生成します。返すインスタンスは共通です。
        /// </summary>
        public static NextPermutationEnumerator<T> NextPermutation<T>(IEnumerable<T> orig) where T : IComparable<T>
            => new NextPermutationEnumerator<T>(orig);
        #endregion NextPermutation

        #region BinarySerch
        /// <summary>
        /// <paramref name="ok"/> と <paramref name="ng"/> の間で <paramref name="Ok"/>(i) == true を満たす最も <paramref name="ng"/> に近い値を取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="Ok"/>(<paramref name="ok"/>) &amp;&amp; !<paramref name="Ok"/>(<paramref name="ng"/>)</para>
        /// <para>計算量: O(log |<paramref name="ok"/> - <paramref name="ng"/>|)</para>
        /// </remarks>
        public static int BinarySearch(int ok, int ng, Predicate<int> Ok)
        {
            Debug.Assert(Ok(ok));
            Debug.Assert(!Ok(ng));
            while (Math.Abs(ok - ng) > 1)
            {
                var m = (ok + ng) >> 1;
                if (Ok(m)) ok = m;
                else ng = m;
            }
            return ok;
        }

        /// <summary>
        /// <paramref name="ok"/> と <paramref name="ng"/> の間で <paramref name="Ok"/>(i) == true を満たす最も <paramref name="ng"/> に近い値を取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="Ok"/>(<paramref name="ok"/>) &amp;&amp; !<paramref name="Ok"/>(<paramref name="ng"/>)</para>
        /// <para>計算量: O(log |<paramref name="ok"/> - <paramref name="ng"/>|)</para>
        /// </remarks>
        public static long BinarySearch(long ok, long ng, Predicate<long> Ok)
        {
            Debug.Assert(Ok(ok));
            Debug.Assert(!Ok(ng));
            while (Math.Abs(ok - ng) > 1)
            {
                var m = (ok + ng) >> 1;
                if (Ok(m)) ok = m;
                else ng = m;
            }
            return ok;
        }

        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> 以上の値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int LowerBound<T>(this T[] a, T v, IComparer<T> cmp) => BinarySearch(a.AsSpan(), v, cmp, true);

        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> 以上の値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int LowerBound<T>(this T[] a, T v) => BinarySearch(a.AsSpan(), v, Comparer<T>.Default, true);

        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> より大きい値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int UpperBound<T>(this T[] a, T v, IComparer<T> cmp) => BinarySearch(a.AsSpan(), v, cmp, false);

        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> より大きい値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int UpperBound<T>(this T[] a, T v) => BinarySearch(a.AsSpan(), v, Comparer<T>.Default, false);
        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> 以上の値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int LowerBound<T>(this IList<T> a, T v, IComparer<T> cmp) => BinarySearch(a, v, cmp, true);

        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> 以上の値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int LowerBound<T>(this IList<T> a, T v) => BinarySearch(a, v, Comparer<T>.Default, true);

        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> より大きい値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int UpperBound<T>(this IList<T> a, T v, IComparer<T> cmp) => BinarySearch(a, v, cmp, false);

        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> より大きい値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int UpperBound<T>(this IList<T> a, T v) => BinarySearch(a, v, Comparer<T>.Default, false);
        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> 以上の値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int LowerBound<T>(this Span<T> a, T v, IComparer<T> cmp) => BinarySearch(a, v, cmp, true);

        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> 以上の値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int LowerBound<T>(this Span<T> a, T v) => BinarySearch(a, v, Comparer<T>.Default, true);

        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> より大きい値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int UpperBound<T>(this Span<T> a, T v, IComparer<T> cmp) => BinarySearch(a, v, cmp, false);

        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> より大きい値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int UpperBound<T>(this Span<T> a, T v) => BinarySearch(a, v, Comparer<T>.Default, false);
        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> 以上の値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int LowerBound<T>(this ReadOnlySpan<T> a, T v, IComparer<T> cmp) => BinarySearch(a, v, cmp, true);

        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> 以上の値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int LowerBound<T>(this ReadOnlySpan<T> a, T v) => BinarySearch(a, v, Comparer<T>.Default, true);

        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> より大きい値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int UpperBound<T>(this ReadOnlySpan<T> a, T v, IComparer<T> cmp) => BinarySearch(a, v, cmp, false);

        /// <summary>
        /// <paramref name="a"/> の要素のうち、<paramref name="v"/> より大きい値が現れる最小のインデックスを取得します。
        /// </summary>
        /// <remarks>
        /// <para>制約: <paramref name="a"/> がソート済み</para>
        /// <para>計算量: O(log <paramref name="n"/>)</para>
        /// </remarks>
        public static int UpperBound<T>(this ReadOnlySpan<T> a, T v) => BinarySearch(a, v, Comparer<T>.Default, false);
        private static int BinarySearch<T>(this IList<T> a, T v, IComparer<T> cmp, bool isLowerBound)
        {
            int ok = a.Count;
            int ng = -1;
            while (ok - ng > 1)
            {
                var m = (ok + ng) >> 1;
                var c = cmp.Compare(a[m], v);
                if (c > 0 || (c == 0 && isLowerBound)) ok = m;
                else ng = m;
            }
            return ok;
        }
        private static int BinarySearch<T>(this ReadOnlySpan<T> a, T v, IComparer<T> cmp, bool isLowerBound)
        {
            int ok = a.Length;
            int ng = -1;
            while (ok - ng > 1)
            {
                var m = (ok + ng) >> 1;
                var c = cmp.Compare(a[m], v);
                if (c > 0 || (c == 0 && isLowerBound)) ok = m;
                else ng = m;
            }
            return ok;
        }
        #endregion BinarySerch
    }
}