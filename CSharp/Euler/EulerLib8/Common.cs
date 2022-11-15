using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace EulerLib8
{
	public static class Common
	{
		public static string GetText(string url)
		{
			using var http = new HttpClient();
			var task = http.GetStringAsync(url);
			task.Wait();
			return task.Result;
		}

		public static T Max<T, TKey>(T o1, T o2, Func<T, TKey> toKey) where TKey : IComparable<TKey> => toKey(o1).CompareTo(toKey(o2)) >= 0 ? o1 : o2;
		public static T Min<T, TKey>(T o1, T o2, Func<T, TKey> toKey) where TKey : IComparable<TKey> => toKey(o1).CompareTo(toKey(o2)) <= 0 ? o1 : o2;
	}
}
