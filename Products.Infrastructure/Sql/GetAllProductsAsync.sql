-- Description: SQL query to retrieve all products from the PRODUCTS table.
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