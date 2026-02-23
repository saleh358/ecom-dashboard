using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECom_wep_app.Migrations
{
    /// <inheritdoc />
    public partial class orders_to_order_items : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.Sql(
                """
                INSERT INTO [OrderItems] ([OrderId], [ProductId], [Quantity])
                SELECT [Id], [ProductId], [Quantity]
                FROM [Orders]
                WHERE [Quantity] > 0;
                """
            );

            migrationBuilder.DropColumn(name: "ProductId", table: "Orders");
            migrationBuilder.DropColumn(name: "Quantity", table: "Orders");

            migrationBuilder.Sql(
                """
                IF NOT EXISTS (SELECT 1 FROM [Orders] WHERE [Id] = 1001)
                BEGIN
                    SET IDENTITY_INSERT [Orders] ON;
                    INSERT INTO [Orders] ([Id], [CustomerId], [OrderDate]) VALUES
                    (1001, 1, '2026-01-12T10:15:00'),
                    (1002, 2, '2026-01-16T14:30:00'),
                    (1003, 3, '2026-01-18T11:20:00'),
                    (1004, 2, '2026-01-23T09:45:00'),
                    (1005, 4, '2026-01-27T18:10:00'),
                    (1006, 5, '2026-02-01T12:00:00'),
                    (1007, 1, '2026-02-05T16:40:00'),
                    (1008, 3, '2026-02-09T13:05:00'),
                    (1009, 4, '2026-02-14T19:25:00'),
                    (1010, 2, '2026-02-20T08:50:00');
                    SET IDENTITY_INSERT [Orders] OFF;
                END
                """
            );

            migrationBuilder.Sql(
                """
                IF NOT EXISTS (SELECT 1 FROM [OrderItems] WHERE [Id] = 2001)
                BEGIN
                    SET IDENTITY_INSERT [OrderItems] ON;
                    INSERT INTO [OrderItems] ([Id], [OrderId], [ProductId], [Quantity]) VALUES
                    (2001, 1001, 1, 1),
                    (2002, 1001, 3, 2),
                    (2003, 1002, 2, 1),
                    (2004, 1002, 5, 1),
                    (2005, 1003, 4, 3),
                    (2006, 1004, 1, 2),
                    (2007, 1004, 6, 1),
                    (2008, 1005, 7, 1),
                    (2009, 1005, 2, 2),
                    (2010, 1006, 3, 1),
                    (2011, 1006, 8, 1),
                    (2012, 1007, 9, 2),
                    (2013, 1007, 1, 1),
                    (2014, 1008, 10, 1),
                    (2015, 1008, 5, 2),
                    (2016, 1009, 6, 1),
                    (2017, 1009, 2, 1),
                    (2018, 1009, 4, 1),
                    (2019, 1010, 3, 2),
                    (2020, 1010, 7, 1);
                    SET IDENTITY_INSERT [OrderItems] OFF;
                END
                """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(
                """
                UPDATE o
                SET
                    [ProductId] = ISNULL(x.[ProductId], 0),
                    [Quantity] = ISNULL(x.[Quantity], 0)
                FROM [Orders] o
                OUTER APPLY (
                    SELECT TOP 1 oi.[ProductId], oi.[Quantity]
                    FROM [OrderItems] oi
                    WHERE oi.[OrderId] = o.[Id]
                    ORDER BY oi.[Id]
                ) x;
                """
            );

            migrationBuilder.DropTable(name: "OrderItems");
        }
    }
}
