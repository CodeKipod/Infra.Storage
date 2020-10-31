using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.CompositeKey;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey.UnitOfWork;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.Helpers;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.NonHierarchicalEntityTests
{
    public static class Arranger
    {
        public static CompsiteKeyPerson CreatePerson(byte? ageOverride = null,
            string firstNamePostFix = null,
            string lastNamePostFix = null)
        {
            return new CompsiteKeyPerson(age: ageOverride.HasValue ? ageOverride.Value : (byte)10,
                firstName: firstNamePostFix != null ? $"Roman{firstNamePostFix}" : "Roman",
                lastName: lastNamePostFix != null ? $"Ambinder{lastNamePostFix}" : "Ambinder");
        }

        public static async Task<EFCoreCompositeKeyRepositoryFor<int, CompsiteKeyPerson>> TryGetRepositoryAsync()
        {
            var dbContextProvider = new PreCallPeopleDbContextProvider();
            var repository = new EFCoreCompositeKeyRepositoryFor<int, CompsiteKeyPerson>(
                dbContextProvider);
            await dbContextProvider.TryMigrateAsync(recreate: true)
                .ConfigureAwait(false);
            return repository;
        }

        public static async Task<EFCoreUnitOfWorkRepositoryFor<int, CompsiteKeyPerson>> TryGetUnitOfWorkRepositoryAsync()
        {
            var dbContextProvider = new SingleInstancePeopleDbContextProvider();
          
            var repository = new EFCoreUnitOfWorkRepositoryFor<int, CompsiteKeyPerson>(
                dbContextProvider);

            await dbContextProvider.TryMigrateAsync(recreate: true)
                .ConfigureAwait(false);

            return repository;
        }
    }
}
