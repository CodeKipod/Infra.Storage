using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.Test
{
    [TestClass]
    public class PrimaryKeyExpressionBuilderForSingleKeyTests
    {

        [TestMethod]
        public void ValidSingleValueTypeKey_TryBuildForSingleKey_SuccessfulyBuilt()
        {
            //Arrange 
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act 
            var buildKeyPredicteOpres = builder.TryBuildForSingleKey<int, SingleValueTypeKeyEntity>(ctx, key: 1);

            //Assert
            Assert.IsTrue(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }

        [TestMethod]
        public void InvalidTypeSingleValueTypeKey_TryBuildForSingleKey_SuccessfulyBuilt()
        {
            //Arrange 
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act 
            var buildKeyPredicteOpres = builder.TryBuildForSingleKey<string, SingleValueTypeKeyEntity>(ctx, key: "1");

            //Assert
            Assert.IsFalse(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }


        [TestMethod]
        public void ValidSingleRefypeKey_TryBuildForSingleKey_SuccessfulyBuilt()
        {
            //Arrange 
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act 
            var buildKeyPredicteOpres = builder.TryBuildForSingleKey<string, SingleRefTypeKeyEntity>(ctx, key: "1");

            //Assert
            Assert.IsTrue(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }


        [TestMethod]
        public void InValidTypeSingleRefypeKey_TryBuildForSingleKey_SuccessfulyBuilt()
        {
            //Arrange 
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act 
            var buildKeyPredicteOpres = builder.TryBuildForSingleKey<int, SingleRefTypeKeyEntity>(ctx, key: 1);

            //Assert
            Assert.IsFalse(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }
    }
}
