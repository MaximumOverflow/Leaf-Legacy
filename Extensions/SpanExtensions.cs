using System;

namespace Extensions
{
	public static class SpanExtensions
	{
		public static bool SequenceEquals<T>(this Span<T> span, Span<T> other) where T : IEquatable<T>
		{
			if (span.Length != other.Length)
				return false;
			
			for(var i = 0; i < span.Length; i++)
				if (!span[i].Equals(other[i]))
					return false;

			return true;
		}
		
		public static bool SequenceEquals<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other) where T : IEquatable<T>
		{
			if (span.Length != other.Length)
				return false;
			
			for(var i = 0; i < span.Length; i++)
				if (!span[i].Equals(other[i]))
					return false;

			return true;
		}
	}
}