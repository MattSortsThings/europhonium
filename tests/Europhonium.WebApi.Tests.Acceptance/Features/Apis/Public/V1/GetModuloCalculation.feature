Feature: Get Modulo Calculation

    Scenario: Calculation requested
        Given I am using version 1.0 of the Public API
        When I request the calculation 10 mod 4
        Then the response status code should be "OK"
        And the response content should match expectations
