using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.Tattoo;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Azure;
using TatooMarket.Domain.Repositories.Tattoo;

namespace TatooMarket.Application.UseCases.Tattoo
{
    public class DeleteTattoo : IDeleteTattoo
    {
        private readonly IAzureStorageService _storageService;
        private readonly ITattooReadOnly _tattooRead;
        private readonly ITattooWriteOnly _tattooWrite;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTattoo(IAzureStorageService storageService, ITattooReadOnly tattooRead, ITattooWriteOnly tattooWrite, IUnitOfWork unitOfWork)
        {
            _storageService = storageService;
            _tattooRead = tattooRead;
            _tattooWrite = tattooWrite;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id)
        {
            var tattoo = await _tattooRead.TattooById(id);

            await _storageService.DeleteContainer(tattoo.Id.ToString());

            _tattooWrite.Delete(tattoo);
            await _unitOfWork.Commit();
        }
    }
}
