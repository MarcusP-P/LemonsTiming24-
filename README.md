# LemonsTiming24

Timing for a certain 24 hour car race

## Re-building templates

If you ever need to re-build the templates (such as upgrading between .net
previews), You can use `dotnet new blazorwasm --hosted true --auth None --name LemonsTiming24 --output .`, which will generate a new project.

Once the Blazor WASM templates have been updated, you can update the API components by using `dotnet new webapi --auth None --name LemonsTiming24.Server --output Server --force`.

## Dealing with UTF8 BOM

Some of the .Net generated template files have a pesky UTF* BOM at the start of the file. To find these, you can use `find . -type f -exec grep -Hl "^$(printf '\xef\xbb\xbf')" {} \;`. To remove them, use `find . -type f -exec sed -i  "s/^$(printf '\xef\xbb\xbf')//" {} \;`, but make sure you don't touch the .git directory...


## Configuring for development

### User Secrets

The SQL connection string and base URL are not kept in the source. Instead you will need to use [user secrets](https://docs.microsoft.com/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows) to manage them. Use ``dotnet user-secrets init  --project Server\`` to create the user secrets store for the application.

The following secrets need to be configured:
* Base URL  
    ``dotnet user-secrets set "Timing:BaseUrl" "<<base URL>>" --project Server``
* Raw Database  
 ``dotnet user-secrets set "ConnectionStrings:TimingRawContext" "<<Database Connection String>>" --project Server``  

### Setting up the LocalDB database

To create the database:
* In Visual Studio, on the menu bar, choose **View**, **SQL Server Object Explorer**.
* Open **SQL Server**.
* Open **(localdb)\MSSQLLocalDB**.
* Right click on **Databases**.
* Select **Add New Database**.
* Give the database a name.
* Use the connection string to setup the connection string ``ConnectionStrings:TimingRawContext" "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LemonsTiming;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False``.
