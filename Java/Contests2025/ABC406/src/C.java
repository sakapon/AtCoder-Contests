import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Arrays;

public class C {
	static BufferedReader in = new BufferedReader(new InputStreamReader(System.in));

	static int[] read() throws IOException {
		return Arrays.stream(in.readLine().split(" ")).mapToInt(s -> Integer.parseInt(s)).toArray();
	}

	public static void main(String[] args) throws IOException {
		System.out.println(solve());
	}

	static Object solve() throws IOException {
		var n = Integer.parseInt(in.readLine());
		var p = read();

		var cs = new ArrayList<Integer>();
		var t = 0;
		for (int i = 1; i < n; i++) {
			if (p[i - 1] < p[i]) {
				t++;
			} else {
				if (t != 0) {
					cs.add(t);
					t = 0;
				}
			}
		}
		if (t != 0) {
			cs.add(t);
		}

		var r = 0L;
		for (int i = 1; i < cs.size(); i++)
			r += (long) cs.get(i - 1) * cs.get(i);
		return r;
	}
}
