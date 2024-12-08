Feature: Get Greetings

    Scenario: Greetings requested
        Given I am a client using no API key
        When I request 3 greetings in Dutch
        Then the response status code should be "Ok"
        And the response content should match expectations
