Feature: DemoBlazePurchase
	Purchase flow in DemoBlaze

	Scenario: I make a purchase
		Given I have DemoBlaze open in my browser
		When I search for "Sony vaio i5" in the Laptops category to add it in the cart
		And I move back to the Home page
		And I search for "Dell i7 8gb" in the Laptops category to add it in the cart
		And I move to the cart page
		And I delete the "Dell i7 8gb" from the cart
		And I make the purchase
		Then the amount should be the expected