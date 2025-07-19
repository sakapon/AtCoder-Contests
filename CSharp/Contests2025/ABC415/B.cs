class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		var a = Enumerable.Range(1, n).Where(i => s[i - 1] == '#').ToArray();
		var g = new SeqArray2<int>(a.Length / 2, 2, a);
		return string.Join("\n", g.Select(r => string.Join(",", r)));
	}
}

public class SeqArray2<T> : IEnumerable<ArraySegment<T>>
{
	public readonly int n1, n2;
	public readonly T[] a;
	public SeqArray2(int _n1, int _n2, T[] _a = null) => (n1, n2, a) = (_n1, _n2, _a ?? new T[_n1 * _n2]);

	public ArraySegment<T> this[int i] => new ArraySegment<T>(a, n2 * i, n2);
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
	public IEnumerator<ArraySegment<T>> GetEnumerator() { for (int i = 0; i < n1; ++i) yield return this[i]; }
}
