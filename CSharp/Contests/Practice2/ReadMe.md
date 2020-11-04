# ACL Practice

## D
市松模様の二部マッチング。  
頂点の数は `NM + 2`、辺の数は `O(NM)`。

## E
行と列の二部マッチングを構築し、最小費用流で求める。  
頂点の数は `2N + 2`、辺の数は `N^2 + 2N`。

行と列を結ぶ辺にコスト `-A_{ij}` を設定する。  
`NK` 以下の流量で最小となるものを求める。「以下」のため、ステップごとに監視する。