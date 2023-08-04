# Cinema
----
A site for buying movie tickets to various cinemas

----
The first thing to do is to specify the name of the server and the name of the database in the appsettings.json file

![Untitled](https://github.com/RuslanPidhainyi/AppCinema/assets/136593314/9ae5b41b-e96d-49c8-b719-8d10ea2b4272)

----
Install the package driver for the visual studio:
- Microsoft.EntityFrameworkCore			
- Microsoft.EntityFrameworkCore.SqlServer 		 
- Microsoft.EntityFrameworkCore.Sqlite 		
- Microsoft.EntityFrameworkCore.Tools			
- Microsoft.AspNetCore.Identity.EntityFrameworkCore	
- Microsoft.VisualStudio.Web.CodeGeneration.Design

----

Then in the console meneger packet using the commands:
- add-migration InitialCreate -context AppDbContext
- update-database -context AppDbContext
- add-migration Identity_Adde -context AppDbContext
- update-database -context AppDbContext

---
Appearance of registration

https://github.com/RuslanPidhainyi/AppCinema/assets/136593314/60bebc28-39f7-4b2d-a3d5-e63e968e4ce6

----
Page appearance, user side

https://github.com/RuslanPidhainyi/AppCinema/assets/136593314/b22f7f5d-212b-4bae-9071-2d201c21c43a

----
Page appearance, admin side:

- Add Movie

https://github.com/RuslanPidhainyi/AppCinema/assets/136593314/d8746a92-d1f2-4e7f-b816-4ebc421084bf


- Edit Movie 

https://github.com/RuslanPidhainyi/AppCinema/assets/136593314/1e8efbaa-9426-48e2-a1f0-444825484f55


- Add Actor (also add Producer and Cinema)

https://github.com/RuslanPidhainyi/AppCinema/assets/136593314/b6a780fe-1ee8-4b77-8b22-042129748121


- Edit Cinema (also edit Actro and Producer)

https://github.com/RuslanPidhainyi/AppCinema/assets/136593314/7954d30c-65ef-4f6a-82cf-2f12364ae780








