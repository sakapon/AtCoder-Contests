h, w = map(int, input().split())
c = [input() for _ in range(h)]

i_min, i_max = h, 0
j_min, j_max = w, 0

for i in range(h):
    for j in range(w):
        if c[i][j] == "#":
            i_min = min(i_min, i)
            i_max = max(i_max, i)
            j_min = min(j_min, j)
            j_max = max(j_max, j)

for s in c[i_min:i_max + 1]:
    print(s[j_min:j_max + 1])
