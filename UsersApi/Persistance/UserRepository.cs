using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UsersApi.Repository;
using UsersApi.ViewModel;

namespace UsersApi.Persistance
{
    public class UserRepository : IUserRepository
    {
        //private readonly IRepository<UsersApi.Repository.User> _userRepo;
        private readonly IUow<UsersApi.Repository.User> _uow;
        public UserRepository(
            //IRepository<UsersApi.Repository.User> userRepo
            IUow<UsersApi.Repository.User> uow)
        {
            //_userRepo = userRepo;
            _uow = uow;
        }

        public async Task<List<ViewModel.User>> GetAllUser()
        {
            var users = await _uow.GetRepository().GetAll().ToListAsync();
            var usersVM = new List<ViewModel.User>();
            users.ForEach(x => usersVM.Add(this.GetViewModel(x)));
            return usersVM;
        }

        public async Task<ViewModel.User> GetUser(int id)
        {
            var user = await _uow.GetRepository().GetAll().Where(x => x.Id == id).FirstOrDefaultAsync();
            return this.GetViewModel(user);
        }

        public async Task<ViewModel.User> AddUser(ViewModel.User user)
        {
            var entity = this.GetEntity(user);
            entity.CreatedDate = DateTime.UtcNow;
            await _uow.GetRepository().AddAsync(entity);
            await _uow.CommitAsync();
            return this.GetViewModel(entity);
        }

        public async Task<ViewModel.User> UpdateUser(ViewModel.User user)
        {
            var existingUser = await _uow.GetRepository().GetAll().Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                existingUser.Avatar = user.Avatar;
                existingUser.First_name = user.First_name;
                existingUser.Last_name = user.Last_name;
                existingUser.Email = user.Email;
                existingUser.UpdatedDate = DateTime.UtcNow;
            }
            await _uow.GetRepository().UpdateAsync(existingUser);
            await _uow.CommitAsync();
            return user;
        }

        public async Task DeleteUser(int id)
        {
            var existingUser = await this.GetUser(id);
            if (existingUser != null)
            {
                await _uow.GetRepository().RemoveAsync(this.GetEntity(existingUser));
                await _uow.CommitAsync();
            }
        }

        private ViewModel.User GetViewModel(Repository.User user)
        {
            if(user == null)
            {
                return null;
            }
            return new ViewModel.User()
            {
                Avatar = user.Avatar,
                CreatedDate = user.CreatedDate,
                Email = user.Email,
                First_name = user.First_name,
                Id = user.Id,
                Last_name = user.Last_name,
                UpdatedDate = user.UpdatedDate
            };
        }
        private Repository.User GetEntity(ViewModel.User user)
        {
            if (user == null)
            {
                return null;
            }
            return new Repository.User()
            {
                Avatar = user.Avatar,
                CreatedDate = user.CreatedDate,
                Email = user.Email,
                First_name = user.First_name,
                Id = user.Id,
                Last_name = user.Last_name,
                UpdatedDate = user.UpdatedDate
            };
        }
    }
}
