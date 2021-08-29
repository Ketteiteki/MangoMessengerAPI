﻿namespace MangoAPI.Presentation.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface IContactsController
    {
        Task<IActionResult> AddContact(string contactId, CancellationToken cancellationToken);

        Task<IActionResult> DeleteContact(string contactId, CancellationToken cancellationToken);

        Task<IActionResult> GetContacts(CancellationToken cancellationToken);

        Task<IActionResult> SearchesAsync(string displayName, CancellationToken cancellationToken);
    }
}
