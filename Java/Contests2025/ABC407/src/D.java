import java.util.ArrayList;
import java.util.Arrays;
import java.util.Scanner;
import java.util.stream.IntStream;

public class D {

	static class Tuple {
		public int u, v;

		public Tuple(int u, int v) {
			this.u = u;
			this.v = v;
		}
	}

	static Scanner sc = new Scanner(System.in);

	static long[] readL(int n) {
		return IntStream.range(0, n).mapToLong(i -> sc.nextLong()).toArray();
	}

	public static void main(String[] args) {
		System.out.println(solve());
	}

	static Object solve() {
		var h = sc.nextInt();
		var w = sc.nextInt();

		var n = h * w;
		var a = readL(n);

		var pairs = new ArrayList<Tuple>();
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++) {
				var v = w * i + j;

				if (i != 0)
					pairs.add(new Tuple(v - w, v));
				if (j != 0)
					pairs.add(new Tuple(v - 1, v));
			}

		var dp = new long[1 << n];
		Arrays.fill(dp, -1);
		dp[0] = Arrays.stream(a).reduce(0, (x, y) -> x ^ y);
		var r = dp[0];

		for (int x = 0; x < 1 << n; x++) {
			if (dp[x] == -1)
				continue;

			for (var p : pairs) {
				var u = p.u;
				var v = p.v;
				if ((x & (1 << u)) != 0 || (x & (1 << v)) != 0)
					continue;
				var nx = x | (1 << u) | (1 << v);
				dp[nx] = dp[x] ^ a[u] ^ a[v];
				r = Math.max(r, dp[nx]);
			}
		}
		return r;
	}
}
