## Del 5 Efter Scaffold
### 1. Undersök koden som har skapats:
#### a. Vilka metoder har genererats?
I den genererade koden finns följande metoder:
- I __ProductsController.cs__ 
	- konstructor   
		- Två GetProduct metoder, en som returnerar en samling produkter och en som enbart hämtar en specifik produkt. 

			- ``` Task<ActionResult<IEnumerable<Product>>> GetProduct()  ```
			- ```Task<ActionResult<Product>> GetProduct(int id)```
	- ``` Task<IActionResult> PutProduct(int id, Product product) ```: En PutProduct metod för att ersätta en produkt. 
	- ``Task<ActionResult<Product>> PostProduct(Product product)``: En PostProduct metod för att skapa en produkt. 
	- ``Task<IActionResult> DeleteProduct(int id)``: En DeleteProduct metod for att ta bort en produkt. 
	- ``bool ProductExists(int id)``: Och slutligen metoden ProductExists som kontrollerar om en produkt finns. 
- I __StorageApiContextModelSnapshot.cs__
	-  ``void BuildModel(ModelBuilder modelBuilder)	``: Som bygger databasmodellen.

#### b. Hur används StorageContext?
- StorageContext hanterar kommunikationen mellan databasen och applikationen. Den länkar samman entitetsklasser med deras motsvarande tabeller i databasen.
Varje tabell representeras som en separat egenskap, DBSet\<T>, i StorageContext, vilket gör att varje entitet kan hanteras individuellt i koden.


#### c. Hur fungerar CreatedAtAction, Ok, NotFound osv?
-	ActionResult används för att ge feedback på de requests som API:n (servern) hanterar. Denna feedback uttrycks genom 
	HTTP-statuskoder, som delas in i olika nivåer beroende på vad som hände med klientens förfrågan. Metoder som CreatedAtAction, 
	Ok, NotFound med flera används för att skicka dessa svar till klienten på ett strukturerat sätt.

De vanligaste statuskoderna delas in i följande kategorier:
- 100-serien: Informativa svar
- 200-serien: Lyckade (framgångsrika) svar
- 300-serien: Omdirigeringar
- 400-serien: Klientfel (t.ex. felaktig begäran)
- 500-serien: Serverfel (t.ex. interna fel)

- __CreatedAtAction__: Skickar en 201 Created-statuskod, vilket används när en ny resurs har skapats. Den inkluderar ofta en länk till den nyligen skapade resursen, så att klienten kan hämta den direkt.
- __Ok__: Skickar en 200 OK-statuskod. Detta indikerar att förfrågan lyckades. Ett valfritt svarsinnehåll kan också skickas med (kan också vara tom).
- __NotFound__: Skickar en 404 Not Found-statuskod. Detta innebär att resursen som efterfrågades inte kunde hittas – t.ex. om en angiven URL eller ID inte matchar något i databasen.


### 2. Använd Postman för att testa dina endpoints:

#### a. POST /api/products – skapa produkt
- När man skickar en HTTP POST-förfrågan till `/api/products` måste följande fält anges: **Name, Category, Shelf och Description**, 
eftersom dessa är markerade som obligatoriska (`nullable: false`). Saknas något av dessa returneras ett **400 Bad Request**.
Fälten **Price** och **Count** är också obligatoriska, men eftersom de är värdetyper (`int`) kommer de automatiskt att få värdet `0` om 
inget anges, såvida inte annan validering finns. I så fall kommer en produkt att skapas med dessa värden. ID genereras automatiskt 
med en auto inkrement.

	- Utan Name = 400 error
	- Utan Price = Price fick värdet 0: 200 Ok 
	- Utan Category = 400 error
	- Utan Shelf = 400 error
	- Utan Count = Count fick värdet 0: 200 Ok 
	- Utan Discription = 400 error

  
#### b. GET /api/products – hämta alla produkter
- Hämtar alla värden i tabellen `Product` från databasen.

#### c. GET /api/products/{id} – hämta en produkt
- Hämtar en specifik produkt baserat på det angivna ID:t. Om produkten inte finns returneras en **404 Not Found**.
 