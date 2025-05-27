import java.util.ArrayList;
import java.util.Scanner;
import java.util.stream.IntStream;

public class C {
	static Scanner sc = new Scanner(System.in);

	static int[] read(int n) {
		return IntStream.range(0, n).map(i -> sc.nextInt()).toArray();
	}

	public static void main(String[] args) {
		System.out.println(solve());
	}

	static Object solve() {
		var n = sc.nextInt();
		var p = read(n);

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
