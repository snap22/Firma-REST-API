# REST API pre firmu

## Spustenie
1. Stiahnutie projektu ([zip verzia](https://github.com/snap22/Firma-REST-API/archive/refs/heads/master.zip))
2. Vytvorenie databázy (spustenie skriptu v [SQL Scripts/database creation.sql](SQL%20Scripts/database%20creation.sql)
   - *pozn. databáza by sa mala volať RestDB, pretože tak je to nastavené aj v [nastaveniach projektu](FirmaRest/appsettings.json)*
3. (Voliteľné) Vygenerovanie dummy data pre databázu (spustenie skriptu [SQL Scripts/dummy data.sql](SQL%20Scripts/dummy%20data.sql))
4. Spustenie **FirmaRest.sln** v priečinku [FirmaRest](FirmaRest/)

Po spustení projektu bude stránka behať na  https://localhost:44390
<br/> <br/>


# Prehľad requestov
## Zamestnanci

|  | Request | Response |
| ------------- | ------------- | ------------- |
| **GET**  | /api/Employees  | Vráti všetkých zamestnancov nachádzajúcich sa v databáze  |
| **GET**  | /api/Employees/{id}  | Vráti zamestnanca s konkrétnym id  |
| **GET**  | /api/Employees/{id}/Unemployed  | Vráti všetkých zamestnancov, ktorí nie sú v žiadnej firme  |
| **POST**  | /api/Employees  | Vytvorí nového zamestnanca so zadanými údajmi |
| **PUT** | /api/Employees/{id}  | Upraví zamestnanca s daným id |
| **DELETE** | /api/Employees/{id}  | Vymaže zamestnanca s daným id |

#### Schéma

```javascript
{
  "id": 0,
  "firstName": "string",
  "lastName": "string",
  "title": "string",
  "email": "string",
  "contact": "string",
  "companyId": 0
}
```
  
  
## Firma

|  | Request | Response |
| ------------- | ------------- | ------------- |
| **GET**  | /api/Companies  | Vráti všetky firmy nachádzajúce sa v databáze  |
| **GET**  | /api/Companies/{id}  | Vráti firmu s konkrétnym id  |
| **GET**  | /api/Companies/{id}/Employees  | Vráti všetkých zamestnancov firmy s konkrétnym id  |
| **GET**  | /api/Companies/{id}/Divisions  | Vráti všetky divízie v danej firme s konkrétnym id  |
| **POST**  | /api/Companies  | Vytvorí novú firmu so zadanými údajmi |
| **PUT** | /api/Companies/{id}  | Upraví firmu s daným id |
| **DELETE** | /api/Companies/{id}  | Vymaže firmu s daným id |

#### Schéma

```javascript
{
  "id": 0,
  "title": "string",
  "code": "string",
  "director": 0
}
```  

## Divízia

|  | Request | Response |
| ------------- | ------------- | ------------- |
| **GET**  | /api/Divisions  | Vráti všetky divízie nachádzajúce sa v databáze  |
| **GET**  | /api/Divisions/{id}  | Vráti divíziu s konkrétnym id  |
| **GET**  | /api/Divisions/{id}/Projects  | Vráti všetky projekty v danej divízii s konkrétnym id  |
| **POST**  | /api/Divisions  | Vytvorí novú divíziu so zadanými údajmi |
| **PUT** | /api/Divisions/{id}  | Upraví divíziu s daným id |
| **DELETE** | /api/Divisions/{id}  | Vymaže divíziu s daným id |

#### Schéma

```javascript
{
  "id": 0,
  "title": "string",
  "code": "string",
  "leader": 0,
  "companyId": 0
}
```

## Projekt

|  | Request | Response |
| ------------- | ------------- | ------------- |
| **GET**  | /api/Projects  | Vráti všetky projekty nachádzajúce sa v databáze  |
| **GET**  | /api/Projects/{id}  | Vráti projekt s konkrétnym id  |
| **GET**  | /api/Projects/{id}/Departments  | Vráti všetky oddelenia v danom projekte s konkrétnym id  |
| **POST**  | /api/Projects  | Vytvorí nový projekt so zadanými údajmi |
| **PUT** | /api/Projects/{id}  | Upraví projekt s daným id |
| **DELETE** | /api/Projects/{id}  | Vymaže projekt s daným id |

#### Schéma

```javascript
{
  "id": 0,
  "title": "string",
  "code": "string",
  "leader": 0,
  "divisionId": 0
}
```  
## Oddelenie

|  | Request | Response |
| ------------- | ------------- | ------------- |
| **GET**  | /api/Departments  | Vráti všetky oddelenia nachádzajúce sa v databáze  |
| **GET**  | /api/Departments/{id}  | Vráti oddelenie s konkrétnym id  |
| **POST**  | /api/Departments  | Vytvorí nové oddelenie so zadanými údajmi |
| **PUT** | /api/Departments/{id}  | Upraví oddelenie s daným id |
| **DELETE** | /api/Departments/{id}  | Vymaže oddelenie s daným id |

#### Schéma

```javascript
{
  "id": 0,
  "title": "string",
  "code": "string",
  "leader": 0,
  "projectId": 0
}
```
