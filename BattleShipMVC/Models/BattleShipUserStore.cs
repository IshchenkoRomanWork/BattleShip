using BattleShipMVC.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BattleShipMVC.Models
{
    public class BattleShipUserStore : IUserStore<BattleShipUserIdentity, int>,
                                       IUserPasswordStore<BattleShipUserIdentity, int>,
                                       IUserLockoutStore<BattleShipUserIdentity, int>,
                                       IUserTwoFactorStore<BattleShipUserIdentity, int>
    {
        IBattleShipRepository<BattleShipUserIdentity> _repository;
        public BattleShipUserStore(IBattleShipRepository<BattleShipUserIdentity> repository)
        {
            _repository = repository;
        }
        public Task CreateAsync(BattleShipUserIdentity user)
        {
            _repository.Create(user);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task DeleteAsync(BattleShipUserIdentity user)
        {
            _repository.Delete(user);

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            _repository = null;
            GC.SuppressFinalize(this);
        }

        public Task<BattleShipUserIdentity> FindByIdAsync(int userId)
        {
            var result = _repository.Get(userId);
            return Task.FromResult(result);
        }

        public Task<BattleShipUserIdentity> FindByNameAsync(string userName)
        {
            var fullList = _repository.GetAll();
            var firstName = fullList.SingleOrDefault(ident => ident.UserName == userName);
            return Task.FromResult(firstName);
        }

        public Task<int> GetAccessFailedCountAsync(BattleShipUserIdentity user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(BattleShipUserIdentity user)
        {
            return Task.FromResult(false);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(BattleShipUserIdentity user)
        {
            return Task.FromResult(DateTimeOffset.Now.AddMilliseconds(1));
        }

        public Task<string> GetPasswordHashAsync(BattleShipUserIdentity user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> GetTwoFactorEnabledAsync(BattleShipUserIdentity user)
        {
            return Task.FromResult(false);
        }

        public Task<bool> HasPasswordAsync(BattleShipUserIdentity user)
        {
            return Task.FromResult(string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task<int> IncrementAccessFailedCountAsync(BattleShipUserIdentity user)
        {
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(BattleShipUserIdentity user)
        {
            return Task.Run(() => { });
        }

        public Task SetLockoutEnabledAsync(BattleShipUserIdentity user, bool enabled)
        {
            return Task.Run(() => { });
        }

        public Task SetLockoutEndDateAsync(BattleShipUserIdentity user, DateTimeOffset lockoutEnd)
        {
            return Task.Run(() => { });
        }

        public Task SetPasswordHashAsync(BattleShipUserIdentity user, string passwordHash)
        {
            user.PasswordHash = passwordHash;

            return Task.FromResult(IdentityResult.Success);
        }

        public Task SetTwoFactorEnabledAsync(BattleShipUserIdentity user, bool enabled)
        {
            return Task.Run(() => { });
        }

        public Task UpdateAsync(BattleShipUserIdentity user)
        {
            _repository.Update(user);

            return Task.FromResult(IdentityResult.Success);
        }
    }
}