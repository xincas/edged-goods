using EdgedGoods.Domain.Common.ValueObjects;
using FluentAssertions;

namespace EdgedGoods.Domain.UnitTests.Common.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Equal_WhenCurrencyNotEqual_ShouldBeFalse()
    {
        //Arrange
        var money1 = new Money(50, "USD");
        var money2 = new Money(50, "RUB");
        
        //Act
        //Assert
        (money1 == money2).Should().Be(false);
    }
    
    [Fact]
    public void Equal_WhenValueNotEqual_ShouldBeFalse()
    {
        //Arrange
        var money1 = new Money(50, "USD");
        var money2 = new Money(51, "USD");
        
        //Act
        //Assert
        (money1 == money2).Should().Be(false);
    }
    
    [Fact]
    public void Equal_WhenCurrencyAndValueEqual_ShouldBeTrue()
    {
        //Arrange
        var money1 = new Money(50, "USD");
        var money2 = new Money(50, "USD");
        
        //Act
        //Assert
        (money1 == money2).Should().Be(true);
    }

    [Fact]
    public void ComparisonLessOrEqual_WhenCurrencyAndValueEqual_ShouldBeTrue()
    {
        //Arrange
        var money1 = new Money(50, "USD");
        var money2 = new Money(50, "USD");
        
        //Act
        //Assert
        (money1 <= money2).Should().Be(true);
    }
    
    [Fact]
    public void ComparisonLessOrEqual_When1stLessAndCurrencyEqual_ShouldBeTrue()
    {
        //Arrange
        var money1 = new Money(20, "USD");
        var money2 = new Money(50, "USD");
        
        //Act
        //Assert
        (money1 <= money2).Should().Be(true);
    }
    
    [Fact]
    public void ComparisonLessOrEqual_When1stGreaterAndCurrencyEqual_ShouldBeFalse()
    {
        //Arrange
        var money1 = new Money(70, "USD");
        var money2 = new Money(50, "USD");
        
        //Act
        //Assert
        (money1 <= money2).Should().Be(false);
    }
    
    [Fact]
    public void ComparisonLessOrEqual_WhenCurrencyNotEqual_ShouldBeThrowInvalidOperationException()
    {
        //Arrange
        var money1 = new Money(20, "USD");
        var money2 = new Money(50, "RUB");
        
        //Act
        var act = () => { var b = money1 <= money2; };
        
        //Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Can't compare money value of different currency");
    }
    
    [Fact]
    public void ComparisonGreaterOrEqual_WhenCurrencyAndValueEqual_ShouldBeTrue()
    {
        //Arrange
        var money1 = new Money(50, "USD");
        var money2 = new Money(50, "USD");
        
        //Act
        //Assert
        (money1 >= money2).Should().Be(true);
    }
    
    [Fact]
    public void ComparisonGreaterOrEqual_When1stGreaterAndCurrencyEqual_ShouldBeTrue()
    {
        //Arrange
        var money1 = new Money(70, "USD");
        var money2 = new Money(50, "USD");
        
        //Act
        //Assert
        (money1 >= money2).Should().Be(true);
    }
    
    [Fact]
    public void ComparisonGreaterOrEqual_When1stLessAndCurrencyEqual_ShouldBeFalse()
    {
        //Arrange
        var money1 = new Money(20, "USD");
        var money2 = new Money(50, "USD");
        
        //Act
        //Assert
        (money1 >= money2).Should().Be(false);
    }
    
    [Fact]
    public void ComparisonGreaterOrEqual_WhenCurrencyNotEqual_ShouldBeThrowInvalidOperationException()
    {
        //Arrange
        var money1 = new Money(20, "USD");
        var money2 = new Money(50, "RUB");
        
        //Act
        var act = () => { var b = money1 >= money2; };
        
        //Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Can't compare money value of different currency");
    }
    
    [Fact]
    public void ComparisonLess_When1stLessAndCurrencyEqual_ShouldBeTrue()
    {
        //Arrange
        var money1 = new Money(20, "USD");
        var money2 = new Money(50, "USD");
        
        //Act
        //Assert
        (money1 < money2).Should().Be(true);
    }
    
    [Fact]
    public void ComparisonLess_When1stGreaterAndCurrencyEqual_ShouldBeFalse()
    {
        //Arrange
        var money1 = new Money(70, "USD");
        var money2 = new Money(50, "USD");
        
        //Act
        //Assert
        (money1 < money2).Should().Be(false);
    }
    
    [Fact]
    public void ComparisonLess_WhenValueAndCurrencyEqual_ShouldBeFalse()
    {
        //Arrange
        var money1 = new Money(50, "USD");
        var money2 = new Money(50, "USD");
        
        //Act
        //Assert
        (money1 < money2).Should().Be(false);
    }
    
    [Fact]
    public void ComparisonLess_WhenCurrencyNotEqual_ShouldBeThrowInvalidOperationException()
    {
        //Arrange
        var money1 = new Money(20, "USD");
        var money2 = new Money(50, "RUB");
        
        //Act
        var act = () => { var b = money1 < money2; };
        
        //Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Can't compare money value of different currency");
    }
    
    [Fact]
    public void ComparisonGreater_When1stGreaterAndCurrencyEqual_ShouldBeTrue()
    {
        //Arrange
        var money1 = new Money(70, "USD");
        var money2 = new Money(50, "USD");
        
        //Act
        //Assert
        (money1 > money2).Should().Be(true);
    }
    
    [Fact]
    public void ComparisonGreater_When1stLessAndCurrencyEqual_ShouldBeFalse()
    {
        //Arrange
        var money1 = new Money(20, "USD");
        var money2 = new Money(50, "USD");
        
        //Act
        //Assert
        (money1 > money2).Should().Be(false);
    }
    
    [Fact]
    public void ComparisonGreater_WhenValueAndCurrencyEqual_ShouldBeFalse()
    {
        //Arrange
        var money1 = new Money(50, "USD");
        var money2 = new Money(50, "USD");
        
        //Act
        //Assert
        (money1 > money2).Should().Be(false);
    }
    
    [Fact]
    public void ComparisonGreater_WhenCurrencyNotEqual_ShouldBeThrowInvalidOperationException()
    {
        //Arrange
        var money1 = new Money(20, "USD");
        var money2 = new Money(50, "RUB");
        
        //Act
        var act = () => { var b = money1 > money2; };
        
        //Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Can't compare money value of different currency");
    }
}