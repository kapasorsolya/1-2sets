# random

## Feladat leírás:
  Adott p darab változó.
    - ha n=10, vagyis a doménium elemei 1,2,3...10
   - ha p=6, olyan megoldásokat keresünk ahol az elemek száma egyenlő p-vel.
  Építsünk fel egy p elemből álló számot úgy hogy teljesitse a következő kényszereket:
    - x1 != x2 
    - x2 != x3
    - x3 != x4
    - x5 != x3
    - x6 = x5/2
  Tehát az első p-1 változó nem lehet egyforma és a p.dik fele kell legyen a p-1 -nek.
  
 
## Eredmények:
  
  ### nyers backtracking (1):
      - első megoldás: 1 2 3 4 10 5
      - értékadások száma: 110
      
  ### bactracking + MVR + forward checking (2):
      - első megoldás: 1 2 3 4 10 5
      - értékadások száma: 50
      
  ### backtracking + MVR + Ac3 (3):
