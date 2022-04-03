$SolutionPath = "$($env:USERPROFILE)\Projects\Firewatch\"

dotnet restore "$($SolutionPath)\src\Firewatch\Firewatch.csproj"

New-Item -Path "$($SolutionPath)\src\Firewatch\wwwroot\" -Name "lib" -ItemType "directory"
New-Item -Path "$($SolutionPath)\src\Firewatch\wwwroot\lib\" -Name "bootstrap" -ItemType "directory"
New-Item -Path "$($SolutionPath)\src\Firewatch\wwwroot\lib\bootstrap\" -Name "css" -ItemType "directory"
New-Item -Path "$($SolutionPath)\src\Firewatch\wwwroot\lib\bootstrap\" -Name "js" -ItemType "directory"
New-Item -Path "$($SolutionPath)\src\Firewatch\wwwroot\lib\" -Name "jquery" -ItemType "directory"

Copy-Item -Path "$($env:USERPROFILE)\.nuget\packages\bootstrap\4.0.0-beta\content\Content\*" -Destination "$($SolutionPath)\src\Firewatch\wwwroot\lib\bootstrap\css" -Recurse -Force
Copy-Item -Path "$($env:USERPROFILE)\.nuget\packages\bootstrap\4.0.0-beta\content\Scripts\*" -Destination "$($SolutionPath)\src\Firewatch\wwwroot\lib\bootstrap\js" -Recurse -Force
Copy-Item -Path "$($env:USERPROFILE)\.nuget\packages\jquery\3.2.1\Content\Scripts\*" -Destination "$($SolutionPath)\src\Firewatch\wwwroot\lib\jquery" -Recurse -Force
Copy-Item -Path "$($env:USERPROFILE)\.nuget\packages\popper.js\1.11.0\*.js" -Destination "$($SolutionPath)\src\Firewatch\wwwroot\lib\bootstrap\js" -Recurse -Force
Copy-Item -Path "$($env:USERPROFILE)\.nuget\packages\popper.js\1.11.0\*.map" -Destination "$($SolutionPath)\src\Firewatch\wwwroot\lib\bootstrap\js" -Recurse -Force

dotnet build "$($SolutionPath)\src\Firewatch\Firewatch.csproj" --configuration Debug