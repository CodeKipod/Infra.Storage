using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.NonHierarchicalEntityTests
{
    [TestClass]
    public class EFCoreCompositeKeyRepositoryForTests
    {
        [TestMethod]
        public async Task NonExistingPerson_Add_Added()
        {
            //Arrange
            var repository = await CompsiteKeyRepositoryArranger.TryGetRepositoryAsync()
                .ConfigureAwait(false);
            var person = CompsiteKeyRepositoryArranger.CreatePerson();

            //Act
            var opRes = await repository.TryAddAsync(person)
                .ConfigureAwait(false);

            //Assert
            Assert.IsTrue(opRes, opRes.ErrorMessage);
        }

        [TestMethod]
        public async Task ExistingPerson_Get_ReturnedExistingPerson()
        {
            //Arrange
            var repository = await CompsiteKeyRepositoryArranger.TryGetRepositoryAsync().ConfigureAwait(false);
            var person = CompsiteKeyRepositoryArranger.CreatePerson();
            var addOpRes = await repository.TryAddAsync(person)
                .ConfigureAwait(false);
            Assert.IsTrue(addOpRes, addOpRes.ErrorMessage);
            var existingEntityId = new object[] { addOpRes.Value.Key1, addOpRes.Value.Key2, addOpRes.Value.Key3 };



            //Act
            var getOpRes = await repository.TryGetSingleAsync(existingEntityId)
                .ConfigureAwait(false);

            //Assert
            Assert.IsTrue(getOpRes, getOpRes.ErrorMessage);
            Assert.AreEqual(getOpRes.Value, addOpRes.Value);
        }


        [TestMethod]
        public async Task ExistingPerson_Update_Updated()
        {
            //Arrange
            const string updatedValue = "Updated";
            var repository = await CompsiteKeyRepositoryArranger.TryGetRepositoryAsync().ConfigureAwait(false);
            var person = CompsiteKeyRepositoryArranger.CreatePerson();
            var addOpRes = await repository.TryAddAsync(person)
                .ConfigureAwait(false);
            var existingEntityId = new object[] { addOpRes.Value.Key1, addOpRes.Value.Key2, addOpRes.Value.Key3 };
            Assert.IsTrue(addOpRes, addOpRes.ErrorMessage);
            var getOpRes = await repository.TryGetSingleAsync(existingEntityId)
                .ConfigureAwait(false);
            Assert.IsTrue(getOpRes, getOpRes.ErrorMessage);
            Assert.AreEqual(getOpRes.Value, addOpRes.Value);

            //Act
            var updateOpRes = await repository.TryUpdateAsync(existingEntityId,
                p =>
                {
                    p.FirstName = updatedValue;
                    p.LastName = updatedValue;
                }).ConfigureAwait(false);

            //Assert
            Assert.IsTrue(updateOpRes, updateOpRes.ErrorMessage);
            getOpRes = await repository.TryGetSingleAsync(existingEntityId).ConfigureAwait(false);
            Assert.AreEqual(getOpRes.Value.FirstName, updatedValue);
            Assert.AreEqual(getOpRes.Value.LastName, updatedValue);
        }

        [TestMethod]
        public async Task ExistingPerson_Remove_Removed()
        {
            //Arrange
            var repository = await CompsiteKeyRepositoryArranger.TryGetRepositoryAsync().ConfigureAwait(false);
            var person = CompsiteKeyRepositoryArranger.CreatePerson();
            var addOpRes = await repository.TryAddAsync(person)
                .ConfigureAwait(false);
            var existingEntityId = new object[] { addOpRes.Value.Key1, addOpRes.Value.Key2, addOpRes.Value.Key3 };

            //Act
            var removeOpRes = await repository.TryRemoveAsync(existingEntityId)
                .ConfigureAwait(false);

            //Assert
            Assert.IsTrue(removeOpRes, removeOpRes.ErrorMessage);
            var getOpRes = await repository.TryGetSingleAsync(existingEntityId).ConfigureAwait(false);
            Assert.IsFalse(getOpRes);
        }

    }
}