using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.Test
{
    [TestClass]
    public class PrimaryKeyExpressionBuilderForSameTypeCompositeKeyTests
    {
        [TestMethod]
        public void ValidCompositValueTypeKeys_TryBuildForCompositeKey_SuccessfulyBuilt()
        {
            //Arrange
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act
            var buildKeyPredicteOpres = builder.
                TryBuildForCompositeKey<int, SameValueTypeComposedKeysEntity>(
                ctx, compostiteKeyParts:
                new[] { 1, 2, 3 });

            //Assert
            Assert.IsTrue(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }

        [TestMethod]
        public void InvalidNumberOfCompositValueTypeKeys_TryBuildForCompositeKey_BuiltFailure()
        {
            //Arrange
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act
            var buildKeyPredicteOpres = builder
                .TryBuildForCompositeKey<int, SameValueTypeComposedKeysEntity>(
                    ctx, compostiteKeyParts:
                    new[] { 1, 2 });

            //Assert
            Assert.IsFalse(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }

        [TestMethod]
        public void ValidCompositRefTypeKeys_TryBuildForCompositeKey_SuccessfulyBuilt()
        {
            //Arrange
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act
            var buildKeyPredicteOpres = builder
                .TryBuildForCompositeKey<string, SameRefTypeKeysComposedEntity>(
                ctx, compostiteKeyParts:
                new[] { "1", "2", "3" });

            //Assert
            Assert.IsTrue(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }
    }
}