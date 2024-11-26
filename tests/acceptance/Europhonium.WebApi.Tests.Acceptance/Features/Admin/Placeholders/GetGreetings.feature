Feature: Get Greetings

    Scenario: Requesting greetings
        Given I am a client using no API key
        When I request 10 greetings
        Then the response status code should be "Ok"
