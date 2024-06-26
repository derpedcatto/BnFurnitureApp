﻿using BnFurniture.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BnFurniture.Application.Abstractions;

public interface IHandlerContext
{
    ApplicationDbContext DbContext { get; }
    ILogger Logger { get; }
    HttpContext HttpContext { get; }
}

public class HandlerContext(
    ApplicationDbContext dbContext,
    ILogger<HandlerContext> logger,
    IHttpContextAccessor httpContextAccessor) : IHandlerContext
{
    public ApplicationDbContext DbContext { get; private set; } = dbContext;
    public ILogger Logger { get; private set;} = logger;
    public HttpContext HttpContext { get; private set; } = httpContextAccessor.HttpContext!;
}
