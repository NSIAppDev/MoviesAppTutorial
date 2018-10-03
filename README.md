# Movie DB (Vue Web App)
This project is meant to provide an example application of a .Net Core, Vue web application
Technologies Used:
* .NET Core 2.0
* Vue 2.5.X
* NoDB (for data storage)
* SASS (for CSS development)
* webpack (for compiling and loading packages)
## Getting Starting
1. Download and Install Visual Studio Community Edition 
    1. Download from https://visualstudio.microsoft.com/
    2. Start Installation, using option for "ASP.NET and web development" package    
2. Setup Respository
    1. Start Visual Studio Community Edition
    2. Signin with your microsoft ID (it's free)
    2. Connect to respository
        1. From file menu, select "Team" then "Manage Connections"
        2. Under "Local Git Repositories" select "Clone"
        3. Use the URL from the "Clone or Download" button on this GIT page
        4. Select "Clone"
3. Run Solution
    1. Open "MoviesApp.Sln" on the Solution Explorer
    2. Hit F5 or the "IIS Express" button on the toolbar
        1. NOTE: The first time it is run, it will download and install supporting packages, this can take up to 10 minutes
        2. If you get a build error related to a "Missing building" with node-sass, then create a command window in your project directory and type "npm rebuild node-sass", run the command and rebuild
    3. The application should build and open in a browser.  It will have menu and copyright line.
4. Start the tutorial
