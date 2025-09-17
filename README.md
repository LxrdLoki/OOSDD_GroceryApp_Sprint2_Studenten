# GroceryApp sprint2 

## Docentversie  
In deze versie zijn de wijzigingen doorgevoerd en is de code compleet.  

## Studentversie:  
### UC04 Kiezen kleur boodschappenlijst  
Is compleet.

### UC05 Product op boodschappenlijst plaatsen:   
- `GetAvailableProducts()`  
	De header van de functie bestaat maar de inhoud niet.  
	Zorg dat je een lijst maakt met de beschikbare producten (dit zijn producten waarvan nog voorraad bestaat en die niet al op de boodschappenlijst staat).  
- `AddProduct()`   
	Zorg dat het gekozen beschikbare product op de boodschappenlijst komt (door middel van de GroceryListItemsService).  

### UC06 Inloggen  
Een collega is ziek maar heeft al een deel van de inlogfunctionaliteit gemaakt.  
Dit betreft het Loginscherm (LoginView) met bijbehorend ViewModel (LoginViewModel),  
maar ook al een deel van de authenticatieService (AuthServnn,mnmice in Grocery.Core),  
de clientrepository (ClientRepository in Grocery.Core.Data)  
en de client class (Client in Grocery.Core).  
De opdracht is om zelfstandig de login functionaliteit te laten werken.  

*Stappenplan:*  
1. Begin met de Client class en zorg dat er gebruik wordt gemaakt van Properties.  
2. In de ClienRepository wordt nu steeds een vaste client uit de lijst geretourneerd. Werk dit uit zodat de juiste Client wordt geretourneerd.  
3. Werk de klasse AuthService verder uit, zodat daadwerkelijk de controle op het ingevoerde password wordt uitgevoerd.
4. Zorg dat de LoginView.xaml wordt toegevoegd aan het Grocery.App project in de Views folder (Add ExistingItem). De file bevindt zich al op deze plek, maar wordt nu niet meegecompileerd.  
5. In MauiProgramm class van de Grocery.App staan de registraties van de AuthService en de LoginView in comment --> haal de // weg.  
6. In App.xaml.cs staat /*LoginViewModel viewModel*/ haal hier /* en */ weg, zodat het LoginViewModel beschikbaar komt.  
7. In App.xaml.cs staat //MainPage = new LoginView(viewModel); Haal hier de // weg en zet de regel erboven in commentaar, zodat AppShell wordt uitgeschakeld.  
8. Uncomment de route naar het Login scherm in AppShell.xaml.cs: //Routing.RegisterRoute("Login", typeof(LoginView)); 


## GitFlow beschrijving
Ik maak gebruik van een eenvoudige branchin-strategie gebaseerd op GitFlow-principes.
De kernregels van deze branching strategie gaan als volgt.

* Main: de mainbranch bevat alleen geteste, productieklaar code. Merges van dev gebeuren hier na volledige validatie.
* dev: Integratiebranch voor het samenvoegen van alle features/ bugfixes. Hier draaien de pipelines vor tests en builds.

# Feature branches:
* Naamconventie gaat als volgt: feature/<FEATURECODE> (bijv. feature/UC1).
* Doel: Nieuwe functionaliteiten ontwikkelen.
* Workflow: Nieuwe branch gebasseerd op dev, na het implementeren van de functionaliteit merge naar dev via een pull request.

# Bugfix branches:
* Naamconventie gaat als volgt: bugfix/<BUGBESCHRIJVING> (bijv. bugfix/YML1)
* Doel: oplossen van bugs
* Workflow: Nieuwe branch gebasseerd op dev, na het oplossen van de bug merge naar dev via een pull request.

# Workflow
1. Maak een nieuwe branch gebasseerd op dev (bijv. git checkout -b feature/UC1 dev)
2. Ontwikkel en commit
* Gebruik conventionele commit berichten
* Push regelmatig
3. Merge naar dev
* Maak een pullrequest van je feature/bug branch naar dev
* Review (door jezelf tijdens deze sprints)
4. Integratie en testen
* Na het mergen naar dev, triggeren de pipelines automatisch
* Kijk op het einde zelf nog goed door de code heen voordat er een merge request naar main gaat
5. Release naar main
* Wanneer dev goed is en de pipelines zijn geslaagt, merge dev naar main via een pullrequest
* Tag de release (bijv. v0.0.1  commando: git tag v0.0.1)

# Extra informatie
* Geen directe pushes naar main of dev, altijd via pull requests werken.
* Verwijder een feature of bugfix branch na een pull request

