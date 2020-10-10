﻿using Roman.Ambinder.DataTypes.OperationResults;

namespace Roman.Ambinder.Storage.Common.Interfaces.NotUsed
{
    public interface IKeyProviderOf<TKey, in TValue>
    {
        OperationResultOf<TKey> TryGetKey(TValue value);
    }
}