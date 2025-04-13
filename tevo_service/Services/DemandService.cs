using Microsoft.EntityFrameworkCore;
using tevo_service.Entities;
using tevo_service.Models;
using tevo_service.Models.DTOs;

namespace tevo_service.Services
{
    public class DemandService
    {
        private readonly AppDbContext appDbContext;

        public DemandService(AppDbContext context)
        {
            appDbContext = context;
        }

        public async Task<ResultModel<string>> CreateAsync(DemandCreateModel model)
        {
            try
            {
                var newDemand = new Demand
                {
                    DemandedMilk = model.DemandedMilk,
                    DeliveredMilk = 0,
                    Price = 0,
                    Currency = model.Currency ?? "₺",
                    State = "Talep Oluşturuldu",
                    DelivererUserId = model.DelivererUserId,
                    RecipientUserId = model.RecipientUserId,
                    ContactInfoId = model.ContactInfoId,
                    AddressInfoId = model.AddressInfoId,
                    Date = model.Date,
                };

                await appDbContext.Demand.AddAsync(newDemand);
                await appDbContext.SaveChangesAsync();

                return new ResultModel<string>().Success("Talep başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return new ResultModel<string>().Fail("Talep oluşturulamadı: " + ex.Message);
            }
        }

        public async Task<ResultModel<List<DemandDTO>>> GetByUserAsync(Guid userId)
        {
            try
            {
                var demands = await appDbContext.Demand
                    .Where(d => d.RecipientUserId == userId)
                    .Include(d => d.DelivererUser)
                    .Include(d => d.RecipientUser)
                    .OrderByDescending(d => d.DemandId)
                    .ToListAsync();

                var addressIds = demands.Select(d => d.AddressInfoId).Distinct().ToList();
                var addressDict = await appDbContext.AddressInfo
                    .Where(a => addressIds.Contains(a.AddressInfoId))
                    .ToDictionaryAsync(a => a.AddressInfoId);


                var contactIds = demands.Select(d => d.ContactInfoId).Distinct().ToList();
                var contactDict = await appDbContext.ContactInfo
                    .Where(a => contactIds.Contains(a.ContactInfoId))
                    .ToDictionaryAsync(a => a.ContactInfoId);

                var sellerIds = demands.Select(d => d.DelivererUserId).Distinct().ToList();

                var sellerAddresses = await appDbContext.AddressInfo
                    .Where(a => sellerIds.Contains(a.UserId))
                    .GroupBy(a => a.UserId)
                    .Select(g => g.FirstOrDefault())
                    .ToDictionaryAsync(a => a.UserId);

                var sellerContacts = await appDbContext.ContactInfo
                    .Where(c => sellerIds.Contains(c.UserId))
                    .GroupBy(c => c.UserId)
                    .Select(g => g.FirstOrDefault())
                    .ToDictionaryAsync(c => c.UserId);

                var dtoList = demands.Select(d => new DemandDTO
                {
                    DemandId = d.DemandId,
                    DemandedMilk = d.DemandedMilk,
                    DeliveredMilk = d.DeliveredMilk,
                    Price = d.Price,
                    Currency = d.Currency,
                    State = d.State,
                    DelivererUserName = d.DelivererUser?.UserName,
                    RecipientUserName = d.RecipientUser?.UserName,
                    Date = d.Date,
                    AddressInfoModel = addressDict.TryGetValue(d.AddressInfoId.Value, out var address)
                        ? new AddressInfoDTO
                        {
                            AddressInfoId = address.AddressInfoId,
                            Type = address.Type,
                            Value = address.Value,
                            Latitude = address.Latitude,
                            Longitude = address.Longitude
                        }
                        : null,
                    ContactInfoModel = contactDict.TryGetValue(d.ContactInfoId.Value, out var contact)
                        ? new ContactInfoDTO
                        {
                            ContactInfoId = contact.ContactInfoId,
                            Type = contact.Type,
                            Value = contact.Value,
                        }
                        : null,

                    SellerAddressInfoModel = sellerAddresses.TryGetValue(d.DelivererUserId, out var sellerAddress)
                        ? new AddressInfoDTO
                        {
                            AddressInfoId = sellerAddress.AddressInfoId,
                            Type = sellerAddress.Type,
                            Value = sellerAddress.Value,
                            Latitude = sellerAddress.Latitude,
                            Longitude = sellerAddress.Longitude
                        }
                        : null,

                                        SellerContactInfoModel = sellerContacts.TryGetValue(d.DelivererUserId, out var sellerContact)
                        ? new ContactInfoDTO
                        {
                            ContactInfoId = sellerContact.ContactInfoId,
                            Type = sellerContact.Type,
                            Value = sellerContact.Value
                        }
                        : null,

                }).ToList();

                return new ResultModel<List<DemandDTO>>().Success(dtoList);
            }
            catch (Exception ex)
            {
                return new ResultModel<List<DemandDTO>>().Fail("Talepler alınamadı: " + ex.Message);
            }
        }


        public async Task<ResultModel<List<DemandDTO>>> GetAllAsync()
        {
            try
            {
                var demands = await appDbContext.Demand
                    .Include(d => d.RecipientUser)
                    .Include(d => d.DelivererUser)
                    .OrderByDescending(d => d.DemandId)
                    .ToListAsync();

                var addressIds = demands.Select(d => d.AddressInfoId).Distinct().ToList();
                var addressDict = await appDbContext.AddressInfo
                    .Where(a => addressIds.Contains(a.AddressInfoId))
                    .ToDictionaryAsync(a => a.AddressInfoId);


                var contactIds = demands.Select(d => d.ContactInfoId).Distinct().ToList();
                var contactDict = await appDbContext.ContactInfo
                    .Where(a => contactIds.Contains(a.ContactInfoId))
                    .ToDictionaryAsync(a => a.ContactInfoId);

                var dtoList = demands.Select(d => new DemandDTO
                {
                    DemandId = d.DemandId,
                    DemandedMilk = d.DemandedMilk,
                    DeliveredMilk = d.DeliveredMilk,
                    Price = d.Price,
                    Currency = d.Currency,
                    State = d.State,
                    DelivererUserName = d.DelivererUser?.UserName,
                    RecipientUserName = d.RecipientUser?.UserName,
                    Date = d.Date,
                    AddressInfoModel = addressDict.TryGetValue(d.AddressInfoId.Value, out var address)
                        ? new AddressInfoDTO
                        {
                            AddressInfoId = address.AddressInfoId,
                            Type = address.Type,
                            Value = address.Value,
                            Latitude = address.Latitude,
                            Longitude = address.Longitude
                        }
                        : null,
                    ContactInfoModel = contactDict.TryGetValue(d.ContactInfoId.Value, out var contact)
                        ? new ContactInfoDTO
                        {
                            ContactInfoId = contact.ContactInfoId,
                            Type = contact.Type,
                            Value = contact.Value,
                        }
                        : null,
                }).ToList();

                return new ResultModel<List<DemandDTO>>().Success(dtoList);
            }
            catch (Exception ex)
            {
                return new ResultModel<List<DemandDTO>>().Fail("Talepler alınamadı: " + ex.Message);
            }
        }

        public async Task<ResultModel<List<DemandDTO>>> GetForSellerAsync(Guid sellerId)
        {
            try
            {
                var demands = await appDbContext.Demand
                    .Where(d => d.DelivererUserId == sellerId)
                    .Include(d => d.RecipientUser)
                    .OrderByDescending(d => d.DemandId)
                    .Select(d => new DemandDTO
                    {
                        DemandId = d.DemandId,
                        DemandedMilk = d.DemandedMilk,
                        DeliveredMilk = d.DeliveredMilk,
                        Price = d.Price,
                        Currency = d.Currency,
                        State = d.State,
                        RecipientUserName = d.RecipientUser.UserName,
                        Date = d.Date,
                        AddressInfoModel = appDbContext.AddressInfo
                            .Where(a => a.AddressInfoId == d.AddressInfoId)
                            .Select(a => new AddressInfoDTO
                            {
                                Type = a.Type,
                                Value = a.Value,
                                Latitude = a.Latitude,
                                Longitude = a.Longitude
                            })
                            .FirstOrDefault(),

                        ContactInfoModel = appDbContext.ContactInfo
                            .Where(c => c.ContactInfoId == d.ContactInfoId)
                            .Select(c => new ContactInfoDTO
                            {
                                Type = c.Type,
                                Value = c.Value
                            })
                            .FirstOrDefault()
                    })
                    .ToListAsync();

                return new ResultModel<List<DemandDTO>>().Success(demands);
            }
            catch (Exception ex)
            {
                return new ResultModel<List<DemandDTO>>().Fail("Satıcı talepleri alınamadı: " + ex.Message);
            }
        }


        public async Task<ResultModel<DemandDTO>> UpdateBySellerAsync(DemandUpdateModel model)
        {
            try
            {
                var demand = await appDbContext.Demand.FindAsync(model.DemandId);
                if (demand == null)
                    return new ResultModel<DemandDTO>().Fail("Talep bulunamadı");

                if (model.Price.HasValue)
                    demand.Price = model.Price;

                if (model.DeliveredMilk.HasValue)
                    demand.DeliveredMilk = model.DeliveredMilk;

                if (!string.IsNullOrWhiteSpace(model.State))
                    demand.State = model.State;

                await appDbContext.SaveChangesAsync();

                var dto = new DemandDTO
                {
                    DemandId = demand.DemandId,
                    DemandedMilk = demand.DemandedMilk,
                    DeliveredMilk = demand.DeliveredMilk,
                    Price = demand.Price,
                    Currency = demand.Currency,
                    State = demand.State
                };

                return new ResultModel<DemandDTO>().Success(dto);
            }
            catch (Exception ex)
            {
                return new ResultModel<DemandDTO>().Fail("Talep güncellenemedi: " + ex.Message);
            }
        }

        public async Task<ResultModel<string>> ApproveAsync(long demandId)
        {
            var demand = await appDbContext.Demand.FindAsync(demandId);
            if (demand == null)
                return new ResultModel<string>().Fail("Talep bulunamadı");

            if (demand.State != "Teklif Verildi")
                return new ResultModel<string>().Fail("Bu talep şu anda onaylanamaz");

            demand.State = "Alıcı Onayladı";
            await appDbContext.SaveChangesAsync();

            return new ResultModel<string>().Success("Talep onaylandı");
        }

        public async Task<ResultModel<string>> CancelAsync(long demandId)
        {
            var demand = await appDbContext.Demand.FindAsync(demandId);
            if (demand == null)
                return new ResultModel<string>().Fail("Talep bulunamadı");

            if (demand.State == "Tamamlandı" || demand.State == "İptal Edildi")
                return new ResultModel<string>().Fail("Bu talep iptal edilemez");

            demand.State = "İptal Edildi";
            await appDbContext.SaveChangesAsync();

            return new ResultModel<string>().Success("Talep iptal edildi");
        }


    }
}
