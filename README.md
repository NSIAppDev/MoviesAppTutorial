# Movie DB (Vue Web App)
This project is meant to provide an example application of a .Net Core, Vue web application
Technologies Used:
* [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/) 2.0
* [Vue.js](https://vuejs.org/) 2.5.X
* [NoDB](https://github.com/cloudscribe/NoDb) (for data storage)
* [SASS](https://sass-lang.com/) (for CSS development)
* [Webpack](https://webpack.js.org/) (for compiling and loading packages)
## Getting Starting
1. Download and Install Visual Studio Community Edition 
    1. Download from https://visualstudio.microsoft.com/
    2. Start Installation, using option for `ASP.NET and web development` package    
2. Setup Repository
    1. Start Visual Studio Community Edition
    2. Sign in with your Microsoft ID (it's free)
    2. Connect to repository
        1. From file menu, select `Team` then `Manage Connections`
        2. Under `Local Git Repositories` select `Clone`
        3. Use the URL from the `Clone or Download` button on this GIT page
        4. Select `Clone`
3. Run Solution
    1. Open `MoviesApp.Sln` on the Solution Explorer
    2. Hit `Cntl-F5` to Start without Debugging
        1. NOTE: The first time it is run, it will download and install supporting packages, this can take up to 10 minutes
        2. If you get a build error related to a "Missing binding" with node-sass, then create [follow these instructions](Tutorial/Node-SassError.md)
    3. The application should build and open in a browser.  It will display two movies.
    ![Initial Screen](Tutorial/images/FirstScreen.jpg?raw=true)
4. Start the [tutorial](Tutorial/Readme.md)
