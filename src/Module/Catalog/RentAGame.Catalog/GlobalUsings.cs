/*
    Proje içerisindeki dosyalardaki bazı using bildirimleri GlobalUsings dosyasına alınmakta.
    Bunlar proje genelinde kullanılan using blokları.
    Bu ilgili kod dosyalarının daha okunabilir olmasını da sağlıyor.
*/
global using RentAGame.Shared.Domain;
global using RentAGame.Catalog.Games.Models;
global using RentAGame.Catalog.Games.Events;
global using System.Reflection;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using RentAGame.Catalog.Data;
global using RentAGame.Catalog.Data.Interceptors;
global using RentAGame.Shared.Data;
global using RentAGame.Catalog.Games.Dtos;
global using RentAGame.Shared.Mediator;
global using Mapster;
global using MediatR;
global using Microsoft.Extensions.Logging;
global using Carter;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Routing;
global using FluentValidation;