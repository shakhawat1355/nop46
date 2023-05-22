using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using Microsoft.AspNetCore.Http.HttpResults;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Migrations.UpgradeTo460
{

    [NopMigration("2023/05/18 14:30:00:0000000", "addnewProperty", UpdateMigrationType.Data, MigrationProcessType.Update)]

    public class ProductSerial: AutoReversingMigration
    {
        /// <summary>Collect the UP migration expressions</summary>
        
        
        public override void Up()
        {
            Create.Column(nameof(Product.ProductSerial))
            .OnTable(nameof(Product))
            .AsString(300)
            .Nullable();
        }
    }
}
