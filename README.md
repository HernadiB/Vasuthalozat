# Vasúti hálozat

- Legyen felhasználó kezelés, regisztráció és bejelentkeztetés
- Bejelentkezés után a felhasználó két dropdown (combo box) segítségével képes legyen megadni a kiinduló és cél állomásokat, amelyekre a program egy lista nézetben adja vissza az összes lehetséges megoldást, ahogy el lehet jutni a kiinduló városból a cél városba (akár átszállással is). Minden lehetőség tartalmazza az utazás költségét, ami 15Ft/km árral legyen számolva és költség szerint növekvő sorrendbe legyen rendezve. Tehát amit a listában egy listaelemként meg kell jeleníteni:
    - Járatok egymás után sorrendben és az átszállás helyének városa
      ▪ pl: kiinduló város -> „A járat” -> „B városon átszállás” -> „C járat” -> „D városon átszállás” -> „E járat” -> cél város
    - Legyen látható az utazás teljes költsége és távolsága
    - Lehessen jegyet foglalni a kiválasztott járatra, amely adatbázisban is kerüljön rögzítésre
        - plusz pontért: kapjon a felhasználó visszaigazoló emailt a jegyfoglalásról (nem kell HTML, elég sima plain text)
- Legyen egy admin felület, ami el kell szeparálódjon a felhasználói felületről
    - Ide egye előre rögzített felhasználónév + jelszó kombinációval lehessen belépni
- Ezen a felületen lehessen a városokkal és a vasúthálózattal kapcsolatos CRUD műveletek elvégzésére pl:
    - Új város hozzáadása, meglévő módosítása és törlése
    - Meglévő városok között új direkt vasútjárat hozzáadása
-Továbbá a vasúti társaság a közeljövőben tervezi a sínek hálózat felújítását, ehhez azt a követelményt szabták meg, hogy egy minimális hosszúságú sínhálózat legyen kiválasztva, amelyekkel le lehet még fedni az ország teljes vasúthálózatát (Kruskal-algoritmusa)

## Admin:
- bejelentkezés :white_check_mark:
- beolvasás :white_check_mark:
- járat hozzáadása :white_check_mark:
- járat módosítása :x:
- járat törlése :white_check_mark:
- város hozzáadása :white_check_mark:
- város módosítása :white_check_mark:
- város törlése :white_check_mark:

## User
- regisztráció :white_check_mark:
- bejelentkezés :white_check_mark:
- kiinduló állomás combobox
- cél állomás combobox
- lehetséges eljutások A-ból B-be
- költség 15 Ft / km
- jegy foglalás egy adott járatra
- visszaigazoló email a foglalásról