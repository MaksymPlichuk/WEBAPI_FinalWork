FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY ["WEBAPI_FinalWork.API/WEBAPI_FinalWork.API.csproj", "WEBAPI_FinalWork.API/"]
COPY ["WEBAPI_FinalWork.BLL/WEBAPI_FinalWork.BLL.csproj", "WEBAPI_FinalWork.BLL/"]
COPY ["WEBAPI_FinalWork.DAL/WEBAPI_FinalWork.DAL.csproj", "WEBAPI_FinalWork.DAL/"]

RUN dotnet restore ./WEBAPI_FinalWork.API/WEBAPI_FinalWork.API.csproj

COPY . ./
RUN dotnet publish -c Release -o out ./WEBAPI_FinalWork.API/WEBAPI_FinalWork.API.csproj

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "WEBAPI_FinalWork.API.dll"]