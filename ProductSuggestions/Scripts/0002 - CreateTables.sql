CREATE TABLE dbo.Products
(
  ProductID        INT           NOT NULL IDENTITY(1,1),
  Name             NVARCHAR(200) NOT NULL,
  Cancelled        BIT           NOT NULL,
  Category         NVARCHAR(50)  NOT NULL,
  Price            MONEY         NOT NULL,
  QuantityOnHand   INT           NOT NULL,

  CONSTRAINT PK_Product PRIMARY KEY (ProductID)
)