FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Bootstrapper/Hotelio.Bootstrapper/Hotelio.Bootstrapper.csproj", "src/Bootstrapper/Hotelio.Bootstrapper/"]
COPY ["src/Modules/Booking/Hotelio.Modules.Booking.Infrastructure/Hotelio.Modules.Booking.Infrastructure.csproj", "src/Modules/Booking/Hotelio.Modules.Booking.Infrastructure/"]
COPY ["src/Modules/Booking/Hotelio.Modules.Booking.Domain/Hotelio.Modules.Booking.Domain.csproj", "src/Modules/Booking/Hotelio.Modules.Booking.Domain/"]
COPY ["src/Modules/Booking/Hotelio.Modules.Booking.Shared/Hotelio.Modules.Booking.Shared.csproj", "src/Modules/Booking/Hotelio.Modules.Booking.Shared/"]
COPY ["src/Shared/Hotelio.Shared/Hotelio.Shared.csproj", "src/Shared/Hotelio.Shared/"]
COPY ["src/Modules/Booking/Hotelio.Modules.Booking.Application/Hotelio.Modules.Booking.Application.csproj", "src/Modules/Booking/Hotelio.Modules.Booking.Application/"]
COPY ["src/CrossContext/Hotelio.CrossContext.Contract/Hotelio.CrossContext.Contract.csproj", "src/CrossContext/Hotelio.CrossContext.Contract/"]
COPY ["src/Modules/Booking/Hotelio.Modules.Booking.Api/Hotelio.Modules.Booking.Api.csproj", "src/Modules/Booking/Hotelio.Modules.Booking.Api/"]
COPY ["src/Modules/HotelManagement/Hotelio.Modules.HotelManagement.Api/Hotelio.Modules.HotelManagement.Api.csproj", "src/Modules/HotelManagement/Hotelio.Modules.HotelManagement.Api/"]
COPY ["src/Modules/Availability/Hotelio.Modules.Availability.Infrastructure/Hotelio.Modules.Availability.Infrastructure.csproj", "src/Modules/Availability/Hotelio.Modules.Availability.Infrastructure/"]
COPY ["src/Modules/Availability/Hotelio.Modules.Availability.Application/Hotelio.Modules.Availability.Application.csproj", "src/Modules/Availability/Hotelio.Modules.Availability.Application/"]
COPY ["src/Modules/Availability/Hotelio.Modules.Availability.Domain/Hotelio.Modules.Availability.Domain.csproj", "src/Modules/Availability/Hotelio.Modules.Availability.Domain/"]
COPY ["src/Modules/Availability/Hotelio.Modules.Availability.Api/Hotelio.Modules.Availability.Api.csproj", "src/Modules/Availability/Hotelio.Modules.Availability.Api/"]
RUN dotnet restore "src/Bootstrapper/Hotelio.Bootstrapper/Hotelio.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/src/Bootstrapper/Hotelio.Bootstrapper"
RUN dotnet build "Hotelio.Bootstrapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hotelio.Bootstrapper.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hotelio.Bootstrapper.dll"]
