using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey.UnitOfWork;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.Entities;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.NonHierarchicalEntityTests
{
    [TestClass]
    public class EFCoreUnitOfWorkRepositoryFor
    {
        [TestMethod]
        public async Task NonExistingPerson_Add_Added()
        {
            //Arrange
            var unitOfWork = await Arranger.TryGetUnitOfWorkRepositoryAsync().ConfigureAwait(false);
            var person = Arranger.CreatePerson();

            //Act
            var addOpRes = unitOfWork.LocalChangesReposiotry.TryAdd(person);
            var saveOpRes = await unitOfWork.TryCommitChangesAsync().ConfigureAwait(false);

            //Assert
            Assert.IsTrue(addOpRes, addOpRes.ErrorMessage);
            Assert.IsTrue(saveOpRes, saveOpRes.ErrorMessage);
        }

        [TestMethod]
        public async Task ExistingPerson_Get_ReturnedExistingPerson()
        {
            //Arrange
            var unitOfWork = await Arranger.TryGetUnitOfWorkRepositoryAsync().ConfigureAwait(false);
            var person = Arranger.CreatePerson();
            var addOpRes = unitOfWork.LocalChangesReposiotry.TryAdd(person);
            Assert.IsTrue(addOpRes, addOpRes.ErrorMessage);
            var commitOpRes = await unitOfWork.TryCommitChangesAsync().ConfigureAwait(false);
            Assert.IsTrue(commitOpRes, commitOpRes.ErrorMessage);

            //Act
            var getOpRes = await unitOfWork.LocalChangesReposiotry.TryGetSingleAsync(person.Key1)
                .ConfigureAwait(false);

            //Assert
            Assert.IsTrue(getOpRes, getOpRes.ErrorMessage);
            Assert.AreEqual(getOpRes.Value, person);
        }


        [TestMethod]
        public async Task ExistingPerson_Update_Updated()
        {
            //Arrange
            const string updatedValue = "Updated";
            var unitOfWork = await Arranger.TryGetUnitOfWorkRepositoryAsync().ConfigureAwait(false);
            var person = await CreateAndCommitPersonAsync(unitOfWork).ConfigureAwait(false);
            var existingEntityId = person.Key1;

            //Act
            var updateOpRes = await unitOfWork.LocalChangesReposiotry.TryUpdateAsync(existingEntityId,
                p =>
                {
                    p.FirstName = updatedValue;
                    p.LastName = updatedValue;
                }).ConfigureAwait(false);

            //Assert
            Assert.IsTrue(updateOpRes, updateOpRes.ErrorMessage);
            var getOpRes = await unitOfWork.LocalChangesReposiotry.TryGetSingleAsync(existingEntityId)
                .ConfigureAwait(false);
            Assert.AreEqual(getOpRes.Value.FirstName, updatedValue);
            Assert.AreEqual(getOpRes.Value.LastName, updatedValue);
        }

        [TestMethod]
        public async Task ExistingPerson_Remove_Removed()
        {
            //Arrange
            var unitOfWork = await Arranger.TryGetUnitOfWorkRepositoryAsync().ConfigureAwait(false);
            var person = await CreateAndCommitPersonAsync(unitOfWork).ConfigureAwait(false);
            var existingEntityId = person.Key1;

            //Act
            var removeOpRes = await unitOfWork.LocalChangesReposiotry.TryRemoveAsync(existingEntityId)
                .ConfigureAwait(false);
            var commitOpRes = await unitOfWork.TryCommitChangesAsync().ConfigureAwait(false);

            //Assert
            Assert.IsTrue(commitOpRes, commitOpRes.ErrorMessage);
            Assert.IsTrue(removeOpRes, removeOpRes.ErrorMessage);
            var getOpRes = await unitOfWork.LocalChangesReposiotry.TryGetSingleAsync(existingEntityId)
                .ConfigureAwait(false);
            Assert.IsFalse(getOpRes);
        }

        private static async Task<CompsiteKeyPerson> CreateAndCommitPersonAsync(
            EFCoreUnitOfWorkRepositoryFor<int, CompsiteKeyPerson> unitOfWork)
        {
            var person = Arranger.CreatePerson();
            var addOpRes = unitOfWork.LocalChangesReposiotry.TryAdd(person);
            Assert.IsTrue(addOpRes, addOpRes.ErrorMessage);
            var commitOpRes = await unitOfWork.TryCommitChangesAsync().ConfigureAwait(false);
            Assert.IsTrue(commitOpRes, commitOpRes.ErrorMessage);
            return person;
        }
    }
}