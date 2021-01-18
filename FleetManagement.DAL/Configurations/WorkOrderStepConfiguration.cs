using FleetManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.DAL.Configurations
{
    public class WorkOrderStepConfiguration : IEntityTypeConfiguration<WorkOrderStep>
    {
        public void Configure(EntityTypeBuilder<WorkOrderStep> builder)
        {
            builder
                .Property(wos => wos.Description)
                .HasMaxLength(200);
        }
    }
}
