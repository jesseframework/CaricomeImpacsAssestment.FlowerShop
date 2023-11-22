## Instructions to Run the Project

Once you download the project, please follow these instructions to run it:

1. Install .NET Core SDK: Ensure that your.NET Core versions are up to date, ranging from 2.0 to 8.0.

2. Install Yarn and Node.js: Make sure you have Yarn and Node.js installed on your system before trying to install ABP.

3. Set up the MySQL database connection string to your Windwos MySQL Server: In the `appconfiguration.json` file, configure the database connection string for DB migration and the web project.

4. Install the ABP library running the project:
   - From the web project directory, open a .NET command prompt.
   - Run the following command to install ABP libraries:
     ```
     abp install-libs
     ```

5. Run ABP Project:
   - Install NPM Packages:
     ```
     npm install
     ```
   - Add ABP SignalR Package:
     ```
     abp add-package Volo.Abp.AspNetCore.SignalR
     
     ```
   - Install SignalR:
     ```
     yarn add @abp/signalr
     ```

6. For more information on ABP CLI, you can refer to the [ABP CLI Documentation](https://docs.abp.io/en/abp/latest/CLI).

7. For UI customization using the Basic Theme, you can check out the [ABP Basic Theme Documentation](https://docs.abp.io/en/abp/latest/UI/AspNetCore/Basic-Theme).
 
