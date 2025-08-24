import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.stream.IntStream;

public class C {
	static class Tuple {
		public int u, v;

		public Tuple(int u, int v) {
			this.u = u;
			this.v = v;
		}
	}

	static BufferedReader in = new BufferedReader(new InputStreamReader(System.in));

	static String readLine() {
		try {
			return in.readLine();
		} catch (IOException ex) {
			return "";
		}
	}

	static int[] read() {
		return Arrays.stream(readLine().split(" ")).mapToInt(Integer::parseInt).toArray();
	}

	static Tuple[] readTuple(int n) {
		var r = new Tuple[n];
		for (int i = 0; i < n; i++) {
			var a = read();
			r[i] = new Tuple(a[0], a[1]);
		}
		return r;
	}

	public static void main(String[] args) {
		System.out.println(solve());
	}

	static Object solve() {
		var z = read();
		var n = z[0];
		var m = z[1];
		var ps = readTuple(m);

		var a = new int[n + 2];
		for (var p : ps) {
			a[p.u]++;
			a[p.v + 1]--;
		}
		for (int i = 0; i < n; i++) {
			a[i + 1] += a[i];
		}
		return IntStream.range(1, n + 1).map(i -> a[i]).min().getAsInt();
	}
}
