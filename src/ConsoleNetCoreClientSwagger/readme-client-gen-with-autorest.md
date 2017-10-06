# Installing AutoRest 

Installing AutoRest on Windows, MacOS or Linux involves two steps:

1. __Install [Node.js](https://nodejs.org/en/)__ (7.10.0 or greater)

    You can install the [Node Version Manager for Windows](https://github.com/coreybutler/nvm-windows)
    
    Install the latest release of the [Node Version Manager](https://github.com/coreybutler/nvm-windows/releases/download/1.1.5/nvm-setup.zip) and then run the following commands:
    
    `nvm install 7.10.0`<br>`nvm use 7.10.0`

2. __Install AutoRest__ using `npm`

  ``` powershell
  # Depending on your configuration you may need to be elevated or root to run this. (on OSX/Linux use 'sudo' )
  npm install -g autorest
  ```

### Updating AutoRest
  To update AutoRest if you have previous versions installed, please run:
    
  ``` powershell
  autorest --latest
  ``` 
or 
  ```powershell
  # Removes all other versions and installs the latest
  autorest --reset
  ```
  For more information, run  `autorest --help`

# Generate C# client

The client was generated from the Web API Swagger file, using [Autorest](https://github.com/Azure/autorest).

1. Run the Web API
2. Open a powershell prompt on the ConsoleNetCoreClientSwagger root folder
3. Generate the C# code executing this command:

   `autorest --input-file=http://localhost:57479/swagger/v1/swagger.json --csharp --namespace=AspNetCoreSwagger.Sdk --output-folder=AspNetCoreSwagger.Sdk`

4. Install the nuget package `Microsoft.Rest.ClientRuntime`