FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /solution

COPY webapi-with-test.webapi/*.csproj ./webapi-with-test.webapi/  
RUN ls webapi-with-test.webapi/  
RUN dotnet restore ./webapi-with-test.webapi/webapi-with-test.webapi.csproj

COPY webapi-with-test.test/*.csproj ./webapi-with-test.test/  
RUN dotnet restore ./webapi-with-test.test/webapi-with-test.test.csproj

COPY . .

RUN dotnet test ./webapi-with-test.test/webapi-with-test.test.csproj

RUN dotnet publish ./webapi-with-test.webapi/webapi-with-test.webapi.csproj --output /out/ --configuration Release

FROM microsoft/aspnetcore:2.0

WORKDIR /app  
COPY --from=build-env /out .  
ENTRYPOINT ["dotnet", "webapi-with-test.webapi.dll"]