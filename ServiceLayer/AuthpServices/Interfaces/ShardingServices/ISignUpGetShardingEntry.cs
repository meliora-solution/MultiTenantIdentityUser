﻿// Copyright (c) 2023 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using StatusGeneric;

namespace AuthPermissions.AspNetCore.ShardingServices;

/// <summary>
/// This interface defines a service that a developer has to add if the application uses
/// 1. The SignInAndCreateTenant service
/// 2. And the multi-tenant is using sharding
/// Its job is to find or create a database for the new tenant and returns its <see cref="ShardingEntry"/> Name
/// </summary>
public interface ISignUpGetShardingEntry
{
    /// <summary>
    /// This will allow you to find of create a <see cref="ShardingEntry"/> for the new sharding tenant
    /// and return the existing / new <see cref="ShardingEntry"/>'s Name.
    /// 1. Hybrid sharding: you might have existing <see cref="ShardingEntry"/> / databases or might create a
    /// new <see cref="ShardingEntry"/>.
    /// 2. Sharding-only: In this case you will be creating a new <see cref="ShardingEntry"/>
    /// </summary>
    /// <param name="hasOwnDb">If true the tenant needs its own database. False means it shares a database.</param>
    /// <param name="createTimestamp">If you create a new <see cref="ShardingEntry"/> you should include this timestamp
    /// in the name of the entry. This is useful to the App Admin when looking at a SignUp that failed.</param>
    /// <param name="region">If not null this provides geographic information to pick the nearest database server.</param>
    /// <param name="version">Optional: provides the version name in case that effects the database selection</param>
    /// <returns>Status with the DatabaseInfoName, or error if it can't find a database to work with</returns>
    public Task<IStatusGeneric<string>> FindOrCreateShardingEntryAsync(
        bool hasOwnDb, string createTimestamp, string region, string version = null);
}