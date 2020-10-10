using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.Entities;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.Helpers;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.UnitOfWork;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.NonHierarchicalEntityTests
{
    public static class Arranger
    {
        public static Person CreatePerson(byte? ageOverride = null,
            string firstNamePostFix = null,
            string lastNamePostFix = null)
        {
            return new Person(age: ageOverride.HasValue ? ageOverride.Value : (byte)10,
                firstName: firstNamePostFix != null ? $"Roman{firstNamePostFix}" : "Roman",
                lastName: lastNamePostFix != null ? $"Ambinder{lastNamePostFix}" : "Ambinder");
        }

        public static async Task<EFCoreSingleKeyRepositoryFor<int, Person>> TryGetRepositoryAsync()
        {
            var dbContextProvider = new PreCallPeopleDbContextProvider();
            var repository = new EFCoreSingleKeyRepositoryFor<int, Person>(
                dbContextProvider);
            await dbContextProvider.TryMigrateAsync(recreate: true)
                .ConfigureAwait(false);
            return repository;
        }

        public static async Task<EFCoreUnitOfWorkRepositoryFor<int, Person>> TryGetUnitOfWorkRepositoryAsync()
        {
            var dbContextProvider = new SingleInstancePeopleDbContextProvider();
            var repository = new EFCoreUnitOfWorkRepositoryFor<int, Person>(
                dbContextProvider);
            await dbContextProvider.TryMigrateAsync(recreate: true)
                .ConfigureAwait(false);
            return repository;
        }
    }
}
