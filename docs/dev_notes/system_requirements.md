# System requirements

This document details the system requirements for the *Europhonium* project.

- [System requirements](#system-requirements)
  - [Summary](#summary)
  - [User profiles](#user-profiles)
    - [The Euro-Fan and the *Public Module*](#the-euro-fan-and-the-public-module)
    - [The Admin and the *Admin Module*](#the-admin-and-the-admin-module)
  - [A - *Admin Module* requirements](#a---admin-module-requirements)
    - [A/1 - Countries](#a1---countries)
      - [A/1/1 - Create Country](#a11---create-country)
      - [A/1/2 - Get Country](#a12---get-country)
      - [A/1/3 - Get Countries](#a13---get-countries)
      - [A/1/4 - Delete Country](#a14---delete-country)
    - [A/2 - Contests](#a2---contests)
      - [A/2/1 - Create Contest](#a21---create-contest)
      - [A/2/2 - Get Contest](#a22---get-contest)
      - [A/2/3 - Get Contests](#a23---get-contests)
      - [A/2/4 - Delete Contest](#a24---delete-contest)
      - [A/2/5 - Create Broadcast](#a25---create-broadcast)
    - [A/3 - Broadcasts](#a3---broadcasts)
      - [A/3/1 - Get Broadcast](#a31---get-broadcast)
      - [A/3/2 - Get Broadcasts](#a32---get-broadcasts)
      - [A/3/3 - Delete Broadcast](#a33---delete-broadcast)
      - [A/3/4 - Award Points](#a34---award-points)
    - [A/4 - Security](#a4---security)
      - [A/4/1 - Admin API key security](#a41---admin-api-key-security)
  - [P - *Public Module* requirements](#p---public-module-requirements)
    - [P/1 - Queryables](#p1---queryables)
      - [P/1/1 - Get Queryable Broadcasts](#p11---get-queryable-broadcasts)
      - [P/1/2 - Get Queryable Contests](#p12---get-queryable-contests)
      - [P/1/3 - Get Queryable Countries](#p13---get-queryable-countries)
    - [P/2 - Competitor by broadcast rankings](#p2---competitor-by-broadcast-rankings)
      - [P/2/1 - Get Competitor By Broadcast Points Average Rankings](#p21---get-competitor-by-broadcast-points-average-rankings)
      - [P/2/2 - Get Competitor By Broadcast Points Consensus Rankings](#p22---get-competitor-by-broadcast-points-consensus-rankings)
      - [P/2/3 - Get Competitor By Broadcast Points In Range Rankings](#p23---get-competitor-by-broadcast-points-in-range-rankings)
      - [P/2/4 - Get Competitor By Broadcast Points Share Rankings](#p24---get-competitor-by-broadcast-points-share-rankings)
    - [P/3 - Competitor by contest rankings](#p3---competitor-by-contest-rankings)
      - [P/3/1 - Get Competitor By Contest Points Average Rankings](#p31---get-competitor-by-contest-points-average-rankings)
      - [P/3/2 - Get Competitor By Contest Points Consensus Rankings](#p32---get-competitor-by-contest-points-consensus-rankings)
      - [P/3/3 - Get Competitor By Contest Points In Range Rankings](#p33---get-competitor-by-contest-points-in-range-rankings)
      - [P/3/4 - Get Competitor By Contest Points Share Rankings](#p34---get-competitor-by-contest-points-share-rankings)
    - [P/4 - Competitor overall rankings](#p4---competitor-overall-rankings)
      - [P/4/1 - Get Competitor Overall Points Average Rankings](#p41---get-competitor-overall-points-average-rankings)
      - [P/4/2 - Get Competitor Overall Points Consensus Rankings](#p42---get-competitor-overall-points-consensus-rankings)
      - [P/4/3 - Get Competitor Overall Points In Range Rankings](#p43---get-competitor-overall-points-in-range-rankings)
      - [P/4/4 - Get Competitor Overall Points Share Rankings](#p44---get-competitor-overall-points-share-rankings)
    - [P/5 - Voter overall rankings](#p5---voter-overall-rankings)
      - [P/5/1 - Get Voter Overall Points Average Rankings](#p51---get-voter-overall-points-average-rankings)
      - [P/5/2 - Get Voter Overall Points Consensus Rankings](#p52---get-voter-overall-points-consensus-rankings)
      - [P/5/3 - Get Voter Overall Points In Range Rankings](#p53---get-voter-overall-points-in-range-rankings)
      - [P/5/4 - Get Voter Overall Points Share Rankings](#p54---get-voter-overall-points-share-rankings)
      - [P/5/4 - Get Voter Overall Points Similarity Rankings](#p54---get-voter-overall-points-similarity-rankings)
    - [P/6 - Security](#p6---security)
      - [P/6/1 - Public API key security](#p61---public-api-key-security)
  - [Non-functional requirements](#non-functional-requirements)

## Summary

- *Europhonium* is a .NET web API for analysing voting patterns in the Eurovision Song Contest, 2016-present.
- *Europhonium* is composed of two modules: the *Public Module* and the *Admin Module*.
- The *Public Module* provides a wide range of analytics queries that can be applied on voting data held in the system database.
- The *Admin Module* provides the means to populate the system database with data for countries, contests and broadcasts, to be queried by the *Public Module*.

## User profiles

Two user profiles are defined: the **Euro-Fan**, who uses the *Public Module*, and the **Admin**, who uses the *Admin Module*. The Euro-Fan and the Admin **do not interact in any way**.

### The Euro-Fan and the *Public Module*

The Euro-Fan is an anonymous member of the public located anywhere in the world, who interacts with the system using its *Public Module*.

The Euro-Fan has at least some familiarity with the Eurovision Song Contest and how it works.

The Euro-Fan uses the system because they are interested in discovering insights from the way the votes have been distributed in recent contests.

Questions posed by the Euro-Fan may include:

- "Of all the competing countries in all the broadcasts, which competitors received the highest average points award value?"
- "Of all the competing countries in the 2022 contest, which competitor received the highest share of the maximum possible points they could have received?"
- "Of all the competing countries overall, which competitor has received the highest relative frequency of non-zero televote points?"
- "Of all the competing countries overall, in the grand finals of the contests between 2023 and the present, which competitor has had the highest consensus between the jury points and the televote points they received?"
- "Of all the voting countries overall, which voter has awarded jury points most similar to those awarded by the UK?"

### The Admin and the *Admin Module*

The Admin is a single software developer (myself), who interacts with the system using its *Admin Module*.

The Admin is very familiar with the Eurovision Song Contest and how it works. They are also very familiar with the internal workings of the system, since they wrote it.

The Admin uses the system because they want to populate it with the data that is to be queried to support the *Public Module*'s functions, that is, the countries, contests, and broadcasts for the Eurovision Song Contest, 2016-present.

Additionally, the Admin wishes to gain some first-hand experience of working with Domain-Driven Design concepts and methods. Therefore, the *Admin Module* is implemented using a domain module with entities, rules and behaviours that imitate the way the voting is conducted in the real-world Eurovision Song Contest, even though it would be sufficient simply to seed the system database from a single JSON file.

## A - *Admin Module* requirements

### A/1 - Countries

#### A/1/1 - Create Country

**User story**

*As the Admin, I want to create a new country in the system, so that I can reference it in contests and broadcasts that I will go on to create.*

**Request**

```http
POST {{host}}/api/admin/countries
```

```json
{
  "countryCode": "GB",
  "name": "United Kingdom"
}
```

**Response**

```http
201 CreatedAtRoute
Location: {{host}}/api/admin/countries/00000000-0000-0000-0000-000000000000
```

```json
{
  "country": {
    "id": "00000000-0000-0000-0000-000000000000",
    "countryCode": "GB",
    "name": "United Kingdom",
    "participatingContestIds": [ ],
    "competingBroadcastIds": [ ],
    "votingBroadcastIds": [ ]
  }
}
```

#### A/1/2 - Get Country

**User story**

*As the Admin, I want to retrieve a single country from the system, identified by supplying its ID, so that I can see the contests and broadcasts in which it is involved.*

**Request**

```http
GET {{host}}/api/admin/countries/00000000-0000-0000-0000-000000000000
```

**Response**

```http
200 Ok
```

```json
{
  "country": {
    "id": "00000000-0000-0000-0000-000000000000",
    "countryCode": "GB",
    "name": "United Kingdom",
    "participatingContestIds": [
      "00000000-0000-0000-0000-000000000000"
    ],
    "competingBroadcastIds": [
      "00000000-0000-0000-0000-000000000000",
      "00000000-0000-0000-0000-000000000000"
    ],
    "votingBroadcastIds": [
      "00000000-0000-0000-0000-000000000000",
      "00000000-0000-0000-0000-000000000000"
    ]
  }
}
```

#### A/1/3 - Get Countries

**User story**

*As the Admin, I want to retrieve all the countries from the system, ordered by country code, so that I can determine which countries I still need to create.*

**Request**

```http
GET {{host}}/api/admin/countries
```

**Response**

```http
200 Ok
```

```json
{
  "countries": [
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "countryCode": "GB",
      "name": "United Kingdom",
      "participatingContestIds": [
        "00000000-0000-0000-0000-000000000000"
      ],
      "competingBroadcastIds": [
        "00000000-0000-0000-0000-000000000000",
        "00000000-0000-0000-0000-000000000000"
      ],
      "votingBroadcastIds": [
        "00000000-0000-0000-0000-000000000000",
        "00000000-0000-0000-0000-000000000000"
      ]
    }
  ]
}
```

#### A/1/4 - Delete Country

**User story**

*As the Admin, I want to delete a single country from the system, identified by supplying its ID, so that I can recreate it later with corrected details.*

**Request**

```http
DELETE {{host}}/api/admin/countries/00000000-0000-0000-0000-000000000000
```

**Response**

```http
204 NoContent
```

### A/2 - Contests

#### A/2/1 - Create Contest

**User story**

*As the Admin, I want to create a new contest in the system, so that I can use it to create its constituent broadcasts.*

**Request**

```http
POST {{host}}/api/admin/contests
```

```json
{
  "contestYear": 2022,
  "hostCityName": "Turin",
  "votingRules": "Stockholm",
  "semiFinal1Participants": [
    {
      "countryId": "00000000-0000-0000-0000-000000000000",
      "performerName": "Malik Harris",
      "songTitle": "Rockstars"
    }
  ],
  "semiFinal2Participants": [
    {
      "countryId": "00000000-0000-0000-0000-000000000000",
      "performerName": "Sam Ryder",
      "songTitle": "Space Man"
    }
  ],
  "nonCompetingCountryIds": [ ]
}
```

```json
{
  "contestYear": 2023,
  "hostCityName": "Liverpool",
  "votingRules": "Liverpool",
  "semiFinal1Participants": [
    {
      "countryId": "00000000-0000-0000-0000-000000000000",
      "performerName": "Lord of the Lost",
      "songTitle": "Blood & Glitter"
    }
  ],
  "semiFinal2Participants": [
    {
      "countryId": "00000000-0000-0000-0000-000000000000",
      "performerName": "Mae Muller",
      "songTitle": "I Wrote A Song"
    }
  ],
  "nonCompetingCountryIds": [
    "00000000-0000-0000-0000-000000000000"
  ]
}
```

**Response**

```http
201 CreatedAtRoute
Location: {{host}}/api/admin/contests/00000000-0000-0000-0000-000000000000
```

```json
{
  "contest": {
    "id": "00000000-0000-0000-0000-000000000000",
    "contestYear": 2022,
    "hostCityName": "Turin",
    "votingRules": "Stockholm",
    "completed": false,
    "participatingCountries": [
      {
        "id": "00000000-0000-0000-0000-000000000000",
        "performerName": "Malik Harris",
        "songTitle": "Rockstars",
        "votingRoles": [
          {
            "contestStage": "SemiFinal1",
            "televoteOnly": false
          },
          {
            "contestStage": "GrandFinal",
            "televoteOnly": false
          }
        ]
      },
      {
        "id": "00000000-0000-0000-0000-000000000000",
        "performerName": "Sam Ryder",
        "songTitle": "Space Man",
        "votingRoles": [
          {
            "contestStage": "SemiFinal2",
            "televoteOnly": false
          },
          {
            "contestStage": "GrandFinal",
            "televoteOnly": false
          }
        ]
      }
    ],
    "constituentBroadcasts": [ ]
  }
}
```

```json
{
  "contest": {
    "id": "00000000-0000-0000-0000-000000000000",
    "contestYear": 2023,
    "hostCityName": "Liverpool",
    "votingRules": "Liverpool",
    "completed": false,
    "participatingCountries": [
      {
        "id": "00000000-0000-0000-0000-000000000000",
        "performerName": "Lord of the Lost",
        "songTitle": "Blood & Glitter",
        "votingRoles": [
          {
            "contestStage": "SemiFinal1",
            "televoteOnly": true
          },
          {
            "contestStage": "GrandFinal",
            "televoteOnly": false
          }
        ]
      },
      {
        "id": "00000000-0000-0000-0000-000000000000",
        "performerName": "Mae Muller",
        "songTitle": "I Wrote A Song",
        "votingRoles": [
          {
            "contestStage": "SemiFinal2",
            "televoteOnly": true
          },
          {
            "contestStage": "GrandFinal",
            "televoteOnly": false
          }
        ]
      },
      {
        "id": "00000000-0000-0000-0000-000000000000",
        "performerName": null,
        "songTitle": null,
        "votingRoles": [
          {
            "contestStage": "SemiFinal1",
            "televoteOnly": true
          },
          {
            "contestStage": "SemiFinal2",
            "televoteOnly": true
          },
          {
            "contestStage": "GrandFinal",
            "televoteOnly": true
          }
        ]
      }
    ],
    "constituentBroadcasts": [ ]
  }
}
```

#### A/2/2 - Get Contest

**User story**

*As the Admin, I want to retrieve a single contest from the system, identified by supplying its ID, so that I can see whether it is completed.*

**Request**

```http
GET {{host}}/api/admin/contests/00000000-0000-0000-0000-000000000000
```

**Response**

```http
200 Ok
```

```json
{
  "contest": {
    "id": "00000000-0000-0000-0000-000000000000",
    "contestYear": 2022,
    "hostCityName": "Turin",
    "votingRules": "Stockholm",
    "completed": false,
    "participatingCountries": [
      {
        "id": "00000000-0000-0000-0000-000000000000",
        "performerName": "Malik Harris",
        "songTitle": "Rockstars",
        "votingRoles": [
          {
            "contestStage": "SemiFinal1",
            "televoteOnly": false
          },
          {
            "contestStage": "GrandFinal",
            "televoteOnly": false
          }
        ]
      },
      {
        "id": "00000000-0000-0000-0000-000000000000",
        "performerName": "Sam Ryder",
        "songTitle": "Space Man",
        "votingRoles": [
          {
            "contestStage": "SemiFinal2",
            "televoteOnly": false
          },
          {
            "contestStage": "GrandFinal",
            "televoteOnly": false
          }
        ]
      }
    ],
    "constituentBroadcasts": [
      {
        "broadcastId": "00000000-0000-0000-0000-000000000000",
        "contestStage": "SemiFinal1",
        "completed": false
      }
    ]
  }
}
```

#### A/2/3 - Get Contests

**User story**

*As the Admin, I want to retrieve all the contests from the system, ordered by contest year, so that I can determine which contests I still need to create.*

**Request**

```http
GET {{host}}/api/admin/contests
```

**Response**

```http
200 Ok
```

```json
{
  "contests": [
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "contestYear": 2022,
      "hostCityName": "Turin",
      "votingRules": "Stockholm",
      "completed": false,
      "participatingCountries": [
        {
          "id": "00000000-0000-0000-0000-000000000000",
          "performerName": "Malik Harris",
          "songTitle": "Rockstars",
          "votingRoles": [
            {
              "contestStage": "SemiFinal1",
              "televoteOnly": false
            },
            {
              "contestStage": "GrandFinal",
              "televoteOnly": false
            }
          ]
        },
        {
          "id": "00000000-0000-0000-0000-000000000000",
          "performerName": "Sam Ryder",
          "songTitle": "Space Man",
          "votingRoles": [
            {
              "contestStage": "SemiFinal2",
              "televoteOnly": false
            },
            {
              "contestStage": "GrandFinal",
              "televoteOnly": false
            }
          ]
        }
      ],
      "constituentBroadcasts": [
        {
          "broadcastId": "00000000-0000-0000-0000-000000000000",
          "contestStage": "SemiFinal1",
          "completed": false
        }
      ]
    }
  ]
}
```

#### A/2/4 - Delete Contest

**User story**

*As the Admin, I want to delete a single contest from the system, identified by supplying its ID, so that I can recreate it later with corrected details.*

**Request**

```http
DELETE {{host}}/api/admin/contests/00000000-0000-0000-0000-000000000000
```

**Response**

```http
204 NoContent
```

#### A/2/5 - Create Broadcast

**User story**

*As the Admin, I want to create a broadcast for a single contest in the system, identified by supplying its ID, so that I can award the points in the broadcast.*

**Request**

```http
POST {{host}}/api/admin/contests/00000000-0000-0000-0000-000000000000/broadcasts
```

```json
{
  "contestStage": "SemiFinal1",
  "transmissionDate": "2022-05-09",
  "competingCountryIds": [
    "00000000-0000-0000-0000-000000000000",
    "00000000-0000-0000-0000-000000000000"
  ]
}
```

**Response**

```http
201 CreatedAtRoute
Location: {{host}}/api/admin/broadcasts/00000000-0000-0000-0000-000000000000
```

```json
{
  "broadcast": {
    "id": "00000000-0000-0000-0000-000000000000",
    "contestId": "00000000-0000-0000-0000-000000000000",
    "contestStage": "SemiFinal1",
    "transmissionDate": "2022-05-09",
    "televoteOnly": false,
    "completed": false,
    "competingCountries": [
      {
        "id": "00000000-0000-0000-0000-000000000000",
        "performingOrder": 1,
        "finishingOrder": 1,
        "televotePoints": 0,
        "juryPoints": 0,
        "totalPoints": 0,
        "televoteAwards": [ ],
        "juryAwards": [ ]
      }
    ],
    "votingCountries": [
      {
        "id": "00000000-0000-0000-0000-000000000000",
        "canAwardTelevotePoints": true,
        "canAwardJuryPoints": true
      }
    ]
  }
}
```

### A/3 - Broadcasts

#### A/3/1 - Get Broadcast

**User story**

*As the Admin, I want to retrieve a single broadcast from the system, identified by supplying its ID, so that I can see which points I need to award, if any.*

**Request**

```http
GET {{host}}/api/admin/broadcasts/00000000-0000-0000-0000-000000000000
```

**Response**

```http
200 Ok
```

```json
{
  "broadcast": {
    "id": "00000000-0000-0000-0000-000000000000",
    "contestId": "00000000-0000-0000-0000-000000000000",
    "contestStage": "SemiFinal1",
    "transmissionDate": "2022-05-09",
    "televoteOnly": false,
    "completed": false,
    "competingCountries": [
      {
        "id": "00000000-0000-0000-0000-000000000000",
        "performingOrder": 1,
        "finishingOrder": 1,
        "televotePoints": 12,
        "juryPoints": 0,
        "totalPoints": 12,
        "televoteAwards": [
          {
            "votingCountryId": "00000000-0000-0000-0000-000000000000",
            "pointsValue": 12
          }
        ],
        "juryAwards": [
          {
            "votingCountryId": "00000000-0000-0000-0000-000000000000",
            "pointsValue": 0
          }
        ]
      }
    ],
    "votingCountries": [
      {
        "id": "00000000-0000-0000-0000-000000000000",
        "canAwardTelevotePoints": false,
        "canAwardJuryPoints": false
      }
    ]
  }
}
```

#### A/3/2 - Get Broadcasts

**User story**

*As the Admin, I want to retrieve all the broadcasts from the system, ordered by transmission date then by contest stage, so that I can see which broadcasts I need to create.*

**Request**

```http
GET {{host}}/api/admin/broadcasts
```

**Response**

```http
200 Ok
```

```json
{
  "broadcasts": [
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "contestId": "00000000-0000-0000-0000-000000000000",
      "contestStage": "SemiFinal1",
      "transmissionDate": "2022-05-09",
      "televoteOnly": false,
      "completed": false,
      "competingCountries": [
        {
          "id": "00000000-0000-0000-0000-000000000000",
          "performingOrder": 1,
          "finishingOrder": 1,
          "televotePoints": 12,
          "juryPoints": 0,
          "totalPoints": 12,
          "televoteAwards": [
            {
              "votingCountryId": "00000000-0000-0000-0000-000000000000",
              "pointsValue": 12
            }
          ],
          "juryAwards": [
            {
              "votingCountryId": "00000000-0000-0000-0000-000000000000",
              "pointsValue": 0
            }
          ]
        }
      ],
      "votingCountries": [
        {
          "id": "00000000-0000-0000-0000-000000000000",
          "canAwardTelevotePoints": false,
          "canAwardJuryPoints": false
        }
      ]
    }
  ]
}
```

#### A/3/3 - Delete Broadcast

**User story**

*As the Admin, I want to delete a single broadcast from the system, identified by supplying its ID, so that I can recreate it later with corrected details.*

**Request**

```http
DELETE {{host}}/api/admin/broadcasts/00000000-0000-0000-0000-000000000000
```

**Response**

```http
204 NoContent
```

#### A/3/4 - Award Points

**User story**

*As the Admin, I want to award a set of televote or jury points for a single voting country in a single broadcast in the system, identified by supplying its ID, so that I can add to the voting data that will support the Public Module's queries.*

**Request**

```http
PATCH {{host}}/api/admin/broadcasts/00000000-0000-0000-0000-000000000000/awards
```

```json
{
  "votingCountryId": "00000000-0000-0000-0000-000000000000",
  "votingMethod": "Televote",
  "rankedCompetingCountryIds": [
    "00000000-0000-0000-0000-000000000000",
    "00000000-0000-0000-0000-000000000000",
    "00000000-0000-0000-0000-000000000000"
  ]
}
```

**Response**

```http
204 NoContent
```

### A/4 - Security

#### A/4/1 - Admin API key security

The system will use API key authentication with a role-based authorization policy. Any HTTP request to an *Admin Module* endpoint must include the secret *Admin API Key* as an `"X-Api-Key"` request header.

## P - *Public Module* requirements

### P/1 - Queryables

#### P/1/1 - Get Queryable Broadcasts

**User story**

*As a Euro-Fan, I want to retrieve a list of all the queryable broadcasts in the system, ordered by contest year then by contest stage, so that I can plan my queries.*

**Request**

```http
GET {{host}}/api/public/queryables/broadcasts
```

**Response**

```http
200 Ok
```

```json
{
  "queryableBroadcasts": [
    {
      "contestYear": 2022,
      "contestStage": "SemiFinal1",
      "transmissionDate": "2022-05-09",
      "televoteOnly": false,
      "competingCountryCodes": 15,
      "votingCountries": 18
    }
  ],
  "meta": {
    "totalItems": 10
  }
}
```

#### P/1/2 - Get Queryable Contests

**User story**

*As a Euro-Fan, I want to retrieve a list of all the queryable contests in the system, ordered by contest year, so that I can plan my queries.*

**Request**

```http
GET {{host}}/api/public/queryables/contests
```

**Response**

```http
200 Ok
```

```json
{
  "queryableContests": [
    {
      "contestYear": 2022,
      "hostCityName": "Turin",
      "participatingCountries": 35
    }
  ],
  "meta": {
    "totalItems": 10
  }
}
```

#### P/1/3 - Get Queryable Countries

**User story**

*As a Euro-Fan, I want to retrieve a list of all the countries in the system, ordered by country code, so that I can plan my queries.*

**Request**

```http
GET {{host}}/api/public/queryables/countries
```

**Response**

```http
200 Ok
```

```json
{
  "queryableCountries": [
    {
      "countryCode": "GB",
      "name": "United Kingdom",
      "participatingContests": 3,
      "competingBroadcasts": 5,
      "votingBroadcasts": 9
    }
  ],
  "meta": {
    "totalItems": 10
  }
}
```

### P/2 - Competitor by broadcast rankings

#### P/2/1 - Get Competitor By Broadcast Points Average Rankings

**User story**

*As a Euro-Fan, I want to rank every competing country in every broadcast by the average value of all the individual points awards it received.*

**Request**

```http
GET {{host}}/api/public/rankings/competitors-by-broadcast/points-average?...
```

**Query parameters**

| Name           |         Type          | Required | Description                                                                                       |
|:---------------|:---------------------:|:--------:|:--------------------------------------------------------------------------------------------------|
| `votingMethod` | `VotingMethod` (enum) |    no    | Restricts queried data to points awarded using the specified voting method (default `Undefined`). |
| `contestYear`  |        `int?`         |    no    | Restricts queried data to the broadcasts in the specified contest year (default `null`).          |
| `contestStage` | `ContestStage` (enum) |    no    | Restricts queried data to broadcasts with the specified contest stage (default `Undefined`).      |
| `descending`   |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                                   |
| `itemsPerPage` |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                                |
| `pageIndex`    |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                              |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "contestYear": 2022,
      "contestStage": "GrandFinal",
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsAverage": 10.5,
      "pointsAwards": 20,
      "performerName": "Sam Ryder",
      "songTitle": "Space Man",
      "hostCityName": "Turin"
    }
  ],
  "meta": {
    "votingMethod": "Undefined",
    "contestYear": null,
    "contestStage": "Undefined",
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### P/2/2 - Get Competitor By Broadcast Points Consensus Rankings

**User story**

*As a Euro-Fan, I want to rank every competing country in every broadcast by the cosine similarity of all the televote and jury points awards it received.*

**Request**

```http
GET {{host}}/api/public/rankings/competitors-by-broadcast/points-consensus?...
```

**Query parameters**

| Name           |         Type          | Required | Description                                                                                  |
|:---------------|:---------------------:|:--------:|:---------------------------------------------------------------------------------------------|
| `contestYear`  |        `int?`         |    no    | Restricts queried data to the broadcasts in the specified contest year (default `null`).     |
| `contestStage` | `ContestStage` (enum) |    no    | Restricts queried data to broadcasts with the specified contest stage (default `Undefined`). |
| `descending`   |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                              |
| `itemsPerPage` |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                           |
| `pageIndex`    |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                         |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "contestYear": 2022,
      "contestStage": "GrandFinal",
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsCosineSimilarity": 0.5,
      "pointsAwardPairs": 10,
      "performerName": "Sam Ryder",
      "songTitle": "Space Man",
      "hostCityName": "Turin"
    }
  ],
  "meta": {
    "contestYear": null,
    "contestStage": "Undefined",
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### P/2/3 - Get Competitor By Broadcast Points In Range Rankings

**User story**

*As a Euro-Fan, I want to rank every competing country in every broadcast by the relative frequency of all the individual points awards it received that were in a given range.*

**Request**

```http
GET {{host}}/api/public/rankings/competitors-by-broadcast/points-in-range?minPoints=1&maxPoints=12...
```

**Query parameters**

| Name           |         Type          | Required | Description                                                                                       |
|:---------------|:---------------------:|:--------:|:--------------------------------------------------------------------------------------------------|
| `minPoints`    |         `int`         |   yes    | Specifies the inclusive minimum points value.                                                     |
| `maxPoints`    |         `int`         |   yes    | Specifies the inclusive maximum points value.                                                     |
| `votingMethod` | `VotingMethod` (enum) |    no    | Restricts queried data to points awarded using the specified voting method (default `Undefined`). |
| `contestYear`  |        `int?`         |    no    | Restricts queried data to the broadcasts in the specified contest year (default `null`).          |
| `contestStage` | `ContestStage` (enum) |    no    | Restricts queried data to broadcasts with the specified contest stage (default `Undefined`).      |
| `descending`   |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                                   |
| `itemsPerPage` |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                                |
| `pageIndex`    |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                              |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "contestYear": 2022,
      "contestStage": "GrandFinal",
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsInRangeRelativeFrequency": 0.9,
      "pointsAwardsInRange": 18,
      "pointsAwards": 20,
      "performerName": "Sam Ryder",
      "songTitle": "Space Man",
      "hostCityName": "Turin"
    }
  ],
  "meta": {
    "minPoints": 1,
    "maxPoints": 12,
    "votingMethod": "Undefined",
    "contestYear": null,
    "contestStage": "Undefined",
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### P/2/4 - Get Competitor By Broadcast Points Share Rankings

**User story**

*As a Euro-Fan, I want to rank every competing country in every broadcast by the total points it received as a share of the maximum possible points it could have received.*

**Request**

```http
GET {{host}}/api/public/rankings/competitors-by-broadcast/points-share?...
```

**Query parameters**

| Name           |         Type          | Required | Description                                                                                       |
|:---------------|:---------------------:|:--------:|:--------------------------------------------------------------------------------------------------|
| `votingMethod` | `VotingMethod` (enum) |    no    | Restricts queried data to points awarded using the specified voting method (default `Undefined`). |
| `contestYear`  |        `int?`         |    no    | Restricts queried data to the broadcasts in the specified contest year (default `null`).          |
| `contestStage` | `ContestStage` (enum) |    no    | Restricts queried data to broadcasts with the specified contest stage (default `Undefined`).      |
| `descending`   |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                                   |
| `itemsPerPage` |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                                |
| `pageIndex`    |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                              |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "contestYear": 2022,
      "contestStage": "GrandFinal",
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsShare": 0.75,
      "totalPoints": 75,
      "maxPossiblePoints": 100,
      "pointsAwards": 20,
      "performerName": "Sam Ryder",
      "songTitle": "Space Man",
      "hostCityName": "Turin"
    }
  ],
  "meta": {
    "votingMethod": "Undefined",
    "contestYear": null,
    "contestStage": "Undefined",
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

### P/3 - Competitor by contest rankings

#### P/3/1 - Get Competitor By Contest Points Average Rankings

**User story**

*As a Euro-Fan, I want to rank every competing country in every contest by the average value of all the individual points awards it received.*

**Request**

```http
GET {{host}}/api/public/rankings/competitors-by-contest/points-average?...
```

**Query parameters**

| Name           |         Type          | Required | Description                                                                                       |
|:---------------|:---------------------:|:--------:|:--------------------------------------------------------------------------------------------------|
| `votingMethod` | `VotingMethod` (enum) |    no    | Restricts queried data to points awarded using the specified voting method (default `Undefined`). |
| `contestYear`  |        `int?`         |    no    | Restricts queried data to the broadcasts in the specified contest year (default `null`).          |
| `descending`   |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                                   |
| `itemsPerPage` |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                                |
| `pageIndex`    |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                              |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "contestYear": 2022,
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsAverage": 10.5,
      "pointsAwards": 20,
      "broadcasts": 1,
      "performerName": "Sam Ryder",
      "songTitle": "Space Man",
      "hostCityName": "Turin"
    }
  ],
  "meta": {
    "votingMethod": "Undefined",
    "contestYear": null,
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### P/3/2 - Get Competitor By Contest Points Consensus Rankings

**User story**

*As a Euro-Fan, I want to rank every competing country in every contest by the cosine similarity of all the televote and jury points awards it received.*

**Request**

```http
GET {{host}}/api/public/rankings/competitors-by-contest/points-consensus?...
```

**Query parameters**

| Name           |         Type          | Required | Description                                                                                  |
|:---------------|:---------------------:|:--------:|:---------------------------------------------------------------------------------------------|
| `contestYear`  |        `int?`         |    no    | Restricts queried data to the broadcasts in the specified contest year (default `null`).     |
| `descending`   |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                              |
| `itemsPerPage` |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                           |
| `pageIndex`    |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                         |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "contestYear": 2022,
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsCosineSimilarity": 0.5,
      "pointsAwardPairs": 10,
      "broadcasts": 1,
      "performerName": "Sam Ryder",
      "songTitle": "Space Man",
      "hostCityName": "Turin"
    }
  ],
  "meta": {
    "contestYear": null,
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### P/3/3 - Get Competitor By Contest Points In Range Rankings

**User story**

*As a Euro-Fan, I want to rank every competing country in every contest by the relative frequency of all the individual points awards it received that were in a given range.*

**Request**

```http
GET {{host}}/api/public/rankings/competitors-by-contest/points-in-range?minPoints=1&maxPoints=12...
```

**Query parameters**

| Name           |         Type          | Required | Description                                                                                       |
|:---------------|:---------------------:|:--------:|:--------------------------------------------------------------------------------------------------|
| `minPoints`    |         `int`         |   yes    | Specifies the inclusive minimum points value.                                                     |
| `maxPoints`    |         `int`         |   yes    | Specifies the inclusive maximum points value.                                                     |
| `votingMethod` | `VotingMethod` (enum) |    no    | Restricts queried data to points awarded using the specified voting method (default `Undefined`). |
| `contestYear`  |        `int?`         |    no    | Restricts queried data to the broadcasts in the specified contest year (default `null`).          |
| `descending`   |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                                   |
| `itemsPerPage` |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                                |
| `pageIndex`    |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                              |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "contestYear": 2022,
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsInRangeRelativeFrequency": 0.9,
      "pointsAwardsInRange": 18,
      "pointsAwards": 20,
      "broadcasts": 1,
      "performerName": "Sam Ryder",
      "songTitle": "Space Man",
      "hostCityName": "Turin"
    }
  ],
  "meta": {
    "minPoints": 1,
    "maxPoints": 12,
    "votingMethod": "Undefined",
    "contestYear": null,
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### P/3/4 - Get Competitor By Contest Points Share Rankings

**User story**

*As a Euro-Fan, I want to rank every competing country in every contest by the total points it received as a share of the maximum possible points it could have received.*

**Request**

```http
GET {{host}}/api/public/rankings/competitors-by-contest/points-share?...
```

**Query parameters**

| Name           |         Type          | Required | Description                                                                                       |
|:---------------|:---------------------:|:--------:|:--------------------------------------------------------------------------------------------------|
| `votingMethod` | `VotingMethod` (enum) |    no    | Restricts queried data to points awarded using the specified voting method (default `Undefined`). |
| `contestYear`  |        `int?`         |    no    | Restricts queried data to the broadcasts in the specified contest year (default `null`).          |
| `descending`   |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                                   |
| `itemsPerPage` |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                                |
| `pageIndex`    |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                              |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "contestYear": 2022,
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsShare": 0.75,
      "totalPoints": 75,
      "maxPossiblePoints": 100,
      "pointsAwards": 20,
      "broadcasts": 1,
      "performerName": "Sam Ryder",
      "songTitle": "Space Man",
      "hostCityName": "Turin"
    }
  ],
  "meta": {
    "votingMethod": "Undefined",
    "contestYear": null,
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

### P/4 - Competitor overall rankings

#### P/4/1 - Get Competitor Overall Points Average Rankings

**User story**

*As a Euro-Fan, I want to rank every competing country overall by the average value of all the individual points awards it received.*

**Request**

```http
GET {{host}}/api/public/rankings/competitors-overall/points-average?...
```

**Query parameters**

| Name                |         Type          | Required | Description                                                                                        |
|:--------------------|:---------------------:|:--------:|:---------------------------------------------------------------------------------------------------|
| `votingCountryCode` |       `string?`       |    no    | Restricts queried data to points awarded by the specified voting country (default `null`).         |
| `votingMethod`      | `VotingMethod` (enum) |    no    | Restricts queried data to points awarded using the specified voting method (default `Undefined`).  |
| `minContestYear`    |         `int`         |    no    | Restricts queried data to the broadcasts in or after the specified contest year (default `2016`).  |
| `maxContestYear`    |         `int`         |    no    | Restricts queried data to the broadcasts in or before the specified contest year (default `2050`). |
| `grandFinalsOnly`   |        `bool`         |    no    | Restricts queried data to grand final broadcasts only (default `false`).                           |
| `descending`        |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                                    |
| `itemsPerPage`      |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                                 |
| `pageIndex`         |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                               |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsAverage": 10.5,
      "pointsAwards": 50,
      "contests": 5,
      "broadcasts": 5
    }
  ],
  "meta": {
    "votingCountryCode": null,
    "votingMethod": "Undefined",
    "minContestYear": 2016,
    "maxContestYear": 2050,
    "grandFinalsOnly": false,
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### P/4/2 - Get Competitor Overall Points Consensus Rankings

**User story**

*As a Euro-Fan, I want to rank every competing country overall by the cosine similarity of all the televote and jury points awards it received.*

**Request**

```http
GET {{host}}/api/public/rankings/competitors-overall/points-consensus?...
```

**Query parameters**

| Name                |   Type    | Required | Description                                                                                        |
|:--------------------|:---------:|:--------:|:---------------------------------------------------------------------------------------------------|
| `votingCountryCode` | `string?` |    no    | Restricts queried data to points awarded by the specified voting country (default `null`).         |
| `minContestYear`    |   `int`   |    no    | Restricts queried data to the broadcasts in or after the specified contest year (default `2016`).  |
| `maxContestYear`    |   `int`   |    no    | Restricts queried data to the broadcasts in or before the specified contest year (default `2050`). |
| `grandFinalsOnly`   |  `bool`   |    no    | Restricts queried data to grand final broadcasts only (default `false`).                           |
| `descending`        |  `bool`   |    no    | Orders all rankings in descending rank order (default `false`).                                    |
| `itemsPerPage`      |   `int`   |    no    | Specifies the pagination page size to be retrieved (default `10`).                                 |
| `pageIndex`         |   `int`   |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                               |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsCosineSimilarity": 0.5,
      "pointsAwardPairs": 10,
      "broadcasts": 5,
      "contests": 5
    }
  ],
  "meta": {
    "votingCountryCode": null,
    "minContestYear": 2016,
    "maxContestYear": 2050,
    "grandFinalsOnly": false,
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### P/4/3 - Get Competitor Overall Points In Range Rankings

**User story**

*As a Euro-Fan, I want to rank every competing overall by the relative frequency of all the individual points awards it received that were in a given range.*

**Request**

```http
GET {{host}}/api/public/rankings/competitors-overall/points-in-range?minPoints=1&maxPoints=12...
```

**Query parameters**

| Name                |         Type          | Required | Description                                                                                        |
|:--------------------|:---------------------:|:--------:|:---------------------------------------------------------------------------------------------------|
| `minPoints`         |         `int`         |   yes    | Specifies the inclusive minimum points value.                                                      |
| `maxPoints`         |         `int`         |   yes    | Specifies the inclusive maximum points value.                                                      |
| `votingCountryCode` |       `string?`       |    no    | Restricts queried data to points awarded by the specified voting country (default `null`).         |
| `votingMethod`      | `VotingMethod` (enum) |    no    | Restricts queried data to points awarded using the specified voting method (default `Undefined`).  |
| `minContestYear`    |         `int`         |    no    | Restricts queried data to the broadcasts in or after the specified contest year (default `2016`).  |
| `maxContestYear`    |         `int`         |    no    | Restricts queried data to the broadcasts in or before the specified contest year (default `2050`). |
| `grandFinalsOnly`   |        `bool`         |    no    | Restricts queried data to grand final broadcasts only (default `false`).                           |
| `descending`        |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                                    |
| `itemsPerPage`      |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                                 |
| `pageIndex`         |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                               |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsInRangeRelativeFrequency": 0.9,
      "pointsAwardsInRange": 18,
      "pointsAwards": 20,
      "broadcasts": 5,
      "contests": 5
    }
  ],
  "meta": {
    "minPoints": 1,
    "maxPoints": 12,
    "votingCountryCode": null,
    "votingMethod": "Undefined",
    "minContestYear": 2016,
    "maxContestYear": 2050,
    "grandFinalsOnly": false,
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### P/4/4 - Get Competitor Overall Points Share Rankings

**User story**

*As a Euro-Fan, I want to rank every competing country overall by the total points it received as a share of the maximum possible points it could have received.*

**Request**

```http
GET {{host}}/api/public/rankings/competitors-overall/points-share?...
```

**Query parameters**

| Name                |         Type          | Required | Description                                                                                        |
|:--------------------|:---------------------:|:--------:|:---------------------------------------------------------------------------------------------------|
| `votingCountryCode` |       `string?`       |    no    | Restricts queried data to points awarded by the specified voting country (default `null`).         |
| `votingMethod`      | `VotingMethod` (enum) |    no    | Restricts queried data to points awarded using the specified voting method (default `Undefined`).  |
| `minContestYear`    |         `int`         |    no    | Restricts queried data to the broadcasts in or after the specified contest year (default `2016`).  |
| `maxContestYear`    |         `int`         |    no    | Restricts queried data to the broadcasts in or before the specified contest year (default `2050`). |
| `grandFinalsOnly`   |        `bool`         |    no    | Restricts queried data to grand final broadcasts only (default `false`).                           |
| `descending`        |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                                    |
| `itemsPerPage`      |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                                 |
| `pageIndex`         |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                               |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsShare": 0.75,
      "totalPoints": 75,
      "maxPossiblePoints": 100,
      "pointsAwards": 20,
      "broadcasts": 5,
      "contests": 5
    }
  ],
  "meta": {
    "votingCountryCode": null,
    "votingMethod": "Undefined",
    "minContestYear": 2016,
    "maxContestYear": 2050,
    "grandFinalsOnly": false,
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

### P/5 - Voter overall rankings

#### P/5/1 - Get Voter Overall Points Average Rankings

**User story**

*As a Euro-Fan, I want to rank every voting country overall by the average value of all the individual points awards it gave to the specified competing country.*

**Request**

```http
GET {{host}}/api/public/rankings/voters-overall/points-average?competingCountryCode=FR...
```

**Query parameters**

| Name                   |         Type          | Required | Description                                                                                        |
|:-----------------------|:---------------------:|:--------:|:---------------------------------------------------------------------------------------------------|
| `competingCountryCode` |       `string`        |   yes    | Specifies the competing country.                                                                   |
| `votingMethod`         | `VotingMethod` (enum) |    no    | Restricts queried data to points awarded using the specified voting method (default `Undefined`).  |
| `minContestYear`       |         `int`         |    no    | Restricts queried data to the broadcasts in or after the specified contest year (default `2016`).  |
| `maxContestYear`       |         `int`         |    no    | Restricts queried data to the broadcasts in or before the specified contest year (default `2050`). |
| `grandFinalsOnly`      |        `bool`         |    no    | Restricts queried data to grand final broadcasts only (default `false`).                           |
| `descending`           |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                                    |
| `itemsPerPage`         |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                                 |
| `pageIndex`            |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                               |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsAverage": 10.5,
      "pointsAwards": 50,
      "broadcasts": 5,
      "contests": 5
    }
  ],
  "meta": {
    "competingCountryCode": "FR",
    "votingMethod": "Undefined",
    "minContestYear": 2016,
    "maxContestYear": 2050,
    "grandFinalsOnly": false,
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### P/5/2 - Get Voter Overall Points Consensus Rankings

**User story**

*As a Euro-Fan, I want to rank every voting country overall by the cosine similarity of all the televote and jury points awards it gave.*
**Request**

```http
GET {{host}}/api/public/rankings/voters-overall/points-consensus?...
```

**Query parameters**

| Name                   |   Type    | Required | Description                                                                                        |
|:-----------------------|:---------:|:--------:|:---------------------------------------------------------------------------------------------------|
| `competingCountryCode` | `string?` |    no    | Restricts queried data to points awarded to the specified competing country (default `null`).      |
| `minContestYear`       |   `int`   |    no    | Restricts queried data to the broadcasts in or after the specified contest year (default `2016`).  |
| `maxContestYear`       |   `int`   |    no    | Restricts queried data to the broadcasts in or before the specified contest year (default `2050`). |
| `grandFinalsOnly`      |  `bool`   |    no    | Restricts queried data to grand final broadcasts only (default `false`).                           |
| `descending`           |  `bool`   |    no    | Orders all rankings in descending rank order (default `false`).                                    |
| `itemsPerPage`         |   `int`   |    no    | Specifies the pagination page size to be retrieved (default `10`).                                 |
| `pageIndex`            |   `int`   |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                               |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsCosineSimilarity": 0.5,
      "pointsAwardPairs": 10,
      "broadcasts": 5,
      "contests": 5
    }
  ],
  "meta": {
    "competingCountryCode": null,
    "minContestYear": 2016,
    "maxContestYear": 2050,
    "grandFinalsOnly": false,
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### P/5/3 - Get Voter Overall Points In Range Rankings

**User story**

*As a Euro-Fan, I want to rank every voting country overall by the relative frequency of all the individual points awards it gave to the specified competing country that were in a given range.*

**Request**

```http
GET {{host}}/api/public/rankings/voters-overall/points-in-range?minPoints=1&maxPoints=12&competingCountryCode=FR...
```

**Query parameters**

| Name                   |         Type          | Required | Description                                                                                        |
|:-----------------------|:---------------------:|:--------:|:---------------------------------------------------------------------------------------------------|
| `minPoints`            |         `int`         |   yes    | Specifies the inclusive minimum points value.                                                      |
| `maxPoints`            |         `int`         |   yes    | Specifies the inclusive maximum points value.                                                      |
| `competingCountryCode` |       `string`        |   yes    | Specifies the competing country.                                                                   |
| `votingMethod`         | `VotingMethod` (enum) |    no    | Restricts queried data to points awarded using the specified voting method (default `Undefined`).  |
| `minContestYear`       |         `int`         |    no    | Restricts queried data to the broadcasts in or after the specified contest year (default `2016`).  |
| `maxContestYear`       |         `int`         |    no    | Restricts queried data to the broadcasts in or before the specified contest year (default `2050`). |
| `grandFinalsOnly`      |        `bool`         |    no    | Restricts queried data to grand final broadcasts only (default `false`).                           |
| `descending`           |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                                    |
| `itemsPerPage`         |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                                 |
| `pageIndex`            |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                               |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsInRangeRelativeFrequency": 0.9,
      "pointsAwardsInRange": 18,
      "pointsAwards": 20,
      "broadcasts": 5,
      "contests": 5
    }
  ],
  "meta": {
    "minPoints": 1,
    "maxPoints": 12,
    "competingCountryCode": "FR",
    "votingMethod": "Undefined",
    "minContestYear": 2016,
    "maxContestYear": 2050,
    "grandFinalsOnly": false,
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### P/5/4 - Get Voter Overall Points Share Rankings

**User story**

*As a Euro-Fan, I want to rank every voting country overall by the total points it gave to the specified competing country as a share of the maximum possible points it could have given.*

**Request**

```http
GET {{host}}/api/public/rankings/voters-overall/points-share?competingCountryCode=FR...
```

**Query parameters**

| Name                   |         Type          | Required | Description                                                                                        |
|:-----------------------|:---------------------:|:--------:|:---------------------------------------------------------------------------------------------------|
| `competingCountryCode` |       `string`        |   yes    | Specifies the competing country.                                                                   |
| `votingMethod`         | `VotingMethod` (enum) |    no    | Restricts queried data to points awarded using the specified voting method (default `Undefined`).  |
| `minContestYear`       |         `int`         |    no    | Restricts queried data to the broadcasts in or after the specified contest year (default `2016`).  |
| `maxContestYear`       |         `int`         |    no    | Restricts queried data to the broadcasts in or before the specified contest year (default `2050`). |
| `grandFinalsOnly`      |        `bool`         |    no    | Restricts queried data to grand final broadcasts only (default `false`).                           |
| `descending`           |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                                    |
| `itemsPerPage`         |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                                 |
| `pageIndex`            |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                               |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsShare": 0.75,
      "totalPoints": 75,
      "maxPossiblePoints": 100,
      "pointsAwards": 20,
      "broadcasts": 5,
      "contests": 5
    }
  ],
  "meta": {
    "competingCountryCode": "FR",
    "votingMethod": "Undefined",
    "minContestYear": 2016,
    "maxContestYear": 2050,
    "grandFinalsOnly": false,
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

#### P/5/4 - Get Voter Overall Points Similarity Rankings

**User story**

*As a Euro-Fan, I want to rank every voting country overall by the cosine similarity of all the individual points awards it gave compared with those given by the specified voting country.*

**Request**

```http
GET {{host}}/api/public/rankings/voters-overall/points-share?competingCountryCode=FR...
```

**Query parameters**

| Name                |         Type          | Required | Description                                                                                        |
|:--------------------|:---------------------:|:--------:|:---------------------------------------------------------------------------------------------------|
| `votingCountryCode` |       `string`        |   yes    | Specifies the voting country.                                                                      |
| `votingMethod`      | `VotingMethod` (enum) |    no    | Restricts queried data to points awarded using the specified voting method (default `Undefined`).  |
| `minContestYear`    |         `int`         |    no    | Restricts queried data to the broadcasts in or after the specified contest year (default `2016`).  |
| `maxContestYear`    |         `int`         |    no    | Restricts queried data to the broadcasts in or before the specified contest year (default `2050`). |
| `grandFinalsOnly`   |        `bool`         |    no    | Restricts queried data to grand final broadcasts only (default `false`).                           |
| `descending`        |        `bool`         |    no    | Orders all rankings in descending rank order (default `false`).                                    |
| `itemsPerPage`      |         `int`         |    no    | Specifies the pagination page size to be retrieved (default `10`).                                 |
| `pageIndex`         |         `int`         |    no    | Specifies the zero-indexed page index to be retrieved (default `0`).                               |

**Response**

```http
200 Ok
```

```json
{
  "items": [
    {
      "rank": 1,
      "countryCode": "GB",
      "countryName": "United Kingdom",
      "pointsCosineSimilarity": 0.5,
      "pointsAwardPairs": 10,
      "broadcasts": 5,
      "contests": 5
    }
  ],
  "meta": {
    "votingCountryCode": "FR",
    "votingMethod": "Undefined",
    "minContestYear": 2016,
    "maxContestYear": 2050,
    "grandFinalsOnly": false,
    "descending": false,
    "itemsPerPage": 10,
    "pageIndex": 0,
    "totalItems": 100,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

### P/6 - Security

#### P/6/1 - Public API key security

The system will use API key authentication with a role-based authorization policy. Any HTTP request to a *Public Module* endpoint must include either the *Public API Key* or the secret *Admin API Key* as an `"X-Api-Key"` request header.

## Non-functional requirements

- The language of the system is UK English.
- The web API uses a cloud-hosted Azure web app server.
- The system database uses a cloud-hosted Azure SQL database server.
- Both the web API server and database server use Azure's free of charge (or absolutely minimal cost) service tiers.
- The system does not use logging.
- If an uncaught exception is thrown while the system is handling an HTTP request, the API returns an HTTP response with an `"InternalServerError"` status code and a serialized `ProblemDetails` object that states the name of the exception that was thrown but does not expose any implementation details.
- The system serves a Swagger document in development and production, that describes all the endpoints.
- The system uses versioning for all endpoints.
- Every domain entity has an immutable Guid ID, which is assigned on the server by the application before the entity is persisted to the database.
