import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;

public class C {

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

	public static void main(String[] args) {
		System.out.println(solve());
	}

	static Object solve() {
		var z = read();
		var l = z[1];
		var d = read();

		if (l % 3 != 0)
			return 0;

		var x = 0;
		var cs = new long[l];
		cs[x]++;

		for (var y : d) {
			x += y;
			x %= l;
			cs[x]++;
		}

		l /= 3;
		var r = 0L;
		for (int i = 0; i < l; i++)
			r += cs[i] * cs[i + l] * cs[i + 2 * l];
		return r;
	}
}
