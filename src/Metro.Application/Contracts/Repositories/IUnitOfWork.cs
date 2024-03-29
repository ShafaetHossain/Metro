﻿namespace Metro.Application.Contracts.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
