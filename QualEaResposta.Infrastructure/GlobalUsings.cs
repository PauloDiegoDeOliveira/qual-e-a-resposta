﻿global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Polly;
global using Polly.Retry;
global using QualEaResposta.Domain.Core.Interfaces.Repositories;
global using QualEaResposta.Domain.Core.Interfaces.Services;
global using QualEaResposta.Domain.Core.Notificacoes;
global using QualEaResposta.Domain.Enums;
global using QualEaResposta.Domain.Model;
global using QualEaResposta.Infrastructure.Extensions;
global using System.Linq.Expressions;
global using System.Net.Http.Headers;
global using System.Net.Http.Json;
global using System.Security.Claims;
global using System.Text.Json;