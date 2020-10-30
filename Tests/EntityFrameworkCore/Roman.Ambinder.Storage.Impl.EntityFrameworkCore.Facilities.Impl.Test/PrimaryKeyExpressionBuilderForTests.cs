using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.Test
{
    [TestClass]
    public class PrimaryKeyExpressionBuilderForTests
    {
        [TestMethod]
        public void ValidCompositValueTypeKeys_TryBuildCompositeKey_SuccessfulyBuilt()
        {
            //Arrange 
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act 
            var buildKeyPredicteOpres = builder.TryBuildForCompositeKey<SameValueTypeComposedKeysEntity>(
                ctx, compostiteKeyParts:
                new object[] { 1, 2, 3 });

            //Assert
            Assert.IsTrue(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }

        [TestMethod]
        public void InvalidNumberOfCompositValueTypeKeys_TryBuildCompositeKey_BuiltFailure()
        {
            //Arrange 
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act 
            var buildKeyPredicteOpres = builder.TryBuildForCompositeKey<SameValueTypeComposedKeysEntity>(
                ctx, compostiteKeyParts:
                new object[] { 1, 2 });

            //Assert
            Assert.IsFalse(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }

        [TestMethod]
        public void InvalidTypeOfCompositValueTypeKeys_TryBuildCompositeKey_BuiltFailure()
        {
            //Arrange 
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act 
            var buildKeyPredicteOpres = builder.TryBuildForCompositeKey<SameValueTypeComposedKeysEntity>(
                ctx, compostiteKeyParts:
                new object[] { 1, 2 ,"3"});

            //Assert
            Assert.IsFalse(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }

        [TestMethod]
        public void ValidCompositRefTypeKeys_TryBuildCompositeKey_SuccessfulyBuilt()
        {
            //Arrange 
            var builder = new PrimaryKeyExpressionBuilder();
            using var ctx = new MyDbContext();

            //Act 
            var buildKeyPredicteOpres = builder.TryBuildForCompositeKey<SameRefTypeKeysComposedEntity>(
                ctx, compostiteKeyParts:
                new object[] { "1", "2", "3" });

            //Assert
            Assert.IsTrue(buildKeyPredicteOpres, buildKeyPredicteOpres.ErrorMessage);
        }

        [TestMethod]
        public void ValidSingleValueTypeKey_TryBuildSingleKey_SuccessfulyBuilt()
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
        public void InvalidTypeSingleValueTypeKey_TryBuildSingleKey_SuccessfulyBuilt()
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
        public void ValidSingleRefypeKey_TryBuildSingleKey_SuccessfulyBuilt()
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
        public void InValidTypeSingleRefypeKey_TryBuildSingleKey_SuccessfulyBuilt()
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
