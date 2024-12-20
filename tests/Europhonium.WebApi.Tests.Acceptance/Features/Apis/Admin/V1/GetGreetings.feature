Feature: Get Greetings

    Scenario: Greetings requested
        Given I am using version 1.0 of the Admin API
        When I request 4 greetings in Dutch
        Then the response status code should be "OK"
        And the response content should match expectations
