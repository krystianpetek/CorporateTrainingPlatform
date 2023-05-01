﻿using GarageGenius.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageGenius.Modules.Users.Core.Persistance.Configurations;
internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(t => t.Permissions)
            .HasConversion(x => string.Join(',', x), x => x.Split(',', StringSplitOptions.None));
    }
}
