using System;
using System.Collections.Generic;

namespace Extensions
{
	public static class EnumerableExtensions
	{
		public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T>? collection)
			=> collection is null || collection.Count == 0;

		public static bool SequenceEqual<T, TIn>(this IReadOnlyList<T> list, IReadOnlyList<TIn> comp, Func<TIn, T> select) where T : IEquatable<T>
		{
			if (list.Count != comp.Count)
				return false;

			for (var i = 0; i < list.Count; i++)
				if (!list[i].Equals(select(comp[i])))
					return false;

			return true;
		}
		
		public static bool SequenceEqual<T, TIn>(this IReadOnlyList<T> list, IReadOnlyList<TIn> comp, Func<T, TIn> select) where TIn : IEquatable<TIn>
		{
			if (list.Count != comp.Count)
				return false;

			for (var i = 0; i < list.Count; i++)
				if (!select(list[i]).Equals(comp[i]))
					return false;

			return true;
		}
		
		public static bool SequenceEqual<T, TIn>(this IReadOnlyList<T> list, Span<TIn> comp, Func<TIn, T> select) where T : IEquatable<T>
		{
			if (list.Count != comp.Length)
				return false;

			for (var i = 0; i < list.Count; i++)
				if (!list[i].Equals(select(comp[i])))
					return false;

			return true;
		}
		
		public static bool IncompleteSequenceEqual<T>(this IReadOnlyList<T> list, IReadOnlyList<T> comp) where T : IEquatable<T>
		{
			if (comp.Count < list.Count)
				return false;
			for (int index = 0; index < list.Count; ++index)
			{
				if (!list[index].Equals(comp[index]))
					return false;
			}
			return true;
		}
		
		public static bool IncompleteSequenceEqual<T, TIn>(this IReadOnlyList<T> list, IReadOnlyList<TIn> comp, Func<TIn, T> select) where T : IEquatable<T>
		{
			if (comp.Count < list.Count)
				return false;
			
			for (var i = 0; i < list.Count; ++i)
			{
				if (!list[i].Equals(select(comp[i])))
					return false;
			}
			return true;
		}
		
		public static bool IncompleteSequenceEqual<T, TIn>(this IReadOnlyList<T> list, Span<TIn> comp, Func<TIn, T> select) where T : IEquatable<T>
		{
			if (comp.Length < list.Count)
				return false;
			
			for (var i = 0; i < list.Count; ++i)
			{
				if (!list[i].Equals(select(comp[i])))
					return false;
			}
			return true;
		}
		
		public static TOut[] ArraySelect<TKey, TValue, TOut>(this IReadOnlyDictionary<TKey, TValue> dict, Func<TKey, TOut> select)
		{
			var i = 0;
			var arr = new TOut[dict.Count];
			foreach (var key in dict.Keys)
				arr[i++] = select(key);

			return arr;
		}

		public static TOut[] ArraySelect<TKey, TValue, TOut>(this IReadOnlyDictionary<TKey, TValue> dict, Func<TValue, TOut> select)
		{
			var i = 0;
			var arr = new TOut[dict.Count];
			foreach (var value in dict.Values)
				arr[i++] = select(value);

			return arr;
		}
	}
}