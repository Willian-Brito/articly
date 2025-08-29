﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Articly.Core.Domain.Base;
using Articly.Infrastructure.Data.Context;
using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Articly.Infrastructure.Data.Base;

public abstract class BaseRepository<TEntity, TModel> : IBaseRepository<TEntity, TModel>
    where TEntity : class
    where TModel : class
{
    #region Properties
    protected readonly IMapper mapper;
    protected readonly DbSet<TModel> dbSet;
    protected readonly IDbConnection dbConnection;
    protected readonly ApplicationDbContext context;
    #endregion

    #region Constructor
    public BaseRepository(ApplicationDbContext context, IMapper mapper, IDbConnection dbConnection)
    {
        this.context = context;
        this.dbSet = context.Set<TModel>();
        this.mapper = mapper;
        this.dbConnection = dbConnection;
    }
    #endregion

    #region Methods

    #region Queries (Dapper)

    #region GetPaged
    // public async Task<IEnumerable<TEntity>> GetPaged(int pageNumber, int pageLimit)
    // {
    //     pageNumber = pageNumber == 0 ? 0 : pageNumber - 1;

    //     var models = await dbSet.Skip(pageNumber * pageLimit).Take(pageLimit).ToListAsync();
    //     var entities = mapper.Map<IEnumerable<TEntity>>(models);
    //     return entities;
    // }

    public virtual async Task<IEnumerable<TEntity>> GetPaged(int pageNumber, int pageLimit)
    {
        pageNumber = pageNumber == 0 ? 0 : pageNumber - 1;
        var skip = pageNumber * pageLimit;
        var tableName = GetTableName();
        var sql = $"SELECT * FROM {tableName} {DeletedAt()}";
        var models = await dbConnection.QueryAsync<TModel>(sql);
        var entities = mapper.Map<IEnumerable<TEntity>>(models);

        return entities.Skip(skip).Take(pageLimit).ToList();
    }
    #endregion

    #region GetAll
    // public async Task<IEnumerable<TEntity>> GetAll()
    // {
    //     var models = await dbSet.AsNoTracking().ToListAsync();
    //     return mapper.Map<IEnumerable<TEntity>>(models);
    // }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        var tableName = GetTableName();
        var query = $"SELECT * FROM {tableName} {DeletedAt()}";
        var models = await dbConnection.QueryAsync<TModel>(query);

        return mapper.Map<IEnumerable<TEntity>>(models);
    }
    #endregion

    #region Get
    // public async Task<TEntity> Get(int id)
    // {
    //     var model = await dbSet.AsNoTracking()
    //         .FirstOrDefaultAsync(e => EF.Property<object>(e, "ID").Equals(id));            
    //     return mapper.Map<TEntity>(model);
    // }

    public virtual async Task<TEntity> Get(int id)
    {
        var tableName = GetTableName();
        var query = $"SELECT * FROM {tableName} WHERE id = @id {DeletedAt(false)}";
        var model = await dbConnection.QueryFirstOrDefaultAsync<TModel>(query, new { id = id });
        return mapper.Map<TEntity>(model);
    }
    #endregion

    #region Count
    public async Task<int> Count()
    {
        var tableName = GetTableName();
        var count = $"SELECT COUNT(id) FROM {tableName} {DeletedAt()}";
        return await dbConnection.ExecuteScalarAsync<int>(count);
    }
    #endregion

    #region GetTableName
    private string GetTableName()
    {
        var tableAttribute = typeof(TModel)
            .GetCustomAttributes(false)
            .OfType<TableAttribute>()
            .FirstOrDefault();
        
        return tableAttribute != null ? tableAttribute.Name : typeof(TModel).Name;
    }
    #endregion
    
    #region DeletedAt
    private string DeletedAt(bool isWhere = true)
    {
        var clause = isWhere ? "WHERE" : "AND";
        var hasDeletedAtProperty = typeof(TModel).GetProperty("DeletedAt") != null;
        var whereClause = hasDeletedAtProperty ? $"{clause} deleted_at IS NULL" : string.Empty;

        return whereClause;
    }
    #endregion

    #endregion

    #region Commands (Entity Framework)

    #region Insert
    public async Task<TModel> Insert(TEntity entity)
    {
        var model = mapper.Map<TModel>(entity);
        await dbSet.AddAsync(model);        
        return model;
    }
    #endregion

    #region Update
    public async Task<TEntity> Update(TEntity entity)
    {
        var model = mapper.Map<TModel>(entity);
        dbSet.Update(model);
        return entity;
    }
    #endregion

    #region Delete
    public async Task<TEntity> Delete(int id)
    {
        var model = await dbSet.FindAsync(id);
        var entity = mapper.Map<TEntity>(model);
        dbSet.Remove(model); 

        return entity;
    }
    #endregion

    #endregion

    #endregion
}