# laboratoriniai

Release (01):

GetStudentDataFromConsole(ref StudentsList);

Funkcija leidžia vartotojui suvesti namų darbų pažymius į konsolę. Po kiekvieno pažymio įvedimo yra paklausiama:
"Would you like to enter another one ? y/n". 

Į konsolę įvedus 'y' - programa leidžia įvesti dar vieną pažymį.
Į konsolę įvedus 'n' - programa visus pažymius patalpina į atitinkamo dydžio masyvą.

Funkcijos pabaigoje yra grąžinamas List tipo masyvas.


FinalPointsCalculator(ref StudentsList);

Programa, perdavus List tipo sąrašą per parametrus leidžia pasirinkti:

1 - Išvedama studento vardas, pavarde ir galutinis balas pagal pažymių vidurkį.
2 - Išvedama studento vardas, pavarde ir galutinis balas pagal pažymių medianą.

--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////--

Release (02)

versija leidžia nuskaityti studentus iš txt failo.


ReadStudentsFromFile(ref StudentsList, "Students.txt");

Funkcijos parametruose nurodyto txt failo kiekviena eilutė yra nuskaitoma. Iš gautų duomenų yra sukuriami Student objektai,
patalpinami į StudentList sarąšą. Vėliau sąrašas yra surikiuojamas pagal studento vardą ir grąžinamas.

--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////--


Release (03)

Pridėtas try/catch blokas prie txt failo nuskaitymo. Jei nuskaitymas nepavyksta - varotojas
informuojamas, kad patikrintų ar teisingai nurodė failo kelią.

--///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////--

Final release (v04 + v05 + v1)

Generate4StudentsFiles() - funkcija sugeneruoja 4 atskirus txt failus, talpinančius atitinkamai 10000, 100000, 1000000 ir
10000000 įrašų.

GenerateStudentsFile(string fileName, int amount) - funkcija leidžia per parametrus paduoti failo pavadinimą ir norimą įrašų
kiekį. Programa sugeneruoja įrašus faile.


ReadStudentsFromFile(ref List<Student> StudentsList, string txtFileName) - Funkcija, nurodžius txt failo pavadinimą ir pavavus
List sąrašą - nuskaito failo įrašus ir grąžina patalpintame sąraše.

SortingStudents(List<Student> StudentsList) - funkcija paduotą sąrašą išskirsto. Galimi du skirtymo būdai.

1 - programa skaičiuoja galutinį studento balą ir pagal gautą reikšmę perkelia studentą į passed arba failed txt failą.
2 - programa skaičiuoja galutinį studento balą ir pagal gautą reiktšmę perkelia į atskirą failed.txt failą tik 5 balų
nesurinkusius studentus ir juos pašalina iš sąrašo. Sąraše lieka tik teigiamą galutinį balą turintys studentai.


Generavimo, rūšiavimo, skaitymo ir rašymo funkcijų vykdymo laikas buvo skaičiuojamas pasinaudojant System.Diagnostics.Stopwatch()
funkcija.


