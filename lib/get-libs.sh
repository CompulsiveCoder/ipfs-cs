wget http://nuget.org/nuget.exe

mozroots --import --sync

mono nuget.exe install nunit -version 2.6.4
mono nuget.exe install nunit.runners -version 2.6.4
