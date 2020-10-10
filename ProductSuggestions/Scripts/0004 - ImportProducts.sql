SET IDENTITY_INSERT dbo.Products ON

INSERT INTO dbo.Products
(ProductID, Name,                                             Category,                  Price, QuantityOnHand, Cancelled)
VALUES
(5,         'Mr. Coffee Simple Brew Coffee Maker',            'Coffee Maker',            25.47, 10,   0),
(6,         'Cuisinart DCC-3200P1 Perfectemp Coffee Maker',   'Coffee Maker',            99.95, 74,   0),
(7,         'Cuisinart CHW-12P1 Programmable Coffeemaker',    'Coffee Maker',            99.95, 83,   0),
(8,         'Hamilton Beach FlexBrew Coffee Maker',           'Coffee Maker',           109.68, 62,   0),
(9,         'Sboly Single Serve Coffee Maker Brewer',         'Coffee Maker',            58.96, 62,   0)

SET IDENTITY_INSERT dbo.Products OFF