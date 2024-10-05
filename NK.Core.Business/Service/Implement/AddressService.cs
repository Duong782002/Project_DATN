using Mapster;
using NK.Core.Business.Model.Address;
using NK.Core.DataAccess.Repository;
using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(IAddressRepository addressRepository, IUnitOfWork unitOfWork)
        {
            _addressRepository = addressRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Address> AddNew(AddressAPI address)
        {
            var newAddress = address.Adapt<Address>();
            if (address.SetAsDefault)
            {
                var addresses = await _addressRepository.GetByUserId(address.UserId);
                foreach (var item in addresses)
                {
                    item.SetAsDefault = false;
                }
                _addressRepository.UpdateRange(addresses.ToList());
            }
            await _addressRepository.Add(newAddress);
            await _unitOfWork.SaveChangeAsync();

            return newAddress;
        }

        public async Task<IEnumerable<AddressDto>> GetByUserId(string id)
        {
            var addresses = await _addressRepository.GetByUserId(id);

            var addressesDto = addresses.Select(p => new AddressDto
            {
                Id = p.Id,
                AddressLine = p.AddressLine ?? string.Empty,
                PhoneNumber = p.PhoneNumber ?? string.Empty,
                CityCode = p.CityCode,
                ProvinceCode = p.ProvinceCode,
                WardCode = p.WardCode ?? string.Empty,
                FullName = p.FullName ?? string.Empty,
                SetAsDefault = p.SetAsDefault
            }).ToList();

            return addressesDto;
        }

        public async Task Update(AddressUpdateAPI address)
        {
            var addr = address.Adapt<Address>();
            if (address.SetAsDefault)
            {
                var addresses = await _addressRepository.GetByUserId(address.UserId);
                foreach (var item in addresses)
                {
                    item.SetAsDefault = false;
                }
                _addressRepository.UpdateRange(addresses.ToList());
            }
            await _addressRepository.Update(address.Id, addr);

            await _unitOfWork.SaveChangeAsync();
        }
    }
}
