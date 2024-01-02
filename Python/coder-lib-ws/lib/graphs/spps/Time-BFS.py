import time
import random
from unweightedgraph import ListUnweightedGraph
from unweightedgraph_e import ListUnweightedGraph_e
from unweightedgraph_t import ListUnweightedGraph_t

n = 200000
edges = []

for i in range(2 * n):
    x = random.randint(0, n - 1)
    y = random.randint(0, n - 1)
    edges.append((x, y))

start_time = time.perf_counter()
graph = ListUnweightedGraph(n)
for a, b in edges:
    graph.add_edge(a, b, True)
r = graph.shortest_by_bfs(0)
print(time.perf_counter() - start_time)

print("end")
