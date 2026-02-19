using DataEntities;

namespace TinyShop.Tests;

[TestClass]
public class ProductTests
{
    #region Default Value Tests

    [TestMethod]
    public void Product_NewInstance_HasDefaultValues()
    {
        // Verifies that a new Product instance has expected default values
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.AreEqual(0, product.Id, "Id should default to 0");
        Assert.IsNull(product.Name, "Name should default to null");
        Assert.IsNull(product.Description, "Description should default to null");
        Assert.AreEqual(0m, product.Price, "Price should default to 0");
        Assert.IsNull(product.ImageUrl, "ImageUrl should default to null");
    }

    #endregion

    #region Id Property Tests

    [TestMethod]
    [DataRow(1)]
    [DataRow(100)]
    [DataRow(int.MaxValue)]
    public void Id_SetPositiveValue_ReturnsCorrectValue(int expectedId)
    {
        // Verifies that the Id property correctly stores and returns positive values
        // Arrange
        var product = new Product();

        // Act
        product.Id = expectedId;

        // Assert
        Assert.AreEqual(expectedId, product.Id, $"Id should be {expectedId}");
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    [DataRow(int.MinValue)]
    public void Id_SetZeroOrNegativeValue_ReturnsCorrectValue(int expectedId)
    {
        // Verifies that the Id property accepts zero and negative values (edge cases)
        // Arrange
        var product = new Product();

        // Act
        product.Id = expectedId;

        // Assert
        Assert.AreEqual(expectedId, product.Id, $"Id should be {expectedId}");
    }

    #endregion

    #region Name Property Tests

    [TestMethod]
    [DataRow("Tent")]
    [DataRow("Camping Gear")]
    [DataRow("Outdoor Adventure Kit Pro")]
    public void Name_SetValidString_ReturnsCorrectValue(string expectedName)
    {
        // Verifies that the Name property correctly stores and returns valid strings
        // Arrange
        var product = new Product();

        // Act
        product.Name = expectedName;

        // Assert
        Assert.AreEqual(expectedName, product.Name, $"Name should be '{expectedName}'");
    }

    [TestMethod]
    public void Name_SetEmptyString_ReturnsEmptyString()
    {
        // Verifies that the Name property accepts an empty string
        // Arrange
        var product = new Product();

        // Act
        product.Name = string.Empty;

        // Assert
        Assert.AreEqual(string.Empty, product.Name, "Name should be an empty string");
    }

    [TestMethod]
    public void Name_SetNull_ReturnsNull()
    {
        // Verifies that the Name property accepts null values
        // Arrange
        var product = new Product { Name = "Initial Value" };

        // Act
        product.Name = null;

        // Assert
        Assert.IsNull(product.Name, "Name should be null");
    }

    [TestMethod]
    public void Name_SetWhitespace_ReturnsWhitespace()
    {
        // Verifies that the Name property accepts whitespace strings
        // Arrange
        var product = new Product();
        var whitespace = "   ";

        // Act
        product.Name = whitespace;

        // Assert
        Assert.AreEqual(whitespace, product.Name, "Name should preserve whitespace");
    }

    #endregion

    #region Description Property Tests

    [TestMethod]
    [DataRow("A high-quality tent for camping.")]
    [DataRow("Short")]
    [DataRow("This is a very long description that contains multiple sentences. It describes the product in great detail and includes all the important features and specifications.")]
    public void Description_SetValidString_ReturnsCorrectValue(string expectedDescription)
    {
        // Verifies that the Description property correctly stores and returns valid strings
        // Arrange
        var product = new Product();

        // Act
        product.Description = expectedDescription;

        // Assert
        Assert.AreEqual(expectedDescription, product.Description, "Description should match the set value");
    }

    [TestMethod]
    public void Description_SetEmptyString_ReturnsEmptyString()
    {
        // Verifies that the Description property accepts an empty string
        // Arrange
        var product = new Product();

        // Act
        product.Description = string.Empty;

        // Assert
        Assert.AreEqual(string.Empty, product.Description, "Description should be an empty string");
    }

    [TestMethod]
    public void Description_SetNull_ReturnsNull()
    {
        // Verifies that the Description property accepts null values
        // Arrange
        var product = new Product { Description = "Initial Description" };

        // Act
        product.Description = null;

        // Assert
        Assert.IsNull(product.Description, "Description should be null");
    }

    #endregion

    #region Price Property Tests

    [TestMethod]
    [DataRow("19.99")]
    [DataRow("0.01")]
    [DataRow("99999.99")]
    public void Price_SetPositiveValue_ReturnsCorrectValue(string priceString)
    {
        // Verifies that the Price property correctly stores and returns positive decimal values
        // Arrange
        var product = new Product();
        var expectedPrice = decimal.Parse(priceString);

        // Act
        product.Price = expectedPrice;

        // Assert
        Assert.AreEqual(expectedPrice, product.Price, $"Price should be {expectedPrice}");
    }

    [TestMethod]
    public void Price_SetZero_ReturnsZero()
    {
        // Verifies that the Price property accepts zero value
        // Arrange
        var product = new Product { Price = 10.00m };

        // Act
        product.Price = 0m;

        // Assert
        Assert.AreEqual(0m, product.Price, "Price should be 0");
    }

    [TestMethod]
    [DataRow("-1.00")]
    [DataRow("-99.99")]
    public void Price_SetNegativeValue_ReturnsNegativeValue(string priceString)
    {
        // Verifies that the Price property accepts negative values (edge case - may need validation in business logic)
        // Arrange
        var product = new Product();
        var expectedPrice = decimal.Parse(priceString);

        // Act
        product.Price = expectedPrice;

        // Assert
        Assert.AreEqual(expectedPrice, product.Price, $"Price should be {expectedPrice}");
    }

    [TestMethod]
    public void Price_SetMaxDecimalValue_ReturnsCorrectValue()
    {
        // Verifies that the Price property handles large decimal values
        // Arrange
        var product = new Product();
        var expectedPrice = decimal.MaxValue;

        // Act
        product.Price = expectedPrice;

        // Assert
        Assert.AreEqual(expectedPrice, product.Price, "Price should handle maximum decimal value");
    }

    [TestMethod]
    public void Price_SetPrecisionValue_RetainsPrecision()
    {
        // Verifies that the Price property retains decimal precision
        // Arrange
        var product = new Product();
        var expectedPrice = 123.456789m;

        // Act
        product.Price = expectedPrice;

        // Assert
        Assert.AreEqual(expectedPrice, product.Price, "Price should retain decimal precision");
    }

    #endregion

    #region ImageUrl Property Tests

    [TestMethod]
    [DataRow("https://example.com/image.jpg")]
    [DataRow("/images/product1.png")]
    [DataRow("data:image/png;base64,iVBORw0KGgo=")]
    public void ImageUrl_SetValidUrl_ReturnsCorrectValue(string expectedUrl)
    {
        // Verifies that the ImageUrl property correctly stores and returns valid URL strings
        // Arrange
        var product = new Product();

        // Act
        product.ImageUrl = expectedUrl;

        // Assert
        Assert.AreEqual(expectedUrl, product.ImageUrl, $"ImageUrl should be '{expectedUrl}'");
    }

    [TestMethod]
    public void ImageUrl_SetEmptyString_ReturnsEmptyString()
    {
        // Verifies that the ImageUrl property accepts an empty string
        // Arrange
        var product = new Product();

        // Act
        product.ImageUrl = string.Empty;

        // Assert
        Assert.AreEqual(string.Empty, product.ImageUrl, "ImageUrl should be an empty string");
    }

    [TestMethod]
    public void ImageUrl_SetNull_ReturnsNull()
    {
        // Verifies that the ImageUrl property accepts null values
        // Arrange
        var product = new Product { ImageUrl = "https://example.com/image.jpg" };

        // Act
        product.ImageUrl = null;

        // Assert
        Assert.IsNull(product.ImageUrl, "ImageUrl should be null");
    }

    #endregion

    #region Full Object Tests

    [TestMethod]
    public void Product_SetAllProperties_ReturnsCorrectValues()
    {
        // Verifies that all properties can be set together and return correct values
        // Arrange
        var product = new Product();
        var expectedId = 42;
        var expectedName = "Camping Tent";
        var expectedDescription = "A durable 4-person camping tent";
        var expectedPrice = 199.99m;
        var expectedImageUrl = "https://example.com/tent.jpg";

        // Act
        product.Id = expectedId;
        product.Name = expectedName;
        product.Description = expectedDescription;
        product.Price = expectedPrice;
        product.ImageUrl = expectedImageUrl;

        // Assert
        Assert.AreEqual(expectedId, product.Id, "Id should match");
        Assert.AreEqual(expectedName, product.Name, "Name should match");
        Assert.AreEqual(expectedDescription, product.Description, "Description should match");
        Assert.AreEqual(expectedPrice, product.Price, "Price should match");
        Assert.AreEqual(expectedImageUrl, product.ImageUrl, "ImageUrl should match");
    }

    [TestMethod]
    public void Product_ObjectInitializer_SetsAllProperties()
    {
        // Verifies that object initializer syntax works correctly
        // Arrange & Act
        var product = new Product
        {
            Id = 1,
            Name = "Backpack",
            Description = "Hiking backpack with multiple compartments",
            Price = 89.50m,
            ImageUrl = "/images/backpack.png"
        };

        // Assert
        Assert.AreEqual(1, product.Id, "Id should be 1");
        Assert.AreEqual("Backpack", product.Name, "Name should be 'Backpack'");
        Assert.AreEqual("Hiking backpack with multiple compartments", product.Description, "Description should match");
        Assert.AreEqual(89.50m, product.Price, "Price should be 89.50");
        Assert.AreEqual("/images/backpack.png", product.ImageUrl, "ImageUrl should match");
    }

    #endregion
}
