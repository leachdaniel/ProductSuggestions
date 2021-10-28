CREATE TABLE dbo.Products
(
  ItemNumberId     INT           NOT NULL IDENTITY(1,1),
  Name             NVARCHAR(200) NOT NULL,
  Category         NVARCHAR(50)  NOT NULL,
  Price            MONEY         NOT NULL,
  VirtualGroupId   INT               NULL,

  CONSTRAINT PK_Product PRIMARY KEY (ItemNumberId)
)