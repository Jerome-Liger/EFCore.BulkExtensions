﻿using EFCore.BulkExtensions.Sqlite.SqlAdapters.SQLite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;

namespace EFCore.BulkExtensions.Sqlite.SqlAdapters;

/// <summary>
/// A list of database servers supported by EFCore.BulkExtensions
/// </summary>
public enum DbServer
{
    /// <summary>
    /// Indicates database is Microsoft's SQL Server
    /// </summary>
    //[Description("SqlServer")] //DbProvider name in attribute
    //SQLServer,

    /// <summary>
    /// Indicates database is SQL Lite
    /// </summary>
    [Description("Sqlite")]
    SQLite,

    /// <summary>
    /// Indicates database is Npgql
    /// </summary>
    //[Description("Npgql")]
    //PostgreSQL,

    //[Description("MySQL")]
    //MySQL,
}

#pragma warning disable CS1591 // No XML comment required here
public static class SqlAdaptersMapping
{
#pragma warning restore CS1591 // No XML comment required here
    /// <summary>
    /// Dictionary that contains a list of DBServers and their operations server adapter
    /// </summary>
    public static readonly Dictionary<DbServer, ISqlOperationsAdapter> SqlOperationAdapterMapping =
        new()
        {
            {DbServer.SQLite, new SqliteOperationsAdapter()},
        };

    /// <summary>
    /// Dictionary that contains a list of DBServers and their server dialects
    /// </summary>
    public static readonly Dictionary<DbServer, IQueryBuilderSpecialization> SqlQueryBuilderSpecializationMapping =
        new()
        {
            {DbServer.SQLite, new SqliteDialect()},
        };

    /// <summary>
    /// Creates the bulk operations adapter
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static ISqlOperationsAdapter CreateBulkOperationsAdapter(DbContext context)
    {
        var providerType = GetDatabaseType(context);
        return SqlOperationAdapterMapping[providerType];
    }

    /// <summary>
    /// Returns the Adapter dialect to be used
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IQueryBuilderSpecialization GetAdapterDialect(DbContext context)
    {
        var providerType = GetDatabaseType(context);
        return GetAdapterDialect(providerType);
    }

    /// <summary>
    /// Returns the Adapter dialect to be used
    /// </summary>
    /// <param name="providerType"></param>
    /// <returns></returns>
    public static IQueryBuilderSpecialization GetAdapterDialect(DbServer providerType)
    {
        return SqlQueryBuilderSpecializationMapping[providerType];
    }

    /// <summary>
    /// Returns the Database type
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static DbServer GetDatabaseType(DbContext context)
    {
        return DbServer.SQLite;
    }
}
