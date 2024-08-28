﻿global using Asp.Versioning;
global using Asp.Versioning.ApiExplorer;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using Hangfire;
global using Hangfire.MemoryStorage;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using Microsoft.AspNetCore.SignalR;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Options;
global using Microsoft.OpenApi.Any;
global using Microsoft.OpenApi.Models;
global using Newtonsoft.Json;
global using Newtonsoft.Json.Converters;
global using QualEaResposta.Api.Configuration;
global using QualEaResposta.Api.Controllers;
global using QualEaResposta.Api.Hubs;
global using QualEaResposta.Application.Dtos.ApiVersionDto;
global using QualEaResposta.Application.Dtos.Pergunta;
global using QualEaResposta.Application.Interfaces;
global using QualEaResposta.Application.Mappers;
global using QualEaResposta.Application.Services;
global using QualEaResposta.Domain.Core.Interfaces.Repositories;
global using QualEaResposta.Domain.Core.Interfaces.Services;
global using QualEaResposta.Domain.Services;
global using QualEaResposta.Infrastructure.Data;
global using QualEaResposta.Infrastructure.Data.Repositories;
global using QualEaResposta.Infrastructure.Services;
global using Serilog;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using System.ComponentModel.DataAnnotations;
global using System.Globalization;
global using System.Net.Mime;
global using System.Reflection;
global using System.Text.Json.Serialization;
global using ILogger = Serilog.ILogger;