using System;

namespace CoderLib8.Values
{
	public abstract class OrderedBase<T> : IEquatable<OrderedBase<T>>, IComparable<OrderedBase<T>> where T : struct, IEquatable<T>, IComparable<T>
	{
		protected abstract T OrderedValue { get; }

		#region Equality Operators
		public bool Equals(OrderedBase<T> other) => !(other is null) && OrderedValue.Equals(other.OrderedValue);
		public static bool Equals(OrderedBase<T> v1, OrderedBase<T> v2) => v1?.Equals(v2) ?? (v2 is null);
		public static bool operator ==(OrderedBase<T> v1, OrderedBase<T> v2) => Equals(v1, v2);
		public static bool operator !=(OrderedBase<T> v1, OrderedBase<T> v2) => !Equals(v1, v2);
		public override bool Equals(object obj) => Equals(obj as OrderedBase<T>);
		public override int GetHashCode() => OrderedValue.GetHashCode();
		#endregion

		#region Comparison Operators
		public int CompareTo(OrderedBase<T> other) => (other is null) ? 1 : OrderedValue.CompareTo(other.OrderedValue);
		public static int Compare(OrderedBase<T> v1, OrderedBase<T> v2) => v1?.CompareTo(v2) ?? (v2 is null ? 0 : -1);
		public static bool operator <(OrderedBase<T> v1, OrderedBase<T> v2) => Compare(v1, v2) < 0;
		public static bool operator >(OrderedBase<T> v1, OrderedBase<T> v2) => Compare(v1, v2) > 0;
		public static bool operator <=(OrderedBase<T> v1, OrderedBase<T> v2) => Compare(v1, v2) <= 0;
		public static bool operator >=(OrderedBase<T> v1, OrderedBase<T> v2) => Compare(v1, v2) >= 0;
		#endregion
	}
}
