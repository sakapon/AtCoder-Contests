using System;

namespace CoderLib8.Extra
{
	[Obsolete("乱択の精度が不十分です。")]
	public class IntRandom
	{
		const long M = 998244353;
		long v;
		public IntRandom() => v = new Random().Next();
		public int Next(int max_ex) => (int)((v += M) % max_ex);
		public int Next(int min_in, int max_ex) => min_in + Next(max_ex - min_in);
	}
}
