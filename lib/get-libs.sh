NUGET_FILE="nuget.exe"
 
if [ ! -f "$NUGET_FILE" ];
then
    wget http://nuget.org/nuget.exe
fi

mozroots --import --sync

mono nuget.exe install nunit -version 2.6.4
