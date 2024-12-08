Feature: Get Greetings

    Scenario: Greetings requested
        Given I am a client using the Admin API key
        When I request 3 greetings in Dutch
        Then the response status code should be "Ok"
