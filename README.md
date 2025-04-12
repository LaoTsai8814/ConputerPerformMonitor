#ConputerPerformMonitor
ConputerPerformMonitor is a Windows desktop application developed using WPF (.NET) that monitors and logs system performance metrics, including CPU, GPU, and RAM usage. It offers both manual and automatic logging capabilities, allowing users to track system performance over time.​

##Features
Real-time Monitoring: Displays current CPU, GPU, and RAM usage.​

Manual Logging: Allows users to manually log performance data at any time.​(Developing)

Automatic Logging: Enables scheduled logging based on user-defined intervals.​(Developing)

SQLite Integration: Stores logged data in SQLite databases for persistent storage.​

User-Friendly Interface: Intuitive UI built with WPF for ease of use.​

##Installation
Clone the Repository:

Open in Visual Studio:

Open the solution file ConputerPerformMonitor.sln in Visual Studio.

Restore NuGet Packages:

Visual Studio should automatically restore the required NuGet packages. If not, restore them manually via Tools > NuGet Package Manager > Manage NuGet Packages for Solution.

##Build and Run:

Build the solution (Build > Build Solution).

Run the application (Debug > Start Debugging).​

##Dependencies
.NET Framework: Ensure you have the appropriate .NET Framework installed to run WPF applications.​

System.Data.SQLite: Used for interacting with SQLite databases.​

LibreHardwareMonitor:  Library for accessing hardware sensors to retrieve performance metrics.​
