using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.Helpers;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities;
using System.Threading;
using System.Threading.Tasks;
using CompositeKeyRepository = Roman.Ambinder.Storage.Impl.EntityFrameworkCore.CompositeKey.EFCoreCompositeKeyRepositoryFor<Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities.CompsiteKeyPerson>;
using CompositeKeyUnitOfWorkRepository = Roman.Ambinder.Storage.Impl.EntityFrameworkCore.CompositeKey.EFCoreCompositeKeyUnitOfWorkRepositoryFor<Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities.CompsiteKeyPerson>;

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

        public static async Task<CompositeKeyRepository> TryGetRepositoryAsync()
        {
            var dbContextProvider = new CompositeKeyPreCallPeopleDbContextProvider();

            var repository = new CompositeKeyRepository(
                dbContextProvider);

            await dbContextProvider.TryMigrateAsync(recreate: true)
                .ConfigureAwait(false);

            return repository;
        }

        public static async Task<CompositeKeyUnitOfWorkRepository> TryGetUnitOfWorkRepositoryAsync()
        {
            var dbContextProvider = new CompositeKeySingleInstancePeopleDbContextProvider();

            var repository = new CompositeKeyUnitOfWorkRepository(
                dbContextProvider);

            await dbContextProvider.TryMigrateAsync(recreate: true)
                .ConfigureAwait(false);

            return repository;
        }
    }
}