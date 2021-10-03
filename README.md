# WebFEAutomation
Test for Exercise 2

## Execution instructions

Purchase flow is defined in the `DemoBlazePurchase.feature` gherkin file and implemented in `DemoBlazePurchaseSteps.cs` . Visual Studio's test explore should detect it as a test to be executed. NUnit's logs will register each step or issue found.

## Short description

It is a purchase flow that runs with Selenium. I am using the Page Object pattern (one page, one class) to make it more readable and maintanable. Shared page behavior is defined in an abstract class where all page classes inherit.

## Dealing with SpecFlow

I was not entirely satisfied with the gherkin implementation and also added a normal NUnit Selenium flow contained in `ETests.cs`. My main concern is about dealing with webpage navigation with gherkins statements. That flow will be identified as another test by Visual Studio. Both tests execute the same flow.

## Beware!

Demoblaze webpage works terrible bad during working hours (UTC), during the late afternoon it is suddenly a good page.
