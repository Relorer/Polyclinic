using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLYCLINIC.BLL.Services
{
    public class CancelVoucherService : ICancleVoucherService
    {
        private readonly IBaseManager baseManager;
        public CancelVoucherService(IBaseManager baseManager)
        {
            this.baseManager = baseManager;
        }

        public void CancelVoucher(int voucherId)
        {
            baseManager.VoucherForAppointment.Find(voucherId).State = VoucherState.Canceled;
            baseManager.Save();
        }
    }
}
