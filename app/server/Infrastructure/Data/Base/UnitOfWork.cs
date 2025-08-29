﻿using Articly.Core.Domain.Base;
using Articly.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Articly.Infrastructure.Data.Base;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext dbContext;
    
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;        
    }

    public async Task Commit()
    {
        await dbContext.SaveChangesAsync();
    }

    public void Rollback()
    {
        dbContext.Dispose();
    }
}


