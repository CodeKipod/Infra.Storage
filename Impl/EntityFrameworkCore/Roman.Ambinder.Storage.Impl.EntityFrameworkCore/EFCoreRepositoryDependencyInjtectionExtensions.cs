using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Roman.Ambinder.Storage.Common.Interfaces.Common;
using Roman.Ambinder.Storage.Common.Interfaces.CompositeKey;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.CompositeKey;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore
{
    public static class EFCoreRepositoryDependencyInjtectionExtensions
    {
        /// <summary>
        /// Registers EFCoreSingleKeyRepositoryFor<TKey, TEntity, TDbContext>
        /// under IRepositoryFor<TKey, TEntity>
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        public static void RegisterSingleKeyRepositoryFor<TKey, TEntity, TDbContext>(
            this IServiceCollection services)
            where TEntity : class, new()
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>();
            services.AddScoped<IRepositoryFor<TKey, TEntity>,
               EFCoreSingleKeyRepositoryFor<TKey, TEntity, TDbContext>>();
        }

        /// <summary>
        /// Registers EFCoreSingleKeyUnitOfWorkRepositoryFor<TKey, TEntity, TDbContext>
        /// under IUnitOfWorkRepositoryFor<TKey, TEntity>
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        public static void RegisterSingleKeyUnitOfWorkRepositoryFor<TKey, TEntity, TDbContext>(
            this IServiceCollection services)
            where TEntity : class, new()
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>();
            services.AddScoped<IUnitOfWorkRepositoryFor<TKey, TEntity>,
               EFCoreSingleKeyUnitOfWorkRepositoryFor<TKey, TEntity, TDbContext>>();
        }


        /// <summary>
        /// Registers EFCoreSingleKeyRepositoryFor<TKey, TEntity, TDbContext>
        /// under ICompositeKeyRepositoryFor<TEntity>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        public static void RegisterCompositeKeyRepositoryFor<TEntity, TDbContext>(
           this IServiceCollection services)
           where TEntity : class, new()
           where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>();
            services.AddScoped<ICompositeKeyRepositoryFor<TEntity>,
               EFCoreCompositeKeyRepositoryFor<TEntity, TDbContext>>();
        }

        /// <summary>
        /// Registers FCoreCompositeKeyUnitOfWorkRepositoryFor<TEntity, TDbContext>
        /// under IUnitOfWorkRepositoryFor<object[], TEntity>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        public static void RegisterCompositeKeyUnitOfWorkRepositoryFor<TEntity, TDbContext>(
           this IServiceCollection services)
           where TEntity : class, new()
           where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>();
            services.AddScoped<IUnitOfWorkRepositoryFor<object[], TEntity>,
               EFCoreCompositeKeyUnitOfWorkRepositoryFor<TEntity, TDbContext>>();
        }
    }
}
