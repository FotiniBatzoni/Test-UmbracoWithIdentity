using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAuthentication.ValueObjects
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; private set; }
        public DateTime? DeactivatedAt { get; private set; }

        public Status()
        {
            IsActive = true;
        }

        public Status(bool isActive)
        {
            IsActive = isActive;
        }

        public void Update(bool isActive)
        {
            if (IsActive == isActive)
                return;

            IsActive = isActive;
            if (!isActive)
                DeactivatedAt = DateTime.UtcNow;
        }
    }
}
