--Get all products from Products table
SELECT ProductID AS Id,
    SupplierId,
    CategoryId,
    ProductName,
    QuantityPerUnit,
    UnitPrice,
    UnitsInStock,
    UnitsOnOrder,
    ReorderLevel,
    Discontinued
FROM PRODUCTS;