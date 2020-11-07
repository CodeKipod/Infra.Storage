using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities;
using System.Threading.Tasks;
using SingleKeyRepository = Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey.EFCoreSingleKeyRepositoryFor<int, Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities.SingleKeyPerson>;
using SingleKeyUnitOfWorkRespositry = Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey.EFCoreSingleKeyUnitOfWorkRepositoryFor<int, Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities.SingleKeyPerson>;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Helpers
{
    public static class SingleKeyRepositoryArranger
    {
        public static SingleKeyPerson CreatePerson(byte? ageOverride = null,
            string firstNamePostFix = null,
            string lastNamePostFix = null)
        {
            return new SingleKeyPerson(age: ageOverride.HasValue ? ageOverride.Value : (byte)10,
                firstName: firstNamePostFix != null ? $"Roman{firstNamePostFix}" : "Roman",
                lastName: lastNamePostFix != null ? $"Ambinder{lastNamePostFix}" : "Ambinder");
        }

        public static async Task<SingleKeyRepository> TryGetRepositoryAsync()
        {
            var dbContextProvider = new PreCallPeopleDbContextProvider();
            var repository = new SingleKeyRepository(
                dbContextProvider);
            await dbContextProvider.TryMigrateAsync(recreate: true)
                .ConfigureAwait(false);
            return repository;
        }

        public static async Task<SingleKeyUnitOfWorkRespositry> TryGetUnitOfWorkRepositoryAsync()
        {
            var dbContextProvider = new SingleInstancePeopleDbContextProvider();

            var repository = new SingleKeyUnitOfWorkRespositry(
                dbContextProvider);

            await dbContextProvider.TryMigrateAsync(recreate: true)
                .ConfigureAwait(false);

            return repository;
        }
    }
}