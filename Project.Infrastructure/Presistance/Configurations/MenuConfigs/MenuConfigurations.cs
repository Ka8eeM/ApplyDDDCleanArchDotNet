
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.HostAggregate.ValueObjects;
using Project.Domain.MenuAggregate;
using Project.Domain.MenuAggregate.Entities;
using Project.Domain.MenuAggregate.ValueObjects;

namespace Project.Infrastructure.Presistance.Configurations.MenuConfigs;

public class MenuConfigurations : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        ConfigureMenusTable(builder);
        ConfigureMenuSectionsTable(builder);
        ConfigureMenuDinnersIds(builder);
        ConfigureMenuReviewIds(builder);
    }

    private void ConfigureMenuReviewIds(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.MenuReviewsIds, dib =>
        {
            dib.ToTable("MenuReviewIds");
            dib.WithOwner().HasForeignKey("MenuId");
            dib.HasKey("Id");
            dib.Property(v => v.Value)
               .HasColumnName("ReviewId")
               .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Menu.MenuReviewsIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenuDinnersIds(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.DinnerIds, dib =>
        {
            dib.ToTable("MenuDinnerIds");
            dib.WithOwner().HasForeignKey("MenuId");
            dib.HasKey("Id");
            dib.Property(v => v.Value)
               .HasColumnName("DinnerId")
               .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Menu.DinnerIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenuSectionsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.Sections, sb =>
        {
            sb.ToTable("MenuSections");

            sb.WithOwner()
              .HasForeignKey("MenuId");

            sb.HasKey("Id", "MenuId");

            sb.Property(s => s.Id)
              .HasColumnName("MenuSectionId")
              .ValueGeneratedNever()
              .HasConversion(
                id => id.Value,
                value => MenuSectionId.CreateUnique(value));

            sb.Property(x => x.Name)
              .HasMaxLength(100);

            sb.Property(x => x.Description)
              .HasMaxLength(100);

            sb.OwnsMany(s => s.Items, ib =>
            {

                ib.ToTable("MenuItems");

                ib.WithOwner()
                 .HasForeignKey("MenuSectionId", "MenuId");

                ib.HasKey(nameof(MenuItem.Id), "MenuSectionId", "MenuId");

                ib.Property(i => i.Id)
                  .HasColumnName("MenuItemId")
                  .ValueGeneratedNever()
                  .HasConversion(
                    id => id.Value,
                    value => MenuItemId.CreateUnique(value));

                ib.Property(x => x.Name)
                  .HasMaxLength(100);

                ib.Property(x => x.Description)
                  .HasMaxLength(100);

            });


            // for access the private properties
            sb.Navigation(i => i.Items).Metadata.SetField("_items");
            sb.Navigation(i => i.Items).UsePropertyAccessMode(PropertyAccessMode.Field);

        });
        // for access the private properties
        builder.Metadata.FindNavigation(nameof(Menu.Sections))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenusTable(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id.Value,
            value => MenuId.CreateUnique(value));

        builder
            .Property(n => n.Name)
            .HasMaxLength(100);

        builder
           .Property(n => n.Description)
           .HasMaxLength(100);

        builder.OwnsOne(p => p.AverageRating);

        builder
           .Property(n => n.HostId)
           .HasConversion(
            id => id.Value,
            value => HostId.CreateUnique(value));
    }
}