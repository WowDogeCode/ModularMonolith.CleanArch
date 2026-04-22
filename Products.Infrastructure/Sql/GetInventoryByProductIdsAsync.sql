-- Description: Retrieves inventory details for a list of product IDs.
SELECT
    ProductID,
    Discontinued,
    UnitsInStock,
    UnitsOnOrder
FROM Products
WHERE ProductID IN (@ProductIds);