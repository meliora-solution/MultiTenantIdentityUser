using EasyStockDb.Context;
using EasyStockDb.Entity;
using Microsoft.EntityFrameworkCore;
using SharedServices.Models;
using SharedServices.Models.EasyStock;

namespace EasyStockServices
{
    public class EasyStockContactServices
    {
        private readonly EasyStockDbContext _context;

        public EasyStockContactServices(EasyStockDbContext context)
        {
            _context = context;
        }

      
        public async Task<ServiceResponseDto<bool>> CreateAsync(ContactDto objDTO)
        {
            ServiceResponseDto<bool> result = new();
           
            try
            {
                var itemToAdd = new Contact
                {
                    Name = objDTO.Name,
                    type = objDTO.type,
                    Address = objDTO.Address,
                    Phone = objDTO.Phone,
                    Description = objDTO.Description
                };

                _context.Add(itemToAdd);

                var affectedRows = await _context.SaveChangesAsync();
                result.Success = affectedRows > 0;
                result.Message = result.Success ? "Data is saved" : "Data is not saved";
                return result;



            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }
        public async Task<IEnumerable<ContactDto>?> GetCustomerListAsycn()
        {
            try
            {
                var query = (from o in _context.Contacts
                             where o.type == "C"
                             select new ContactDto
                             {
                                 ContactId = o.ContactId,
                                 Name = o.Name
                             });
                var data = await query.ToListAsync();
                return data.AsQueryable();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<ContactDto>?> GetSupplierListAsycn()
        {
            try
            {
                var query = (from o in _context.Contacts
                             where o.type == "S"
                             select new ContactDto
                             {
                                 ContactId = o.ContactId,
                                 Name = o.Name
                             });
                var data = await query.ToListAsync();
                return data.AsQueryable();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<ContactDto>?> GetContactListAsycn()
        {
            try
            {

                var query = (from o in _context.Contacts
                             select new ContactDto
                             {
                                 ContactId = o.ContactId,
                                 Name = o.Name,
                                 type = o.type,
                                 Phone = o.Phone,
                                 Address = o.Address,
                                 Description = o.Description,
                                 Datakey = o.DataKey
                             });
                var data = await query.ToListAsync();
                return data.AsQueryable();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ServiceResponseDto<bool>> DeleteAsync(int contactId)
        {
            ServiceResponseDto<bool> result = new();
            try
            {
                // Step 1: Retrieve the entity you want to delete
                var itemToDelete = await _context.Contacts.FindAsync(contactId);

                if (itemToDelete != null)
                {
                    // Step 2: Remove the entity from the context
                    _context.Contacts.Remove(itemToDelete);

                    // Step 3: Save the changes to the database
                    var affectedRows = await _context.SaveChangesAsync();
                    result.Success = affectedRows > 0;
                    result.Message = result.Success ? "Data is deleted" : "Data is not deleted";
                    return result;

                }
                else
                {
                    result.Success = false;
                    result.Message = "Data is not deleted";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }

        }
        public async Task<ServiceResponseDto<bool>> UpdateAsync(ContactDto objDTO)
        {
            ServiceResponseDto<bool> result = new();
            try
            {
                var o = await _context.Contacts.FirstOrDefaultAsync(u => u.ContactId == objDTO.ContactId);
                if (o != null)
                {
                    o.Name = objDTO.Name;
                    o.type = objDTO.type;
                    o.Phone = objDTO.Phone;
                    o.Address = objDTO.Address;
                    o.Description = objDTO.Description;
                }

                var affectedRows = await _context.SaveChangesAsync();
                result.Success = affectedRows > 0;
                result.Message = result.Success ? "Data is saved" : "Data is not saved";
                return result;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Data is not saved";
                return result;
            }
        }
        public ContactDto? GetContact(int Id)
        {
            try
            {
                // IEnumerable<ProductDto>? products;
                var query = (from o in _context.Contacts
                             where o.ContactId == Id
                             select new ContactDto
                             {
                                 ContactId = o.ContactId,
                                 Name = o.Name,
                                 type = o.type,
                                 Phone = o.Phone,
                                 Address = o.Address,
                                 Description = o.Description
                             });
                var data = query.SingleOrDefault(); // Use SingleOrDefault() here
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
