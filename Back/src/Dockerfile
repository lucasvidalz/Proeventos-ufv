# Dockerfile localizado em Back/src/

# Estágio 1: Build da Aplicação
# Use a imagem do SDK do .NET 5.0
FROM mcr.microsoft.com/dotnet/sdk:5.0.402 AS build-env
WORKDIR /app

# Copie o arquivo de solução (.sln)
COPY ProEventos.sln ./

# Para ProEventos.API:
# A sua pasta no host é ProEventos.API, mas o .sln espera ProEventos.Api (conforme o erro)
COPY ProEventos.API/ProEventos.API.csproj ./ProEventos.API/

# Para os outros projetos (assumindo que o .sln os referencia com o mesmo casing dos nomes das pastas)
COPY ProEventos.Application/ProEventos.Application.csproj ./ProEventos.Application/
COPY ProEventos.Domain/ProEventos.Domain.csproj ./ProEventos.Domain/
COPY ProEventos.Persistence/ProEventos.Persistence.csproj ./ProEventos.Persistence/

COPY ProEventos.Tests/ProEventos.Tests.csproj ./ProEventos.Tests/

# Restaure as dependências para a solução (esta é a linha 20 que deu erro)
RUN dotnet restore "ProEventos.sln"

# Copie todo o restante do código fonte dos projetos para dentro do workdir /app
COPY ProEventos.API/ ./ProEventos.API/ 
COPY ProEventos.Application/ ./ProEventos.Application/
COPY ProEventos.Domain/ ./ProEventos.Domain/
COPY ProEventos.Persistence/ ./ProEventos.Persistence/
COPY ProEventos.Tests/ ./ProEventos.Tests/

# Publique a aplicação ProEventos.API
# O caminho para o csproj da API deve usar o nome da pasta criada no container (ProEventos.Api)
RUN dotnet publish "ProEventos.API/ProEventos.API.csproj" -c Release -o out

# Estágio 2: Imagem de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

# Copie a aplicação publicada do estágio de build
COPY --from=build-env /app/out .

# Defina a variável de ambiente para rodar em modo de produção
# e especifique as URLs que o Kestrel deve escutar.
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

# Exponha a porta 80 (ou a porta que sua aplicação usa internamente)
EXPOSE 80

ENTRYPOINT ["dotnet", "ProEventos.API.dll"]