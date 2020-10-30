using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.Test
{
    [TestClass]
    public class PrimaryKeyExpressionBuilderForMultitypeCompositeKeyTests
    {
        [TestMethod]
        public void ValidCompositValueTypeKeys_TryBuildForMultitypeCompositeKey_SuccessfulyBuilt()
        {
            //Arrange 
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act 
            var buildKeyPredicteOpres = builder.TryBuildForMultitypeCompositeKey<SameValueTypeComposedKeysEntity>(
                ctx, compostiteKeyParts:
                new object[] { 1, 2, 3 });

            //Assert
            Assert.IsTrue(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }

        [TestMethod]
        public void InvalidNumberOfCompositValueTypeKeys_TryBuildForMultitypeCompositeKey_BuiltFailure()
        {
            //Arrange 
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act 
            var buildKeyPredicteOpres = builder.TryBuildForMultitypeCompositeKey<SameValueTypeComposedKeysEntity>(
                ctx, compostiteKeyParts:
                new object[] { 1, 2 });

            //Assert
            Assert.IsFalse(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }

        [TestMethod]
        public void InvalidTypeOfCompositValueTypeKeys_TryBuildForMultitypeCompositeKey_BuiltFailure()
        {
            //Arrange 
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act 
            var buildKeyPredicteOpres = builder.TryBuildForMultitypeCompositeKey<SameValueTypeComposedKeysEntity>(
                ctx, compostiteKeyParts:
                new object[] { 1, 2 ,"3"});

            //Assert
            Assert.IsFalse(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }

        [TestMethod]
        public void ValidCompositRefTypeKeys_TryBuildForMultitypeCompositeKey_SuccessfulyBuilt()
        {
            //Arrange 
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act 
            var buildKeyPredicteOpres = builder.TryBuildForMultitypeCompositeKey<SameRefTypeKeysComposedEntity>(
                ctx, compostiteKeyParts:
                new object[] { "1", "2", "3" });

            //Assert
            Assert.IsTrue(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }
    }
}
