using GameRateApp.Data.Entities;
using GameRateApp.Data.Repositories;
using GameRateApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRateApp.Business.Operations.Setting
{
    public class SettingManager : ISettingService
    {
        private readonly IRepository<SettingEntity> _settingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SettingManager(IRepository<SettingEntity> settingRepository, IUnitOfWork unitOfWork)
        {
            _settingRepository = settingRepository;
            _unitOfWork = unitOfWork;
        }

        public bool GetMaintenenceState()
        {
            var maintenenceState = _settingRepository.GetById(1).MaintenenceMode;
            return maintenenceState;
        }

        public async Task ToggleMaintenence()
        {
            var setting = _settingRepository.GetById(1);

            setting.MaintenenceMode = !setting.MaintenenceMode;

            _settingRepository.Update(setting);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
