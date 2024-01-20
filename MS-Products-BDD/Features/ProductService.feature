Feature: ProductService
  In order to manage products
  As a product manager
  I want to be able to perform CRUD operations on products

  Scenario: Create a new product
    Given a product with ID "123" and name "Test Product"
    When I create the product
    Then the product should be created successfully
