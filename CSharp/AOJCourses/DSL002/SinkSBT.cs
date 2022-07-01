using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8.DataTrees.SBTs
{
	// top-down segment binary tree
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class SinkSBT<TOp>
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Op}, Count = {Count}\}")]
		public class Node
		{
			public TOp Op;
			public int Count = 1;
			public Node Left, Right;
		}

		public Monoid<TOp> Sink { get; }
		Node[] Leaves;
		public int Count => Leaves.Length;
		Node Root;

		public SinkSBT(int n, Monoid<TOp> sink, IEnumerable<TOp> items = null)
		{
			Sink = sink;
			var a = items as TOp[] ?? items?.ToArray();
			if (a?.Length < n) throw new ArgumentException("", nameof(items));
			Leaves = new Node[n];
			for (int i = 0; i < n; ++i) Leaves[i] = new Node { Op = a != null ? a[i] : sink.Id };
			Root = CreateSubtree(0, n);
		}

		Node CreateSubtree(int l, int r)
		{
			if (r - l == 1) return Leaves[l];
			var m = (l + r) >> 1;
			return new Node
			{
				Op = Sink.Id,
				Count = r - l,
				Left = CreateSubtree(l, m),
				Right = CreateSubtree(m, r),
			};
		}

		public TOp this[int index]
		{
			get => Get(Root, index);
			set => SetRange(Root, index, index + 1, value);
		}

		public TOp this[int left, int right]
		{
			set => SetRange(Root, left, right, value);
		}

		TOp Get(Node n, int i)
		{
			if (n.Count == 1) return n.Op;
			n.Left.Op = Sink.Op(n.Op, n.Left.Op);
			n.Right.Op = Sink.Op(n.Op, n.Right.Op);
			n.Op = Sink.Id;

			if (i < n.Left.Count) return Get(n.Left, i);
			else return Get(n.Right, i - n.Left.Count);
		}

		void SetRange(Node n, int l, int r, TOp op)
		{
			if (l <= 0 && n.Count <= r) { n.Op = Sink.Op(op, n.Op); return; }
			n.Left.Op = Sink.Op(n.Op, n.Left.Op);
			n.Right.Op = Sink.Op(n.Op, n.Right.Op);
			n.Op = Sink.Id;

			var lc = n.Left.Count;
			if (r <= lc) SetRange(n.Left, l, r, op);
			else if (lc <= l) SetRange(n.Right, l - lc, r - lc, op);
			else
			{
				SetRange(n.Left, l, lc, op);
				SetRange(n.Right, 0, r - lc, op);
			}
		}
	}
}
