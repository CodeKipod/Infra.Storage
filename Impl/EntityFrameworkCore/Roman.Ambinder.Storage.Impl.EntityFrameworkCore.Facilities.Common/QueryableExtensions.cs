﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Common
{
    public static class QueryableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<TEntity> AppendIncludeExpressions<TEntity>(
          this IQueryable<TEntity> query,
          Expression<Func<TEntity, object>>[] toBeIncluded)
          where TEntity : class
        {
            if (toBeIncluded != null && toBeIncluded.Length > 0)
            {
                query = toBeIncluded.Aggregate(query,
                    (current, includeExpression) => current.Include(includeExpression));
            }

            return query;
        }
 
    }
}