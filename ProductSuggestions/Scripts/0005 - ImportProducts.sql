SET IDENTITY_INSERT dbo.Products ON

INSERT INTO dbo.Products
(ProductID,  Name,                                             Category,                  Price, QuantityOnHand, Cancelled)
VALUES
(10,         'BLACK+DECKER DLX1050B 12-cup Programmable',      'Coffee Maker',            34.99,  0,   0),
(11,         'Veken French Press Coffee & Tea Maker',          'Coffee Maker',            17.99,  1,   1),
(12,         'Ninja Specialty Coffee Maker',                   'Coffee Maker',           149.95,  0,   1),
(13,         'Cuisinart SS-15P1 Coffee Center',                'Coffee Maker',           159.99, 33,   0),
(14,         'Mr. Coffee 10 Cup Coffee Maker | Optimal Brew',  'Coffee Maker',            69.99, 62,   1)

SET IDENTITY_INSERT dbo.Products OFF