
# Ghid pentru utilizarea Git Submodule

Acest document oferă explicații despre cum să folosești submodulele în Git, cum să le actualizezi și cum să revii la o versiune mai veche.

---

## Ce este un submodul?
Un submodul în Git este un repository Git inclus într-un alt repository Git. Acesta permite includerea și gestionarea unui proiect separat în cadrul unui alt proiect.

---

## Cum să adaugi un submodul?
1. Navighează în repository-ul principal.
2. Rulează comanda:
   ```bash
   git submodule add <URL-repo-submodul> <cale/relativă>
   ```
   Exemplu:
   ```bash
   git submodule add https://github.com/exemplu/proiect-submodul librarii/proiect-submodul
   ```

3. Commit schimbările:
   ```bash
   git commit -m "Adăugat submodul"
   ```

---

## Cum să dai update la un submodul?
1. Asigură-te că te afli în repository-ul principal și actualizează informațiile despre submodule:
   ```bash
   git submodule update --remote
   ```
2. Dacă ai mai multe submodule, poți actualiza toate submodulele în același timp:
   ```bash
   git submodule foreach git pull origin main
   ```
3. Commit noile schimbări:
   ```bash
   git add <cale/relativă/submodul>
   git commit -m "Actualizat submodul la ultima versiune"
   ```

---

## Cum să faci revert la o versiune mai veche a unui submodul?
1. Navighează în directorul submodulului:
   ```bash
   cd <cale/relativă/submodul>
   ```
2. Verifică versiunile disponibile:
   ```bash
   git log
   ```
3. Checkout la o versiune anterioară folosind hash-ul commitului dorit:
   ```bash
   git checkout <hash-commit>
   ```
4. Întoarce-te în repository-ul principal și salvează schimbările:
   ```bash
   cd ..
   git add <cale/relativă/submodul>
   git commit -m "Revenit la o versiune mai veche a submodulului"
   ```

---

## Alte comenzi utile pentru submodule
- **Inițializează submodulele după clonarea repository-ului principal:**
  ```bash
  git submodule update --init --recursive
  ```

- **Șterge un submodul:**
  1. Șterge referința din `.gitmodules` și din index:
     ```bash
     git rm --cached <cale/relativă/submodul>
     ```
  2. Șterge fișierele de pe disc:
     ```bash
     rm -rf <cale/relativă/submodul>
     ```
  3. Commit schimbările:
     ```bash
     git commit -m "Șters submodul"
     ```

---

Dacă ai întrebări sau întâmpini probleme, verifică documentația oficială Git: [Git Documentation](https://git-scm.com/doc)
