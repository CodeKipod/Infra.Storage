using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.NonHierarchicalEntityTests
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

            //Act
            var getOpRes = await unitOfWork.LocalChangesReposiotry.TryGetSingleAsync(person.Id)
                .ConfigureAwait(false);

            //Assert
            Assert.IsTrue(getOpRes, getOpRes.ErrorMessage);
            Assert.AreEqual(getOpRes.Value, person);
        }


        //[TestMethod]
        //public async Task ExistingPerson_Update_Updated()
        //{
        //    //Arrange
        //    const string updatedValue = "Updated";
        //    var repository = await Arranger.TryGetUnitOfWorkRepositoryAsync().ConfigureAwait(false);
        //    var person = Arranger.CreatePerson();
        //    var addOpRes = await repository.TryAddAsync(person)
        //        .ConfigureAwait(false);
        //    var existingEntityId = addOpRes.Value.Id;
        //    Assert.IsTrue(addOpRes, addOpRes.ErrorMessage);
        //    var getOpRes = await repository.TryGetSingleAsync(existingEntityId)
        //        .ConfigureAwait(false);
        //    Assert.IsTrue(getOpRes, getOpRes.ErrorMessage);
        //    Assert.AreEqual(getOpRes.Value, addOpRes.Value);

        //    //Act
        //    var updateOpRes = await repository.TryUpdateAsync(existingEntityId,
        //        p =>
        //        {
        //            p.FirstName = updatedValue;
        //            p.LastName = updatedValue;
        //        }).ConfigureAwait(false);

        //    //Assert
        //    Assert.IsTrue(updateOpRes, updateOpRes.ErrorMessage);
        //    getOpRes = await repository.TryGetSingleAsync(existingEntityId).ConfigureAwait(false);
        //    Assert.AreEqual(getOpRes.Value.FirstName, updatedValue);
        //    Assert.AreEqual(getOpRes.Value.LastName, updatedValue);
        //}

        //[TestMethod]
        //public async Task ExistingPerson_Remove_Removed()
        //{
        //    //Arrange
        //    var repository = await Arranger.TryGetUnitOfWorkRepositoryAsync().ConfigureAwait(false);
        //    var person = Arranger.CreatePerson();
        //    var addOpRes = await repository.TryAddAsync(person)
        //        .ConfigureAwait(false);
        //    var existingEntityId = addOpRes.Value.Id;

        //    //Act
        //    var removeOpRes = await repository.TryRemoveAsync(existingEntityId)
        //        .ConfigureAwait(false);

        //    //Assert
        //    Assert.IsTrue(removeOpRes, removeOpRes.ErrorMessage);
        //    var getOpRes = await repository.TryGetSingleAsync(existingEntityId).ConfigureAwait(false);
        //    Assert.IsFalse(getOpRes);
        //}

    }
}