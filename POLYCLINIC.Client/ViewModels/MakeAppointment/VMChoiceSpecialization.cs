using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.Client.Infrastructure.Commands;
using POLYCLINIC.Client.Interfaces;
using POLYCLINIC.Data.Entities;
using System.Collections.Generic;

namespace POLYCLINIC.Client.ViewModels
{
    class VMChoiceSpecialization : VMBase
    {
        private readonly IBaseManager baseManager;

        public IEnumerable<Specialization> Specializations => baseManager.Specialization.List;

        private ChooseSpecializationCommand chooseSpecializationCommand;
        public ChooseSpecializationCommand ChooseSpecializationCommand => chooseSpecializationCommand ??
                  (chooseSpecializationCommand = new ChooseSpecializationCommand(IoC.Get<IAppointmentNavigation>(), IoC.Get<IСreatingVoucherService>()));

        public VMChoiceSpecialization()
        {
            this.baseManager = IoC.Get<IBaseManager>();
            baseManager.Specialization.TableChanged += (sender, e) => OnPropertyChanged();
        }
    }
}
