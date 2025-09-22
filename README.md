# Kávéautomata

## Projekt áttekintése
A projekt egy WPF alapú asztali alkalmazás, amely egy kávéautomata működését szimulálja. A felhasználó különböző italokat választhat, módosíthatja a cukor mennyiségét, majd a fizetési folyamatot hangok kíséretében tudja lezárni. A háttérlogika a `MainWindow.xaml` és a `MainWindow.xaml.cs` fájlokban található, a felhasználói felület kialakítása pedig XAML segítségével történt.

## Felhasználói felület (XAML)
- **Gombstílus**: A `RoundedButtonStyle` stílus biztosítja az egységes megjelenést és hovereffectet minden gombra (`MainWindow.xaml`).
- **Bal oldali italválaszték**: A vásárolható italokat (pl. *Fekete Kávé*, *Latte*, *Forró Csoki*) egy rögzített elrendezésű gombsor jeleníti meg.
- **Ár és cukor szekció**: A jobb oldalon található a kiválasztott ital ára és a cukor mennyiségének állítására szolgáló vezérlők.
- **Vásárlás gomb**: A terminál képe (`terminal.jpg`) jelenik meg gombháttérként; a gomb kezeli a kifizetés folyamatát és a hangok lejátszását.
- **Alsó banner**: A `banner.jpg` kép dekorációként funkcionál a felület alján.

## Alkalmazáslogika (C#)
- **Inicializálás**: A `MainWindow` konstruktora beolvassa az `anyagok.txt` és `receptek.txt` fájlokat, majd beállítja a rendelkezésre álló alapanyagok mennyiségét (`kavepor`, `tejpor`, `cukor`, `kakaopor`, `viz`). Hibás fájlformátum esetén figyelmeztet és bezárja az alkalmazást.
- **Receptválasztás**: Minden ital gomb egy-egy `Selected_Coffe` hívást indít, amely betölti az adott recepthez szükséges alapanyagokat és árat, majd lejátssza a megfelelő hangfájlt.
- **Cukor kezelése**: A cukor mennyisége 0 és 3 közötti értékre módosítható. A cukor növelése növeli az árat, és a gombok csak akkor aktívak, ha van elegendő cukorkészlet.
- **Vásárlási folyamat**: A `Buy_Click` esemény első megnyomáskor üdvözlő hangot játszik le. Második megnyomáskor levonja az alapanyagokat, lejátsza a fizetés hangját, frissíti az `anyagok.txt` fájlt, majd visszaállítja az alapértelmezett állapotot.
- **Gombok frissítése**: Az `UpdateButtons` metódus biztosítja, hogy mindig csak a választható italok legyenek aktívak, figyelembe véve a rendelkezésre álló készleteket és az aktuális állapotot.
- **Alapanyagok visszaállítása**: A `Reset` gomb 50-50 egységre állítja vissza az alapanyagokat, és azonnal kiírja az új értékeket az `anyagok.txt` fájlba.

## Adatfájlok
- **`anyagok.txt`**: A készletek aktuális állapotát tárolja `Név;Mennyiség` formátumban. A program induláskor innen tölti be a kiinduló értékeket, majd vásárláskor frissíti a tartalmát.
- **`receptek.txt`**: Minden sor egy ital receptjét írja le `Név;Kávépor;Tejpor;Cukor;Kakaópor;Víz;Ár` formátumban. A program ezek alapján ellenőrzi, hogy van-e elegendő alapanyag az adott italhoz.

## Hang- és médiaerőforrások
A `hangok` mappában található `.wav` fájlok szolgáltatják a visszajelzést az italválasztás és a fizetés során (`udvozlom.wav`, `koszonjuk.wav`, italok neve szerinti fájlok). A felületen a `banner.jpg` és `terminal.jpg` képek jelennek meg.

## Fordítás és futtatás
1. Nyisd meg a megoldást Visual Studio-ban (`kaveautomata.sln`).
2. Győződj meg róla, hogy a `receptek.txt`, `anyagok.txt`, valamint a `hangok` és médiafájlok a kimeneti könyvtárba másolódnak (Build Action: *Content*, Copy to Output Directory: *Copy if newer*).
3. Futtasd az alkalmazást (`F5` vagy `Ctrl+F5`).
4. A program Windows rendszeren, .NET 6.0 (vagy újabb) WPF környezetben futtatható.

## Ismert korlátozások és javaslatok
- Az `anyagok.txt` fájl sorainak száma fix; eltérés esetén az alkalmazás leáll.
- A `receptek.txt` sorainak helytelen formátuma futási hibát okozhat.
- További fejlesztési lehetőség: hibaüzenetek lokalizálása, felhasználói visszajelző felület (pl. készletkijelzés), illetve naplózás bevezetése.

## Szerzők
Belt Bálint Viktor, Kiss Máté, Horváth-Gortva Zalán
