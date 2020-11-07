using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.CompositeKey;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.Helpers;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.NonHierarchicalEntityTests
{
    public static class CompsiteKeyRepositoryArranger
    {
        private static int idsCounter = 1;

        public static CompsiteKeyPerson CreatePerson(byte? ageOverride = null,
            string firstNamePostFix = null,
            string lastNamePostFix = null)
        {
            var person = new CompsiteKeyPerson(age: ageOverride.HasValue ? ageOverride.Value : (byte)10,
                firstName: firstNamePostFix != null ? $"Roman{firstNamePostFix}" : "Roman",
                lastName: lastNamePostFix != null ? $"Ambinder{lastNamePostFix}" : "Ambinder");

            person.SetId(Interlocked.Increment(ref idsCounter));

            return person;
        }

        public static async Task<EFCoreCompositeKeyRepositoryFor<CompsiteKeyPerson>> TryGetRepositoryAsync()
        {
            var dbContextProvider = new PreCallPeopleDbContextProvider();
            var repository = new EFCoreCompositeKeyRepositoryFor<CompsiteKeyPerson>(
                dbContextProvider);
            await dbContextProvider.TryMigrateAsync(recreate: true)
                .ConfigureAwait(false);
            return repository;
        }

        public static async Task<EFCoreCompositeKeyUnitOfWorkRepositoryFor<CompsiteKeyPerson>> TryGetUnitOfWorkRepositoryAsync()
        {
            var dbContextProvider = new SingleInstancePeopleDbContextProvider();

            var repository = new EFCoreCompositeKeyUnitOfWorkRepositoryFor<CompsiteKeyPerson>(
                dbContextProvider);

            await dbContextProvider.TryMigrateAsync(recreate: true)
                .ConfigureAwait(false);

            return repository;
        }
    }
}
