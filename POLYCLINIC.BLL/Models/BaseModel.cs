using POLYCLINIC.Data.Entities;

namespace POLYCLINIC.BLL.Models
{
    public abstract class BaseModel<EntityType> where EntityType : BaseEntity
    {
        public EntityType Entity { get; set; }
    }
}
