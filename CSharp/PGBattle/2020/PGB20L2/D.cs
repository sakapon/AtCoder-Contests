using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	const long M = 1000000007;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		// 縦長
		var (w, h) = Read2();

		if (h % 2 == 1 && w % 2 == 1) return 0;

		var states = RowState.Create(w);
		var pow = states.Length;

		var dp = Array.ConvertAll(states, s => s.IsTop ? 1L : 0L);
		var dt = new long[pow];

		for (int i = 1; i < h; i++)
		{
			for (int j = 0; j < pow; j++)
			{
				foreach (var ns in states[j].NextRowStates)
				{
					var nj = ns.Id;
					dt[nj] += dp[j];
					dt[nj] %= M;
				}
			}

			(dp, dt) = (dt, dp);
			Array.Clear(dt, 0, dt.Length);
		}
		return states.Where(s => s.IsBottom).Sum(s => dp[s.Id]) % M;
	}
}

// 横の長さは十分小さいと仮定します。
[System.Diagnostics.DebuggerDisplay(@"Id = {Id}, IsRowAvailable = {IsRowAvailable}")]
public class RowState
{
	public static RowState[] Create(int width)
	{
		var pow = 1;
		for (int k = 0; k < width; ++k) pow *= CellStatesCount;

		var states = new RowState[pow];
		for (int id = 0; id < pow; ++id) states[id] = new RowState(id, width);

		for (int i = 0; i < pow; ++i)
		{
			if (!states[i].IsRowAvailable) continue;
			for (int j = 0; j < pow; ++j)
			{
				if (!states[j].IsRowAvailable) continue;
				if (GetIsNextAvailable(states[i].Cells, states[j].Cells)) states[i].NextRowStates.Add(states[j]);
			}
		}
		return states;
	}

	// 横置きの左側・右側、縦置きの上側・下側
	public enum CellState
	{
		Left,
		Right,
		Top,
		Bottom,
	}
	const int CellStatesCount = 4;

	public int Id { get; }
	public CellState[] Cells { get; }
	public bool IsRowAvailable { get; }
	public bool IsTop { get; }
	public bool IsBottom { get; }
	public List<RowState> NextRowStates { get; } = new List<RowState>();

	RowState(int id, int width)
	{
		Id = id;
		Cells = GetCells(id, width);
		IsRowAvailable = GetIsRowAvailable(Cells);
		IsTop = IsRowAvailable && Cells.All(x => x != CellState.Bottom);
		IsBottom = IsRowAvailable && Cells.All(x => x != CellState.Top);
	}

	static CellState[] GetCells(int id, int w)
	{
		var c = new CellState[w];
		for (int i = 0; i < w; ++i)
		{
			id = Math.DivRem(id, CellStatesCount, out var rem);
			c[i] = (CellState)rem;
		}
		return c;
	}

	static bool GetIsRowAvailable(CellState[] c)
	{
		var w = c.Length;
		for (int i = 0; i < w; ++i)
		{
			if (c[i] == CellState.Left)
			{
				if (i == w - 1 || c[i + 1] != CellState.Right) return false;
			}
			else if (c[i] == CellState.Right)
			{
				if (i == 0 || c[i - 1] != CellState.Left) return false;
			}
		}
		return true;
	}

	static bool GetIsNextAvailable(CellState[] c, CellState[] nc)
	{
		var w = c.Length;
		for (int i = 0; i < w; ++i)
		{
			if (c[i] == CellState.Top)
			{
				if (nc[i] != CellState.Bottom) return false;
			}
			else if (nc[i] == CellState.Bottom)
			{
				if (c[i] != CellState.Top) return false;
			}
		}
		return true;
	}
}
