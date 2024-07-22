# Razor Pages Sample
A simple project using Razor pages, fluent migrator and LLBLGen to create Users page.
## Step 1 |  The Migrations
Used to create the database schema
- Update appsettings.json with your connection string
  
 ![Screenshot 2024-07-22 172821](https://github.com/user-attachments/assets/9555c8d6-277f-4c33-b834-d82badff7f94)

make sure that the database testDb is created in postgres
- Add Migrations Folder and then CreateInitialTable.cs
  
  ![Screenshot 2024-07-22 173226](https://github.com/user-attachments/assets/30491bff-472b-434b-b6f2-413479ec68a2)
  
- Add the fluentmigrator service in program.cs
  
 ![image](https://github.com/user-attachments/assets/6b9cb288-a3ed-4ea4-b682-c21037859934)


- Run this command in cmd Dotnet run --Up

## Step 2 | The ORM (LLBLGen)
- Create new project in LLBLGen Pro
- Connect to postgres database (testDb)
- Choose the desired tables
- Generate source code for desired entities
- Copy the generated files (Database Specific and Database Generic) and paste them in your project directory in visual studio
- Add LLBLGen runtime Configuration in program.cs
  
  ![Screenshot 2024-07-22 175928](https://github.com/user-attachments/assets/467eb3fb-458b-43d2-a585-a3ea2fdc27a7)


## Step 3 | Create User Model
- Create folder called Models
- Create User.cs class
  
![image](https://github.com/user-attachments/assets/2e91790f-e20c-4a75-bd07-d2c542467b87)

## Step 4 | Update Index.cshtml.cs class
- Inject IConfiguration into the constructor
  
![image](https://github.com/user-attachments/assets/a39d41e7-a340-4b01-beec-cba814417121)

- Add Properties
  
![image](https://github.com/user-attachments/assets/328072fd-f07a-45f3-8b5c-76f8e11250eb)

Users: A property to hold the list of UserEntity objects, which will be used to display users on the page.

NewUserName: A property bound to the form input for adding a new user.

- Edit OnGet method

  ![image](https://github.com/user-attachments/assets/7d05df04-009a-460d-9d91-4009a25ce4f2)
  
  This method is called when the page is requested via an HTTP GET. It loads the list of users by calling LoadUsers().

- Add OnPost method
  
  ![image](https://github.com/user-attachments/assets/71ba5ceb-e389-453c-8e14-8954304416af)

  This method handles form submissions to add a new user.
  - Connection String: Retrieves the connection string from configuration.
  - Validation: Checks if the connection string is set.
  - Data Access: Creates a DataAccessAdapter to interact with the database.
  - Create and Save User: Creates a new UserEntity with the provided NewUserName and saves it to the database.
  - Reload Users: Refreshes the list of users by calling LoadUsers().

- LoadUsers method
  
  ![image](https://github.com/user-attachments/assets/4cea6cca-cfb6-49a4-9060-c120ffbfe9f5)
  
  A private method to fetch users from the database.
  - Connection String: Retrieves the connection string from configuration.
  - Data Access: Creates a DataAccessAdapter to interact with the database.
  - Query Users: Uses LinqMetaData to query the User table and converts the result to a list.
  - Set Users Property: Assigns the list of users to the Users property.
    
## Step 5 | Update Index.cshtml
 - Table to fetch all users
   
 ![image](https://github.com/user-attachments/assets/c0a4bce7-b786-4ecc-ab4d-f2ddeb47c903)

- Form to add new users
  
  ![image](https://github.com/user-attachments/assets/30008610-3646-4eb9-ae0b-2c95df6a0ef5)

