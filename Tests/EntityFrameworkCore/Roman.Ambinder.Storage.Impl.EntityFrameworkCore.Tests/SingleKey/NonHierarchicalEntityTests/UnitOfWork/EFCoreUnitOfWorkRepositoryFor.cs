using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.SingleKey.UnitOfWork;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.SingleKey.NonHierarchicalEntityTests
{
    [TestClass]
    public class EFCoreUnitOfWorkRepositoryFor
    {
        [TestMethod]
        public async Task NonExistingPerson_Add_Added()
        {
            //Arrange
            var unitOfWork = await SingleKeyRepositoryArranger.TryGetUnitOfWorkRepositoryAsync().ConfigureAwait(false);
            var person = SingleKeyRepositoryArranger.CreatePerson();

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
            var unitOfWork = await SingleKeyRepositoryArranger.TryGetUnitOfWorkRepositoryAsync().ConfigureAwait(false);
            var person = SingleKeyRepositoryArranger.CreatePerson();
            var addOpRes = unitOfWork.LocalChangesReposiotry.TryAdd(person);
            Assert.IsTrue(addOpRes, addOpRes.ErrorMessage);
            var commitOpRes = await unitOfWork.TryCommitChangesAsync().ConfigureAwait(false);
            Assert.IsTrue(commitOpRes, commitOpRes.ErrorMessage);

            //Act
            var getOpRes = await unitOfWork.LocalChangesReposiotry.TryGetSingleAsync(person.Id)
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
            var unitOfWork = await SingleKeyRepositoryArranger.TryGetUnitOfWorkRepositoryAsync().ConfigureAwait(false);
            var person = await CreateAndCommitPersonAsync(unitOfWork).ConfigureAwait(false);
            var existingEntityId = person.Id;

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
            var unitOfWork = await SingleKeyRepositoryArranger.TryGetUnitOfWorkRepositoryAsync().ConfigureAwait(false);
            var person = await CreateAndCommitPersonAsync(unitOfWork).ConfigureAwait(false);
            var existingEntityId = person.Id;

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

        private static async Task<SingleKey.Entities.SingleKeyPerson> CreateAndCommitPersonAsync(BaseEFCoreUnitOfWorkRepositoryFor<int, Entities.SingleKeyPerson> unitOfWork)
        {
            var person = SingleKeyRepositoryArranger.CreatePerson();
            var addOpRes = unitOfWork.LocalChangesReposiotry.TryAdd(person);
            Assert.IsTrue(addOpRes, addOpRes.ErrorMessage);
            var commitOpRes = await unitOfWork.TryCommitChangesAsync().ConfigureAwait(false);
            Assert.IsTrue(commitOpRes, commitOpRes.ErrorMessage);
            return person;
        }
    }
}