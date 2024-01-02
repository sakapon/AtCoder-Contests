# https://atcoder.jp/contests/atc001/tasks/dfs_a

from collections import deque

def connectivity_by_dfs(map, sv, ev):
    n = len(map)
    u = [False] * n
    q = deque()
    u[sv] = True
    q.append(sv)
    
    while q:
        v = q.pop()
        if v == ev: return u
        for nv in map[v]:
            if u[nv]: continue
            u[nv] = True
            q.append(nv)
    return u

h, w = map(int, input().split())
s = [list(input()) for _ in range(h)]

wall = '#'
map = [[] for _ in range(h * w)]
for i in range(h):
    for j in range(1, w):
        if s[i][j] == wall or s[i][j - 1] == wall: continue
        v = w * i + j
        map[v].append(v - 1)
        map[v - 1].append(v)
for j in range(w):
    for i in range(1, h):
        if s[i][j] == wall or s[i - 1][j] == wall: continue
        v = w * i + j
        map[v].append(v - w)
        map[v - w].append(v)

for i in range(h):
    for j in range(w):
        if s[i][j] == 's': sv = w * i + j
        if s[i][j] == 'g': ev = w * i + j

u = connectivity_by_dfs(map, sv, ev)
print("Yes" if u[ev] else "No")
