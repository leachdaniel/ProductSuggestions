SET IDENTITY_INSERT dbo.Products ON;
INSERT INTO dbo.Products
(ItemNumberId, Name,                                                Category,           Price, VirtualGroupId)
VALUES
(1,  'Cuisinart Coffee Maker',                            'Coffee Maker',     95.79, NULL),
(2,  'Hamilton Beach Coffee Maker',                       'Coffee Maker',     60.67, NULL),
(3,  'Keurig Coffee Maker',                               'Coffee Maker',     82.44, NULL),
(4,  'Mr. Coffee Coffee Maker',                           'Coffee Maker',     68.44, NULL),
(5,  'Mr. Coffee Simple Brew Coffee Maker',               'Coffee Maker',     25.47, NULL),
(6,  'Cuisinart DCC-3200P1 Perfectemp Coffee Maker',      'Coffee Maker',     99.95, NULL),
(7,  'Cuisinart CHW-12P1 Programmable Coffeemaker',       'Coffee Maker',     99.95, NULL),
(8,  'Hamilton Beach FlexBrew Coffee Maker',              'Coffee Maker',    109.68, NULL),
(9,  'Sboly Single Serve Coffee Maker Brewer',            'Coffee Maker',     58.96, NULL),
(10, 'BLACK+DECKER DLX1050B 12-cup Programmable',         'Coffee Maker',     34.99, NULL),
(11, 'Veken French Press Coffee & Tea Maker',             'Coffee Maker',     17.99, NULL),
(12, 'Ninja Specialty Coffee Maker',                      'Coffee Maker',    149.95, NULL),
(13, 'Cuisinart SS-15P1 Coffee Center (black)',           'Coffee Maker',    159.99, 1),
(14, 'Cuisinart SS-15P1 Coffee Center (red)',             'Coffee Maker',    159.99, 1),
(15, 'Cuisinart SS-15P1 Coffee Center (white)',           'Coffee Maker',    163.99, 1),
(16, 'Cuisinart SS-15P1 Coffee Center (stainless steel)', 'Coffee Maker',    165.99, 1),
(17, 'Mr. Coffee 10 Cup Coffee Maker | Optimal Brew',     'Coffee Maker',     69.99, NULL)
SET IDENTITY_INSERT dbo.Products OFF;