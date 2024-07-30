﻿using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CommonLibrary.Database
{
    public class OrderStateMap : SagaClassMap<OrderState>
    {
        protected override void Configure(EntityTypeBuilder<OrderState> entity, ModelBuilder model)
        {
            entity.Property(x => x.CurrentState).HasMaxLength(100);
            entity.Property(x => x.PaymentId).HasMaxLength(100);
            entity.Property(x => x.Products).HasMaxLength(100);
            entity.Property(x => x.CartId);
        }
    }
}
