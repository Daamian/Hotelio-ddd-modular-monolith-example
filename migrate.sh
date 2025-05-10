#!/bin/bash

echo "▶ Migrating ResourceDbContext..."
dotnet ef database update --startup-project src/Bootstrapper/Hotelio.Bootstrapper/Hotelio.Bootstrapper.csproj --context ResourceDbContext
echo "▶ Migrating ReservationDbContext..."
dotnet ef database update --startup-project src/Bootstrapper/Hotelio.Bootstrapper/Hotelio.Bootstrapper.csproj --context ReservationDbContext
echo "▶ Migrating HotelDbContext..."
dotnet ef database update --startup-project src/Bootstrapper/Hotelio.Bootstrapper/Hotelio.Bootstrapper.csproj --context HotelDbContext
echo "▶ Migrating PricingDbContext..."
dotnet ef database update --startup-project src/Bootstrapper/Hotelio.Bootstrapper/Hotelio.Bootstrapper.csproj --context PricingDbContext

echo "▶ Completed..."
