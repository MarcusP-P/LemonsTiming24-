# LemonsTiming24

Timing for a certain 24 hour car race

## Re-building templates

If you ever need to re-build the templates (such as upgrading between .net
previews), You can use `dotnet new blazorwasm --hosted true --auth None --name LemonsTiming24 --output .`, which will generate a new project.

Once the Blazor WASM templates have been updated, you can update the API components by using `dotnet new webapi --auth None --name LemonsTiming24.Server --output Server --force`.

## Dealing with UTF8 BOM

Some of the .Net generated template files have a pesky UTF* BOM at the start of the file. To find these, you can use `find . -type f -exec grep -Hl "^$(printf '\xef\xbb\xbf')" {} \;`. To remove them, use `find . -type f -exec sed -i  "s/^$(printf '\xef\xbb\xbf')//" {} \;`, but make sure you don't touch the .git directory...

## Configuring for development

The SQL connection string and base URL are not kept in the source. Instead you will need to use [user secrets](https://docs.microsoft.com/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows) to manage them. Use ``dotnet user-secrets init  --project Server\`` to create the user secrets store for the application.

* To store the secret for the base URL, use ``dotnet user-secrets set "Timing:BaseUrl" "base URL" --project Server``