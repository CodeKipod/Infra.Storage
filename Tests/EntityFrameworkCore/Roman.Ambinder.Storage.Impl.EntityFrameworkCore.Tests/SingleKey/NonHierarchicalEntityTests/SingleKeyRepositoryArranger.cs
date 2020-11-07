using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Helpers;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.NonHierarchicalEntityTests
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

        public static async Task<EFCoreSingleKeyRepositoryFor<int, SingleKeyPerson>> TryGetRepositoryAsync()
        {
            var dbContextProvider = new PreCallPeopleDbContextProvider();
            var repository = new EFCoreSingleKeyRepositoryFor<int, SingleKeyPerson>(
                dbContextProvider);
            await dbContextProvider.TryMigrateAsync(recreate: true)
                .ConfigureAwait(false);
            return repository;
        }

        public static async Task<EFCoreSingleKeyUnitOfWorkRepositoryFor<int, SingleKeyPerson>> TryGetUnitOfWorkRepositoryAsync()
        {
            var dbContextProvider = new SingleInstancePeopleDbContextProvider();
          
            var repository = new EFCoreSingleKeyUnitOfWorkRepositoryFor<int, SingleKeyPerson>(
                dbContextProvider);

            await dbContextProvider.TryMigrateAsync(recreate: true)
                .ConfigureAwait(false);

            return repository;
        }
    }
}
