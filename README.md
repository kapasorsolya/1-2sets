# random

## Feladat leírás:
  Adott p darab változó.
    - ha n=10, vagyis a doménium elemei 1,2,3...10
    - p=6, olyan megoldásokat keresünk ahol az elemek száma 6. (abcdef alakú szám)
    ( megjegyzés: p-nem változtatható mivel, a szomszédág felépítése nehézkes akkor).
  
  Építsünk fel egy p elemből álló számot úgy hogy teljesitse a következő kényszereket:
    - x1 != x2 
    - x2 != x3
    - x3 != x4
    - x5 != x3
    - x6 = x5/2
  Tehát az első p-1 változó nem lehet egyforma és a p.dik fele kell legyen a p-1 -nek.

## Parancssor argumentumainak száma
  - mindig 2
  
  ### első paraméter:
    - megadja a domenium legfelső értékét (pl. 8 esetén a doménium értéke 1,2,3,4,5,6,7,8)
  ### masodik paraméter:
    - megoldási módszert jelöli ( értékei: 1, 2 vagy 3).
    
    1. nyers backtracking
    2. backtracking + MVR + forward checking
    3. backtracking + MVR + AC-3

      
 
## Eredmények:
  
  ### nyers backtracking (1):
      - első megoldás: 1 2 3 4 10 5
      - értékadások száma: 110
      
  ### bactracking + MVR + forward checking (2):
      - első megoldás: 1 2 3 4 10 5
      - értékadások száma: 50
      
  ### backtracking + MVR + Ac3 (3):
      - első megoldás: 1 2 3 4 10 5
      - értékadások száma : 75
     

