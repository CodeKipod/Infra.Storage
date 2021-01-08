using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.Helpers;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Tests.CompositeKey.NonHierarchicalEntityTests.Query
{
    [TestClass]
    public class EFCoreSingleKeyRepositoryPagingTests
    {
        [TestMethod]
        public async Task GetMultipleWithPaging_SingleItemPerPage_ExpectedNumberOfPagesWithResultsReturned()
        {
            //Arrange
            const byte numberOfPeople = 10;
            const byte minimalAge = 10;
            var repository = await CompsiteKeyRepositoryArranger
                .TryGetRepositoryAsync()
                .ConfigureAwait(false);

            for (var i = 0; i < numberOfPeople; i++)
            {
                var postfix = (i + 1).ToString();
                byte age = (byte)(i + minimalAge);
                var person = CompsiteKeyRepositoryArranger.CreatePerson(age, postfix, postfix);
                var addOpRes = await repository.TryAddAsync(person)
                    .ConfigureAwait(false);
                Assert.IsTrue(addOpRes, addOpRes.ErrorMessage);
            }

            const int itemsPerPage = 1;
            int expctedNumberOfPages = 10;
            int currentPage = 1;

            for (int i = 0; i < expctedNumberOfPages; i++)
            {
                //Act
                var getOpRes = await repository.TryGetMultipleAsync(
                    p => p.Age >= minimalAge,
                    pagingParams: new Storage.Common.DataTypes.PagingParams(currentPage, itemsPerPage))
                    .ConfigureAwait(false);

                ++currentPage;

                //Assert
                Assert.IsTrue(getOpRes, getOpRes.ErrorMessage);
                Assert.AreEqual(getOpRes.Value.ItemsPerPage, itemsPerPage);
                Assert.AreEqual(getOpRes.Value.TotalNumberOfPages, expctedNumberOfPages);
                Assert.AreEqual(getOpRes.Value.Items.Count, itemsPerPage);
                Assert.AreEqual(getOpRes.Value.TotalNumberOfItems, numberOfPeople);
            }
        }
    }
}